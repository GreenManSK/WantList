using System;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WantList.Data.Interfaces;
using WantList.DTO;

namespace WantList.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AnidbAnimeController : ControllerBase
    {
        private readonly ILogger<AnidbAnimeController> _logger;
        private IAnidbAnimeData _anidbAnimeData;
        private readonly IMapper _mapper;

        public AnidbAnimeController(ILogger<AnidbAnimeController> logger, IAnidbAnimeData anidbAnimeData, IMapper mapper)
        {
            _logger = logger;
            _anidbAnimeData = anidbAnimeData;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<AnidbAnimeDto[]> Get()
        {
            try
            {
                var animes = _anidbAnimeData.GetAll();
                return _mapper.Map<AnidbAnimeDto[]>(animes);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error while getting all anidb anime");
                return StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }
    }
}