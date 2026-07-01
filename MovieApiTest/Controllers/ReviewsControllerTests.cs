using Microsoft.AspNetCore.Mvc;
using Moq;
using MovieApi.Controllers;
using MovieApi.Core.Interfaces;
using MovieApi.Dtos;
using MovieApi.Interfaces;
using MovieApi.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MovieApiTest.Controllers
{
    public class ReviewsControllerTests
    {

        Mock<IReviewRepository> mockService = new Mock<IReviewRepository>();
        Mock<IUnitOfWork> mockUnit = new Mock<IUnitOfWork>();



        //GetReviews
        [Fact]
        public async Task GetReviewsAsync_ReturnsOkWithReviews()
        {
            //Arrange
            mockUnit.Setup(x => x.Reviews).Returns(mockService.Object);
            mockService.Setup(s => s.GetReviewsAsync(1))
            .ReturnsAsync(new List<ReviewDto>()
            {
                new ReviewDto()
                {
                    ReviewerName= "ReviewerGuy",
                    Comment = "Some comment",
                    Rating = 3,
                    MovieId = 1
                }
            });

            mockService.Setup(s => s.MovieExists(1))
                .Returns(true);

            var controller = new ReviewsController(mockUnit.Object);

            //Act
            var result = await controller.GetReviews(1);

            //Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var reviews = Assert.IsType<List<ReviewDto>>(okResult.Value);
            Assert.NotEmpty(reviews);
            Assert.Equal("ReviewerGuy", reviews.First().ReviewerName);
        }

        [Fact]
        public async Task GetReviewsAsync_NoSuchMovie_ReturnsNotFound()
        {
            //Arrange
            mockUnit.Setup(x => x.Reviews).Returns(mockService.Object);
            mockService.Setup(s => s.GetReviewsAsync(1))
            .ReturnsAsync(new List<ReviewDto>()
            {
                new ReviewDto()
                {
                    ReviewerName= "ReviewerGuy",
                    Comment = "Some comment",
                    Rating = 3,
                    MovieId = 1
                }
            });

            mockService.Setup(s => s.MovieExists(1))
                .Returns(false);

            var controller = new ReviewsController(mockUnit.Object);

            //Act
            var result = await controller.GetReviews(1);

            //Assert
            var okResult = Assert.IsType<NotFoundObjectResult>(result.Result);
            var message = Assert.IsType<string>(okResult.Value);
            Assert.Equal("No movie with id 1 exists in the database.", message);
        }


        //PostReview
        [Fact]
        public async Task AddReviewAsync_ReturnsCreatedAtAction() 
        { 
            var reviewDto = new ReviewDto() { ReviewerName = "Someguy", Comment = "This is a comment", Rating = 5, MovieId = 1 };

            //Arrange
            mockUnit.Setup(x => x.Reviews).Returns(mockService.Object);
            mockService.Setup(s => s.AddReviewAsync(1, reviewDto))
            .ReturnsAsync(new Review()
            {
                
                    ReviewerName= "ReviewerGuy",
                    Comment = "Some comment",
                    Rating = 3,
                    MovieId = 1
                
            });

            mockService.Setup(s => s.MovieExists(1))
            .Returns(true);

            var controller = new ReviewsController(mockUnit.Object);

            //Act
            var result = await controller.PostReview(1, reviewDto);

            //Assert
            var caaResult = Assert.IsType<CreatedAtActionResult>(result.Result);
            var review = Assert.IsType<Review>(caaResult.Value);
            Assert.NotNull(review);
            Assert.Equal("ReviewerGuy", review.ReviewerName);
        }

        [Fact]
        public async Task AddReviewAsync_BadData_ReturnsBadRequest()
        {
            ReviewDto reviewDto = null;

            //Arrange
            mockUnit.Setup(x => x.Reviews).Returns(mockService.Object);
            mockService.Setup(s => s.AddReviewAsync(1, reviewDto))
            .ReturnsAsync((Review)null);

            mockService.Setup(s => s.MovieExists(1))
            .Returns(true);

            var controller = new ReviewsController(mockUnit.Object);

            //Act
            var result = await controller.PostReview(1, reviewDto);

            //Assert
            var brResult = Assert.IsType<BadRequestObjectResult>(result.Result);
            var message = Assert.IsType<string>(brResult.Value);
            Assert.Equal("Incomplete or bad data.", message);
        }

        [Fact]
        public async Task AddReviewAsync_WrongId_ReturnsBadRequest()
        {
            var reviewDto = new ReviewDto() { Comment = "This is a comment", Rating = 5, MovieId = 2 };

            //Arrange
            mockUnit.Setup(x => x.Reviews).Returns(mockService.Object);
            mockService.Setup(s => s.AddReviewAsync(1, reviewDto))
            .ReturnsAsync((Review)null);

            mockService.Setup(s => s.MovieExists(1))
            .Returns(true);

            var controller = new ReviewsController(mockUnit.Object);

            //Act
            var result = await controller.PostReview(1, reviewDto);

            //Assert
            var brResult = Assert.IsType<BadRequestObjectResult>(result.Result);
            var message = Assert.IsType<string>(brResult.Value);
            Assert.Equal("Incomplete or bad data.", message);
        }

        [Fact]
        public async Task AddReviewAsync_NoSuchMovie_ReturnsBadRequest()
        {
            var reviewDto = new ReviewDto() { Comment = "This is a comment", Rating = 5, MovieId = 1 };

            //Arrange
            mockUnit.Setup(x => x.Reviews).Returns(mockService.Object);
            mockService.Setup(s => s.MovieExists(1))
            .Returns(false);
           
            var controller = new ReviewsController(mockUnit.Object);

            //Act
            var result = await controller.PostReview(1, reviewDto);

            //Assert
            var brResult = Assert.IsType<BadRequestObjectResult>(result.Result);
            var message = Assert.IsType<string>(brResult.Value);
            Assert.Equal("No movie with id 1 exists in the database.", message);
        }

        //DeleteReview
        [Fact]
        public async Task DeleteReviewAsync_ReturnsNoContent()
        {

            //Arrange
            mockUnit.Setup(x => x.Reviews).Returns(mockService.Object);
            mockService.Setup(s => s.DeleteReviewAsync(1))
            .ReturnsAsync(true);

            var controller = new ReviewsController(mockUnit.Object);

            //Act
            var result = await controller.DeleteReview(1);

            //Assert
            var ncResult = Assert.IsType<NoContentResult>(result.Result);
        }

        [Fact]
        public async Task DeleteReviewAsync_BadId_ReturnsNotFound()
        {

            //Arrange
            mockUnit.Setup(x => x.Reviews).Returns(mockService.Object);
            mockService.Setup(s => s.DeleteReviewAsync(1))
            .ReturnsAsync(false);

            var controller = new ReviewsController(mockUnit.Object);

            //Act
            var result = await controller.DeleteReview(1);

            //Assert
            var nfResult = Assert.IsType<NotFoundObjectResult>(result.Result);
            var message = Assert.IsType<string>(nfResult.Value);
            Assert.Equal("No movie with id 1 exists in the database.", message);
        }

    }
}
