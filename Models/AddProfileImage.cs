using System.ComponentModel.DataAnnotations;

namespace CoreDemo.Models
{
    public class AddProfileImage
    {
        public int WriteId { get; set; }

        [Required(ErrorMessage = "Yazar adı soyadı kısmı boş geçilemez")]
        public string WriteName { get; set; }
        public string WriteAbout { get; set; }
        public IFormFile WriterImage { get; set; }

        [Required(ErrorMessage = "Mail adresi boş geçilemez")]
        public string WriterMail { get; set; }

        [Required(ErrorMessage = "Sifre boş geçilemez")]
        public string WriterPassword { get; set; }

        public bool WriterStatus { get; set; }
    }
}
