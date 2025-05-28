using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    [Table("userrole")]
    public class UserRole 
    {
        public Guid RoleId { get; set; }
        public Role Role { get; set; } = default!;

        public Guid UserId { get; set; }
        public User User { get; set; } = default!;

    }
}
