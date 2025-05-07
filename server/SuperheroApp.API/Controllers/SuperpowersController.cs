using Microsoft.AspNetCore.Mvc;
using SuperheroApp.Core.DTOs;
using SuperheroApp.Core.Entities;
using SuperheroApp.Core.Interfaces;
using System.Net.Mime;

namespace SuperheroApp.API.Controllers
{
    /// <summary>
    /// Controlador API para gerenciamento de superpoderes
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    public class SuperpowersController : ControllerBase
    {
        private readonly ISuperpowerRepository _superpowerRepository;

        public SuperpowersController(ISuperpowerRepository superpowerRepository)
        {
            _superpowerRepository = superpowerRepository;
        }

        /// <summary>
        /// Obter todos os superpoderes
        /// </summary>
        /// <returns>Uma lista de todos os superpoderes</returns>
        /// <response code="200">Retorna a lista de superpoderes</response>
        /// <response code="204">Se não houver superpoderes</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<SuperpowerDto>))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult<IEnumerable<SuperpowerDto>>> GetSuperpowers()
        {
            var superpowers = await _superpowerRepository.GetAllAsync();
            
            // Retorna 204 No Content se não houver superpoderes
            if (!superpowers.Any())
            {
                return NoContent();
            }
            
            var superpowersDto = superpowers.Select(s => new SuperpowerDto
            {
                Id = s.Id,
                Name = s.Name,
                Description = s.Description
            });

            return Ok(superpowersDto);
        }

        /// <summary>
        /// Obter um superpoder específico pelo ID
        /// </summary>
        /// <param name="id">O ID do superpoder a ser recuperado</param>
        /// <returns>O superpoder com o ID especificado</returns>
        /// <response code="200">Retorna o superpoder</response>
        /// <response code="404">Se o superpoder não for encontrado</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SuperpowerDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<SuperpowerDto>> GetSuperpower(int id)
        {
            var superpower = await _superpowerRepository.GetByIdAsync(id);

            if (superpower == null)
            {
                return NotFound($"Superpoder com ID {id} não foi encontrado.");
            }

            var superpowerDto = new SuperpowerDto
            {
                Id = superpower.Id,
                Name = superpower.Name,
                Description = superpower.Description
            };

            return Ok(superpowerDto);
        }

        /// <summary>
        /// Criar um novo superpoder
        /// </summary>
        /// <param name="createSuperpowerDto">Os dados do superpoder a ser criado</param>
        /// <returns>O superpoder recém-criado</returns>
        /// <response code="201">Retorna o superpoder recém-criado</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(SuperpowerDto))]
        public async Task<ActionResult<SuperpowerDto>> CreateSuperpower(CreateSuperpowerDto createSuperpowerDto)
        {
            var superpower = new Superpower
            {
                Name = createSuperpowerDto.Name,
                Description = createSuperpowerDto.Description
            };

            await _superpowerRepository.AddAsync(superpower);

            return CreatedAtAction(nameof(GetSuperpower), new { id = superpower.Id }, new SuperpowerDto
            {
                Id = superpower.Id,
                Name = superpower.Name,
                Description = superpower.Description
            });
        }

        /// <summary>
        /// Atualizar um superpoder existente
        /// </summary>
        /// <param name="id">O ID do superpoder a ser atualizado</param>
        /// <param name="updateSuperpowerDto">Os dados atualizados do superpoder</param>
        /// <returns>Sem conteúdo se bem-sucedido</returns>
        /// <response code="204">Se o superpoder foi atualizado com sucesso</response>
        /// <response code="404">Se o superpoder não for encontrado</response>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateSuperpower(int id, UpdateSuperpowerDto updateSuperpowerDto)
        {
            var superpower = await _superpowerRepository.GetByIdAsync(id);

            if (superpower == null)
            {
                return NotFound($"Superpoder com ID {id} não foi encontrado.");
            }

            superpower.Name = updateSuperpowerDto.Name;
            superpower.Description = updateSuperpowerDto.Description;

            await _superpowerRepository.UpdateAsync(superpower);

            return NoContent();
        }

        /// <summary>
        /// Excluir um superpoder
        /// </summary>
        /// <param name="id">O ID do superpoder a ser excluído</param>
        /// <returns>Sem conteúdo se bem-sucedido</returns>
        /// <response code="204">Se o superpoder foi excluído com sucesso</response>
        /// <response code="404">Se o superpoder não for encontrado</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteSuperpower(int id)
        {
            var superpower = await _superpowerRepository.GetByIdAsync(id);

            if (superpower == null)
            {
                return NotFound($"Superpoder com ID {id} não foi encontrado.");
            }

            await _superpowerRepository.DeleteAsync(id);

            return NoContent();
        }
    }
}