using Microsoft.AspNetCore.Mvc;
using SuperheroApp.Core.DTOs;
using SuperheroApp.Core.Entities;
using SuperheroApp.Core.Interfaces;
using System.Net.Mime;

namespace SuperheroApp.API.Controllers
{
    /// <summary>
    /// Controlador API para gerenciamento de super-heróis
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    public class HeroesController : ControllerBase
    {
        private readonly IHeroRepository _heroRepository;
        private readonly ISuperpowerRepository _superpowerRepository;

        public HeroesController(IHeroRepository heroRepository, ISuperpowerRepository superpowerRepository)
        {
            _heroRepository = heroRepository;
            _superpowerRepository = superpowerRepository;
        }

        /// <summary>
        /// Obter todos os heróis
        /// </summary>
        /// <returns>Uma lista de todos os super-heróis</returns>
        /// <response code="200">Retorna a lista de heróis</response>
        /// <response code="204">Se não houver heróis</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<HeroDto>))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult<IEnumerable<HeroDto>>> GetHeroes()
        {
            var heroes = await _heroRepository.GetAllAsync();
            
            // Retorna 204 No Content se não houver heróis
            if (!heroes.Any()) {
                return NoContent();
            }
            
            var heroesDto = heroes.Select(h => new HeroDto
            {
                Id = h.Id,
                Name = h.Name,
                HeroName = h.HeroName,
                Description = h.Description,
                DateOfBirth = h.DateOfBirth,
                Height = h.Height,
                Weight = h.Weight,
                Superpowers = h.HeroSuperpowers.Select(hs => new SuperpowerDto
                {
                    Id = hs.Superpower.Id,
                    Name = hs.Superpower.Name,
                    Description = hs.Superpower.Description
                }).ToList()
            });

            return Ok(heroesDto);
        }

        /// <summary>
        /// Obter um herói específico pelo ID
        /// </summary>
        /// <param name="id">O ID do herói a ser recuperado</param>
        /// <returns>O herói com o ID especificado</returns>
        /// <response code="200">Retorna o herói</response>
        /// <response code="404">Se o herói não for encontrado</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(HeroDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<HeroDto>> GetHero(int id)
        {
            var hero = await _heroRepository.GetByIdAsync(id);

            if (hero == null)
            {
                return NotFound($"Herói com ID {id} não foi encontrado.");
            }

            var heroDto = new HeroDto
            {
                Id = hero.Id,
                Name = hero.Name,
                HeroName = hero.HeroName,
                Description = hero.Description,
                DateOfBirth = hero.DateOfBirth,
                Height = hero.Height,
                Weight = hero.Weight,
                Superpowers = hero.HeroSuperpowers.Select(hs => new SuperpowerDto
                {
                    Id = hs.Superpower.Id,
                    Name = hs.Superpower.Name,
                    Description = hs.Superpower.Description
                }).ToList()
            };

            return Ok(heroDto);
        }

        /// <summary>
        /// Criar um novo herói
        /// </summary>
        /// <param name="createHeroDto">Os dados do herói a ser criado</param>
        /// <returns>O herói recém-criado</returns>
        /// <response code="201">Retorna o herói recém-criado</response>
        /// <response code="400">Se um herói com o mesmo nome já existir</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(HeroDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<HeroDto>> CreateHero(CreateHeroDto createHeroDto)
        {
            // Verifica se um herói com o mesmo nome já existe
            if (await _heroRepository.ExistsByNameAsync(createHeroDto.Name))
            {
                return BadRequest($"Um herói com o nome '{createHeroDto.Name}' já existe!.");
            }

            var hero = new Hero
            {
                Name = createHeroDto.Name,
                HeroName = createHeroDto.HeroName,
                Description = createHeroDto.Description,
                DateOfBirth = DateTime.SpecifyKind(createHeroDto.DateOfBirth, DateTimeKind.Utc),
                Height = createHeroDto.Height,
                Weight = createHeroDto.Weight
            };

            // Adicionar superpoderes existentes
            foreach (var superpowerId in createHeroDto.SuperpowerIds)
            {
                var superpower = await _superpowerRepository.GetByIdAsync(superpowerId);
                if (superpower != null)
                {
                    hero.HeroSuperpowers.Add(new HeroSuperpower
                    {
                        Hero = hero,
                        SuperpowerId = superpowerId
                    });
                }
            }

            // Criar e adicionar novos superpoderes
            foreach (var newSuperpowerDto in createHeroDto.NewSuperpowers)
            {
                var newSuperpower = new Superpower
                {
                    Name = newSuperpowerDto.Name,
                    Description = newSuperpowerDto.Description
                };

                var createdSuperpower = await _superpowerRepository.AddAsync(newSuperpower);

                hero.HeroSuperpowers.Add(new HeroSuperpower
                {
                    Hero = hero,
                    SuperpowerId = createdSuperpower.Id
                });
            }

            await _heroRepository.AddAsync(hero);

            return CreatedAtAction(nameof(GetHero), new { id = hero.Id }, new HeroDto
            {
                Id = hero.Id,
                Name = hero.Name,
                HeroName = hero.HeroName,
                Description = hero.Description,
                DateOfBirth = hero.DateOfBirth,
                Height = hero.Height,
                Weight = hero.Weight,
                Superpowers = hero.HeroSuperpowers.Select(hs => new SuperpowerDto
                {
                    Id = hs.SuperpowerId,
                    Name = hs.Superpower?.Name ?? string.Empty,
                    Description = hs.Superpower?.Description ?? string.Empty
                }).ToList()
            });
        }

        /// <summary>
        /// Atualizar um herói existente
        /// </summary>
        /// <param name="id">O ID do herói a ser atualizado</param>
        /// <param name="updateHeroDto">Os dados atualizados do herói</param>
        /// <returns>Sem conteúdo se bem-sucedido</returns>
        /// <response code="204">Se o herói foi atualizado com sucesso</response>
        /// <response code="400">Se um herói com o mesmo nome já existir</response>
        /// <response code="404">Se o herói não for encontrado</response>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateHero(int id, UpdateHeroDto updateHeroDto)
        {
            var hero = await _heroRepository.GetByIdAsync(id);

            if (hero == null)
            {
                return NotFound($"Herói com ID {id} não foi encontrado.");
            }

            // Verifica se um herói com o mesmo nome já existe (excluindo o herói atual)
            if (await _heroRepository.ExistsByNameExceptIdAsync(updateHeroDto.Name, id))
            {
                return BadRequest($"Um herói com o nome '{updateHeroDto.Name}' já existe.");
            }

            hero.Name = updateHeroDto.Name;
            hero.HeroName = updateHeroDto.HeroName;
            hero.Description = updateHeroDto.Description;
            hero.DateOfBirth = DateTime.SpecifyKind(updateHeroDto.DateOfBirth, DateTimeKind.Utc);
            hero.Height = updateHeroDto.Height;
            hero.Weight = updateHeroDto.Weight;

            // Atualizar superpoderes
            hero.HeroSuperpowers.Clear();
            
            // Adicionar superpoderes existentes
            foreach (var superpowerId in updateHeroDto.SuperpowerIds)
            {
                var superpower = await _superpowerRepository.GetByIdAsync(superpowerId);
                if (superpower != null)
                {
                    hero.HeroSuperpowers.Add(new HeroSuperpower
                    {
                        HeroId = hero.Id,
                        SuperpowerId = superpowerId
                    });
                }
            }
            
            // Criar e adicionar novos superpoderes
            foreach (var newSuperpowerDto in updateHeroDto.NewSuperpowers)
            {
                var newSuperpower = new Superpower
                {
                    Name = newSuperpowerDto.Name,
                    Description = newSuperpowerDto.Description
                };

                var createdSuperpower = await _superpowerRepository.AddAsync(newSuperpower);

                hero.HeroSuperpowers.Add(new HeroSuperpower
                {
                    HeroId = hero.Id,
                    SuperpowerId = createdSuperpower.Id
                });
            }

            await _heroRepository.UpdateAsync(hero);

            return NoContent();
        }

        /// <summary>
        /// Excluir um herói
        /// </summary>
        /// <param name="id">O ID do herói a ser excluído</param>
        /// <returns>Sem conteúdo se bem-sucedido</returns>
        /// <response code="204">Se o herói foi excluído com sucesso</response>
        /// <response code="404">Se o herói não for encontrado</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteHero(int id)
        {
            var hero = await _heroRepository.GetByIdAsync(id);

            if (hero == null)
            {
                return NotFound($"Herói com ID {id} não foi encontrado.");
            }

            await _heroRepository.DeleteAsync(id);

            return NoContent();
        }
    }
}