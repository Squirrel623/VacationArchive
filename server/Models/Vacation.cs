﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace server.Models
{
    [Table("vacation")]
    public partial class Vacation
    {
        public Vacation()
        {
            VacationActivity = new HashSet<VacationActivity>();
            VacationActivityMedia = new HashSet<VacationActivityMedia>();
        }

        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Column("created_by")]
        public int CreatedBy { get; set; }
        [Required]
        [Column("title", TypeName = "text")]
        public string Title { get; set; }
        [Column("start_date", TypeName = "date")]
        public DateTime StartDate { get; set; }
        [Column("end_date", TypeName = "date")]
        public DateTime? EndDate { get; set; }

        [ForeignKey(nameof(CreatedBy))]
        [InverseProperty(nameof(User.Vacation))]
        public virtual User CreatedByNavigation { get; set; }
        [InverseProperty("Vacation")]
        public virtual ICollection<VacationActivity> VacationActivity { get; set; }
        [InverseProperty("Vacation")]
        public virtual ICollection<VacationActivityMedia> VacationActivityMedia { get; set; }
    }
}
