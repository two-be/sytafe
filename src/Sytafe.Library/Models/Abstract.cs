namespace Sytafe.Library.Models;

public class Abstract
{
    public string Id { get; set; } = Guid.NewGuid().ToString("N");
}