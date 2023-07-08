using System.ComponentModel.DataAnnotations;

namespace WordInEnglishPro.Model
{
    public class WordEN
    {
        [Key]
        public int IdEN { get; set; }

        [MaxLength(20), Required]
        public string MyWord { get; set; }
    }
}