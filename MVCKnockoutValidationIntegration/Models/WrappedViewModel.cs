using System.Collections.Generic;

namespace MVCKnockoutValidationIntegration.Models {

    public class WrappedViewModel<TModel> {

        public TModel Model { get; set; }

        public Dictionary<string, dynamic> ValidationMetadata { get; set; }

    }
}