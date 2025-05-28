using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class ExceptionLogs : IIdentifiable
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public DateTime Logged { get; set; }

        [Required]
        [MaxLength(50)]
        public string Level { get; set; }

        [Required]
        [MaxLength(4000)]
        public string Message { get; set; }

        [MaxLength(250)]
        public string Logger { get; set; }

        [MaxLength(4000)]
        public string StackTrace { get; set; }
    }

}
