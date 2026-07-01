using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using MovieApi.Core.Interfaces;
using MovieApi.Dtos;
using MovieApi.Interfaces;
using MovieApi.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MovieApiTest.Controllers
{
    public class MoviesControllerTests
    {

        Mock<IMovieRepository> mockService = new Mock<IMovieRepository>();        //The service we mock
        Mock<IUnitOfWork> mockUnit = new Mock<IUnitOfWork>();
            
            
            /*


        [Fact]
        public async Task GetMovieAsync_NoDetails_ReturnsOkWithMovie()
        {
            //Arrange
            mockUnit.Setup(x => x.Movies).Returns(mockService.Object);

            mockService.Setup(s => s.GetMovieAsync(1, false))   //The Api we call
            .ReturnsAsync(new MovieDto()
            {                        //What it returns
                Title = "Iron Man 1",
                Year = 2008,
                Genre = "Action",
                Duration = 126
            });

            var controller = new MoviesController(mockUnit.Object);  //The controller we want to test

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
            mockUnit.Setup(x => x.Movies).Returns(mockService.Object);
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

            var controller = new MoviesController(mockUnit.Object);

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
            mockUnit.Setup(x => x.Movies).Returns(mockService.Object);
            mockService.Setup(s => s.GetMovieAsync(99, true))
            .ReturnsAsync((MovieDto)null);

            var controller = new MoviesController(mockUnit.Object);

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
            mockUnit.Setup(x => x.Movies).Returns(mockService.Object);
            mockService.Setup(s => s.GetMoviesAsync("action", 2008))
            .ReturnsAsync(new List<MovieDto>()
            {
                new MovieDto()
                {
                    Title = "Iron Man 1", Year = 2008, Genre = "Action", Duration = 126
                }
            }
            );

            var controller = new MoviesController(mockUnit.Object);

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
            mockUnit.Setup(x => x.Movies).Returns(mockService.Object);
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

            var controller = new MoviesController(mockUnit.Object);

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
            mockUnit.Setup(x => x.Movies).Returns(mockService.Object);
            mockService.Setup(s => s.GetMoviesAsync("comedy", null))
            .ReturnsAsync(new List<MovieDto>()
            );

            var controller = new MoviesController(mockUnit.Object);

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
            mockUnit.Setup(x => x.Movies).Returns(mockService.Object);
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

            var controller = new MoviesController(mockUnit.Object);

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
            mockUnit.Setup(x => x.Movies).Returns(mockService.Object);
            mockService.Setup(s => s.GetMovieDetailsAsync(99))
            .ReturnsAsync((MovieDetails)null);

            var controller = new MoviesController(mockUnit.Object);

            //Act
            var result = await controller.GetMovieDetails(99);

            //Assert
            var nfResult = Assert.IsType<NotFoundObjectResult>(result.Result);
            var details = Assert.IsType<string>(nfResult.Value);
            Assert.Equal("The movie with the id 99 couldn't be found.", details);

        }

        [Fact]
        public async Task PostMovieAsync_ReturnCreatedAtAction()
        {
            //Arrange
            var newMovieDto = new MovieCreateDto()
            {
                Title = "Ferngully",
                Genre = "animation",
                Year = 1992,
                Language = "english",
                Synopsis = "Animated environmental film",
                Budget = 24,
                Duration = 76
            };

            mockUnit.Setup(x => x.Movies).Returns(mockService.Object);
            mockService.Setup(s => s.AddMovieAsync(newMovieDto))
            .ReturnsAsync(new Movie()
            {
                MovieId = 89,
                Title = newMovieDto.Title,
                Genre = newMovieDto.Genre,
                Year = newMovieDto.Year,
                Duration = newMovieDto.Duration,
                MovieDetails = new MovieDetails()

                {
                    Language = newMovieDto.Language,
                    Synopsis = newMovieDto.Synopsis,
                    Budget = 24,
                    MovieId = 89,
                    MovieTitle = newMovieDto.Title
                }
            }


            );

            var controller = new MoviesController(mockUnit.Object);

            //Act
            var result = await controller.PostMovie(newMovieDto);

            //Assert
            var caaResult = Assert.IsType<CreatedAtActionResult>(result.Result);
            var movie = Assert.IsType<Movie>(caaResult.Value);
            Assert.Equal("GetMovie", caaResult.ActionName);
            Assert.Equal("Ferngully", movie.Title);
            Assert.Equal(24, movie.MovieDetails.Budget);

        }

        [Fact]
        public async Task PostMovieAsync_BadData_ReturnBadRequest()
        {
            //Arrange
            var newMovieDto = new MovieCreateDto()
            {
                
                Genre = "animation",
                Year = 1992,
                Language = "english",
                Synopsis = "Animated environmental film",
                Budget = 24,
                Duration = 76
            };

            mockUnit.Setup(x => x.Movies).Returns(mockService.Object);
            mockService.Setup(s => s.AddMovieAsync(newMovieDto))
            .ReturnsAsync((Movie)null);

            var controller = new MoviesController(mockUnit.Object);

            //Act
            var result = await controller.PostMovie(newMovieDto);

            //Assert
            var brResult = Assert.IsType<BadRequestObjectResult>(result.Result);
            var message = Assert.IsType<string>(brResult.Value);
            Assert.Equal("Movie could not be added; faulty data.", message);
        }

        [Fact]
        public async Task PutMovieAsync_ReturnNoContent()
        {
            //Arrange
            var newMovieDto = new MovieUpdateDto()
            {
                Title = "Ferngully",
                Genre = "eco-documentary",
                Year = 1992,
                Duration = 76
            };

            mockUnit.Setup(x => x.Movies).Returns(mockService.Object);
            mockService.Setup(s => s.UpdateMovieAsync(5, newMovieDto))
            .ReturnsAsync(true);

            var controller = new MoviesController(mockUnit.Object);

            //Act
            var result = await controller.PutMovie(5, newMovieDto);

            //Assert
            var ncResult = Assert.IsType<NoContentResult>(result.Result);
        }

        [Fact]
        public async Task PutMovieAsync_BadData_ReturnBadRequest()
        {
            //Arrange
            var newMovieDto = new MovieUpdateDto()
            {
                //Title = "Ferngully",
                Genre = "eco-documentary",
                Year = 1992,
                Duration = 76
            };

            mockUnit.Setup(x => x.Movies).Returns(mockService.Object);
            mockService.Setup(s => s.UpdateMovieAsync(5, newMovieDto))
            .ReturnsAsync(false);

            var controller = new MoviesController(mockUnit.Object);

            //Act
            var result = await controller.PutMovie(5, newMovieDto);

            //Assert
            var nfResult = Assert.IsType<NotFoundObjectResult>(result.Result);
            var message = Assert.IsType<string>(nfResult.Value);
            Assert.Equal("The movie with the id 5 couldn't be found.", message);
        }

        [Fact]
        public async Task DeleteMovieAsync_ReturnNoContent()
        {
            //Arrange
            mockUnit.Setup(x => x.Movies).Returns(mockService.Object);
            mockService.Setup(s => s.DeleteMovieAsync(5))
            .ReturnsAsync(true);

            var controller = new MoviesController(mockUnit.Object);

            //Act
            var result = await controller.DeleteMovie(5);

            //Assert
            var ncResult = Assert.IsType<NoContentResult>(result.Result);
        }

        [Fact]
        public async Task DeleteMovieAsync_BadId_ReturnNotFound()
        {
            //Arrange
            mockUnit.Setup(x => x.Movies).Returns(mockService.Object);
            mockService.Setup(s => s.DeleteMovieAsync(5))
            .ReturnsAsync(false);

            var controller = new MoviesController(mockUnit.Object);

            //Act
            var result = await controller.DeleteMovie(5);

            //Assert
            var nfResult = Assert.IsType<NotFoundObjectResult>(result.Result);
            var message = Assert.IsType<string>(nfResult.Value);
            Assert.Equal("The movie with the id 5 couldn't be found.", message);

        }
            */
    }
}
