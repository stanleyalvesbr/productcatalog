using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using ProductCatalog.Data;
using ProductCatalog.Models;
using ProductCatalog.ViewModels.CategoryViewModels;


namespace ProductCatalog.Repositories
{
   public class CategoryRepository
   {
     private readonly StoreDataContext _context;

      public CategoryRepository(StoreDataContext context)
      {
          _context = context;
      }

      public IEnumerable<ListCategoryViewModel> GetProducts()
      {
         return _context.Categories
                .Include(x => x.Products)
                 .Select(x => new ListCategoryViewModel
                {
                    Id = x.Id,
                    Title = x.Title                   
                })
                .AsNoTracking()
                .ToList();
      }
      public IEnumerable<Category> Get()
      {
         return _context.Categories.AsNoTracking().ToList();
      }
      public Category Get(int id)
      {
         return _context.Categories.Find(id);
      }

      public Category GetById(int id)
      {
         return _context.Categories.AsNoTracking().Where(x => x.Id == id).FirstOrDefault();
      }
      public IEnumerable<Product> GetProducts(int id)
      {
         return _context.Products.AsNoTracking().Where(x => x.CategoryId == id).ToList();
      }

      public void Save(Category category)
     {
          _context.Categories.Add(category);
          _context.SaveChanges();
     }

     public void Update(Category category)
     {
         _context.Entry<Category>(category).State = EntityState.Modified;
         _context.SaveChanges();
     }
     
     public void Delete(Category category)
     {
         _context.Categories.Remove(category);
         _context.SaveChanges();

     }




   }





}