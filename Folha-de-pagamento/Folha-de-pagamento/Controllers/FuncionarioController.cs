using Folha_de_pagamento.Models;
using Folha_de_pagamento.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Folha_de_pagamento.Controllers
{
    [ApiController]
    public class FuncionarioController : Controller
    {
        private readonly FolhaDbContext _context;

        public FuncionarioController(FolhaDbContext context)
        {
            _context = context;
        }

        [Route("api/funcionario/listar")]
        [HttpGet]
        public async Task<IActionResult> GetAllFuncionarios()
        {
            var funcionario = await _context.Funcionario.ToListAsync();
            if (funcionario == null)
            {
                return NotFound("Folha Not Found");
            }
            return Ok(funcionario);
        }

        [Route("api/funcionario/cadastrar")]
        [HttpPost]
        public async Task<IActionResult> PostFuncionario(FuncionarioModel funcionario)
        {
            await _context.Funcionario.AddAsync(funcionario);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(PostFuncionario), funcionario);
        }

    }

}

