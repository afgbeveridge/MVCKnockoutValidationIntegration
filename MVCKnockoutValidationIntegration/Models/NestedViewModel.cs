using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace MVCKnockoutValidationIntegration.Models {

    public class NestedViewModel {

        [Required(ErrorMessage = "Top level must be specified")]
        public string TopLevel { get; set; }

        public SimpleViewModel ChildModel { get; set; }

        public List<SimpleViewModel> ChildModelCollection { get; set; }

    }
}