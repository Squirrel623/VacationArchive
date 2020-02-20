#nullable disable warnings

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
        [Column("name", TypeName = "text")]
        public string Name { get; set; }

        [InverseProperty("CreatedByNavigation")]
        public virtual ICollection<Vacation> Vacation { get; set; }
    }
}
