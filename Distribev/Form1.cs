using Distribev.Models;
using Distribev.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Distribev
{
    public partial class Form1 : Form
    {
        MainWindowViewModel viewModel = new();
        CancellationTokenSource token = new CancellationTokenSource();
        DateTime scanStarted = new DateTime();

        public Form1()
        {
            InitializeComponent();

            viewModel.ConfigureListViewSource(listViewProducts);
            viewModel.ConfigureProductsCounter(lProductsCount);
            viewModel.ConfigurePagesCounter(lPagesVisited);

            cbWebsites.Items.AddRange(viewModel.Websites.ToArray());
            cbWebsites.DisplayMember = "Name";


        }

        private async void button1_Click(object sender, EventArgs e)
        {
            if (viewModel.SelectedWebsite == null)
            {
                MessageBox.Show("Gotta select a website man. You want me to scan the whole internet? That's at least 5kb");
                return;
            }

            if(viewModel.saveFilePath == null)
            {
                MessageBox.Show("Please, set a path to save file. It'd be saved in the default place and you wouldn't find it. Heck. Me neither.");
                return;
            }

            if (!viewModel.isLoading)
            {
                scanStarted = DateTime.Now;

                viewModel.isLoading = !viewModel.isLoading;

                button1.Text = "Cancel";

                tSaveFile.Enabled = false;
                bSavePath.Enabled = false;
                cbWebsites.Enabled = false;

                tSaveFile.Start();
                viewModel.saveFileName = $"{viewModel.SelectedWebsite.Name}_{DateTime.Now.Year}_{DateTime.Now.Month}_{DateTime.Now.Day}";

                listViewProducts.Items.Clear();
                viewModel.ClearViewModel();
                token = new CancellationTokenSource();

                //most likely stuck on this for a while
                await viewModel.MapWebsiteByPageAddress(token);
            }
            else
            {
                viewModel.isLoading = !viewModel.isLoading;
            }

            tSaveFile.Enabled = true;
            bSavePath.Enabled = true;
            cbWebsites.Enabled = true;

            button1.Text = "Load products";



            await viewModel.SaveFile();
            tSaveFile.Stop();
            token.Cancel();
        }

        private void cbWebsites_SelectedIndexChanged(object sender, EventArgs e)
        {
            var currentWebsite = viewModel.Websites.FirstOrDefault(w => w == cbWebsites.SelectedItem);

            viewModel.SelectedWebsite = currentWebsite;

            button1.Enabled = viewModel.SelectedWebsite != null && tbSavePath.Text.Length > 0;
        }

        private void bSavePath_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog())
            {
                //openFileDialog.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*"; 

                if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
                {
                    tbSavePath.Text = folderBrowserDialog.SelectedPath;
                }
            }
        }

        private void tbSavePath_TextChanged(object sender, EventArgs e)
        {
            viewModel.saveFilePath = tbSavePath.Text;
            button1.Enabled = viewModel.SelectedWebsite != null && viewModel.saveFilePath.Length > 0;
        }

        private async void tSaveFile_Tick(object sender, EventArgs e)
        {
            var result = await viewModel.SaveFile();

            if (result != null)
            {
                viewModel.isLoading = !viewModel.isLoading;
                button1.Text = "Load products";

                tSaveFile.Stop();
                token.Cancel();
                MessageBox.Show(result.Message);
            }
        }

        private void tScannedCounter_Tick(object sender, EventArgs e)
        {
            viewModel.UpdateScanners();
        }
    }
}
