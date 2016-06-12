using System;
using System.Linq;
using MVCKnockoutValidationIntegration.Models;
using MVCKnockoutValidationIntegration.Lib;
using System.Web.Http;
using System.Web.Http.Results;

namespace MVCKnockoutValidationIntegration.Controllers.API
{
    public class ViewModelServingController : ApiController
    {
        [HttpGet]
        public SimpleViewModel SimpleViewModel(string firstName = null, string surname = null, int yob = 0) {
            return new SimpleViewModel {
                FirstName = firstName,
                Surname = surname,
                YearOfBirth = yob
            };
        }

        [HttpGet]
        public WrappedViewModel<SimpleViewModel> WrappedSimpleViewModel(string firstName = null, string surname = null, int yob = 0) {
            return new WrappedViewModel<SimpleViewModel> {
                Model = new SimpleViewModel {
                    FirstName = firstName,
                    Surname = surname,
                    YearOfBirth = yob
                },
                ValidationMetadata = new ValidationMetadataGenerator()
                                        .ExamineType<SimpleViewModel>()
                                        .Generate()
            };
        }

        [HttpPost]
        public IHttpActionResult CheckSimpleViewModel(SimpleViewModel model) {
            return ModelState.IsValid ? Ok() : (IHttpActionResult) BadRequest();
        }

        [HttpGet]
        public NestedViewModel NestedViewModel(int listCount = 5) {
            return new NestedViewModel {
                ChildModelCollection = Enumerable.Repeat(new SimpleViewModel(), Math.Max(listCount, 1)).ToList()
            };
        }
    }
}
