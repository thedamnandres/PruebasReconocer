using System.ComponentModel.DataAnnotations;

namespace ReconocerApp.API.Models;

public class WalletTransaccion
{
    [Key]
    public int TransaccionId { get; set; }
    public string ColaboradorId { get; set; } = string.Empty;
    public int CategoriaId { get; set; }
    public int Cantidad { get; set; }
    public string Descripcion { get; set; } = string.Empty;
    public string Fecha { get; set; } = string.Empty;

    public virtual Colaborador? Colaborador { get; set; }
    public virtual WalletCategoria? Categoria { get; set; }
}
