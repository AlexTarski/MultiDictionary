using Microsoft.AspNetCore.Mvc;
using MultiDictionary.App.Interfaces;
using MultiDictionary.Domain.Entities;

namespace MultiDictionary.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class GlossariesController : ControllerBase
    {
        private readonly IGlossaryService _service;
        private readonly ILogger<GlossariesController> _logger;

        public GlossariesController(IGlossaryService service, ILogger<GlossariesController> logger)
        {
            _service = service;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IEnumerable<Glossary>> Get() => await _service.GetAllAsync();
    }
}
