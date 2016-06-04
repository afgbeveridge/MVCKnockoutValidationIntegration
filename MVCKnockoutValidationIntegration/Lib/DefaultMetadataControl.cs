using System;
using System.Collections.Generic;
using System.Linq;

namespace MVCKnockoutValidationIntegration.Lib {

    public class DefaultMetadataControl : IMetadataControl {

        public virtual string PropertyPrefix {
            get {
                return string.Empty;
            }
        }

        public virtual string PropertySuffix {
            get {
                return string.Empty;
            }
        }

        public virtual string HandlingDirective { get { return "_handling"; } }

        public virtual bool Recurse(Type t) {
            return !t.FullName.StartsWith("System.");
        }

        public virtual bool IsCollectionType(Type t) {
            return t.GetInterfaces()
                .Any(ti => ti.IsGenericType
                     && ti.GetGenericTypeDefinition() == typeof(ICollection<>));
        }

        public virtual Type GetCollectionType(Type t) {
            return t.GetGenericArguments().First();
        }

    }
}