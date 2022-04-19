using Microsoft.AspNetCore.Mvc;
using TestBookAPI.Data;
using TestBookAPI.Data.Models;
using TestBookAPI.Data.EditModels;
using TestBookAPI.Data.ViewModels;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TestBookAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly BookDataContext db;
        public BooksController(BookDataContext context)
        {
            db = context;
        }
        // GET: api/<BookController>
        [ActionName("GetAllBooks")]
        [HttpGet]
        public IEnumerable<ViewBook> GetAllBooks()
        {
            var books = db.Books.ToList();
            var showBooks = new List<ViewBook>();
            foreach (Book book in books)
            {
                var showBook = new ViewBook();
                showBook.Title = book.Title;
                showBook.PublicationDate = book.PublicationDate.ToShortDateString();
                showBook.Id = book.Id;
                var author = db.Authors.ToList().Find(x => x.Id == book.AuthorID);
                showBook.Author = $"{author.Name} {author.Surname}";
                var genre = db.Genres.ToList().Find(x => x.Id == book.GenreId);
                showBook.Genre = genre.GenreName;
                showBooks.Add(showBook);
            }
            return showBooks;
        }

        // GET api/<BookController>/5
        [ActionName("GetBook")]
        [HttpGet("{id}")]
        public object GetBook(int id)
        {
            var book = db.Books.ToList().Find(x => x.Id == id);
            if (book == null) return "Not Found";
            else
            {
                var showBook = new ViewBook();
                showBook.Title = book.Title;
                showBook.PublicationDate = book.PublicationDate.ToShortDateString();
                showBook.Id = book.Id;
                var author = db.Authors.ToList().Find(x => x.Id == book.AuthorID);
                showBook.Author = $"{author.Name} {author.Surname}";
                var genre = db.Genres.ToList().Find(x => x.Id == book.GenreId);
                showBook.Genre = genre.GenreName;
                return showBook;
            }
        }

        // POST api/<BookController>
        [ActionName("AddBook")]
        [HttpPost]
        public string AddBook([FromBody] EditBook editBook)
        {
            if (editBook == null) return "Error, empty fields";
            else
            {
                var book = new Book();
                DateTime publicationDate;
                bool convertDate = DateTime.TryParse(editBook.PublicationDate, out publicationDate);
                if (convertDate == false)
                {
                    return "Incorrect date";
                }
                else
                {
                    if (db.Authors.ToList().Find(x => x.Id == editBook.AuthorID) == null |
                        db.Genres.ToList().Find(x => x.Id == editBook.GenreId) == null) return "Error, incorrect author/genre ID";
                    else
                    {
                        book.Title = editBook.Title;
                        book.AuthorID = editBook.AuthorID;
                        book.GenreId = editBook.GenreId;
                        book.PublicationDate = publicationDate;
                        db.Books.Add(book);
                        db.SaveChanges();
                        return $"Done, book ID = {book.Id}";
                    }
                }    
            }
        }

        // PUT api/<BookController>/5
        [ActionName("EditBook")]
        [HttpPut("{id}")]
        public string EditBook(int id, [FromBody] EditBook editBook)
        {
            var book = db.Books.ToList().Find(x => x.Id == id);
            if (book == null) return "Book not found";
            else
            {
                DateTime publicationDate;
                bool convertDate = DateTime.TryParse(editBook.PublicationDate, out publicationDate);
                if (convertDate == false)
                {
                    return "Incorrect date";
                }
                else
                {
                    if (db.Authors.ToList().Find(x => x.Id == editBook.AuthorID) == null |
                        db.Genres.ToList().Find(x => x.Id == editBook.GenreId) == null) return "Error, incorrect author/genre ID";
                    else
                    {
                        book.Title = editBook.Title;
                        book.AuthorID = editBook.AuthorID;
                        book.GenreId = editBook.GenreId;
                        book.PublicationDate = publicationDate;
                        db.SaveChanges();
                        return "Done";
                    }
                    
                }
            }
        }

        // DELETE api/<BookController>/5
        [ActionName("DeleteBook")]
        [HttpDelete("{id}")]
        public string DeleteBook(int id)
        {
            Book? book = db.Books.ToList().Find(x => x.Id == id);
            if (book == null) return "Error, book not fount";
            else
            {
                db.Books.Remove(book);
                return "Done";
            }
        }
    }
}
