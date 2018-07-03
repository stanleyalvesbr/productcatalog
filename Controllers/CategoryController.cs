using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using ProductCatalog.Data;
using ProductCatalog.Models;
using ProductCatalog.Repositories;
using ProductCatalog.ViewModels.CategoryViewModels;
using ProductCatalog.ViewModels.ProductViewModels;

namespace ProductCatalog.Controllers
{
    public class CategoryController : Controller
    {
        private readonly CategoryRepository _repository;
        public CategoryController(CategoryRepository repository)
        {
           _repository = repository;
        }

        
        [HttpGet]
        [Route("v1/categories")]
        //[ResponseCache(Location = ResponseCacheLocation.Client, Duration = 60)] // Armazenar no cliente
        public IEnumerable<Category> Get()
        {
            return _repository.Get();
        }


        [HttpGet]
        [Route("v1/categories/{id}")] 
        //[ResponseCache(Duration = 60)]
        public Category Get(int id)
        {
           //return _context.Categories.AsNoTracking().Where(x => x.Id == id).FirstOrDefault();
           return _repository.Get(id);
        }

        
        [HttpGet]
        [Route("v1/categories/{id}/products")]
        [ResponseCache(Duration = 60)]
        public IEnumerable<Product> GetProducts(int id)
        {
           return _repository.GetProducts(id);
        } 


        [HttpPost]
        [Route("v1/categories")]
        public ResultViewModel Post([FromBody]EditorCategoryViewModel model)
        {
            model.Validate();
            if(model.Invalid)
               return new ResultViewModel
               {
                   Success = false,
                   Message = "Não foi possível cadastrar Categoria",
                   Data = model.Notifications
               };

            var category = new Category();
            category.Title = model.Title;
            
            _repository.Save(category);


            return new ResultViewModel
            {
                Success = true,
                Message = "Categoria cadastrado com sucesso",
                Data = category
            };
            
        }

        
        [HttpPut]
        [Route("v1/categories")]
        
        public ResultViewModel Put([FromBody]EditorCategoryViewModel model)
        {
            model.Validate();
            if(model.Invalid)
               return new ResultViewModel
               {
                   Success = false,
                   Message = "Não foi possível cadastrar Categoria",
                   Data = model.Notifications
               };

            var category = _repository.Get(model.Id);
            category.Title = model.Title;
            
            _repository.Update(category);
 

            return new ResultViewModel
            {
                Success = true,
                Message = "Categoria alterada com sucesso",
                Data = category
            };
            
        }

        
        [HttpDelete]
        [Route("v1/categories")]
        public ResultViewModel Delete([FromBody]EditorCategoryViewModel model)
        {
            model.Validate();
            if(model.Invalid)
               return new ResultViewModel
               {
                   Success = false,
                   Message = "Não foi possível Excluir Categoria",
                   Data = model.Notifications
               };

            var category = _repository.Get(model.Id);
            category.Title = model.Title;           

            _repository.Delete(category);

            return new ResultViewModel
            {
                Success = true,
                Message = "Categoria Excluída com sucesso",
                Data = category
            };
            
        }
        
    
    }
}
