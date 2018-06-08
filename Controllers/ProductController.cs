using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using ProductCatalog.Data;
using ProductCatalog.Models;
using ProductCatalog.ViewModels.ProductViewModels;


namespace ProductCatalog.Controllers
{
    public class ProductController : Controller
    {
        private readonly StoreDataContext _context;
        public ProductController(StoreDataContext context)
        {
           _context = context;
        }


        [Route("v1/products")]
        [HttpGet]
        public IEnumerable<ListProductViewModel> Get()
        {
            return _context.Products
                .Include(x => x.Category)
                .Select(x => new ListProductViewModel
                {
                    Id = x.Id,
                    Title = x.Title,
                    Price = x.Price,
                    Category = x.Category.Title,
                    CategoryId = x.Category.Id
                })
                .AsNoTracking()
                .ToList();
        }

        [Route("v1/products/{id}")]
        [HttpGet]

        public Product Get(int id)
        {
            return _context.Products.AsNoTracking().Where(x => x.Id == id).FirstOrDefault();
        }




    }
}