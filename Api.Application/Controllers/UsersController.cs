using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Api.Domain.Entities;
using Api.Domain.Interfaces.Services.User;
using Microsoft.AspNetCore.Mvc;

namespace Api.Application.Controllers
{
    //http://localhost:5156/api/users
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private IUserService _service;

        public UsersController(IUserService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            if (!ModelState.IsValid)
            { 
                return BadRequest(ModelState); //Cód 400 - Solicitação inválida!
            }
            try
            {
                return Ok(await _service.GetAll());
            }
            catch (ArgumentException erro)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, erro.Message);
            }
        }

        [HttpGet] // Poderia ser apenas [HttpGet("{id}")] para fazer de forma resumida
        [Route("{id}", Name = "GetById")] /*no Name estamos noemando nossa url. Exemplo: var link = $"localhost:3000/livros/{livro.Id}"; é nossa rota comum, podemos nomear ela
        então nomeada poderíamos fazer: var link = Url.RouteUrl("GetById", new { id = livro.Id });  vamos usar isso no método post para entender*/
        public async Task<IActionResult> GetById(Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); //Cód 400 - Solicitação inválida!
            }
            try
            {
                return Ok(await _service.Get(id));
            }
            catch (ArgumentException erro)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, erro.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] UserEntity user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var userRetornado = await _service.Post(user);
                if (userRetornado != null)
                {
                    return Created(new Uri(Url.Link("GetById", new { id = user.Id })), user);
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (ArgumentException erro)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, erro.Message);
            }
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] UserEntity user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                UserEntity userAtualizado = await _service.Put(user);
                if (userAtualizado != null)
                {
                    return Ok(userAtualizado);
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (ArgumentException erro)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, erro.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id){
            if(!ModelState.IsValid){
                return BadRequest(ModelState);
            }

            try
            {
               return Ok(_service.Delete(id));
            }
            catch (ArgumentException erro)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, erro.Message);
            }


        }



    }
}
