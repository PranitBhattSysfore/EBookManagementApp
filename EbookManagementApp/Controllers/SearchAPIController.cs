using ExceptionsHandling.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ModelClass;
using Services;
using Services.Interface;
using Services.ServiceImpl;

namespace EbookManagementApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SearchAPIController : ControllerBase
    {
        private readonly ISearch _SearchService;
        public SearchAPIController(ISearch searchService)
        {
            _SearchService = searchService;

        }
        [HttpGet]
        [Route("/SearchBookByTitle")]
        public ActionResult SearchByTitle(string title)
        {
            //List<dynamic> book = _SearchService.SearchByTitle(title);
            //if (book != null)
            //{
            //    return Ok(book);
            //}
            //else
            //{
            //    return BadRequest();
            //}
            try
            {
                List<dynamic> books = _SearchService.SearchByTitle(title);
                return Ok(books);
            }
            catch (NoBooksWithThisTitleException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }
        [HttpGet]
        [Route("/SearchBookByGenre")]
        public ActionResult SearchByGenre(int id)
        {
            //List<dynamic> book = _SearchService.SearchByGenre(id);
            try
            {
                List<dynamic> books = _SearchService.SearchByGenre(id);
                return Ok(books);
            }
            catch (NoBooksWithThisGenreException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }
        [HttpGet]
        [Route("/SearchBookByAuthor")]
        public ActionResult SearchByAuthor(Guid id)
        {
            try
            {
                var books = _SearchService.SearchByAuthor(id);
                return Ok(books);
            }
            catch (BookNotFoundException ex)
            {
                return NotFound(new { message = ex.Message }); 
            }
            //List<dynamic> book = _SearchService.SearchByAuthor(id);
            //if (book != null)
            //{
            //    return Ok(book);
            //}
            //else
            //{
            //    return BadRequest();
            //}
        }
    }
}
