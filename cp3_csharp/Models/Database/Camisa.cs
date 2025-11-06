namespace cp3_csharp.Models.Database;


public class Camisa
{
    public int Id { get; set; }
    public int BandaId { get; set; }
    public TamanhoCamisa Tamanho { get; set; }
    public string Modelo { get; set; }
    public string Cor { get; set; }
    public string Tecido { get; set; }
    public decimal Preco { get; set; }
    public int QuantidadeEmEstoque { get; set; }
    public int QuantidadeMinimaAlerta { get; set; }
}


public enum TamanhoCamisa
{
    PP,
    P,
    M,
    G,
    GG,
    XG
}
