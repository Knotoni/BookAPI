using Microsoft.AspNetCore.Mvc;
using TestBookAPI.Data;
using TestBookAPI.Data.Models;
using TestBookAPI.Data.EditModels;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TestBookAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class GenresController : ControllerBase
    {
        private readonly BookDataContext db;
        public GenresController(BookDataContext context)
        {
            db = context;
        }
        // GET: api/<GenresController>
        [ActionName("GetAllGenres")]
        [HttpGet]
        public IEnumerable<Genre> GetAllGenres()
        {
            var genres = db.Genres.ToList();
            return genres;
        }

        // GET api/<GenresController>/5
        [ActionName("GetGenrt")]
        [HttpGet("{id}")]
        public object GetGenre(int id)
        {
            var genre = db.Genres.ToList().Find(x => x.Id == id);
            if (genre != null) return genre;
            else return "Not found";
        }

        // POST api/<GenresController>
        [HttpPost]
        public string AddGenre([FromBody] string editGenre)
        {
            Genre genre = new Genre();
            if (editGenre == null) return "Error, empty fields";
            else
            {
                genre.GenreName = editGenre;
                db.Genres.Add(genre);
                db.SaveChanges();
                return $"Done genre ID = {genre.Id}";
            }
        }

        // PUT api/<GenresController>/5
        [ActionName("EditGenre")]
        [HttpPut("{id}")]
        public string EditGenre(int id, [FromBody] string editGenre)
        {
            var genre = db.Genres.ToList().Find(x => x.Id == id);
            if (genre == null) return "Error, genre not found";
            else
            {
                if (editGenre == null) return "Error, empty fields";
                else
                {
                    genre.GenreName = editGenre;
                    db.SaveChanges();
                    return "Done";
                }
            }
        }

        // DELETE api/<GenresController>/5
        [ActionName("DeleteGenre")]
        [HttpDelete("{id}")]
        public string DeleteGenre(int id)
        {
            if (id == 0) return "Error, empty fields";
            else
            {
                var genre = db.Genres.ToList().Find(x => x.Id == id);
                if (genre == null) return "Error, genre not found";
                else
                {
                    db.Genres.Remove(genre);
                    db.SaveChanges();
                    return "Done";
                }
            }
        }
    }
}
