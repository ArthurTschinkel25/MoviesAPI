using Movies_API.Models;
using Microsoft.AspNetCore.Mvc;
using Filmes_API.Data;
using Filmes_API.Data.Dtos;
using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;

namespace Filmes_API.Controllers;
[ApiController]
[Route("[controller]")]
public class MovieController : ControllerBase
{
    private MovieContext _context;
    private IMapper _mapper;
    public MovieController(MovieContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    /// <summary>
    /// Add a movie to the database
    /// </summary>
    /// <response code="201">In case it's succeeded</response>


    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public IActionResult AddMovie([FromBody] CreateMovieDto movieDto)
    {
        Movie movie = _mapper.Map<Movie>(movieDto);
        _context.Movies.Add(movie);
        _context.SaveChanges();
        return CreatedAtAction(nameof(RecoverMovieById), new { id = movie.Id }, movie);
    }


    /// <summary>
    /// Read all the movies included on the database
    /// </summary>
    /// <response code="200">In case it's succeeded</response>

    [HttpGet("Movies")]
    public IEnumerable<ReadMovieDto> ReadMovies([FromQuery] int skip = 0, [FromQuery] int take = 90)
    {
        return _mapper.Map<List<ReadMovieDto>>(_context.Movies.Skip(skip).Take(take));
    }
    /// <summary>
    /// Read a single movie by it's id on the database
    /// </summary>
    /// <returns>IActionResult</returns>
    /// <response code="200">In case it's succeeded</response>
    [HttpGet("Id/{id}")]
    public IActionResult RecoverMovieById(int id)
    {
        var movie = _context.Movies.FirstOrDefault(movie => movie.Id.Equals(id));
        if (movie == null) return NotFound();
        var movieDto = _mapper.Map<Movie>(movie);
        return Ok(movieDto);
    }
    /// <summary>
    /// Read a movie by it's genre
    /// </summary>
    /// <response code="200">In case it's succeeded</response>

    [HttpGet("Genre/{genre}")]
    public IEnumerable<Movie> RecoverMovieByGenre(string genre)
    {
        var moviesGenre = _context.Movies.Where(movie => movie.Genre.Contains(genre));
        return moviesGenre.ToList();
    }
    /// <summary>
    /// Update a entire movie on the database
    /// </summary>
    ///<returns>IActionResult</returns>
    [HttpPut("{id}")]
    public IActionResult UpdateMovie(int id, [FromBody] UpdateMovieDto movieDto) 
    {
        var movie = _context.Movies.FirstOrDefault(movie => movie.Id.Equals(id));
        if (movie == null) return NotFound();
        _mapper.Map(movieDto, movie);
        _context.SaveChanges();
        return NoContent();
    }
    /// <summary>
    /// Update a specific property of a movie on the database
    /// </summary>
    /// <returns>IActionResult</returns>
    [HttpPatch("{id}")]
    public IActionResult UpdateMoviePatch(int id, JsonPatchDocument<UpdateMovieDto> patch)
    {
        var movie = _context.Movies.FirstOrDefault(movie => movie.Id.Equals(id));
        if (movie == null) return NotFound();
        
        var movieToUpdate = _mapper.Map<UpdateMovieDto>(movie);
        patch.ApplyTo(movieToUpdate, ModelState);

       if(!TryValidateModel(movieToUpdate))
        {
            return ValidationProblem(ModelState);
        }

        _mapper.Map(movieToUpdate, movie);
        _context.SaveChanges();
        return NoContent();
    }
    /// <summary>
    /// Delete a movie from database
    /// </summary>
    [HttpDelete("{id}")]
    public IActionResult DeletMovies(int id ) 
    {
        var movie = _context.Movies.FirstOrDefault(movie => movie.Id.Equals(id));
        if (movie == null) return NotFound();
        _context.Remove(movie);
        _context.SaveChanges();
        return NoContent();
    }
}