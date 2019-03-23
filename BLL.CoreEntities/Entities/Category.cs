using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace BLL.CoreEntities.Entities
{
    public class Category
    {
        public int CategoryId { get; set; }
        [Required]
        [MinLength(3)]
        [StringLength(100)]
        public string CategoryName { get; set; }
        [Required]
        [MinLength(3)]
        [StringLength(100)]
        public string Description { get; set; }

        public byte[] Picture { get; set; }
    }
}