using Core.Entities;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;

[ApiController]
[Route("/api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly StoreContext _context;

    public ProductsController(StoreContext context)
    {
        _context = context;
    }
    
    [HttpGet]
    public async Task<ActionResult<List<Product>>> GetProducts()
    {
        var products = await (_context.Products ?? throw new InvalidOperationException()).ToListAsync();
        return products;
    }
    
    [HttpGet("{id:int}")]
    
    public async Task<ActionResult<Product>> GetProducts(int id)
    {
        if (_context.Products == null) return NotFound();
        var product = await _context.Products.FindAsync(id);
        if (product == null)
        {
            return NotFound();
        }
        return product;

    }
}