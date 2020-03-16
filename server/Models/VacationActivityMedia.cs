using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace server.Models
{
    [Table("vacation_activity_media")]
    public partial class VacationActivityMedia
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Column("activity_id")]
        public int ActivityId { get; set; }
        [Column("vacation_id")]
        public int VacationId { get; set; }
        [Required]
        [Column("uri", TypeName = "text")]
        public string Uri { get; set; }
        [Required]
        [Column("content_type", TypeName = "text")]
        public string ContentType { get; set; }

        [ForeignKey(nameof(ActivityId))]
        [InverseProperty(nameof(VacationActivity.VacationActivityMedia))]
        public virtual VacationActivity Activity { get; set; }
        [ForeignKey(nameof(VacationId))]
        [InverseProperty("VacationActivityMedia")]
        public virtual Vacation Vacation { get; set; }
    }
}
