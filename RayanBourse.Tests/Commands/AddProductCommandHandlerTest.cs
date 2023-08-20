using AutoMapper;
using FluentAssertions;
using Moq;
using RayanBourse.Application.Features.Product.Commands;
using RayanBourse.Domain;
using RayanBourse.Domain.Entities;
using RayanBourse.Infrastructure;
using RayanBourse.Infrastructure.Interfaces;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace RayanBourse.Tests.Commands
{
    public class AddProductCommandHandlerTest
    {
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly Mock<IProductRepository> _productRepository;

        public AddProductCommandHandlerTest()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _productRepository = new();

        }

        [Fact]
        public async Task Handle_Should_ReturnFailure_WhenEmailFormatIncorect()
        {
            //Arrange
            var command = new AddProductCommand()
            {
                Name = "test",
                ManufacturePhone = "09119050105",
                ManufactureEmail = "test@tes.",
                ProduceDate = DateTime.Now,
                IsAvailable = EnumYesNo.Yes,
                UserId = "4156e6d8-caaa-4365-b92f-8ce8e406ae8f"
            };

            _unitOfWorkMock.Setup(uow => uow.ProductRepository).Returns(_productRepository.Object);
            var handler = new AddProductCommandHandler(_unitOfWorkMock.Object);

            //Act
            Exception ex = await Should.ThrowAsync<Exception>(
                async () =>
                {
                    await handler.Handle(command, CancellationToken.None);
                }
                );


            //Assert

            Assert.Equal("email format is invalid!", ex.Message);

        }


        [Fact]
        public async Task Handle_Should_ReturnFailure_WhenMobileIncorect()
        {
            //Arrange
            var command = new AddProductCommand()
            {
                Name = "test",
                ManufacturePhone = "091190505",
                ManufactureEmail = "test@tes.com",
                ProduceDate = DateTime.Now,
                IsAvailable = EnumYesNo.Yes,
                UserId = "4156e6d8-caaa-4365-b92f-8ce8e406ae8f"
            };

            _unitOfWorkMock.Setup(uow => uow.ProductRepository).Returns(_productRepository.Object);
            var handler = new AddProductCommandHandler(_unitOfWorkMock.Object);

            //Act
            Exception ex = await Should.ThrowAsync<Exception>(
                async () =>
                {
                    await handler.Handle(command, CancellationToken.None);
                }
                );


            //Assert

            Assert.Equal("phone number is invalid!", ex.Message);

        }


        [Fact]
        public async Task Handle_Should_ReturnOK_AddProduct()
        {
            //Arrange
            var command = new AddProductCommand()
            {
                Name = "test",
                ManufacturePhone = "09119050105",
                ManufactureEmail = "test@tes.com",
                ProduceDate = DateTime.Now,
                IsAvailable = EnumYesNo.Yes,
                UserId = "4156e6d8-caaa-4365-b92f-8ce8e406ae8f"
            };

            var lstProducts = new List<Product>
            {
                new Product
                {
                 Name = "test",
                ManufacturePhone = "09119050105",
                ManufactureEmail = "test@tes1.com",
                ProduceDate = DateTime.Now,
                IsAvailable = EnumYesNo.Yes,
                UserId = "4156e6d8-caaa-4365-b92f-8ce8e406ae8f"
                }
            };


            _unitOfWorkMock
                .Setup(uow => uow.ProductRepository)
                .Returns(_productRepository.Object);

            _unitOfWorkMock
                .Setup(uow => uow.ProductRepository.Save(It.IsAny<Product>()))
                .ReturnsAsync((Product product) =>
                {
                    lstProducts.Add(product);
                    return product;
                });

            var handler = new AddProductCommandHandler(_unitOfWorkMock.Object);

            //Act
            var result = handler.Handle(command, CancellationToken.None);


            //Assert

            result.Result.Should().NotBeNull();

        }
    }
}
