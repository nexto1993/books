using AutoMapper;
using Book.API.Data;
using Book.API.Dtos.Author;

namespace Book.API.Configuration
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<AuthorCreateDto, Author>();
            CreateMap<AuthorUpdateDto, Author>();
            CreateMap<Author, AuthorReadOnlyDto>().ReverseMap();
        }
    }
}
