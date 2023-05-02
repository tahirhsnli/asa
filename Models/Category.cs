namespace NestStart.Models;

public class Category
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Logo { get; set; } = null!;
    public string Image { get; set; } = null!;
    public bool IsDeleted { get; set; }
    public ICollection<Product> Products { get; set; }
}
