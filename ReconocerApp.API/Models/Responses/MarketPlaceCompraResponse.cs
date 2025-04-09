namespace ReconocerApp.API.Models.Responses;

public class MarketplaceCompraResponse
{
    public int CompraId { get; set; }
    public string ColaboradorId { get; set; } = string.Empty;
    public int PremioId { get; set; }
    public string FechaCompra { get; set; } = string.Empty;
    public string Estado { get; set; } = string.Empty;
    public string ComentarioRevision { get; set; } = string.Empty;
    public string FechaResolucion { get; set; } = string.Empty;
}
