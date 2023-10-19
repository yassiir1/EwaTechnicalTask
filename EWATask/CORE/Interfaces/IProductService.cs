using CORE.DTOs;
using CORE.Entites;
using CORE.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CORE.Interfaces
{
    public interface IProductService
    {
        Task<bool> AddProduct(AddProduct form);
        Task<bool> DeleteProduct(int id);
        Task<IEnumerable<ProductsDto>> GetAllProducts();
        ProductsDto GetProductById(int id);
        bool UpdateProduct(Products form);
    }
}
