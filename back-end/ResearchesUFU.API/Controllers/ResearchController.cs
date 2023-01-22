using Microsoft.AspNetCore.Mvc;
using ResearchesUFU.API.Models;
using ResearchesUFU.API.Services.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace ResearchesUFU.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ResearchController : ControllerBase
    {
        private readonly IResearchService _researchService;

        public ResearchController(IResearchService researchService)
        {
            _researchService = researchService;
        }

        /// <summary>
        /// Get a research by id.
        /// </summary>
        /// <returns><see cref="Research">Research</see></returns>
        /// <response code="200">Returns the research corresponding to the idresponse>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Research))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Get([Required] int id)
        {
            var response = _researchService.Get(id);
            
            return response.HttpStatusCode switch
            {
                StatusCodes.Status200OK => Ok(response.Content),
                StatusCodes.Status404NotFound => NotFound(),
                _ => BadRequest(),
            };
        }
    }
}