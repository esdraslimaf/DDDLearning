using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Api.Domain.Dtos;
using Api.Domain.Entities;
using Api.Domain.Interfaces.Services.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {

        [AllowAnonymous]
        [HttpPost] /* O atributo [FromServices] no ASP.NET Core permite a injeção de um serviço diretamente em um método de ação sem usar a injeção de construtor */
        public async Task<object> Login([FromBody] LoginDto loginDto, [FromServices] ILoginService service)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (loginDto == null)
            {
                return BadRequest();
            }

            try
            {
                var result = await service.FindByLogin(loginDto);
                if (result != null)
                {
                    return Ok(result);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (ArgumentException erro)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, erro.Message);
            }
        }
    }
    /* Isso pode tornar o código mais limpo se o serviço não for usado em outros lugares no controlador. No entanto, é importante notar que a injeção de dependência através do construtor geralmente é preferida para serviços que são usados em vários lugares no controlador.*/
}
