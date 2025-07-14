using AutoMapper;
using Microsoft.AspNetCore.Identity;
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
        public async Task<IActionResult> GetAllGlossariesAsync(bool includeWords = false) 
        {
            try
            {
                var result = await _service.GetAllAsync(includeWords);
                return Ok(_mapper.Map<IEnumerable<GlossaryViewModel>>(result));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to get all glossaries");
                return StatusCode(500, $"Internal Server Error:{ex.Message}");
            }
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            try
            {
                var result = await _service.GetByIdAsync(id);
                return Ok(_mapper.Map<GlossaryViewModel>(result));
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogError(ex, "Failed to get a glossary by ID: {Id}", id);
                return NotFound($"Glossary with ID {id} was not found");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to get a glossary by ID {Id}", id);
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddGlossaryAsync([FromBody] GlossaryViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var newGlossary = _mapper.Map<GlossaryViewModel, Glossary>(model);
                var success = await _service.AddEntityAsync(newGlossary);

                return success ? Created($"/api/glossaries/{newGlossary.Id}", _mapper.Map<Glossary, GlossaryViewModel>(newGlossary)) //return HTTP 201 status (Created)
                        : StatusCode(500, "Failed to save a new glossary");
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogError(ex, "Failed to save a new glossary: {Message}", ex.Message);
                return BadRequest($"Glossary with ID {model.Id} already exists");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to save a new glossary: {Message}", ex.Message);
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteGlossaryAsync(int id)
        {
            try
            {
                var success = await _service.DeleteEntityAsync(id);
                return success ? NoContent() : StatusCode(500, "Failed to delete a glossary");
            }
            catch(KeyNotFoundException ex)
            {
                _logger.LogError(ex, "Failed to delete glossary with ID {Id}", id);
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to delete glossary with ID {Id}", id);
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }
    }
}