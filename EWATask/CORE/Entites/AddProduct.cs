using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CORE.Entites
{
    public class AddProduct
    {
        [Required]
        public string? ProductName { get; set; }

        [Required]
        public decimal Price { get; set; }
    }
}
