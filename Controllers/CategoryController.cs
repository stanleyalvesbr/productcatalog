using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using ProductCatalog.Data;
using ProductCatalog.Models;


namespace ProductCatalog.Controllers
{
    public class CategoryController : Controller
    {
        private readonly StoreDataContext _context;
        public CategoryController(StoreDataContext context)
        {
           _context = context;
        }

        [Route("v1/categories")]
        [HttpGet]

        public IEnumerable<Category> Get()
        {
            return _context.Categories.AsNoTracking().ToList();
        }

        [Route("v1/categories/{id}")]
        [HttpGet]
        public Category Get(int id)
        {
           return _context.Categories.AsNoTracking().Where(x => x.Id == id).FirstOrDefault();
        }

        [Route("v1/categories/{id}/products")]
        [HttpGet]

        public IEnumerable<Product> GetProducts(int id)
        {
             return _context.Products.AsNoTracking().Where(x => x.Category.Id == id).ToList() ;
        }
    }
}
