using cp3_csharp.Models.Database;

namespace cp3_csharp.Models.DTOs
{
  public class EstoqueCriticoDTO
  {
        public int QuantidadeTotal { get; set; }
        public int QuantidadeTipos { get; set; }
        public List<Camisa> Camisas { get; set; } = new();
    }
}
