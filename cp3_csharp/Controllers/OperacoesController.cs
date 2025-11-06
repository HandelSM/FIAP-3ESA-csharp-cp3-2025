using cp3_csharp.Data;
using cp3_csharp.Models.Database;
using cp3_csharp.Models.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace cp3_csharp.Controllers;


[ApiController]
[Route("api/[controller]")]
public class OperacoesController : ControllerBase
{

    private readonly LojaContext _db;
    public OperacoesController(LojaContext db)
    {
        _db = db;
    }


    [HttpGet("GetInfo")]
    public async Task<ActionResult<IEnumerable<InfoEstoqueDTO>>> GetInfo([FromQuery] int? bandaId)
    {
        // Questão 2
        // o endpoint api/Camisas/todas retorna todas as camisas,
        // mas recebemos o ID da banda, não sendo tão descritivo.
        // implemente esse endpoint que retorne uma lista de InfoEstoqueDTO mostrando todas as camisas em estoque.
        // caso bandaId seja informado, filtrar apenas as camisas daquela banda.

        throw new NotImplementedException();
    }


    [HttpPost("GerarEstoqueInicial")]
    public async Task<ActionResult<string>> EstoqueInicial()
    {
        // Questão 3
        // Gerar arquvo <dia_de_hoje>_estoque_inicial.txt em /files
        // e retornar a string do arquivo também na chamada.

        throw new NotImplementedException();
    }

    [HttpPost("GerarEstoqueFinal")]
    public async Task<ActionResult<string>> EstoqueFinal()
    {
        // Questão 4
        // Gerar arquvo <dia_de_hoje>_estoque_final.txt em /files a partir de <dia_de_hoje>_compras.txt em /files
        // retornar a string do arquivo também na chamada.
        throw new NotImplementedException();
    }

    [HttpPost("AtualizarEstoque")]
    public async Task<ActionResult> AtualizarEstoque()
    {
        // Questão 5
        // Atualizar o estoque das camisas com base no arquivo
        // <dia_de_hoje>_estoque_final.txt
        throw new NotImplementedException();
    }

    [HttpPost("SalvarPedidos")]
    public async Task<ActionResult> SalvarPedidos()
    {
        // Questão 6
        // Salvar os pedidos no banco de dados a partir do arquivo
        // <dia_de_hoje>_compras.txt em /files
        // ou seja, adicionar as entradas na tabela Pedidos e ItemPedido
        throw new NotImplementedException();
    }
}
