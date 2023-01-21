using Microsoft.AspNetCore.Mvc;
using ResearchesUFU.API.Models;
using ResearchesUFU.API.Services.Interfaces;

namespace ResearchesUFU.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ResearchesController : ControllerBase
    {
        private readonly ILogger<ResearchesController> _logger;
        private readonly IResearchService _researchService;

        public ResearchesController(ILogger<ResearchesController> logger, IResearchService researchService)
        {
            _logger = logger;
            _researchService = researchService;
        }

        [HttpGet]
        public Research Get(int id)
        {
            var response = _researchService.Get(id);
            
            return response;
        }
    }
}