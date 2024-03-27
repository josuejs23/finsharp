using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace api.DTOs.Comment
{
    public class CreateCommentDto
    {
        [Required]
        [MinLength(5, ErrorMessage ="Title can not be less than 5 chareacthers.")]
        [MaxLength(200, ErrorMessage ="Title must be max 280 chareacthers.")]
        public string Title { get; set; } = string.Empty;

        [Required]
        [MinLength(5, ErrorMessage ="Content can not be less than 5 chareacthers.")]
        [MaxLength(200, ErrorMessage ="Content must be max 280 chareacthers.")]
        public string Content { get; set; } = string.Empty;
    }
}