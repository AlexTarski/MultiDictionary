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
        public async Task<IActionResult> GetAllGlossariesAsync(bool includeWords = false) //param with default value for query
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
        public async Task<IActionResult> GetByIdAsync(int id)
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

        [HttpPost]
        public async Task<IActionResult> AddGlossaryAsync([FromBody] GlossaryViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var newGlossary = _mapper.Map<GlossaryViewModel, Glossary>(model);
                    if(await _service.IsGlossaryExistingAsync(newGlossary.Name))
                    {
                        newGlossary.Name = await UpdateNameAsync(newGlossary.Name);
                    }

                    await _service.AddEntityAsync(newGlossary);
                    if (await _service.SaveAllAsync())
                    {
                        return Created($"/api/glossaries/{newGlossary.Id}", _mapper.Map<Glossary, GlossaryViewModel>(newGlossary)); //return HTTP 201 status (Created)
                    }
                }
                else
                {
                    return BadRequest(ModelState);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Failed to save a new glossary: {ex}", ex);
            }

            return BadRequest("Failed to save a new glossary");
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteGlossaryAsync(int id)
        {
            try
            {
                var glossaryToDelete = await _service.GetByIdAsync(id);
                if (glossaryToDelete != null)
                {
                    _service.DeleteEntity(glossaryToDelete);
                    var success = await _service.SaveAllAsync();
                    return success ? NoContent() : StatusCode(500, "Failed to delete a glossary");
                }
                else
                {
                    return NotFound($"Glossary with ID {id} not found");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to delete glossary with ID {id}", id);
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }

        private async Task<string> UpdateNameAsync(string name)
        {
            string newName = $"{name}_{new Random().Next(0, 99)}";
            while(await _service.IsGlossaryExistingAsync(newName))
            {
                newName = $"{newName}{new Random().Next(0, 99)}";
            }

            return newName;
        }
    }
}
