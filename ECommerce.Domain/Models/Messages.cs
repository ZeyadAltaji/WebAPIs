using System.ComponentModel.DataAnnotations;

namespace ECommerce.Domain.Models
{
    public partial class Messages
    {
        [Key]
        [StringLength(50)]
        public int CodeId { get; set; }

        [StringLength(500)]
        public string? MessageAr { get; set; }

        [StringLength(500)]
        public string? MessageEn { get; set; }

        [StringLength(500)]
        public string? MessageOTHTxt { get; set; }

        [StringLength(500)]
        public string? MessageOTHTxt1 { get; set; }
    }

}
