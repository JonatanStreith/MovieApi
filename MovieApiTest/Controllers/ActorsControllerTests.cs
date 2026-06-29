using Microsoft.AspNetCore.Mvc;
using Moq;
using MovieApi.Controllers;
using MovieApi.Dtos;
using MovieApi.Models;
using MovieApi.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace MovieApiTest.Controllers
{
    public class ActorsControllerTests
    {
        [Fact]
        public async Task GetActorsAsync_ReturnsOkWithActorsList()
        {
            //Arrange
            var mockService = new Mock<IActorService>();    
            mockService.Setup(s => s.GetActorsAsync()) 
            .ReturnsAsync(new List<ActorDto>()
            {                  
                new ActorDto()
                {
                    Name = "Robert Downey Jr.",
                    BirthYear = 1965
                },
                new ActorDto()
                {
                    Name = "Gwyneth Paltrow",
                    BirthYear = 1972
                },
                new ActorDto()
                {
                    Name = "Terrence Howard",
                    BirthYear = 1969
                }            });

            var controller = new ActorsController(mockService.Object);

            //Act
            var result = await controller.GetActors();   

            //Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);  
            var actorList = Assert.IsType<List<ActorDto>>(okResult.Value); 
            Assert.NotEmpty(actorList);
            Assert.Equal(3, actorList.Count);
            Assert.Equal("Robert Downey Jr.", actorList.First().Name);
        }

        //GetActor
        [Fact]
        public async Task GetActorAsync_ReturnsOkWithActor()
        {
            //Arrange
            var mockService = new Mock<IActorService>();
            mockService.Setup(s => s.GetActorAsync(1))
            .ReturnsAsync(new ActorDto() 
                {
                    Name = "Robert Downey Jr.",
                    BirthYear = 1965
                }
                );

            var controller = new ActorsController(mockService.Object);

            //Act
            var result = await controller.GetActor(1);

            //Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var actor = Assert.IsType<ActorDto>(okResult.Value);
            Assert.NotNull(actor);
            Assert.Equal("Robert Downey Jr.", actor.Name);
        }

        [Fact]
        public async Task GetActorAsync_BadId_ReturnsNotFound()
        {
            //Arrange
            var mockService = new Mock<IActorService>();
            mockService.Setup(s => s.GetActorAsync(99))
            .ReturnsAsync((ActorDto)null);

            var controller = new ActorsController(mockService.Object);

            //Act
            var result = await controller.GetActor(99);

            //Assert
            var nfResult = Assert.IsType<NotFoundObjectResult>(result.Result);
            var message = Assert.IsType<string>(nfResult.Value);

            Assert.Equal("The actor with the id 99 couldn't be found.", message);
        }

        //PostActor
        [Fact]
        public async Task AddActorAsync_ReturnsOkWithActor()
        {
            //Arrange
            var actorDto = new ActorDto() { Name = "Jim Carrey", BirthYear = 1962 };

            var mockService = new Mock<IActorService>();
            mockService.Setup(s => s.AddActorAsync(actorDto))
            .ReturnsAsync(new Actor()
            {
                ActorId = 1,
                Name = "Jim Carrey",
                BirthYear = 1962
            }
                );

            var controller = new ActorsController(mockService.Object);

            //Act
            var result = await controller.PostActor(actorDto);

            //Assert
            var caaResult = Assert.IsType<CreatedAtActionResult>(result.Result);
            var actor = Assert.IsType<Actor>(caaResult.Value);
            Assert.Equal("GetActor", caaResult.ActionName);
            Assert.NotNull(actor);
            Assert.Equal("Jim Carrey", actor.Name);
        }

        [Fact]
        public async Task AddActorAsync_BadData_ReturnsbadRequest()
        {
            //Arrange
            ActorDto actorDto = null;

            var mockService = new Mock<IActorService>();
            mockService.Setup(s => s.AddActorAsync(actorDto))
            .ReturnsAsync((Actor)null);

            var controller = new ActorsController(mockService.Object);

            //Act
            var result = await controller.PostActor(actorDto);

            //Assert
            var brResult = Assert.IsType<BadRequestObjectResult>(result.Result);
            var message = Assert.IsType<string>(brResult.Value);
            Assert.Equal("Incomplete or bad data.", message);
        }

        //PutActor
        [Fact]
        public async Task PutActorAsync_ReturnNoContent()
        {
            //Arrange
            var newActorDto = new ActorDto()
            {
                Name = "Lou Ferrigno",
                BirthYear = 1952
            };

            var mockService = new Mock<IActorService>();
            mockService.Setup(s => s.UpdateActorAsync(5, newActorDto))
            .ReturnsAsync(true);

            var controller = new ActorsController(mockService.Object);

            //Act
            var result = await controller.PutActor(5, newActorDto);

            //Assert
            var ncResult = Assert.IsType<NoContentResult>(result.Result);
        }

        [Fact]
        public async Task PutActorAsync_BadData_ReturnBadRequest()
        {
            //Arrange
            var newActorDto = new ActorDto()
            {
                //Name = "Lou Ferrigno",
                BirthYear = 1952
            };

            var mockService = new Mock<IActorService>();
            mockService.Setup(s => s.UpdateActorAsync(5, newActorDto))
            .ReturnsAsync(false);

            var controller = new ActorsController(mockService.Object);

            //Act
            var result = await controller.PutActor(5, newActorDto);

            //Assert
            var nfResult = Assert.IsType<NotFoundObjectResult>(result.Result);
            var message = Assert.IsType<string>(nfResult.Value);
            Assert.Equal("The actor with the id 5 couldn't be found.", message);
        }

        //DeleteActor
        [Fact]
        public async Task DeleteActorAsync_ReturnNoContent()
        {
            //Arrange
            var mockService = new Mock<IActorService>();
            mockService.Setup(s => s.DeleteActorAsync(5))
            .ReturnsAsync(true);

            var controller = new ActorsController(mockService.Object);

            //Act
            var result = await controller.DeleteActor(5);

            //Assert
            var ncResult = Assert.IsType<NoContentResult>(result.Result);
        }

        [Fact]
        public async Task DeleteActorAsync_BadId_ReturnNotFound()
        {
            //Arrange
            var mockService = new Mock<IActorService>();
            mockService.Setup(s => s.DeleteActorAsync(5))
            .ReturnsAsync(false);

            var controller = new ActorsController(mockService.Object);

            //Act
            var result = await controller.DeleteActor(5);

            //Assert
            var nfResult = Assert.IsType<NotFoundObjectResult>(result.Result);
            var message = Assert.IsType<string>(nfResult.Value);
            Assert.Equal("The actor with the id 5 couldn't be found.", message);

        }

        //AddActorToMovie
        [Fact]
        public async Task AddActorToMovieAsync_ReturnNoContent() 
        {
            //Arrange
            var mockService = new Mock<IActorService>();
            mockService.Setup(s => s.AddActorToMovieAsync(5, 3))
            .ReturnsAsync(true);

            var controller = new ActorsController(mockService.Object);

            //Act
            var result = await controller.AddActorToMovie(5, 3);

            //Assert
            var ncResult = Assert.IsType<NoContentResult>(result.Result);
        }


        [Fact]
        public async Task AddActorToMovieAsync_BadId_ReturnNotFound()
        {
            //Arrange
            var mockService = new Mock<IActorService>();
            mockService.Setup(s => s.AddActorToMovieAsync(5, 3))
            .ReturnsAsync(false);

            var controller = new ActorsController(mockService.Object);

            //Act
            var result = await controller.AddActorToMovie(5, 3);

            //Assert
            var nfResult = Assert.IsType<NotFoundObjectResult>(result.Result);
            var message = Assert.IsType<string>(nfResult.Value);
            Assert.Equal("Movie 5 and/or actor 3 not found.", message);
        }


    }
}
