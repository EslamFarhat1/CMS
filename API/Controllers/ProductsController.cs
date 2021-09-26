using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly StoreContext _context;
        public ProductsController(StoreContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> getProducts(){
            return  Ok(await _context.Products.ToListAsync());
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<Product>>> getProducts(int id){
            var product=await _context.Products.FindAsync(id);
            if(product!=null)
            return Ok(product);
            return NotFound();
        }
        [HttpPost]
        public async Task<ActionResult<Product>> CreateProduct([FromBody] Product prod){
            if(prod !=null){
                var result = await _context.AddAsync(prod);
                await _context.SaveChangesAsync();
                return  Ok( );
                
            }
            return BadRequest();
        }
    }
}