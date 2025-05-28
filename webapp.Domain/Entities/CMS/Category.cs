using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    [Table("category")]
    public class Category : LogFields, IIdentifiable
    {
        [Key] public Guid Id { get; set; }

        public Guid? ParentId { get; set; }

        [Required] public string Name { get; set; }
    }

    public class Category_AddEdit : LogFields
    {
        [Required] public Guid Id { get; set; }
        [Required] public string Name { get; set; }
        public Guid? ParentId { get; set; }
    }

    public class Category_Listing
    {
        public string Name { get; set; }
        public string? ParentName { get; set; }
    }

    public class Category_ListRequest : ListRequest
    {
        public Category_ListRequest(int pageNumber, int pageSize) : base(pageNumber, pageSize)
        {
        }
        public Category_ListRequest()
        {

        }
        public string Name { get; set; }
        public string ParentName { get; set; }  

    }
}
