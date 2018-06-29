using System;
using System.Collections.Generic;
using ProductCatalog.Models;

namespace ProductCatalog.ViewModels.CategoryViewModels
{
    public class ListCategoryViewModel
    {
      public int Id { get; set; }
      public string Title { get; set; } 
      public IEnumerable<Product> Products { get; set; }
    }
}