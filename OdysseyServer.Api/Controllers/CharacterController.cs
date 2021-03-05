using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using OdysseyServer.Persistence.Entities;
using OdysseyServer.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace OdysseyServer.Api.Controllers
{
    [Route("api/character")]
    [ApiController]
    public class CharacterController : ControllerBase
    {
        //private IMemoryCache _cache;
        private ICharacterService _characterService;

        public CharacterController(/*IMemoryCache cache*/ICharacterService characterService)
        {
            _characterService = characterService;
            //_cache = cache;
        }
        // GET: api/<CharacterController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<CharacterController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<CharacterController>
        [HttpPost]
        public async Task Post([FromBody] object value)
        {
            var ch = new Character
            {
                Name = "Yaroslav",
                CharacterAbilities = null,
                GearTier = 200,
                Level = 1,
                Power = 100,
                Xp = 200
            };
            try
            {
                await _characterService.CreateCharacter(ch);
            }
            catch (Exception e)
            {
                e.ToString();
            }
           
        }

        // PUT api/<CharacterController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<CharacterController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
