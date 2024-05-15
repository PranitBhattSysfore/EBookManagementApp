using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ModelClass;
using ModelClass.Dto;
using Services.Interface;

namespace EbookManagementApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BooksController: ControllerBase
    {
        private readonly IBooks _bookService;
        public BooksController(IBooks bookService)
        {
            _bookService = bookService;
        }
        [Authorize(Roles ="User")]
        [HttpGet]
        [Route("/GetAllBooks")]
        public ActionResult GetAllBooks()
        {
            List<Book> book = new List<Book>();
            book = _bookService.GetAllBooks();
            if(book != null)
            {
                return Ok(book);
            }
            else
            {
                return BadRequest();
            }
        }
        [HttpGet]
        [Route("/GetBookById")]
        public ActionResult GetBookById(Guid id)
        {
            Book book = new Book();
            book = _bookService.GetBooksById(id);
            if (book != null)
            {
                return Ok(book);
            }
            else
            {
                return BadRequest();
            }
        }
        [HttpPost]
        [Route("/AddNewBook")]
        public IActionResult AddBook([FromBody]BookWithAuthorsModel book) { 
            if(book != null) { 
                _bookService.CreateNewBook(book.Book, book.AuthorIds); 
                return Ok(book);
            }
            else
            { 
                 return NotFound();
            }
        }
        [HttpPut]
        [Route("/UpdateBook")]
        public ActionResult UpdateBook([FromBody] UpdateBookDto Updatebook, Guid id)
        {
            return Ok(_bookService.UpdateBook(Updatebook, id));
        }
        //[HttpDelete]
        //[Route("/SoftDeleteBook")]
        //public ActionResult Softdelete(Guid id)
        //{
        //    if (id != null)
        //    {
        //        _bookService.SoftDelete(id);
        //        return Ok("Soft Deleted");
        //    }
        //    else
        //    {
        //        return BadRequest();
        //    }
        //}
        [HttpDelete]
        [Route("/HardDeleteBook")]
        public ActionResult Harddelete(Guid id)
        {
            if (id != null)
            {
                _bookService.HardDelete(id);
                return Ok("Hard Deleted");
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
