using System;
using System.IO;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WantList.Core;
using WantList.Data.Interfaces;
using WantList.DTO;
using WantList.MangaUpdates;

namespace WantList.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MangaController : ControllerBase
    {
        private readonly ILogger<MangaController> _logger;
        private readonly IMangaData _mangaData;
        private readonly IMapper _mapper;
        private readonly IMangaUpdatesService _mangaUpdatesService;

        public MangaController(ILogger<MangaController> logger, IMangaData mangaData, IMapper mapper,
            IMangaUpdatesService mangaUpdatesService)
        {
            _logger = logger;
            _mangaData = mangaData;
            _mapper = mapper;
            _mangaUpdatesService = mangaUpdatesService;
        }

        [HttpGet]
        public ActionResult<MangaDto[]> Get()
        {
            try
            {
                var mangas = _mangaData.GetAll();
                return _mapper.Map<MangaDto[]>(mangas);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error while getting all manga");
                return StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }

        [HttpGet("{id}")]
        public ActionResult<MangaDto> Get(int id)
        {
            try
            {
                var manga = _mangaData.GetById(id);
                if (manga == null)
                {
                    return NotFound();
                }

                return _mapper.Map<MangaDto>(manga);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error while getting manga with id {id}", id);
                return StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }

        [HttpGet("file/{id}")]
        public ActionResult GetImage(int id)
        {
            var manga = _mangaData.GetById(id);

            if (manga == null)
            {
                return NotFound("Image not found.");
            }

            if (manga.Image == null || manga.Image.Length == 0)
            {
                PopulateMangaImage(manga);
                _mangaData.Commit();
            }

            return File(manga.Image, "image/jpeg");
        }

        [HttpPost]
        public ActionResult<MangaDto> Add(MangaDto mangaDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var mangaData = UpdateMangaData(mangaDto);
                var manga = _mapper.Map<Manga>(mangaDto);
                PopulateMangaImage(manga, mangaData);
                manga.AddedDateTime = DateTime.Now;
                if (manga.MangaUpdatesId == null || _mangaData.GetByMangaUpdatesId(manga.MangaUpdatesId) != null)
                {
                    return BadRequest("Manga already exists");
                }

                _mangaData.Add(manga);
                _mangaData.Commit();
                return _mapper.Map<MangaDto>(manga);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error while adding manga {mangaDto}", mangaDto);
                return StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }

        [HttpPut]
        public ActionResult<MangaDto> Update(MangaDto mangaDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var oldManga = _mangaData.GetById(mangaDto.Id);
                if (oldManga == null)
                {
                    return NotFound($"Could not find manga with id {mangaDto.Id}");
                }

                if (oldManga.MangaUpdatesId != null && oldManga.MangaUpdatesId != mangaDto.MangaUpdatesId)
                {
                    var mangaData = UpdateMangaData(mangaDto);
                    PopulateMangaImage(oldManga, mangaData);
                }

                _mapper.Map(mangaDto, oldManga);
                _mangaData.Commit();
                return _mapper.Map<MangaDto>(oldManga);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error while updating manga {mangaDto}", mangaDto);
                return StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }

        [HttpDelete("{id}")]
        public ActionResult<MangaDto> Delete(int id)
        {
            try
            {
                var manga = _mangaData.GetById(id);
                if (manga == null)
                {
                    return NotFound($"Could not find manga with id {id}");
                }

                var oldMangaUpdatesId = manga.MangaUpdatesId;
                manga.MangaUpdatesId = null;
                manga.Deleted = true;
                _mangaData.Commit();

                return _mapper.Map<MangaDto>(manga);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error while deleting manga with id {id}", id);
                return StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }

        private MangaUpdates.Data.Manga UpdateMangaData(MangaDto mangaDto)
        {
            var data = _mangaUpdatesService.GetData(mangaDto.MangaUpdatesId);
            if (string.IsNullOrWhiteSpace(mangaDto.Name))
            {
                mangaDto.Name = data.Title;
            }

            mangaDto.MissingVolumes ??= data.Volumes.ToString();
            mangaDto.Completed = data.Completed;
            return data;
        }

        private void PopulateMangaImage(Manga manga, MangaUpdates.Data.Manga mangaData = null)
        {
            if (mangaData == null)
            {
                mangaData = _mangaUpdatesService.GetData(manga.MangaUpdatesId);
            }
            manga.Image = _mangaUpdatesService.DownloadImage(mangaData);
        }
    }
}