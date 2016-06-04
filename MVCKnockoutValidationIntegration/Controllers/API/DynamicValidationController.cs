using System;
using System.Web.Http;
using MVCKnockoutValidationIntegration.Lib;

namespace MVCKnockoutValidationIntegration.Controllers
{
    public class DynamicValidationController : ApiController
    {

        [HttpGet]
        public dynamic MetadataFor(string typeName) {
            return new ValidationMetadataGenerator()
                            .ExamineType(Type.GetType(typeName))
                            .Generate();
        }

    }
}
