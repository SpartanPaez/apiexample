namespace WebApi.Domain.Entities.Products;

public class Shoe
{
    public Guid Id { get; set; }
    public string? Brand { get; set; }
    public string? Model { get; set; }
    public string? Size { get; set; }
    public decimal? Price { get; set; }
    public int Stock { get; set; }
    public string? Season { get; set; }
}