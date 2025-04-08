using System.ComponentModel.DataAnnotations;

namespace ReconocerApp.API.Models;

public class Colaborador
{
    [Key]
    public string ColaboradorId { get; set; } = string.Empty;
    public int OrganizacionId { get; set; }
    public bool ExcepcionConfiguracion { get; set; }

    public virtual Organizacion? Organizacion { get; set; }

    public virtual WalletSaldo? WalletSaldo { get; set; }
    public virtual ICollection<WalletTransaccion>? Transacciones { get; set; }
    public virtual ICollection<MarketplaceCompra>? Compras { get; set; }
    public virtual ICollection<Reconocimiento>? Reconocimientos { get; set; }
}
