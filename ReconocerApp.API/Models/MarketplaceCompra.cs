using System.ComponentModel.DataAnnotations;

namespace ReconocerApp.API.Models;

public class MarketplaceCompra
{
    [Key]
    public int CompraId { get; set; }
    public string ColaboradorId { get; set; } = string.Empty;
    public int PremioId { get; set; }
    public string FechaCompra { get; set; } = string.Empty;
    public string Estado { get; set; } = string.Empty;
    public string ComentarioRevision { get; set; } = string.Empty;
    public string FechaResolucion { get; set; } = string.Empty;

    public virtual Colaborador? Colaborador { get; set; }
    public virtual MarketplacePremio? Premio { get; set; }
}
