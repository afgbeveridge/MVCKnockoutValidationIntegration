using System;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;

namespace MVCKnockoutValidationIntegration.Lib {

    public class ValidationMetadataGenerator {

        public ValidationMetadataGenerator(IMetadataControl control = null, ITranslationDispatcher dispatcher = null) {
            Control = control ?? new DefaultMetadataControl();
            Dispatcher = dispatcher ?? new DefaultTranslationDispatcher();
            Handlers = new List<HandlerBundle> {
                new HandlerBundle {
                    Accepts = (t, c) => c.IsCollectionType(t),
                    Handler = CollectionPropertyHandler
                },
                new HandlerBundle {
                    Accepts = (t, c) => !c.Recurse(t),
                    Handler = SimplePropertyHandler
                },
                new HandlerBundle {
                    Accepts = (t, c) => c.Recurse(t),
                    Handler = ComplexPropertyHandler
                }
            };
        }

        public ValidationMetadataGenerator ExamineType<TModel>() {
            return ExamineType(typeof(TModel));
        }

        public ValidationMetadataGenerator ExamineType(Type sourceType) {
            SourceType = sourceType;
            return this;
        }

        public Dictionary<string, dynamic> Generate() {
            return Process(SourceType, new Dictionary<string, dynamic>());
        }

        private Dictionary<string, dynamic> Process(Type t, Dictionary<string, dynamic> store) {
            t
            .GetProperties()
            .ToList()
            .ForEach(info => {
                var h = Handlers.FirstOrDefault(b => b.Accepts(info.PropertyType, Control));
                h?.Handler(info, Control.PropertyPrefix + info.Name + Control.PropertySuffix, store);
            });
            return store;
        }

        private void SimplePropertyHandler(PropertyInfo info, string name, Dictionary<string, dynamic> store) {
            dynamic md = store[name] = CreateContainer("simple");
            ExamineAnyAnnotations(info, md);
        }

        private void ComplexPropertyHandler(PropertyInfo info, string name, Dictionary<string, dynamic> store) {
            dynamic md = store[name] = CreateContainer("complex");
            Process(info.PropertyType, md);
        }

        private void CollectionPropertyHandler(PropertyInfo info, string name, Dictionary<string, dynamic> store) {
            dynamic md = store[name] = CreateContainer("collection");
            Process(Control.GetCollectionType(info.PropertyType), md);
        }

        private dynamic CreateContainer(string type) {
            return new Dictionary<string, dynamic> { [Control.HandlingDirective] = type };
        }

        private void ExamineAnyAnnotations(PropertyInfo info, Dictionary<string, dynamic> store) {
            info
                .GetCustomAttributes()
                .Where(Dispatcher.Understands)
                .ToList()
                .ForEach(attr => Dispatcher.Dispatch(attr, store));
        }

        private IMetadataControl Control { get; set; }

        private Type SourceType { get; set; }

        private ITranslationDispatcher Dispatcher { get; set; }

        private List<HandlerBundle> Handlers { get; set; }

        private class HandlerBundle {
            internal Func<Type, IMetadataControl, bool> Accepts { get; set; }
            internal Action<PropertyInfo, string, Dictionary<string, dynamic>> Handler { get; set; }
        }

    }
}