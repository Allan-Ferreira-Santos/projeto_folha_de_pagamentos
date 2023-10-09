using Folha_de_pagamento.Models;
using Folha_de_pagamento.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Policy;

[ApiController]
[Route("api/folha")]
public class FolhaController : Controller
{
    private readonly FolhaDbContext _context;

    public FolhaController(FolhaDbContext context)
    {
        _context = context;
    }

    [HttpGet("listar")]
    public async Task<IActionResult> GetAllFolha()
    {
        var folhas = await _context.Folha.ToListAsync();

        var result = folhas.Select(f => new
        {
            folhaId = f.FolhaId,
            valor = f.Valor,
            quantidade = f.Quantidade,
            mes = f.Mes,
            ano = f.Ano,
            salarioBruto = f.CalcularSalarioBruto(),
            impostoIrrf = f.CalcularImpostoIrrf(),
            impostoInss = f.CalcularImpostoInss(),
            impostoFgts = f.CalcularImpostoFgts(),
            salarioLiquido = f.CalcularSalarioLiquido(),
            funcionario = new
            {
                funcionarioId = _context.Funcionario.FirstOrDefault(func => func.FuncionarioId == f.FuncionarioId)?.FuncionarioId,
                nome = _context.Funcionario.FirstOrDefault(func => func.FuncionarioId == f.FuncionarioId)?.Name,
                cpf = _context.Funcionario.FirstOrDefault(func => func.FuncionarioId == f.FuncionarioId)?.CPF
            },
            funcionarioId = f.FuncionarioId,
        }).ToList();
        if (result == null)
        {
            return NotFound("Folhas Not Found");
        }

        return Ok(result);
    }


    [HttpPost("cadastrar")]
    public async Task<IActionResult> PostFolha(FolhaModel folha)
    {
        await _context.Folha.AddAsync(folha);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(PostFolha), folha);
    }

    [HttpGet("buscar/{cpf}/{mes}/{ano}")]
    public async Task<IActionResult> GetFolhaByCPFMesAno(string cpf, int mes, int ano)
    {
        var funcionario = await _context.Funcionario.FirstOrDefaultAsync(f => f.CPF == cpf);

        if (funcionario == null)
        {
            return NotFound("Funcionario Not Found");
        }

        var folha = await _context.Folha.FirstOrDefaultAsync(f =>
            f.FuncionarioId == funcionario.FuncionarioId &&
            f.Mes == mes &&
            f.Ano == ano);

        if (folha == null)
        {
            return NotFound("Folha Not Found");
        }

        var result = new
        {
            folhaId = folha.FolhaId,
            valor = folha.Valor,
            quantidade = folha.Quantidade,
            mes = folha.Mes,
            ano = folha.Ano,
            salarioBruto = folha.CalcularSalarioBruto(),
            impostoIrrf = folha.CalcularImpostoIrrf(),
            impostoInss = folha.CalcularImpostoInss(),
            impostoFgts = folha.CalcularImpostoFgts(),
            salarioLiquido = folha.CalcularSalarioLiquido(),
            funcionario = new
            {
                funcionarioId = funcionario.FuncionarioId,
                nome = funcionario.Name,
                cpf = funcionario.CPF
            },
            funcionarioId = folha.FuncionarioId
        };
        if (result == null)
        {
            return NotFound("Folha Not Found");
        }

        return Ok(result);
    }

}
