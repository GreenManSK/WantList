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
            
            // From DTO
            CreateMap<AnimeDto, Anime>();
        }
    }
}