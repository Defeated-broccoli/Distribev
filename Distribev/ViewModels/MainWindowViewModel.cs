using Distribev.Models;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Distribev.ViewModels
{
    public class MainWindowViewModel
    {
        readonly HtmlWeb web = new HtmlWeb();
        public List<string> visitedPages = new List<string>();
        public List<Product> Products = new List<Product>();
        private ListView listView = new ListView();
        private Label productsCounter = new Label();
        private Label pagesCounter = new Label();
        public bool isLoading = false;
        public string? saveFilePath = null;
        public string? saveFileName = null;


        public List<Website> Websites = new List<Website>
        {
            new Website
            {
                Id = 0,
                Name = "Pinot",
                Root = "https://pinot.pl",
                SectionAddress = "/produkty",
                ProductSelector = ".//*[@class='product-card']",
                NameSelector = ".//h3[@class='product-title']/a",
                PriceSelector = ".//h4[@class='product-price']",
                AddressSelector = ".//h3[@class='product-title']/a",
                BannedStrings = ["list", "table", "grid", ".html", ".png", ".jpg", "#"],
    },
            new Website
            {
                Id = 1,
                Name = "Alkooutlet",
                Root = "https://alkooutlet.pl",
                SectionAddress = "/pl/c",
                ProductSelector = ".//*[@class='product product_view-extended s-grid-3 product-main-wrap product_with-lowest-price']",
                NameSelector = ".//span[@class='productname']",
                PriceSelector = ".//div[@class='price f-row']",
                AddressSelector = ".//a[@class='prodimage f-row']",
                BannedStrings = ["#", "login", "availability", "_at_", "/full", "/news", "/promotion", "/default"],
            },
            new Website
            {
                Id = 2,
                Name = "Świat-Wisky",
                Root = "https://swiat-whisky.sklep.pl",
                SectionAddress = null,
                ProductSelector = ".//*[@class='product-miniature js-product-miniature']",
                NameSelector = ".//div[@class='product_name']/a",
                PriceSelector = ".//span[@class='price']/span[@itemprop='price']",
                AddressSelector = ".//div[@class='product_name']/a",
                BannedStrings = ["#", "mail", "konto", "order", "compare"],
            },
        };

        public Website? SelectedWebsite = null;

        int count = 0;

        public async Task<List<string>> MapWebsiteByPageAddress(CancellationTokenSource token, string? address = null)
        {
            if (!token.IsCancellationRequested)
            {
                var addressString = address != null && address.StartsWith("http") ? address : SelectedWebsite?.Root + address;
                Debug.WriteLine($"Scrapping link: {addressString}");

                try
                {
                    var document = await web.LoadFromWebAsync(addressString);

                    //await GetProductsFromNode(document, root, "product-card", "product-title", "product-price", "product-thumb thumbnail-default");
                    GetProductsFromNode(document, SelectedWebsite.Root, SelectedWebsite.ProductSelector, SelectedWebsite.NameSelector, SelectedWebsite.PriceSelector, SelectedWebsite.AddressSelector);

                    var links = document.DocumentNode
                        .SelectNodes(".//a")
                        ?.Select(node => node.GetAttributeValue("href", null))
                        .Where(link => link != null &&
                                       !SelectedWebsite.BannedStrings.Any(str => link.Contains(str)) &&
                                       link.StartsWith(SelectedWebsite.SectionAddress ?? "") &&
                                       (link.StartsWith("http") || link.StartsWith("https") ? link.StartsWith(SelectedWebsite.Root) : true))
                        .Select(link => link.EndsWith("/") ? link.TrimEnd('/') : link)
                        .Except(visitedPages)
                        .ToList();

                    if (links != null && links.Count > 0)
                    {
                        visitedPages.AddRange(links);

                        var nextLinks = new List<string> { SelectedWebsite.Root + address };

                        foreach (var link in links)
                        {
                            //visitedPages.Add(link);

                            nextLinks.AddRange(await MapWebsiteByPageAddress(token, link));
                        }

                        return nextLinks;
                    }
                    else
                    {
                        return new List<string> { SelectedWebsite.Root + address };
                    }
                }
                catch (OperationCanceledException e)
                {
                    Debug.WriteLine("Operation canceled");
                }
                catch (Exception e)
                {
                    Debug.WriteLine($"Exception {e}, address {SelectedWebsite.Root + address}");
                }
            }

            return new List<string> { SelectedWebsite?.Root + address };

        }

        public void GetProductsFromNode(HtmlAgilityPack.HtmlDocument document, string rootAddress, string productSelector, string titleSelector, string priceSelector, string addressSelector)
        {
            var mainNode = document.DocumentNode;

            var nodes = mainNode.SelectNodes(productSelector);

            if (nodes != null)
            {
                foreach (var node in nodes)
                {
                    var name = node.SelectSingleNode(titleSelector)?.InnerText?.Trim();
                    var pricenode = node.SelectSingleNode(priceSelector);
                    var price = node.SelectSingleNode(priceSelector)?.InnerText.Replace(" ", "") ?? "";
                    string pattern = @"(\d+\,\d+)";

                    Match match = Regex.Match(price, pattern);

                    if (match.Success)
                        price = match.Groups[1].Value ?? "" + "," + match.Groups[2].Value ?? "";

                    var address = node.SelectSingleNode(addressSelector)?.GetAttributeValue("href", null);

                    var product = new Product
                    {
                        Name = name,
                        Price = price,
                        Address = address != null && address.StartsWith("/") ? rootAddress + address : address,
                    };

                    if (product.Name != null && product.Price != null && !Products.Any(p => p.Name == product.Name && p.Price == product.Price))
                    {
                        Products.Add(product);

                        ListViewItem listViewItem = new ListViewItem(product.Name);
                        listViewItem.SubItems.Add(product.Price);
                        listViewItem.SubItems.Add(product.Address);

                        listView.Items.Add(listViewItem);

                        UpdateScanners();
                    }

                }
            }
        }

        public async Task<Exception?> SaveFile()
        {
            Debug.WriteLine("Saving to file...");

            try
            {
                using (StreamWriter sw = new StreamWriter(saveFilePath + "/" + saveFileName + ".csv"))
                {
                    string csvContent = "Name; Price; address \n" + string.Join("\n", Products.Select(p => $"\"{p.Name}\"; \"{p.Price}\"; \"{p.Address}\""));

                    await sw.WriteAsync(csvContent);
                }

                return null;
            }
            catch(Exception ex)
            {
                return ex;
            }
        }

        public void UpdateScanners()
        {
            productsCounter.Text = Products.Count.ToString();
            pagesCounter.Text = visitedPages.Count.ToString();
        }

        public void ConfigureListViewSource(ListView listView)
        {
            this.listView = listView;
        }

        public void ConfigurePagesCounter(Label pagesCounter)
        {
            this.pagesCounter = pagesCounter;
        }

        public void ConfigureProductsCounter(Label productsCounter)
        {
            this.productsCounter = productsCounter;
        }

        public void ClearViewModel()
        {
            Products.Clear();
            visitedPages.Clear();
        }
    }
}
