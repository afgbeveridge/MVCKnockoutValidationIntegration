using System.ComponentModel.DataAnnotations;

namespace MVCKnockoutValidationIntegration.Lib {

    /// <summary>
    /// Interface to objects that can interpret attributed MVC objects in some context
    /// </summary>
    public interface IAttributeTranslator {
        /// <summary>
        /// Handle a 'required' attribute
        /// </summary>
        /// <param name="attr">the attribute of interest</param>
        /// <param name="store">the metadata store that can contain the results of interpretation</param>
        void Required(RequiredAttribute attr, dynamic store);
        /// <summary>
        /// Handle a StringLength attribute
        /// </summary>
        /// <param name="attr">the attribute of interest</param>
        /// <param name="store">the metadata store that can contain the results of interpretation</param>
        void Length(StringLengthAttribute attr, dynamic store);
        /// <summary>
        /// Handle a Range attribute
        /// </summary>
        /// <param name="attr">the attribute of interest</param>
        /// <param name="store">the metadata store that can contain the results of interpretation</param>
        void Range(RangeAttribute attr, dynamic store);
        /// <summary>
        /// Handle a RegularExpression attribute
        /// </summary>
        /// <param name="attr">the attribute of interest</param>
        /// <param name="store">the metadata store that can contain the results of interpretation</param>
        void Regex(RegularExpressionAttribute attr, dynamic store);
        /// <summary>
        /// Handle a base type attribute
        /// </summary>
        /// <param name="attr">the attribute of interest</param>
        /// <param name="store">the metadata store that can contain the results of interpretation</param>
        void General(ValidationAttribute attr, dynamic store);
    }

}