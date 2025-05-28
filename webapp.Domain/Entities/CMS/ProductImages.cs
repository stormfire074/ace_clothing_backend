using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    [Table("productimages")]
    public class ProductImages : LogFields, IIdentifiable
    {
        [Required, Key] public Guid Id { get; set; }
        [Required] public Guid ProductId { get; set; }
        [Required] public string FileName { get; set; }
        [Required] public string FileURL { get; set; }
    }
    public class ProductImages_AddEdit : LogFields
    {
        [Required, Key] public Guid Id { get; set; }
        [Required] public Guid ProductId { get; set; }
        [Required] public string FileName { get; set; }
        [Required] public string FileURL { get; set; }
    }

}
