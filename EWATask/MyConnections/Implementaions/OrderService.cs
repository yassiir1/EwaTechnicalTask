using CORE.Entites;
using CORE.Interfaces;
using CORE.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MyConnections.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyConnections.Implementaions
{
    //[Authorize]
    public class OrderService : IOrderService
    {
        private readonly MyContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public OrderService(MyContext context, UserManager<ApplicationUser> userManager, IHttpContextAccessor httpContextAccessor)
        {
            this._context = context;
            this._userManager = userManager;
            this._httpContextAccessor = httpContextAccessor;
        }
        public async Task<bool> AddOrder(AddOrder form)
        {
           try
            {
           


                decimal totalPrice = form.products.Sum(item => _context.Products.Where(p => p.ProductId == item.ProductId).Select(p => p.Price * item.Qty).FirstOrDefault());
                var Order = new Orders
                {
                    OrderDate = DateTime.Now,
                    TotalPrice = totalPrice,
                };
                _context.Orders.Add(Order);
                _context.SaveChanges();

                foreach (var product in form.products)
                {
                    var GetProduct = _context.Products.Find(product.ProductId);
                    decimal Total = GetProduct.Price * product.Qty;
                    var OrderDetail = new OrderDetails
                    {
                        ProductId = product.ProductId,
                        OrderId = Order.OrderId,
                        PricePerOne = GetProduct.Price,
                        Qty = product.Qty,
                    };
                    _context.OrderDetails.Add(OrderDetail);
                    _context.SaveChanges();
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
