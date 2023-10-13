using System.ComponentModel.DataAnnotations;
using StoreHouse.Api.Model.DTO.ProductListDTO;

namespace StoreHouse.Api.Model.DTO.StorageDTO;

public class StorageSupplyRequest
{
    public int Id { get; set; }
    [Required]
    public DateTime Date { get; set; }
    [Required]
    public string SupplierName { get; set; }
    public string Comment { get; set; }
    [Required]
    public List<SupplyProductListRequest> ProductList { get; set; }
}