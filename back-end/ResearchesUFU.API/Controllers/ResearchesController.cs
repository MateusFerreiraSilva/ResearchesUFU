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
        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Research))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Get([Required] int id)
        {
            var response = await _researchService.GetAsync(id);
            
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
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Get()
        {
            var response = await _researchService.GetAllAsync();

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
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Post([FromBody, Required] Research research)
        {
            var response = await _researchService.PostAsync(research);

            return response.HttpStatusCode switch
            {
                StatusCodes.Status200OK => Ok(response.Content),
                StatusCodes.Status500InternalServerError => StatusCode(response.HttpStatusCode),
                _ => BadRequest(),
            };
        }

        /// <summary>
        /// Update a research of wiht the given id.
        /// </summary>
        /// <returns><see cref="Research">Research after the update</see></returns>
        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Put([Required] int id, [FromBody, Required] Research research)
        {
            var response = await _researchService.PutAsync(id, research);

            return response.HttpStatusCode switch
            {
                StatusCodes.Status200OK => Ok(),
                StatusCodes.Status404NotFound => NotFound(),
                StatusCodes.Status500InternalServerError => StatusCode(response.HttpStatusCode),
                _ => BadRequest(),
            };
        }

        /// <summary>
        /// Update a research of wiht the given id.
        /// </summary>
        /// <returns><see cref="Research">Research after the update</see></returns>
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Delete([Required] int id)
        {
            var response = await _researchService.DeleteAsync(id);

            return response.HttpStatusCode switch
            {
                StatusCodes.Status200OK => Ok(),
                StatusCodes.Status404NotFound => NotFound(),
                StatusCodes.Status500InternalServerError => StatusCode(response.HttpStatusCode),
                _ => BadRequest(),
            };
        }


    }
}