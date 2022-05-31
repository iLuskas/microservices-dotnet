using GeekShopping.ProductApi.Model.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GeekShopping.ProductApi.Model
{
    [Table("TBL_PRODUCT")]
    public class Product : BaseEntity
    {
        [Column("PRO_NAME")]
        [Required]
        [StringLength(150)]
        public string Name { get; set; }
        [Column("PRO_PRICE")]
        [Required]
        [Range(1,10000)]
        public decimal Price { get; set; }
        [Column("PRO_DESCRIPTION")]
        [StringLength(500)]
        public string Description { get; set; }
        [Column("PRO_CATEGORY_NAME")]
        [StringLength(50)]
        public string CategoryName { get; set; }
        [Column("PRO_IMAGE_URL")]
        [StringLength(300)]
        public string ImageURL { get; set; }
    }
}
