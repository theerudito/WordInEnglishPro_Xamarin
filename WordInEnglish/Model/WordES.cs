using System.ComponentModel.DataAnnotations;

namespace WordInEnglishPro.Model
{
    public class WordES
    {
        [Key]
        public int IdES { get; set; }

        [MaxLength(20), Required]
        public string MyWord { get; set; }
    }
}