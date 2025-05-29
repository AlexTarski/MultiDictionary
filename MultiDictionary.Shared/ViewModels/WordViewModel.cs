using System.ComponentModel.DataAnnotations;

namespace MultiDictionary.Shared.ViewModels
{
    public class WordViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Word name is required.")]
        [MinLength(1, ErrorMessage = "Word name must be at least 1 character.")]
        public string WordName { get; set; }

        public string Theme { get; set; }
        public string Definition { get; set; }
        public string AdditionalInfo { get; set; }

        [Required(ErrorMessage = "Glossary ID is required.")]
        public int? GlossaryId { get; set; }
    }
}
