using InvoiceTransformationAPI.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InvoiceTransformationAPI.Presentation.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TransformController : ControllerBase
    {
        private readonly ITransformService _transformService;

        public TransformController(ITransformService transformService)
        {
            _transformService = transformService;
        }

        //[Authorize]
        [HttpPost("xml-to-json")]
        public IActionResult TransformXmlToJson([FromBody] TransformRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Xml))
            {
                return BadRequest(new
                {
                    status = "400",
                    title = "Bad Request",
                    detail = "The 'xml' field is required and cannot be empty."
                });
            }

            try
            {
                var jsonResponse = _transformService.TransformBase64XmlToJson(request.Xml);
                return Ok(jsonResponse);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    status = "500",
                    title = "Internal Server Error",
                    detail = ex.Message
                });
            }
        }
    }

    public class TransformRequest
    {
        public string Xml { get; set; }
    }
}
