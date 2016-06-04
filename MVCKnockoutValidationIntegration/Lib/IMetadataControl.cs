using System;

namespace MVCKnockoutValidationIntegration.Lib {

    /// <summary>
    /// Interface to objects that to a certain extent control metadata progress and discovery
    /// </summary>
    public interface IMetadataControl {

        /// <summary>
        /// Allows control over types whose properties should be recursed into
        /// </summary>
        /// <param name="t">current type under inspection</param>
        /// <returns>true if the properties of t should be examined</returns>
        bool Recurse(Type t);

        /// <summary>
        /// A prefix that should be appied to all generated property names 
        /// </summary>
        string PropertyPrefix { get; }

        /// <summary>
        /// A suffix that should be appied to all generated property names 
        /// </summary>
        string PropertySuffix { get; }

        /// <summary>
        /// The name of the property that will be emitted to represent the scope of directives being applied
        /// </summary>
        string HandlingDirective { get; }

        /// <summary>
        /// Query; is t a collection representation?
        /// </summary>
        /// <param name="t">current type under inspection</param>
        /// <returns>true if t seems to be a collection type</returns>
        bool IsCollectionType(Type t);

        /// <summary>
        /// Access the type of a collection object
        /// </summary>
        /// <param name="t">current type under inspection</param>
        /// <returns>the homogeneous or base type of a collection</returns>
        Type GetCollectionType(Type t);

    }

}