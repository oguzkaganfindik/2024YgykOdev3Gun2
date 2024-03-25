using Courses.Data.Entities;
using System.ComponentModel.DataAnnotations;

namespace Courses.WebUI.Areas.Admin.Models
{
    public class InstructorFormViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Ad")]
        [Required(ErrorMessage = "Eğitmen adı girmek zorunludur.")]
        public string Name { get; set; }
    }
}
