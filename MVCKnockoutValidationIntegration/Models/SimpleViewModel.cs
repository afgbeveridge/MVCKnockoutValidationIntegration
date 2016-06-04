using System.ComponentModel.DataAnnotations;

namespace MVCKnockoutValidationIntegration.Models {

    public class SimpleViewModel {

        [Required]
        [StringLength(10)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(20)]
        public string Surname { get; set; }

        [Required(ErrorMessage = "You must indicate your DOB")]
        [Range(1906, 2016)]
        public int YearOfBirth { get; set; }

        [RegularExpression(@"^[a-z]\d{3}$")]
        public string Pin { get; set; }

    }

}