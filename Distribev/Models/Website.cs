using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Distribev.Models
{
    public class Website
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Root { get; set; }
        public string? SectionAddress { get; set; }
        public string ProductSelector { get; set; }
        public string NameSelector { get; set; }
        public string PriceSelector { get; set; }
        public string AddressSelector { get; set; }
        public string[] BannedStrings { get; set; }
    }
}
