using System;
using System.Collections.Generic;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PlatformService.Data;
using PlatformService.DTOs;
using PlatformService.Models;

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

        [HttpPost]
        public ActionResult<PlatformCreateDTO> CreatePlatform(PlatformCreateDTO createModel){
            var platformModel = _mapper.Map<Platform>(createModel);
            _repository.CreatePlatform(platformModel);
            _repository.SaveChanges();

            var platformReadDTO = _mapper.Map<PlatformReadDTO>(platformModel);
            return CreatedAtRoute(nameof(GetPlatformById),new {ID=platformReadDTO.ID},platformReadDTO);
        }

    }
}