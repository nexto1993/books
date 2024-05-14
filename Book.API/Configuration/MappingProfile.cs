using AutoMapper;
using Book.API.Data;
using Book.API.Dtos.Author;
using Book.API.Dtos.Book;

namespace Book.API.Configuration
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<AuthorCreateDto, Author>();
            CreateMap<AuthorUpdateDto, Author>();
            CreateMap<Author, AuthorReadOnlyDto>().ReverseMap();

            CreateMap<BookCreateDto, Data.Book>().ReverseMap();
            CreateMap<BookUpdateDto, Data.Book>().ReverseMap();
            CreateMap<Data.Book, BookReadOnlyDto>()
                .ForMember(q => q.AuthorName, d => d.MapFrom(map => $"{map.Author.FirstName} {map.Author.LastName}"))
                .ReverseMap();

            CreateMap<Data.Book, BookDetailsDto>()
                .ForMember(q => q.AuthorName, d => d.MapFrom(map => $"{map.Author.FirstName} {map.Author.LastName}"))
                .ReverseMap();
        }
    }
}
