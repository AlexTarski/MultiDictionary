using System.ComponentModel.DataAnnotations;

namespace MultiDictionary.Shared.ViewModels
{
    public class GlossaryViewModel
    {
        public int Id { get; set; }
        [Required]
        [MinLength(1)]
        public string Name { get; set; }

        public ICollection<WordViewModel> Words { get; set; }
    }
}
