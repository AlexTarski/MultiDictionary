using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MultiDictionary.App.Interfaces;
using MultiDictionary.Domain.Entities;
using MultiDictionary.Shared.ViewModels;

namespace MultiDictionary.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class GlossariesController : ControllerBase
    {
        private readonly IGlossaryService _service;
        private readonly ILogger<GlossariesController> _logger;
        private readonly IMapper _mapper;

        public GlossariesController(IGlossaryService service,
            ILogger<GlossariesController> logger,
            IMapper mapper)
        {
            _service = service;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllGlossaries(bool includeWords = false) //param with default value for query
        {
            try
            {
                var result = await _service.GetAllAsync(includeWords);
                return Ok(_mapper.Map<IEnumerable<GlossaryViewModel>>(result));
            }
            catch (Exception ex)
            {
                _logger.LogError("Failed to get glossaries {ex}", ex);
                return BadRequest("Failed to get glossaries");
            }
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var result = await _service.GetByIdAsync(id);
                if (result != null) return Ok(_mapper.Map<GlossaryViewModel>(result));
                else return NotFound();
            }
            catch (Exception ex)
            {
                _logger.LogError("Failed to get glossary {ex}", ex);
                return BadRequest("Failed to get glossary");
            }
        }

    }
}
