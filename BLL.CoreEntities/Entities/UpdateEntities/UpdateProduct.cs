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

        public string QuantityPerUnit { get; set; }

        [Required] public decimal? UnitPrice { get; set; }

        public short? UnitsInStock { get; set; }
        public short? UnitsOnOrder { get; set; }
        public short? ReorderLevel { get; set; }
        public bool Discontinued { get; set; }
    }
}