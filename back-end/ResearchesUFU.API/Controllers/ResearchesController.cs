using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using ResearchesUFU.API.Models;
using ResearchesUFU.API.Services.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace ResearchesUFU.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ResearchesController : ControllerBase
    {
        private readonly IResearchService _researchService;

        public ResearchesController(IResearchService researchService)
        {
            _researchService = researchService;
        }

        /// <summary>
        /// Get a research by id.
        /// </summary>
        /// <returns><see cref="Research">Research</see></returns>
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
                StatusCodes.Status500InternalServerError => StatusCode(response.HttpStatusCode),
                _ => BadRequest(),
            };
        }

        /// <summary>
        /// Get a list researches.
        /// </summary>
        /// <returns><see cref="List{Research}">Research</see></returns>
        [HttpGet]
        [EnableQuery]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<Research>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Get()
        {
            var response = _researchService.GetAll();

            return response.HttpStatusCode switch
            {
                StatusCodes.Status200OK => Ok(response.Content),
                StatusCodes.Status404NotFound => NotFound(),
                StatusCodes.Status500InternalServerError => StatusCode(response.HttpStatusCode),
                _ => BadRequest(),
            };
        }

        /// <summary>
        /// Register a new research
        /// </summary>
        /// <returns><see cref="IdResponse">Id</see> of the new research</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IdResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Post([FromBody] Research research)
        {
            var response = _researchService.Post(research);

            return response.HttpStatusCode switch
            {
                StatusCodes.Status200OK => Ok(response.Content),
                StatusCodes.Status500InternalServerError => StatusCode(response.HttpStatusCode),
                _ => BadRequest(),
            };
        }
    }
}