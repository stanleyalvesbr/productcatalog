using System;
using System.Collections.Generic;
using Flunt.Notifications;
using Flunt.Validations;
using ProductCatalog.Models;

namespace ProductCatalog.ViewModels.CategoryViewModels
{
    public class EditorCategoryViewModel : Notifiable, IValidatable
    {
        public int Id { get; set; }
        public string Title { get; set; } 

        public IEnumerable<Product> Products { get; set; }
        public void Validate()
        {
            AddNotifications(
               new Contract()
               .HasMaxLen(Title,120,"Title","O Título deve conter até 120 caracteres")
               .HasMinLen(Title,3,"Title","O Título deve conter pelo menos 3 caracteres")
            );
        }
    }
}