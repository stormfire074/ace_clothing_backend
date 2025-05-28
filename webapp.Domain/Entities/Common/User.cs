using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using webapp.SharedServices;

namespace Domain.Entities
{
    [Table("user")]
    public class User : LogFields, IIdentifiable
    {
        public User() { }
        public User(User_AddEdit addedit) 
        {
            this.Id = Guid.NewGuid();
            this.Email = addedit.Email;
            this.FirstName = addedit.FirstName;
            this.LastName = addedit.LastName;
            this.DisplayName = $@"{addedit.FirstName} {addedit.LastName}";
            this.Status= addedit.Status??1;
            this.PasswordHash = addedit.Password.GeneratePasswordHash();
        }
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
        public Guid? Id { get; set; }
        [Required] public string FirstName { get; set; }
        [Required] public string LastName { get; set; }

        [Required] public string Email { get; set; }
        [Required] public string Password { get; set; }

        public ICollection<string> UserRoles { get; set; }

    }
    public class User_Listing
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string DisplayName { get; set; }
        public int? Status { get; set; }
    }
}
