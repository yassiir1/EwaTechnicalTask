using CORE.DTOs;
using CORE.Entites;
using CORE.Interfaces;
using CORE.Models;
using Microsoft.EntityFrameworkCore;
using MyConnections.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyConnections.Implementaions
{
    public class ProductService : IProductService
    {
        private readonly MyContext _context;
        public ProductService(MyContext _context)
        {
            this._context = _context;
        }
        public async Task<bool> AddProduct(AddProduct form)
        {
            if (form == null)
            {
                return false;
            }

            try
            {
                var Product = new Products
                {
                    ProductName = form.ProductName,
                    Price = form.Price,
                };

                _context.Products.Add(Product);
                await _context.SaveChangesAsync(); // Use asynchronous SaveChangesAsync

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }


        public async Task<bool> DeleteProduct(int id)
        {
            try
            {
                var Product = _context.Products.Find(id);
                if(Product == null) return false;
                _context.Products.Remove(Product);
                _context.SaveChanges();
                return true;
            }
            catch { return false; }
        }

        public async Task<IEnumerable<ProductsDto>> GetAllProducts()
        {
            try
            {
                var products = await _context.Products
                    .Select(c => new ProductsDto { ProductName = c.ProductName, ProductId = c.ProductId, Price = c.Price })
                    .ToListAsync();

                return products;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public ProductsDto GetProductById(int id)
        {
            try
            {
                var product = _context.Products.Find(id);

                if (product != null)
                {
                    return new ProductsDto
                    {
                        ProductId = product.ProductId,
                        ProductName = product.ProductName,
                        Price = product.Price
                    };
                }

                return null;
            }
            catch (Exception ex)
            {
     
                return null;
            }
        }

        public bool UpdateProduct(Products form)
        {
            try
            {
                var Product = _context.Products.Find(form.ProductId);
                if (Product != null)
                {
                    var UpdatedProduct = new Products
                    {
                        ProductName = form.ProductName,
                        Price = form.Price,
                    };
                    _context.SaveChanges();
                    return true;
                }
                return false;
            }
            catch { return false; }
        }
    }
}
