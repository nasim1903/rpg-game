using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dotnet_prac.Dtos.Character;
using dotnet_prac.Services.CharacterServices;
using Microsoft.AspNetCore.Mvc;

namespace dotnet_prac.Controllers
{

    [ApiController] // Used to serve automatic HTTP responses such as status code 404 
    [Route("[controller]")]
    public class CharacterController : ControllerBase
    {
        private readonly ICharacterService _characterService;
        public CharacterController(ICharacterService characterService)
        {
            _characterService = characterService;
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<ServiceResponse<List<GetCharacterDto>>>> Get()
        {
            return Ok(await _characterService.GetAllCharacters());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceResponse<GetCharacterDto>>> GetSingle(int id)
        {
            return Ok(await _characterService.GetCharacterById(id));
        }

        [HttpPost]
        public async Task<ActionResult<ServiceResponse<List<GetCharacterDto>>>> AddCharacter(AddCharacterDto newCharacter)
        {
            return Ok(await _characterService.AddCharacter(newCharacter));
        }

        [HttpPut("update/{id}")]
        public async Task<ActionResult<ServiceResponse<GetCharacterDto>>> update(UpdateCharacterDto updateCharacter, int id)
        {
            var response = await _characterService.updateCharacter(updateCharacter, id);
            if (response.Data == null)
            {
                return NotFound(response);
            }
            return Ok(response);
        }

        [HttpDelete("delete/{id}")]
        public async Task<ActionResult<ServiceResponse<List<GetCharacterDto>>>> Delete(int id)
        {
            var response = await _characterService.DeleteCharacter(id);
            if (response.Data == null)
            {
                return NotFound(response);
            }
            return Ok(response);        }
    }
}