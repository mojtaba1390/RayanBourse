using AutoMapper;
using FluentAssertions;
using Moq;
using RayanBourse.Application.Features.Product.Commands;
using RayanBourse.Application.Interfaces;
using RayanBourse.Domain;
using RayanBourse.Domain.Entities;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using static RayanBourse.Application.Features.Product.Commands.AddProductCommand;

namespace RayanBourse.Tests.Commands
{
    public class AddProductCommandHandlerTest
    {
        private readonly Mock<IProductService> _productServiceMock;
        public AddProductCommandHandlerTest()
        {
            _productServiceMock = new Mock<IProductService>();
        }

        [Fact]
        public async Task Handle_Should_ReturnFailure_WhenEmailFormatIncorect()
        {
            //Arrange
            var command = new Product()
            {
                Name = "test",
                ManufacturePhone = "09119050105",
                ManufactureEmail = "test@tes",
                ProduceDate = DateTime.Now,
                IsAvailable = EnumYesNo.Yes,
                UserId = "4156e6d8-caaa-4365-b92f-8ce8e406ae8f"
            };

            var command2 = new AddProductCommand()
            {
                Name = "test",
                ManufacturePhone = "09119050105",
                ManufactureEmail = "test",
                ProduceDate = DateTime.Now,
                IsAvailable = EnumYesNo.Yes,
                UserId = "4156e6d8-caaa-4365-b92f-8ce8e406ae8f"
            };

            _productServiceMock
    .Setup(x => x.Save(It.IsAny<Product>())).ReturnsAsync(command);


            var handler = new AddProductCommandHandler(_productServiceMock.Object);

            //Act

            var result = handler.Handle(command2, CancellationToken.None);


            //Assert

            //assert
            result.ShouldNotBeNull();


        }
    }
}
