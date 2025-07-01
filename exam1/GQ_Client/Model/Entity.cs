namespace GQ_Client.Model;

public class Entity
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public override string ToString()
    {
        return $"Id: {Id}, Name: {Name}, Desc: {Description}";
    }
}