using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    [Table("product")]
    public class Product : LogFields, IIdentifiable
    {
        public Product() { }
        public Product(Product_AddEdit addedit)
        {
            this.Id = Guid.NewGuid();
            this.Name = addedit.Name;
            this.Description = addedit.Description;
            this.CategoryId = addedit.CategoryId;
            this.Status = addedit.Status ?? 1;
        }
        [Required, Key] public Guid Id { get; set; }
        [Required] public string Name { get; set; }
        [Required] public string Description { get; set; }
        [ForeignKey("Category")] public Guid CategoryId { get; set; }
        public virtual Category Category { get; set; }
        public string Colors { get; set; }
        public virtual ICollection<ProductVarient>? ProductVarients { get; set; }
        public virtual ICollection<ProductImages>? ProductImages { get; set; }

    }

    public class Product_AddEdit : LogFields
    {
        [Required] public Guid Id { get; set; }
        [Required] public string Name { get; set; }
        [Required] public string Description { get; set; }
        [Required] public Guid CategoryId { get; set; }
        public List<string> Colors { get; set; }
    }

    public class Product_Listing
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
    }
}
