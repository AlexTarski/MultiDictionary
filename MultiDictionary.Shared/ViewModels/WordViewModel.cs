using System.ComponentModel.DataAnnotations;

namespace MultiDictionary.Shared.ViewModels
{
    public class WordViewModel
    {
        public int Id { get; set; }
        
        [Required(ErrorMessage = "Word name is required.")]
        [MinLength(1, ErrorMessage = "Word must be at least 1 character.")]
        [MaxLength(40, ErrorMessage = "Word must be no more than 40 symbols.")]
        public string WordName { get; set; }
        
        [MaxLength(40, ErrorMessage = "Theme must be no more than 40 symbols.")]
        public string Theme { get; set; }

        [MaxLength(500, ErrorMessage = "Definition must be no more than 500 symbols.")]
        public string Definition { get; set; }

        [MaxLength(500, ErrorMessage = "Additional Info must be no more than 500 symbols.")]
        public string AdditionalInfo { get; set; }

        [Required(ErrorMessage = "Glossary ID is required.")]
        public int? GlossaryId { get; set; }
    }
}
