using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    [Table("user")]
    public class User : LogFields, IIdentifiable
    {
        public User() { }
        [Required, Key] public Guid Id { get; set; }
        [Required] public string FirstName { get; set; }
        [Required] public string LastName { get; set; }

        [Required] public string Email { get; set; }
        [Required] public string PasswordHash { get; set; }
        [Required] public string DisplayName { get; set; }
        public ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();

    }

    public class User_AddEdit : LogFields
    {
        [Required] public Guid Id { get; set; }
        [Required] public string FirstName { get; set; }
        [Required] public string LastName { get; set; }

        [Required] public string Email { get; set; }
        [Required] public string Password { get; set; }

        [Required] public string DisplayName { get; set; }

    }
    public class User_Listing
    {
        public string Name { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string DisplayName { get; set; }
    }
}
