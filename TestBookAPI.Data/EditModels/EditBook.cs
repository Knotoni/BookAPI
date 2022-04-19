using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestBookAPI.Data.EditModels
{
    public class EditBook
    {
        public string Title { get; set; }
        public string PublicationDate { get; set; }
        public int AuthorID { get; set; }
        public int GenreId { get; set; }
    }
}
