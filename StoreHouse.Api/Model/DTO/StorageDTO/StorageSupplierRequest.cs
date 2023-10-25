using System.ComponentModel.DataAnnotations;

namespace StoreHouse.Api.Model.DTO.StorageDTO;

public class StorageSupplierRequest
{
    [Required]
    public string Name { get; set; }
    public string MobilePhone { get; set; }
    public string Comment { get; set; }
}