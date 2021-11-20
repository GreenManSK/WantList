using System;
using System.IO;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WantList.Anidb;
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
        private readonly IAnimeData _animeData;
        private readonly IMapper _mapper;
        private readonly AnidbService _anidbService;

        public AnimeController(ILogger<AnimeController> logger, IAnimeData animeData, IMapper mapper, AnidbService anidbService)
        {
            _logger = logger;
            _animeData = animeData;
            _mapper = mapper;
            _anidbService = anidbService;
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
        
        [HttpGet("file/{anidbId}")]
        public ActionResult GetImage(int anidbId)
        {
            return Redirect(Path.Combine(Startup.BaseUrl, Startup.StaticImagesPath, _anidbService.GetImageName(anidbId)));
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
                UpdateAnimeDto(animeDto);
                var anime = _mapper.Map<Anime>(animeDto);
                anime.AddedDateTime = DateTime.Now;
                if (anime.AnidbId == null || _animeData.GetByAnidbId(anime.AnidbId.Value) != null)
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
        public ActionResult<AnimeDto> Update(AnimeDto animeDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var oldAnime = _animeData.GetById(animeDto.Id);
                if (oldAnime == null)
                {
                    return NotFound($"Could not find anime with id {animeDto.Id}");
                }

                if (oldAnime.AnidbId != animeDto.AnidbId && oldAnime.AnidbId != null)
                {
                    _anidbService.DeleteImage(oldAnime.AnidbId.Value);
                    UpdateAnimeDto(animeDto);
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
                var anime = _animeData.GetById(id);
                if (anime == null)
                {
                    return NotFound($"Could not find anime with id {id}");
                }

                var oldAnidbId = anime.AnidbId;
                anime.AnidbId = null;
                anime.Deleted = true;
                _animeData.Commit();
                if (oldAnidbId != null)
                {
                    _anidbService.DeleteImage(oldAnidbId.Value);
                }

                return _mapper.Map<AnimeDto>(anime);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error while deleting anime with id {id}", id);
                return StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }

        private void UpdateAnimeDto(AnimeDto animeDto)
        {
            var data = _anidbService.GetData(animeDto.AnidbId);
            if (animeDto.EpisodeCount == 0)
            {
                animeDto.EpisodeCount = data.EpisodeCount;
            }

            if (animeDto.Type == AnimeType.Series && data.Type != null)
            {
                animeDto.Type = data.Type.Value;
            }

            if (data.ReleaseDate != null)
            {
                animeDto.ReleaseDate = data.ReleaseDate.Value;
            }
            _anidbService.DownloadImage(data);
        }
    }
}