using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    [Table("role")]
    public class Role : IIdentifiable
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();


    }
}
