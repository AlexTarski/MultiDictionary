using System.ComponentModel.DataAnnotations;

namespace MultiDictionary.Shared.ViewModels
{
    public class GlossaryViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Glossary name is required.")]
        [MinLength(1, ErrorMessage = "Glossary name must be at least 1 char.")]
        public string Name { get; set; }

        public ICollection<WordViewModel> Words { get; set; }
    }
}
