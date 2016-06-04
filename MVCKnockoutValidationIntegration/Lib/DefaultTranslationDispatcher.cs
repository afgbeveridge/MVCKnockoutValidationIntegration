using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MVCKnockoutValidationIntegration.Lib {

    public class DefaultTranslationDispatcher : ITranslationDispatcher {

        public DefaultTranslationDispatcher(IAttributeTranslator translator = null) {
            Translator = translator ?? new KnockoutValidationAttributeTranslator();
        }

        public virtual void Dispatch(Attribute attr, Dictionary<string, dynamic> store) {
            AttributeDispatch[attr.GetType()](Translator, store, attr);
        }

        public virtual bool Understands(Attribute attr) {
            return AttributeDispatch.ContainsKey(attr.GetType());
        }

        private IAttributeTranslator Translator { get; set; }

        private static Dictionary<Type, Action<IAttributeTranslator, dynamic, Attribute>> AttributeDispatch = new Dictionary<Type, Action<IAttributeTranslator, dynamic, Attribute>> {
            { typeof(RequiredAttribute),  (trans, store, attr) => trans.Required((RequiredAttribute) attr, store) },
            { typeof(RangeAttribute),  (trans, store, attr) => trans.Range((RangeAttribute) attr, store) },
            { typeof(StringLengthAttribute),  (trans, store, attr) => trans.Length((StringLengthAttribute) attr, store) },
            { typeof(RegularExpressionAttribute),  (trans, store, attr) => trans.Regex((RegularExpressionAttribute) attr, store) }
        };
    }
}