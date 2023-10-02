namespace StoreHouse.Database.Entities;

public class WriteOffCause
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;

    public List<WriteOff> WriteOffs { get; set; } = new();
}