using AutoMapper;
using WantList.Core;

namespace WantList.DTO
{
    public class AutoMapping: Profile
    {
        public AutoMapping()
        {
            // To DTO
            CreateMap<Anime, AnimeDto>();
            CreateMap<AnidbAnime, AnidbAnimeDto>();
            CreateMap<Manga, MangaDto>();
            
            // From DTO
            CreateMap<AnimeDto, Anime>();
            CreateMap<AnidbAnimeDto, AnidbAnime>();
            CreateMap<MangaDto, Manga>();
        }
    }
}