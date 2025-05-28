using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    [Table("productvarient")]
    public class ProductVarient : LogFields, IIdentifiable
    {
        public ProductVarient() { }
        public ProductVarient(ProductVarient_AddEdit addedit)
        {
            this.Id = addedit.Id ?? Guid.NewGuid();
            this.SKU = addedit.SKU;
            this.ProductId = addedit.ProductId;
            this.Quantity = addedit.Quantity;
            this.Price = addedit.Price;
            this.Chest = addedit.Chest;
            this.Waist = addedit.Waist;
            this.Length = addedit.Length;
            this.Status = addedit.Status ?? 1;
        }

        [Required, Key] public Guid Id { get; set; }

        [ForeignKey("Product")] public Guid ProductId { get; set; }
        public virtual Product Product { get; set; }

        [Required] public string SKU { get; set; }

        [Column(TypeName = "decimal(8,8)")] public decimal? Price { get; set; }

        public long? Quantity { get; set; }

        public string Size { get; set; }

        [Column(TypeName = "decimal(3,1)")] public decimal? Waist { get; set; }

        [Column(TypeName = "decimal(3,1)")] public decimal? Chest { get; set; }

        [Column(TypeName = "decimal(3,1)")] public decimal? Length { get; set; }
    }

    public class ProductVarient_AddEdit : LogFields
    {
        public Guid? Id { get; set; }

        public Guid ProductId { get; set; }
        public string SKU { get; set; }

        public decimal? Price { get; set; }

        public long? Quantity { get; set; }

        public string? Size { get; set; }

        public decimal? Waist { get; set; }

        public decimal? Chest { get; set; }

        public decimal? Length { get; set; }
    }
}
