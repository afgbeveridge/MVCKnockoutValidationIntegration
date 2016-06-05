using System;
using System.Collections.Generic;

namespace MVCKnockoutValidationIntegration.Lib {

    /// <summary>
    /// Facade like interface for a metadata generation implementation 
    /// </summary>
    public interface IValidationMetadataGenerator {

        /// <summary>
        /// Nominate the type to be inspected
        /// </summary>
        /// <param name="sourceType">type of interest</param>
        /// <returns>the receiver</returns>
        ValidationMetadataGenerator ExamineType(Type sourceType);

        /// <summary>
        /// Nominate the type to be inspected
        /// </summary>
        /// <typeparam name="TModel">type of interest</typeparam>
        /// <returns>the receiver</returns>
        ValidationMetadataGenerator ExamineType<TModel>();

        /// <summary>
        /// Request that the nominated type is inspected, and its validation decorations be translated into a client digestible form
        /// </summary>
        /// <returns>a dictionary of arbitrary structure</returns>
        Dictionary<string, dynamic> Generate();
    }
}