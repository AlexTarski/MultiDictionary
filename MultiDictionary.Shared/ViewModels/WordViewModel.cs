using System.ComponentModel.DataAnnotations;

namespace MultiDictionary.Shared.ViewModels
{
    public class WordViewModel
    {
        public int Id { get; set; }
        [Required]
        [MinLength(1)]
        public string WordName { get; set; }

        public string Theme { get; set; }
        public string Definition { get; set; }
        public string AdditionalInfo { get; set; }
    }
}
