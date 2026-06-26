using Microsoft.AspNetCore.Mvc;
using Moq;
using MovieApi.Dtos;
using MovieApi.Models;
using MovieApi.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace MovieApiTest.Controllers
{
    public class MoviesControllerTests
    {

        [Fact]
        public async Task GetMovieAsync_NoDetails_ReturnsOkWithMovie()
        {
            //Arrange
            var mockService = new Mock<IMovieService>();        //The service we mock
            mockService.Setup(s => s.GetMovieAsync(1, false))   //The Api we call
            .ReturnsAsync(new MovieDto()
            {                        //What it returns
                Title = "Iron Man 1",
                Year = 2008,
                Genre = "Action",
                Duration = 126
            });

            var controller = new MoviesController(mockService.Object);  //The controller we want to test

            //Act
            var result = await controller.GetMovie(1, false);           //Run phony Api call

            //Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);    //Assert Response is the right one
            var movie = Assert.IsType<MovieDto>(okResult.Value);            //Assert it returns the right object
            Assert.Equal("Iron Man 1", movie.Title);                        //Assert the correct title
        }

        [Fact]
        public async Task GetMovieAsync_WithDetails_ReturnsOkWithMovie()
        {
            //Arrange
            var mockService = new Mock<IMovieService>();
            mockService.Setup(s => s.GetMovieAsync(1, true))
            .ReturnsAsync(new MovieDto()
            {
                Title = "Iron Man 1",
                Year = 2008,
                Genre = "Action",
                Duration = 126,
                Details = new MovieDetailDto()
                {
                    Synopsis = "After being captured blah bla blah",
                    Language = "english",
                    Budget = 150,
                    MovieId = 1
                }
            });

            var controller = new MoviesController(mockService.Object);

            //Act
            var result = await controller.GetMovie(1, true);

            //Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var movie = Assert.IsType<MovieDto>(okResult.Value);
            Assert.Equal("Iron Man 1", movie.Title);
            Assert.NotNull(movie.Details);
            Assert.Equal("english", movie.Details.Language);
        }

        [Fact]
        public async Task GetMovieAsync_BadId_ReturnsNotFound()
        {
            //Arrange
            var mockService = new Mock<IMovieService>();
            mockService.Setup(s => s.GetMovieAsync(99, true))
            .ReturnsAsync((MovieDto)null);

            var controller = new MoviesController(mockService.Object);

            //Act
            var result = await controller.GetMovie(99, true);

            //Assert
            var nfResult = Assert.IsType<NotFoundObjectResult>(result.Result);
            var details = Assert.IsType<string>(nfResult.Value);
            Assert.Equal("The movie with the id 99 couldn't be found.", details);

        }

        [Fact]
        public async Task GetMoviesAsync_WithFiltering_ReturnsOkWithMovieList()
        {
            //Arrange
            var mockService = new Mock<IMovieService>();
            mockService.Setup(s => s.GetMoviesAsync("action", 2008))
            .ReturnsAsync(new List<MovieDto>()
            {
                new MovieDto()
                {
                    Title = "Iron Man 1", Year = 2008, Genre = "Action", Duration = 126
                }
            }
            );

            var controller = new MoviesController(mockService.Object);

            //Act
            var result = await controller.GetMovies("action", 2008);

            //Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var movies = Assert.IsType<List<MovieDto>>(okResult.Value);
            Assert.True(movies.Count == 1);

            var sampleMovie = movies.First();
            Assert.Equal("Iron Man 1", sampleMovie.Title);
            Assert.Null(sampleMovie.Details);
        }

        [Fact]
        public async Task GetMoviesAsync_WithoutFiltering_ReturnsOkWithMovieList()
        {
            //Arrange
            var mockService = new Mock<IMovieService>();
            mockService.Setup(s => s.GetMoviesAsync(null, null))
            .ReturnsAsync(new List<MovieDto>()
            {
                new MovieDto()
                {
                    Title = "Iron Man 1", Year = 2008, Genre = "Action", Duration = 126
                },
                new MovieDto()
                {
                    Title = "Iron Man 2", Year = 2010, Genre = "Action", Duration = 124
                },
                new MovieDto()
                {
                    Title = "Iron Man 3", Year = 2013, Genre = "Action", Duration = 130
                }

            }
            );

            var controller = new MoviesController(mockService.Object);

            //Act
            var result = await controller.GetMovies(null, null);

            //Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var movies = Assert.IsType<List<MovieDto>>(okResult.Value);
            Assert.True(movies.Count == 3);

            var sampleMovie = movies.First();
            Assert.Equal("Iron Man 1", sampleMovie.Title);
            Assert.Null(sampleMovie.Details);
        }

        [Fact]
        public async Task GetMoviesAsync_FilteringOutOfBounds_ReturnsOkWithEmptyMovieList()
        {
            //Arrange
            var mockService = new Mock<IMovieService>();
            mockService.Setup(s => s.GetMoviesAsync("comedy", null))
            .ReturnsAsync(new List<MovieDto>()
            );

            var controller = new MoviesController(mockService.Object);

            //Act
            var result = await controller.GetMovies("comedy", null);

            //Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var movies = Assert.IsType<List<MovieDto>>(okResult.Value);
            Assert.True(movies.Count == 0);

        }

        [Fact]
        public async Task GetMovieDetailsAsync_ReturnsOkWithDetails()
        {
            //Arrange
            var mockService = new Mock<IMovieService>();
            mockService.Setup(s => s.GetMovieDetailsAsync(1))
            .ReturnsAsync(new MovieDetails()
            {
                MovieDetailsId = 1,
                Synopsis = "blah blah blah",
                Language = "english",
                Budget = 150,
                MovieId = 1,
                MovieTitle = "Iron Man 1",
                Movie = new Movie()
            }
            );

            mockService.Setup(s => s.MovieExists(1)).Returns(true);     //Needs to ensure that it thinks the movie exists

            var controller = new MoviesController(mockService.Object);

            //Act
            var result = await controller.GetMovieDetails(1);

            //Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var details = Assert.IsType<MovieDetails>(okResult.Value);
            Assert.NotNull(details);

            Assert.Equal("Iron Man 1", details.MovieTitle);
            Assert.Equal(1, details.MovieId);
            Assert.Equal(150, details.Budget);
        }
        [Fact]
        public async Task GetMovieDetailsAsync_BadId_ReturnNotFound()
        {
            //Arrange
            var mockService = new Mock<IMovieService>();
            mockService.Setup(s => s.GetMovieDetailsAsync(99))
            .ReturnsAsync((MovieDetails)null);

            var controller = new MoviesController(mockService.Object);

            //Act
            var result = await controller.GetMovieDetails(99);

            //Assert
            var nfResult = Assert.IsType<NotFoundObjectResult>(result.Result);
            var details = Assert.IsType<string>(nfResult.Value);
            Assert.Equal("The movie with the id 99 couldn't be found.", details);

        }



        //PostMovie

        //PutMovie

        //DeleteMovie
    }
}
