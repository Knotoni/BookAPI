using Microsoft.AspNetCore.Mvc;
using TestBookAPI.Data;
using TestBookAPI.Data.Models;
using TestBookAPI.Data.EditModels;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TestBookAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        private readonly BookDataContext db;
        public AuthorsController(BookDataContext context)
        {
            db = context;
        }
        // GET: api/<AuthorsController>
        [ActionName("GetAllAuthors")]
        [HttpGet]
        public IEnumerable<Author> GetAllAuthors()
        {
            var authors = db.Authors.ToList();
            return authors;
        }

        // GET api/<AuthorsController>/5
        [ActionName("GetAuthor")]
        [HttpGet("{id}")]
        public object GetAuthor(int id)
        {
            var author = db.Authors.ToList().Find(x => x.Id == id);
            if (author != null) return author;
            else return "Not Found";
        }

        // POST api/<AuthorsController>
        [ActionName("AddAuthor")]
        [HttpPost]
        public string AddAuthor([FromBody] EditAuthor? editAuthor)
        {
            if (editAuthor == null) return "Error, empty fields";
            else
            {
                Author author = new Author();
                author.Surname = editAuthor.Surname;
                author.Name = editAuthor.Name;
                author.Country = editAuthor.Country;
                db.Authors.Add(author);
                db.SaveChanges();
                return $"Done, author ID = {author.Id}";
            };
        }

        // PUT api/<AuthorsController>/5
        [ActionName("EditAuthor")]
        [HttpPut("{id}")]
        public string EditAuthor(int id, [FromBody] EditAuthor? editAuthor)
        {
            if (editAuthor == null | id == 0) return "Error, empty fields";
            else
            {
                Author? author = db.Authors.ToList().Find(x => x.Id == id);
                if (author == null) return "Error, author not fount";
                else
                {
                    author.Surname = editAuthor.Surname;
                    author.Name = editAuthor.Name;
                    author.Country = editAuthor.Country;
                    db.SaveChanges();
                    return "Done";
                }
            }
        }

        // DELETE api/<AuthorsController>/5
        [ActionName("DeleteAuthor")]
        [HttpDelete("{id}")]
        public string DeleteAuthor(int id)
        {
            Author? author = db.Authors.ToList().Find(x => x.Id == id);
            if (author == null) return "Error, author not fount";
            else
            {
                db.Authors.Remove(author);
                return "Done";
            }
        }
    }
}
