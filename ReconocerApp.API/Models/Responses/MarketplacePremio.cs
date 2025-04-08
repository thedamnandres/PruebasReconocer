namespace ReconocerApp.API.Models.Responses;

public class MarketplacePremio
{
    public int PremioId { get; set; }
    public int OrganizacionId { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public string Descripcion { get; set; } = string.Empty;
    public int CostoWallet { get; set; }
    public string ImagenUrl { get; set; } = string.Empty;
    public int CantidadActual { get; set; }
    public string UltimaActualizacion { get; set; } = string.Empty;
    public Org? Organizacion { get; set; }
}
