﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace server.Models
{
    [Table("vacation_activity")]
    public partial class VacationActivity
    {
        public VacationActivity()
        {
            VacationActivityMedia = new HashSet<VacationActivityMedia>();
        }

        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Column("vacation_id")]
        public int VacationId { get; set; }
        [Required]
        [Column("title", TypeName = "text")]
        public string Title { get; set; }
        [Column("date", TypeName = "date")]
        public DateTime Date { get; set; }

        [ForeignKey(nameof(VacationId))]
        [InverseProperty("VacationActivity")]
        public virtual Vacation Vacation { get; set; }
        [InverseProperty("Activity")]
        public virtual ICollection<VacationActivityMedia> VacationActivityMedia { get; set; }
    }
}
