using System.ComponentModel.DataAnnotations;

namespace Courses.WebUI.Areas.Admin.Models
{
    public class CourseFormViewModel
    {
        public int Id { get; set; }

        [Display(Name = "İsim")]
        [Required(ErrorMessage = "Kurs ismi girmek zorunludur.")]
        public string Name { get; set; }

        [Display(Name = "Açıklama")]
        public string? Description { get; set; }

        [Display(Name = "Kurs Fiyatı")]
        public decimal? UnitPrice { get; set; }

        [Display(Name = "Stok Miktarı")]
        public int UnitsInStock { get; set; }

        [Display(Name = "Kategori")]
        [Required(ErrorMessage = "Bir kategori seçmek zorunludur.")]
        public int CategoryId { get; set; }

        [Display(Name = "Kurs Görseli")]
        public IFormFile? File { get; set; }
    }
}
