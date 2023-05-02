namespace NestStart.Models;

public class ProductImage
{
    public int Id { get; set; }
    public string Image { get; set; }
    public bool IsFront { get; set; }
    public bool IsBack { get; set; }
    public int Productid { get; set; }
    public Product Product { get; set; }

}
