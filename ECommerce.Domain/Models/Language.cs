using System.ComponentModel.DataAnnotations;

namespace ECommerce.Domain.Models
{
    public partial class Language
    {
        [Key]
        [StringLength(50)]
        public string MainCode { get; set; }

        [StringLength(500)]
        public string? ArTxt { get; set; }

        [StringLength(500)]
        public string? EnTxt { get; set; }

        [StringLength(500)]
        public string? OTHTxt { get; set; }

        [StringLength(500)]
        public string? OTHTxt1 { get; set; }
    }

}
