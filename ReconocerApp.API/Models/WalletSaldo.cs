using System.ComponentModel.DataAnnotations;

namespace ReconocerApp.API.Models;

public class WalletSaldo
{
    [Key]
    public int WalletSaldoId { get; set; }
    public string ColaboradorId { get; set; } = string.Empty;
    public int SaldoActual { get; set; }

    public virtual Colaborador? Colaborador { get; set; }
    public virtual ICollection<WalletTransaccion>? Transacciones { get; set; }
}
