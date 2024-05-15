using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ModelClass;
using ModelClass.Dto;
using Services.Interface;
using Services.ServiceImpl;

namespace EbookManagementApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {


        private readonly ILogin _loginService;
        public AuthController(ILogin registerloginService)
        {
            _loginService = registerloginService;
        }
        [HttpPost]
        [Route("/Register")]
        public ActionResult RegisterUser(RegisterDto dto)
        {
            _loginService.Register(dto);
            return Ok();
        }
        [HttpPost]
        [Route("/login")]
        public ActionResult LoginUser(LoginDto dto)
        {
            return Ok(_loginService.Login(dto));
        }
    }
}
