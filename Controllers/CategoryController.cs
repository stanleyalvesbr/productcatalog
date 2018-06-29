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

        [Route("v1/categories")]
        [HttpGet]

       public IEnumerable<Category> Get()
        {
            return _repository.Get();
        }


        [Route("v1/categories/{id}")]
        [HttpGet]
        public Category Get(int id)
        {
           //return _context.Categories.AsNoTracking().Where(x => x.Id == id).FirstOrDefault();
           return _repository.Get(id);
        }

        [Route("v1/categories/{id}/products")]
        [HttpGet]
        public IEnumerable<Product> GetProducts(int id)
        {
           return _repository.GetProducts(id);
        } 


        [Route("v1/categories")]
        [HttpPost]
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

        [Route("v1/categories")]
        [HttpPut]
        
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

        [Route("v1/categories")]
        [HttpDelete]
        
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
