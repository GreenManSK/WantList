using System;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WantList.Core;
using WantList.Data.Interfaces;

namespace WantList.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AnimeController : ControllerBase
    {
        private readonly ILogger<AnimeController> _logger;
        private IAnimeData _animeData;

        public AnimeController(ILogger<AnimeController> logger, IAnimeData animeData)
        {
            _logger = logger;
            _animeData = animeData;
        }

        public ActionResult<Anime[]> Get()
        {
            try
            {
                return _animeData.GetAll().ToArray();
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error while getting all anime");
                return StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }
    }
}