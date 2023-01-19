using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebCrawler
{
    public class Product
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Link { get; set; }
        public string Rate { get; set; }
        public string OldPrice { get; set; }
        public string NewPrice { get; set; }
        public string Thumb { get; set; }
    }
}
