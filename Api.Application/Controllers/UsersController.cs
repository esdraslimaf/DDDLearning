using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Api.Domain.Entities;
using Api.Domain.Interfaces.Services.User;
using Microsoft.AspNetCore.Authorization;
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

        [Authorize("Bearer")]
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
                return StatusCode((int)HttpStatusCode.InternalServerError, erro.Message); // Erro interno do servidor (500)
            }
        }

        [Authorize("Bearer")]
        [HttpGet] // Poderia ser apenas [HttpGet("{id}")] para fazer de forma resumida
        [Route("{id}", Name = "GetById")] /*no Name estamos nomeando nossa url. Exemplo: var link = $"localhost:3000/livros/{livro.Id}"; é nossa rota comum, podemos nomea-la
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

        [Authorize("Bearer")]
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
                    return Created(new Uri(Url.Link("GetById", new { id = userRetornado.Id })), userRetornado);
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


        [Authorize("Bearer")]
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


        [Authorize("Bearer")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                return Ok(await _service.Delete(id));
            }
            catch (ArgumentException erro)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, erro.Message);
            }


        }



    }
}
