using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ModelClass;
using ModelClass.Dto;
using Services.Interface;

namespace EbookManagementApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        private readonly IAuthor _authorService;
        public AuthorsController(IAuthor AuthorService)
        {
            _authorService = AuthorService;
        }
        [Authorize(Roles ="Admin")]
        [HttpGet]
        [Route("/GetAllAuthors")]
        public ActionResult GetAllAuthors()
        {
            return Ok(_authorService.GetAllAuthors());
        }
        [HttpPost]
        [Route("/AddNewAuthor")]
        public ActionResult AddNewAuthor([FromBody] AuthorDto ado)
        {
            return Ok(_authorService.AddNewAuthor(ado));
        }
        [HttpDelete]
        [Route("/DeleteAuthor")]
        public ActionResult DeleteAuthor(Guid id)
        {
            _authorService.HardDelete(id);
            return Ok();
        }
        [HttpPut]
        [Route("/UpdateAuthor")]
        public ActionResult UpdateAuthor([FromBody] AuthorUpdateDto audto,Guid id)
        {
            return Ok(_authorService.UpdateAuthor(audto,id));
        }
    }
}
