using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repository;


namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class RegionController : ControllerBase
    {
        private readonly NZWalksDBContext context;
        private readonly IRegionRepository regionRepository;
        private readonly IMapper mapper;

        public RegionController(NZWalksDBContext context, IRegionRepository regionRepository, IMapper mapper)
        {   
            this.context = context;
            this.regionRepository = regionRepository;
            this.mapper = mapper;
        }

        [HttpGet]
        //[Authorize(Roles ="Reader")]
        public async Task<IActionResult> GetAll()
        {
            var regions = await regionRepository.GetAllAsync();
            var regionDTO = mapper.Map<List<RegionDto>>(regions);
            return Ok(new
            {
                data = regionDTO
            });
        }

        [HttpGet]
        [Route("{id:Guid}")]
        [Authorize(Roles ="Reader")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var regionDomain = await regionRepository.GetByIdAsync(id);
            if (regionDomain == null)
            {
                return NotFound();
            }
            var regionDTO = mapper.Map<RegionDto>(regionDomain);
            return Ok(new
            {
                data = regionDTO
            });
        }
        [HttpPost]
        [Authorize(Roles ="Writer")]
        public async Task<IActionResult> Create([FromBody] regionDto4postRequest regionDto4Post)
        {
            if(ModelState.IsValid)
            {
                // Convert the DTO from Domain Model

                var regiondto = mapper.Map<Region>(regionDto4Post);

                var regionC = await regionRepository.CreatAsync(regiondto);
                var regionDto = mapper.Map<RegionDto>(regionC);
                return Ok(new
                {
                    data = regionDto

                });
            }
            else
            {
                return BadRequest(ModelState);
            }

        }

        [HttpPut]
        [Route("{id:Guid}")]
        [Authorize(Roles = "Writer")]

        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] regionDto4PutRequest regionUpdate)
        {
            if (ModelState.IsValid)
            {
                var regionDomainModel = new Region
                {

                    Code = regionUpdate.Code,
                    Name = regionUpdate.Name,
                    RegionImageUrl = regionUpdate.RegionImageUrl
                };

                if (regionDomainModel == null)
                {
                    return NotFound();
                }
                regionDomainModel = await regionRepository.UpdateAsync(id, regionDomainModel);
                var regiondto = mapper.Map<RegionDto>(regionDomainModel);
                // always we should return dto instead of orignal domain model?
                return Ok(new
                {
                    data = regiondto
                });
            }
            else
            {
                return BadRequest(ModelState);
            }

        }

        [HttpDelete]
        [Route("{id:Guid}")]
        [Authorize(Roles = "Writer")]

        public async Task<IActionResult> DeleteById([FromRoute] Guid id)
        {
            var regionDomainModel = await regionRepository.DeleteAsync(id);
            if (regionDomainModel == null)
            {
                return NotFound();
            }
            var regiondto = mapper.Map<RegionDto>(regionDomainModel);
            return Ok(new
            {
                data = regiondto
            });
        }



    }



}
