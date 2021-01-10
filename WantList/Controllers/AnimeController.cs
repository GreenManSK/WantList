using System;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WantList.Core;
using WantList.Data.Interfaces;
using WantList.DTO;

namespace WantList.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AnimeController : ControllerBase
    {
        private readonly ILogger<AnimeController> _logger;
        private IAnimeData _animeData;
        private readonly IMapper _mapper;

        public AnimeController(ILogger<AnimeController> logger, IAnimeData animeData, IMapper mapper)
        {
            _logger = logger;
            _animeData = animeData;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<AnimeDto[]> Get()
        {
            try
            {
                var animes = _animeData.GetAll();
                return _mapper.Map<AnimeDto[]>(animes);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error while getting all anime");
                return StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }

        [HttpGet("{id}")]
        public ActionResult<AnimeDto> Get(int id)
        {
            try
            {
                var anime = _animeData.GetById(id);
                if (anime == null)
                {
                    return NotFound();
                }

                return _mapper.Map<AnimeDto>(anime);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error while getting anime with id {id}", id);
                return StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }

        [HttpPost]
        public ActionResult<AnimeDto> Add(AnimeDto animeDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var anime = _mapper.Map<Anime>(animeDto);
                if (_animeData.GetByAnidbId(anime.AnidbId) != null)
                {
                    return BadRequest("Anime already exists");
                }
                _animeData.Add(anime);
                _animeData.Commit();
                return _mapper.Map<AnimeDto>(anime);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error while adding anime {animeDto}", animeDto);
                return StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }

        [HttpPut]
        public ActionResult<AnimeDto> Update(int id, AnimeDto animeDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var oldAnime = _animeData.GetById(id);
                if (oldAnime == null)
                {
                    return NotFound($"Could not find anime with id {id}");
                }

                _mapper.Map(animeDto, oldAnime);
                _animeData.Commit();
                return _mapper.Map<AnimeDto>(oldAnime);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error while updating anime {animeDto}", animeDto);
                return StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }

        [HttpDelete("{id}")]
        public ActionResult<AnimeDto> Delete(int id)
        {
            try
            {
                var anime = _animeData.Delete(id);
                _animeData.Commit();
                return _mapper.Map<AnimeDto>(anime);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error while deleting anime with id {id}", id);
                return StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }
    }
}