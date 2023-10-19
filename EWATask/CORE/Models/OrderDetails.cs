using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CORE.Models
{
    public class OrderDetails
    {
        [Key]
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public decimal PricePerOne { get; set; }
        public int Qty { get; set; }
        public decimal TotalPrice
        {
            get
            {
                return PricePerOne * Qty;
            }
            set
            {
                TotalPrice = value;
            }
        }
    }
}
