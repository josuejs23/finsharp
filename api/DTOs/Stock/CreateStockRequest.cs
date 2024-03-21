using System.ComponentModel.DataAnnotations;

namespace api.DTOs.Stock
{
    public class CreateStockRequest
    {
        [Required]
        [MinLength(3, ErrorMessage ="Title can not be less than 3 chareacthers.")]
        [MaxLength(5, ErrorMessage ="Title must be max 5 chareacthers.")]
         public string Symbol { get; set; } = string.Empty;

        [Required]
        [MinLength(5, ErrorMessage ="Title can not be less than 5 chareacthers.")]
        [MaxLength(10, ErrorMessage ="Title must be max 10 chareacthers.")]
        public string CompanyName { get; set; } = string.Empty;

        [Required]
        [Range(1,1_000_000_000_000)]
        public decimal Purchase { get; set; }

        [Required]
        [Range(0.001,100)]
        public decimal LastDiv { get; set; }

        [Required]
        [MaxLength(10, ErrorMessage ="Industry must be max 10 chareacthers.")]
        public string Industry { get; set; } = string.Empty;

        [Required]
        [Range(1,5_000_000_000_000)]
        public long MarketCap {get; set;}
    }
}