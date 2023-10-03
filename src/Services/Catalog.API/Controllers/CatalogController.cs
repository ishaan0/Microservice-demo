using Catalog.API.Interfaces.Manager;
using Catalog.API.Models;
using CoreApiResponse;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using System.Net;

namespace Catalog.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CatalogController : BaseController
    {
        IProductManager _productManager;
        public CatalogController(IProductManager productManager)
        {
            _productManager = productManager;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Product>), (int)HttpStatusCode.OK)]
        [ResponseCache(Duration = 5)]
        public IActionResult GetProducts()
        {
            var products = _productManager.GetAll();
            return CustomResult("all works fine", products, HttpStatusCode.OK);
        }

        [HttpGet]
        [ProducesResponseType(typeof(Product), (int)HttpStatusCode.OK)]
        public IActionResult GetById(string Id)
        {
            try
            {
                var product = _productManager.GetById(Id);
                //if (product != null)
                //{
                //    return CustomResult("product retrived successfully", product, HttpStatusCode.OK);
                //}
                //return CustomResult("product retrivation failed", HttpStatusCode.BadRequest);
                return CustomResult("product retrived successfully", product, HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return CustomResult("something went wrong", HttpStatusCode.BadRequest);
            }


        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Product>), (int)HttpStatusCode.OK)]
        public IActionResult GetByCategory(string category)
        {
            try
            {
                var products = _productManager.GetByCategory(category);
                //if (products != null)
                //{
                //    return CustomResult("products retrived successfully", products, HttpStatusCode.OK);
                //}
                //return CustomResult("products retrivation failed", HttpStatusCode.BadRequest);
                return CustomResult("products retrived successfully", products, HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return CustomResult("something went wrong", HttpStatusCode.BadRequest);
            }


        }

        [HttpPost]
        [ProducesResponseType(typeof(Product), (int)HttpStatusCode.Created)]
        public IActionResult CreateProduct([FromBody] Product product)
        {
            try
            {
                product.Id = ObjectId.GenerateNewId().ToString();
                bool isSaved = _productManager.Add(product);
                if (isSaved)
                {
                    return CustomResult("Product has been created", product, HttpStatusCode.Created);
                }
                return CustomResult("Product creation failed", product, HttpStatusCode.BadRequest);

            }
            catch (Exception ex)
            {
                return CustomResult("Something went wrong!!", HttpStatusCode.BadRequest);
            }
        }

        [HttpPut]
        [ProducesResponseType(typeof(Product), (int)HttpStatusCode.OK)]
        public IActionResult UpdateProduct([FromBody] Product product)
        {
            try
            {
                if (string.IsNullOrEmpty(product.Id))
                {
                    return CustomResult("data not found", HttpStatusCode.NotFound);
                }
                bool isUpdated = _productManager.Update(product.Id, product);
                if (isUpdated)
                {
                    return CustomResult("Product has been updated", product, HttpStatusCode.OK);
                }
                return CustomResult("Product updation failed", product, HttpStatusCode.BadRequest);

            }
            catch (Exception ex)
            {
                return CustomResult("Something went wrong!!", HttpStatusCode.BadRequest);
            }
        }

        [HttpDelete]
        [ProducesResponseType(typeof(Product), (int)HttpStatusCode.OK)]
        public IActionResult DeleteProduct(string Id)
        {
            try
            {
                if (string.IsNullOrEmpty(Id))
                {
                    return CustomResult("data not found", HttpStatusCode.NotFound);
                }
                bool isDeleted = _productManager.Delete(Id);
                if (isDeleted)
                {
                    return CustomResult("Product has been deleted", HttpStatusCode.OK);
                }
                return CustomResult("Product deletion failed", HttpStatusCode.BadRequest);

            }
            catch (Exception ex)
            {
                return CustomResult("Something went wrong!!", HttpStatusCode.BadRequest);
            }
        }

    }
}
