using System.ComponentModel.DataAnnotations;
using StoreHouse.Api.Model.DTO.ProductListDTO;

namespace StoreHouse.Api.Model.DTO.StorageDTO;

public class StorageWriteOffRequest
{
    public int Id { get; set; }
    [Required]
    public DateTime Date { get; set; }
    public string Cause { get; set; }
    public string Comment { get; set; }
    [Required]
    public List<WriteOffProductListRequest> ProductList { get; set; }
}