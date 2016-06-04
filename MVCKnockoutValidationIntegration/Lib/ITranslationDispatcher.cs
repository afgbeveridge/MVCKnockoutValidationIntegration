using System;
using System.Collections.Generic;

namespace MVCKnockoutValidationIntegration.Lib {

    /// <summary>
    /// Interface for objects that handle the dispatch of validation attributes to objects that 'understand' what to do 
    /// </summary>
    public interface ITranslationDispatcher {

        /// <summary>
        /// Does the receiver understand what to do with the attribute supplied?
        /// </summary>
        /// <param name="attr">attribute being inspected</param>
        /// <returns>true if the receiver can dispatch the attribute to a handler</returns>
        bool Understands(Attribute attr);

        /// <summary>
        /// Dispatch an attribute to a handler
        /// </summary>
        /// <param name="attr">the attribute being considered</param>
        /// <param name="store">the current metadata store in which to place results of inspection</param>
        void Dispatch(Attribute attr, Dictionary<string, dynamic> store);

    }
}