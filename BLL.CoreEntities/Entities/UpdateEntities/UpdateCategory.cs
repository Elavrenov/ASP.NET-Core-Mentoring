﻿using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace BLL.CoreEntities.Entities.UpdateEntities
{
    public class UpdateCategory
    {
        [Required]
        [MinLength(3)]
        [StringLength(100)]
        public string CategoryName { get; set; }

        [Required]
        [MinLength(3)]
        [StringLength(255)]
        public string Description { get; set; }
        public IFormFile Picture { get; set; }
    }
}