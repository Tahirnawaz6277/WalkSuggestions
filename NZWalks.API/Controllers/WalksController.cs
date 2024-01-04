using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repository;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WalksController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly IwalkRepository walkRepository;

        public WalksController(IMapper mapper, IwalkRepository walkRepository)
        {
            this.mapper = mapper;
            this.walkRepository = walkRepository;
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] WalksRequestDto walksrequestdto)
        {
            // map  DTO to domain model
            var walkDomainModel = mapper.Map<Walk>(walksrequestdto);

            await walkRepository.CreatAsync(walkDomainModel);
            // map domain model to DTO
            return Ok(mapper.Map<WalkDto>(walkDomainModel));
        }

        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] WalkUpdateDto walkUpdatedto)
        {
            var walkdomainmodel = mapper.Map<Walk>(walkUpdatedto);
            walkdomainmodel = await walkRepository.UpdateAsync(id, walkdomainmodel);
            if (walkdomainmodel == null)
            {
                return NotFound();
            }
            var walkdto = mapper.Map<WalkDto>(walkdomainmodel);
            return Ok(new
            {
                data = walkdto
            }
                );
        }
        [HttpGet]
       
        public async Task<IActionResult> GetAll([FromQuery] string? filterOn, 
            [FromQuery] string? filterQuery, [FromQuery] string? sortBy,
           [FromQuery] bool? isAscending, [FromQuery] int pageNumber = 1,
             [FromQuery] int pageSize = 1000)
        {
            var walks = await walkRepository.GetAllAsync(filterOn,filterQuery,sortBy,
                isAscending ?? true, pageNumber, pageSize);

            var walkdto = mapper.Map<List<WalkDto>>(walks);
            return Ok(new 
            {
                data = walkdto
            });
        }
        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetById([FromRoute]Guid id) 
        {
            var walkdomain = await walkRepository.GetByIdAsync(id);
            if (walkdomain == null)
            {
                return NotFound();   
            }
            var walkdto = mapper.Map<WalkDto>(walkdomain);
            return Ok(new
            {
                data = walkdto
            });
        }
        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var walkdomainModel = await walkRepository.DeleteAsync(id);
            if (walkdomainModel == null)
            {
                return NotFound();
            }
            var walkdto = mapper.Map<WalkDto>(walkdomainModel);
            return Ok(new
            {
                data = walkdto
            });
        }
    }
}