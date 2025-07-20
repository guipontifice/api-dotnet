using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjetoDB.Data;
using ProjetoDB.Models;

namespace ProjetoDB.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PersonagensController : ControllerBase
    {
        private readonly AppDbContext _appDbContext;

        public PersonagensController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        [HttpPost]
        public async Task<IActionResult> AddPersonagem([FromBody] Personagem personagem)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            _appDbContext.DB.Add(personagem);
            await _appDbContext.SaveChangesAsync();

            return Created("Personagem adicionado! ", personagem);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Personagem>>> GetPersonagens()
        {
            var personagens = await _appDbContext.DB.ToListAsync();

            return Ok(personagens);
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<Personagem>> GetPersonagens(int id)
        {
            var personagem = await _appDbContext.DB.FindAsync(id);

            if (personagem == null)
            {
                return NotFound("Personagem não encontrado");
            }
            return Ok(personagem);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePersonagem(int id, [FromBody] Personagem personagemAtualizado)
        {
            var personagemExistente = await _appDbContext.DB.FindAsync(id);

            if (personagemExistente == null)
            {
                return NotFound("Personagem não encontrado");
            }

            _appDbContext.Entry(personagemExistente).CurrentValues.SetValues(personagemAtualizado);

            await _appDbContext.SaveChangesAsync();

            return StatusCode(201, personagemExistente);
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePersonagem(int id, [FromBody] Personagem personagemAtualizado)
        {
            var personagem = await _appDbContext.DB.FindAsync(id);

            if (personagem == null)
            {
                return NotFound("Personagem não encontrado");
            }

            _appDbContext.DB.Remove(personagem);

            await _appDbContext.SaveChangesAsync();

            return StatusCode(201, personagem);
        }

    }
}