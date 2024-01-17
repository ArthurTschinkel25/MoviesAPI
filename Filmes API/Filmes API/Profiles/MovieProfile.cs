using AutoMapper;
using Filmes_API.Data.Dtos;
using Movies_API.Models;

namespace Filmes_API.Profiles
{
    public class MovieProfile : Profile
    {
        public MovieProfile()
        {
            CreateMap<CreateMovieDto, Movie>();
            CreateMap<UpdateMovieDto, Movie>();
            CreateMap<Movie, UpdateMovieDto>(); 
            CreateMap<Movie, ReadMovieDto>();
        }
    }
}
