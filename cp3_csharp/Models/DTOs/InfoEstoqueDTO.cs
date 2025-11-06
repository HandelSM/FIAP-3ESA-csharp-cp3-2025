namespace cp3_csharp.Models.DTOs;

public class InfoEstoqueDTO
{
    public string BandaNome { get; set; }
    public string BandaPais { get; set; }
    public string BandaGenero { get; set; }
    public string Tamanho { get; set; }
    public string Modelo { get; set; }
    public string Cor { get; set; }
    public string Tecido { get; set; }
    public decimal Preco { get; set; }
    public int QuantidadeEmEstoque { get; set; }
    public int QuantidadeMinimaAlerta { get; set; }
}
