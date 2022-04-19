using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestBookAPI.Data.ViewModels
{
    public class ViewBook
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string PublicationDate { get; set; }
        public string Author { get; set; }
        public string Genre { get; set; }
    }
}
