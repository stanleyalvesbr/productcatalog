using System;

namespace ProductCatalog.ViewModels.ProductViewModels
{
    public class ListProductViewModel
    {
       public int Id { get; set; }  
       public string Title { get; set; }    
       public string Description { get; set; }  
       public decimal Price { get; set; }
       public int Quantiity { get; set; }   
       public int CategoryId { get; set; }  
       public string Category { get; set; }
    }
}