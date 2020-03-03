using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace server.Models
{
    [Table("user")]
    public partial class User
    {
        public User()
        {
            Vacation = new HashSet<Vacation>();
        }

        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Required]
        [Column("first_name", TypeName = "text")]
        public string FirstName { get; set; }
        [Required]
        [Column("last_name", TypeName = "text")]
        public string LastName { get; set; }
        [Required]
        [Column("email", TypeName = "text")]
        public string Email { get; set; }

        [InverseProperty("CreatedByNavigation")]
        public virtual ICollection<Vacation> Vacation { get; set; }
    }
}
