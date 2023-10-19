using CORE.DTOs;
using CORE.Entites;
using CORE.Interfaces;
using CORE.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EWATask.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        public ProductController(IProductService _productService)
        {
            this._productService = _productService;
        }
        [HttpPost]
        [Route("AddProduct")]
        public async Task<ActionResult<AddProduct>> AddProduct(AddProduct form)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            bool checkAdd = await _productService.AddProduct(form);
            return checkAdd == true ? Ok(new { StatusCode = 200, Message = "Data Inserted Successfully" }) : BadRequest();

        }



        [HttpGet]
        [Route("ListAllProducts")]
        public async Task<ActionResult<IEnumerable<ProductsDto>>> GetAllProducts()
        {
            var data = await _productService.GetAllProducts();
            return data != null ? Ok(new { StatusCode = 200, Message = "Data Returned Successfully", data = data }) : NotFound();
        }



        [HttpGet("{id}")]
        public async Task<ActionResult<ProductsDto>> GetProductDetails(int id)
        {
            if (id <= 0)
                return BadRequest();
            var returneddata = _productService.GetProductById(id);
            return returneddata != null ? Ok(new { StatusCode = 200, Message = "Data Returned Successfully", data = returneddata }) : NotFound();

        }



        [HttpPut]
        [Route("UpdateProduct")]
        public async Task<IActionResult> UpdateProduct(Products form)
        {
            if (!ModelState.IsValid || form.ProductId <= 0)
                return BadRequest();
            bool checkUpdate = _productService.UpdateProduct(form);
            return checkUpdate == true ? Ok(new { StatusCode = 200, Message = "Data Updated Successfully" }) : BadRequest();

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            if (id <= 0)
                return BadRequest();
            bool deleteCheck = await _productService.DeleteProduct(id);
            return deleteCheck == true ? Ok(new { StausCode = 200, Message = "Product Deleted Successfully" }) : NotFound();
        }

    }
}
