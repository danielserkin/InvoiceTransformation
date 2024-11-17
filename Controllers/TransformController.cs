using InvoiceTransformation.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InvoiceTransformation.Presentation.Controllers
{
    [ApiController]
    [Authorize]
    [Route("[controller]")]
    public class TransformController : ControllerBase
    {
        private readonly ITransformService _transformService;

        public TransformController(ITransformService transformService)
        {
            _transformService = transformService;
        }

      
        [HttpPost("document/xml-to-json")]
        public IActionResult TransformXmlToJson([FromBody] TransformRequest request)
        {
            var validationResult = ValidateRequest(request);

            if (validationResult != null)
                return validationResult;

            try
            {
                var jsonResponse = _transformService.TransformBase64XmlToJson(request.Xml);
                return Ok(jsonResponse);
            }
            catch (Exception ex)
            {
                return HandleInternalServerError(ex);
            }
        }


        private IActionResult ValidateRequest(TransformRequest request)
        {
            if (request == null || string.IsNullOrWhiteSpace(request.Xml))
            {
                return BadRequest(new
                {
                    status = "400",
                    title = "Bad Request",
                    detail = "The 'xml' field is required and cannot be empty.",
                    errors = new[] { "Field 'xml' is missing or empty." }
                });
            }

            return null;
        }

        private IActionResult HandleInternalServerError(Exception exception)
        {
            return StatusCode(500, new
            {
                status = "500",
                title = "Internal Server Error",
                detail = exception.Message,
                errors = new[] { "An unexpected error occurred." }
            });
        }
    }

    public class TransformRequest
    {
        public string Xml { get; set; }
    }
}
