using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Movies.Controllers
{
    [ApiController]
    public class MovieController : ControllerBase
    {
        private readonly Context _dbContext;
        public MovieController(Context dbContext)
        {
            _dbContext = dbContext;
        }


        [HttpGet]
        [Route("api/Movie/GetMovies")]
        public async Task<IActionResult> GetMovies()
        {
            List<Movie> movies = await _dbContext.Movies.Include(q => q.Category).ToListAsync();
           
            List<MoviesModel> moviesList = movies.Select(q => new MoviesModel()
            {
                Id = q.Id,
                Name = q.MovieName,
                CategoryId = q.CategoryId,
                CategoryName = q.Category.Name
            }).ToList();

            return Ok(moviesList);
        }


        [HttpGet]
        [Route("api/Movie/GetMovie/{movieId}")]
        public async Task<IActionResult> GetMovie(Guid movieId)
        {
            Movie movie = await _dbContext.Movies.FirstOrDefaultAsync(q => q.Id == movieId);
          
            MoviesModel movietoReturn = new MoviesModel()
            {
                Id = movie.Id,
                Name = movie.MovieName,
                CategoryId = movie.CategoryId,
                CategoryName = movie.Category.Name
            };

            return Ok(movietoReturn);
        }


        [HttpPost]
        [Route("api/Movie/AddMovie")]
        public async Task<IActionResult> AddMovie(AddMovie movie)
        {
            Movie newMovie = new Movie()
            {
                Id = Guid.NewGuid(),
                MovieName = movie.Name,
                CategoryId = movie.CategoryId

            };

            await _dbContext.Movies.AddAsync(newMovie);

            await _dbContext.SaveChangesAsync();

            return Ok();
        }


        [HttpPut]
        [Route("api/Movie/EditMovie")]
        public async Task<IActionResult> EditMovie(EditMovie movie)
        {

            Movie movieToEdit = await _dbContext.Movies.FirstOrDefaultAsync(q => q.Id == movie.Id);

            movieToEdit.MovieName = movie.Name;
            movieToEdit.CategoryId = movie.CategoryId;

            await _dbContext.SaveChangesAsync();

            return Ok();
        }


        [HttpDelete]
        [Route("api/Movie/DeleteMovie/{movieId}")]
        public async Task<IActionResult> DeleteMovie(Guid movieId)
        {
            Movie movieToDelete = await _dbContext.Movies.FirstOrDefaultAsync(q => q.Id == movieId);

            _dbContext.Movies.Remove(movieToDelete);

            await _dbContext.SaveChangesAsync();

            return Ok();
        }



    }
}
