namespace WebApi.Application.Common.Models;

public class ShoeDto
{
    public string? Id { get; set; }
    public string? Brand { get; set; }
    public string? Model { get; set; }
    public decimal? Price { get; set; }
    public int Stock { get; set; }
    public string? Season { get; set; }
}