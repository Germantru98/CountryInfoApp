using System.ComponentModel.DataAnnotations;

namespace CountryInfoAppUI.Models
{
    public class IndexViewModel
    {
        [Required(ErrorMessage = "Данное поле не может быть пустым")]
        public string CountryName { get; set; }
    }
}