using System;
using System.Collections.Generic;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PlatformService.Data;
using PlatformService.DTOs;

namespace PlatformService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlatformsController : ControllerBase
    {
        private readonly IPlatformRepo _repository;
        private IMapper _mapper;

        public PlatformsController(IPlatformRepo repository,IMapper mapper )
        {
            _repository=repository;
            _mapper=mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<PlatformReadDTO>> GetPlatforms(){
            Console.WriteLine("Getting Platforms");
            var platformItems= _repository.GetAllPlatforms();
            return Ok(_mapper.Map<IEnumerable<PlatformReadDTO>>(platformItems));
        }

        [HttpGet("{id}",Name="GetPlatformById")]
        public ActionResult<PlatformReadDTO> GetPlatformById(int id){
            Console.WriteLine("Getting Platform by ID : "+id);
            var platformItem= _repository.GetPlatformById(id);
            if(platformItem!=null)
                return Ok(_mapper.Map<PlatformReadDTO>(platformItem));
            else
                return NotFound();    
        }

    }
}