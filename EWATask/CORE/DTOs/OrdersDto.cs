using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CORE.DTOs
{
    public class OrdersDto
    {
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public int Qty { get; set; }
        public decimal PricePerOne { get; set; }
        public decimal TotalPrice { get; set; }
        public List<ProductsDto> Products { get; set; }
    }
}
