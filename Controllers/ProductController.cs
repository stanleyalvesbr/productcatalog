using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using ProductCatalog.Models;
using ProductCatalog.ViewModels.ProductViewModels;
using ProductCatalog.Repositories;

namespace ProductCatalog.Controllers
{
    public class ProductController : Controller
    {
        private readonly ProductRepository _repository;
        public ProductController(ProductRepository repository)
        {
           _repository = repository;
        }


        
        [HttpGet]
        [Route("v1/products")]
        public IEnumerable<ListProductViewModel> Get()
        {
            return _repository.Get(); 
        }

       
        [HttpGet]
        [Route("v1/products/{id}")]
        public Product Get(int id)
        {
            return _repository.Get(id);
        }

        
        [HttpPost]
        [Route("v1/products")]
        public ResultViewModel Post([FromBody]EditorProductViewModel model)
        {
            model.Validate();
            if(model.Invalid)
               return new ResultViewModel
               {
                   Success = false,
                   Message = "Não foi possível cadastrar produto",
                   Data = model.Notifications
               };

            var product = new Product();
            product.Title = model.Title;
            product.CategoryId = model.CategoryId;
            product.CreateDate = DateTime.Now;
            product.Description = model.Description;
            product.Image = model.Image;
            product.LastUpdateDate =  DateTime.Now;
            product.Price =  model.Price;
            product.Quantity = model.Quantity;

            _repository.Save(product);


            return new ResultViewModel
            {
                Success = true,
                Message = "Produto cadastrado com sucesso",
                Data = product
            };
            
        }

        
        [HttpPut]
        [Route("v1/products")]
        public ResultViewModel Put([FromBody]EditorProductViewModel model)
        {
            model.Validate();
            if(model.Invalid)
               return new ResultViewModel
               {
                   Success = false,
                   Message = "Não foi possível cadastrar produto",
                   Data = model.Notifications
               };

            var product = _repository.Get(model.Id);
            product.Title = model.Title;
            product.CategoryId = model.CategoryId;
            product.CreateDate = DateTime.Now;
            product.Description = model.Description;
            product.Image = model.Image;
            product.LastUpdateDate =  DateTime.Now;
            product.Price =  model.Price;
            product.Quantity = model.Quantity;

            _repository.Update(product);

           

            return new ResultViewModel
            {
                Success = true,
                Message = "Produto alterado com sucesso",
                Data = product
            };
            
        }

        
        [HttpDelete]
        [Route("v1/products")]
        public ResultViewModel Delete([FromBody]EditorProductViewModel model)
        {
            model.Validate();
            if(model.Invalid)
               return new ResultViewModel
               {
                   Success = false,
                   Message = "Não foi possível Excluir Produto",
                   Data = model.Notifications
               };

            var product = _repository.Get(model.Id);
            product.Title = model.Title;
            product.CategoryId = model.CategoryId;
            product.CreateDate = DateTime.Now;
            product.Description = model.Description;
            product.Image = model.Image;
            product.LastUpdateDate =  DateTime.Now;
            product.Price =  model.Price;
            product.Quantity = model.Quantity;

            _repository.Delete(product);

            return new ResultViewModel
            {
                Success = true,
                Message = "Produto Excluído com sucesso",
                Data = product
            };
            
        }





    }
}