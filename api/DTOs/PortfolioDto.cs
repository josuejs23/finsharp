using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace api.DTOs
{
    public class PortfolioDto
    {
        [Required]
        public string UserId { get; set; }
        
        [Required]
        public int StockId { get; set; }
    }
}