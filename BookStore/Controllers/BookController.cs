using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using BookStore.Models;
using System.Linq;

namespace BookStore.Controllers
{
    [Route("api/[controller]")]
    public class BookController : Controller
    {
        private readonly BookContext _context;

        public BookController(BookContext context)
        {
            _context = context;

            if (_context.Books.Count() == 0)
            {
                _context.Books.Add(new Book()
                {
                    Title = "Uma Breve História Sobre o Tempo",
                    Author = "stephen Hawking",
                    Description = "Uma Breve História do Tempo: do Big Bang aos Buracos Negros, é um livro de divulgação científica escrito pelo Professor Stephen Hawking, publicado pela primeira vez em 1988.",
                    ISBN = "122367363",
                    Pages = 123,
                    Publisher = "Intrínseca",
                    Cover = "https://images-na.ssl-images-amazon.com/images/I/51rSMKyy8rL._SX347_BO1,204,203,200_.jpg"
                });
                _context.SaveChanges();
            }
        }

        // Read
        [HttpGet]
        public IEnumerable<Book> GetAll()
        {
            return _context.Books.ToList();
        }

        [HttpGet("{id}", Name = "GetBooks")]
        public IActionResult GetById(long id)
        {
            var item = _context.Books.FirstOrDefault(b => b.Id == id);
            if (item == null)
            {
                return NotFound();
            }
            return new ObjectResult(item);
        }

        // Create
        [HttpPost]
        public IActionResult Create([FromBody] Book item)
        {
            if (item == null)
            {
                return BadRequest();
            }

            _context.Books.Add(item);
            _context.SaveChanges();

            return CreatedAtRoute("GetBooks", new { id = item.Id }, item);
        }

        // Update
        [HttpPut("{id}")]
        public IActionResult Update(long id, [FromBody] Book item)
        {
            if (item == null || item.Id != id)
            {
                return BadRequest();
            }

            var book = _context.Books.FirstOrDefault(b => b.Id == id);

            if (book == null)
            {
                return NotFound();
            }
            else
            {
                book.Title = item.Title;
                book.Description = item.Description;
                book.Author = item.Author;
                book.ISBN = item.ISBN;
                book.Pages = item.Pages;
                book.Publisher = item.Publisher;
            }

            _context.Books.Update(book);
            _context.SaveChanges();

            return new NoContentResult();
        }

        //Delete
        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            var todo = _context.Books.FirstOrDefault(t => t.Id == id);
            if (todo == null)
            {
                return NotFound();
            }

            _context.Books.Remove(todo);
            _context.SaveChanges();
            return new NoContentResult();
        }


    }
}