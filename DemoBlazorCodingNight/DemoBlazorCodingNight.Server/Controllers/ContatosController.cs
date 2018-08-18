using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DemoBlazorCodingNight.Server.Exceptions;
using DemoBlazorCodingNight.Server.Services;
using DemoBlazorCodingNight.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DemoBlazorCodingNight.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContatosController : ControllerBase
    {
        private IContatoService _service;

        public ContatosController(IContatoService service)
        {
            _service = service;
        }

        // GET: api/Contatos
        [HttpGet]
        public ActionResult<IEnumerable<Contato>> ListarTodos(string busca = null)
        {
            if (!String.IsNullOrWhiteSpace(busca))
            {
                return Ok(_service.Buscar(busca));
            }
            return Ok(_service.ListarTodos());
        }

        // GET: api/Contatos/5
        [HttpGet("{id}", Name = "Obter")]
        public ActionResult<Contato> Obter(int id)
        {
            try
            {
                return Ok(_service.Obter(id));
            }
            catch (NaoEncontradoException ex)
            {
                return NotFound(ex);
            }
        }

        // POST: api/Contatos
        [HttpPost]
        public ActionResult<Contato> Post([FromBody] Contato contato)
        {
            try
            {
                var dbContato = _service.Incluir(contato);
                return CreatedAtAction("Obter", dbContato);
            }
            catch (FalhaDeValidacaoException ex)
            {
                return BadRequest(ex);
            }
            
        }

        // PUT: api/Contatos/5
        [HttpPut("{id}")]
        public ActionResult<Contato> Put(int id, [FromBody] Contato contato)
        {
            try
            {
                contato.Id = id;
                _service.Atualizar(contato);
                return Ok(contato);
            }
            catch (FalhaDeValidacaoException ex)
            {
                return BadRequest(ex);
            }
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            try
            {
                _service.Excluir(id);
                return NoContent();
            }
            catch (NaoEncontradoException ex)
            {
                return NotFound(ex);
            }
        }
    }
}
