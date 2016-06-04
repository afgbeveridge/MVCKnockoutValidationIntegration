using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MVCKnockoutValidationIntegration.Lib {

    public class KnockoutValidationAttributeTranslator : IAttributeTranslator {

        public void Required(RequiredAttribute attr, dynamic store) {
            store["required"] = FormDirective(true, attr);
        }

        public void Length(StringLengthAttribute attr, dynamic store) {
            store["minLength"] = FormDirective(attr.MinimumLength, attr);
            store["maxLength"] = FormDirective(attr.MaximumLength, attr); 
        }

        public void Range(RangeAttribute attr, dynamic store) {
            store["min"] = FormDirective(attr.Minimum, attr); 
            store["max"] = FormDirective(attr.Maximum, attr); 
        }

        public void Regex(RegularExpressionAttribute attr, dynamic store) {
            if (!string.IsNullOrEmpty(attr.Pattern))
                store["pattern"] = FormDirective(attr.Pattern, attr);
        }

        public virtual void General(ValidationAttribute attr, dynamic store) {
            throw new ApplicationException("Cannot interpret a general attribute");
        }

        private dynamic FormDirective<TAttr>(object param, TAttr attr, Action<TAttr, Dictionary<string, dynamic>> affector = null) where TAttr : ValidationAttribute {
            dynamic result = param;
            if (!string.IsNullOrEmpty(attr.ErrorMessage)) {
                result = new Dictionary<string, dynamic>();
                result["params"] = param;
                result["message"] = attr.ErrorMessage;
                affector?.Invoke(attr, result);
            }
            return result;
        }

    }

}