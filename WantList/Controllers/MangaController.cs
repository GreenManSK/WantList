using System;
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
    public class MangaController : ControllerBase
    {
        private readonly ILogger<MangaController> _logger;
        private IMangaData _mangaData;
        private readonly IMapper _mapper;

        public MangaController(ILogger<MangaController> logger, IMangaData mangaData, IMapper mapper)
        {
            _logger = logger;
            _mangaData = mangaData;
            _mapper = mapper;
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

        [HttpPost]
        public ActionResult<MangaDto> Add(MangaDto mangaDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var manga = _mapper.Map<Manga>(mangaDto);
                if (_mangaData.GetByMangaUpdatesId(manga.MangaUpdatesId) != null)
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
        public ActionResult<MangaDto> Update(int id, MangaDto mangaDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var oldManga = _mangaData.GetById(id);
                if (oldManga == null)
                {
                    return NotFound($"Could not find manga with id {id}");
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
                var manga = _mangaData.Delete(id);
                _mangaData.Commit();
                return _mapper.Map<MangaDto>(manga);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error while deleting manga with id {id}", id);
                return StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }
    }
}