using System.ComponentModel.DataAnnotations;

namespace BLL.CoreEntities.Entities.UpdateEntities
{
    public class UpdateProduct
    {
        [Required]
        [MinLength(3)]
        [MaxLength(255)]
        public string ProductName { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(255)]
        public string SupplierIdNames { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(255)]
        public string CategoryIdNames { get; set; }

        [MaxLength(20)]
        public string QuantityPerUnit { get; set; }

        public decimal? UnitPrice { get; set; }

        public short? UnitsInStock { get; set; }
        public short? UnitsOnOrder { get; set; }
        public short? ReorderLevel { get; set; }
        public bool Discontinued { get; set; }
    }
}