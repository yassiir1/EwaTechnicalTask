using System;
using Xunit;
using AutoFixture;
using Moq;
using FluentAssertions;
using CORE.Interfaces;
using EWATask.Controllers;
using CORE.DTOs;
using Microsoft.AspNetCore.Mvc;
using CORE.Entites;
using CORE.Models;

namespace EWATask.Test.Controllers
{
    public class ProductControllerTest
    {
        private readonly IFixture _fixture;
        private readonly Mock<IProductService> _serviceMoq;
        private readonly ProductController _controller;
        public ProductControllerTest()
        {
            _fixture = new Fixture();
            _serviceMoq = new Mock<IProductService>();
            _controller = new ProductController(_serviceMoq.Object);
        }
        [Fact]
        public async Task GetAllProduct_ShouldReturnOK_WhenDataFound()
        {
            var MockFeed = _fixture.Create<IEnumerable<ProductsDto>>();
            _serviceMoq.Setup(x => x.GetAllProducts()).ReturnsAsync(MockFeed);
            var result = await _controller.GetAllProducts().ConfigureAwait(false);
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<ActionResult<IEnumerable<ProductsDto>>>();
            result.Result.Should().BeAssignableTo<OkObjectResult>();
            result.Result.As<OkObjectResult>().Value.Should().NotBeNull().And.BeOfType(MockFeed.GetType());
            _serviceMoq.Verify(x => x.GetAllProducts(), Times.Once());
        }
        [Fact]
        public async Task GetAllProduct_ShouldReturnNotFound_WhenDataNotFound()
        {
            List<ProductsDto> products = null;
            _serviceMoq.Setup(x => x.GetAllProducts()).ReturnsAsync(products);
            var result = await _controller.GetAllProducts().ConfigureAwait(false);
            result.Should().NotBeNull();
            result.Result.Should().BeAssignableTo<NotFoundResult>();
            _serviceMoq.Verify(x => x.GetAllProducts(), Times.Once());
        }
        [Fact]
        public async Task GetProductById_ShouldReturnOK_WhenDataFound()
        {
            var product = _fixture.Create<ProductsDto>();
            var id = _fixture.Create<int>();
            _serviceMoq.Setup(x => x.GetProductById(id)).Returns(product);
            var result = await _controller.GetProductDetails(id).ConfigureAwait(false);
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<ActionResult<ProductsDto>>();
            result.Result.Should().BeAssignableTo<ActionResult<OkObjectResult>>();
            result.Result.As<OkObjectResult>().Value.Should().NotBeNull().And.BeOfType(product.GetType());
            _serviceMoq.Verify(c => c.GetProductById(id), Times.Once());
        }
        [Fact]
        public async Task GetProductById_ShouldReturnNotFound_WhenDataNotFound()
        {
            ProductsDto product = null;
            var id = _fixture.Create<int>();
            _serviceMoq.Setup(x => x.GetProductById(id)).Returns(product);
            var result = await _controller.GetProductDetails(id).ConfigureAwait(false);
            result.Should().NotBeNull();
            result.Result.Should().BeAssignableTo<NotFoundResult>();
            _serviceMoq.Verify(x => x.GetProductById(id), Times.Once());
        }
        [Fact]
        public async Task GetProductById_ShouldReturnBadRequest_WhenDataIsZero()
        {
            var product = _fixture.Create<ProductsDto>();
            int id = 0;
            _serviceMoq.Setup(x => x.GetProductById(id)).Returns(product);
            var result = await _controller.GetProductDetails(id).ConfigureAwait(false);
            result.Should().NotBeNull();
            result.Result.Should().BeAssignableTo<BadRequestResult>();
            _serviceMoq.Verify(x => x.GetProductById(id), Times.Once());
        }
        [Fact]
        public async Task AddProduct_ShouldReturnOk_WhenValiedRequest()
        {
            var Requestproduct = _fixture.Create<AddProduct>();
            var Respondproduct = _fixture.Create<AddProduct>();
            _serviceMoq.Setup(x => x.AddProduct(It.IsAny<AddProduct>())).ReturnsAsync(false);
            var result = await _controller.AddProduct(Requestproduct).ConfigureAwait(false);
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<ActionResult<AddProduct>>();
            result.Result.Should().BeAssignableTo<CreatedAtRouteResult>();
            _serviceMoq.Verify(c => c.AddProduct(Respondproduct), Times.Never());

        }
        [Fact]
        public async Task AddProduct_ShouldReturnBadRequest_WhenInValiedRequest()
        {
            var Request = _fixture.Create<AddProduct>();
            _controller.ModelState.AddModelError("Subject", "The Subject Feild is Required");
            var Response = _fixture.Create<AddProduct>();
            _serviceMoq.Setup(x => x.AddProduct(It.IsAny<AddProduct>())).ReturnsAsync(false);
            var result = await _controller.AddProduct(Request).ConfigureAwait(false);
            result.Should().NotBeNull();
            result.Result.Should().BeAssignableTo<BadRequestResult>();
            _serviceMoq.Verify(x => x.AddProduct(Request), Times.Never());

        }
        [Fact]
        public async Task DeleteProduct_ShouldReturnOk_WhenDeleting()
        {
            var id = _fixture.Create<int>();
            _serviceMoq.Setup(x => x.DeleteProduct(id)).ReturnsAsync(true);
            var result = await _controller.DeleteProduct(id).ConfigureAwait(false);
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<NoContentResult>();
        }
        [Fact]
        public async Task DeleteProduct_ShouldReturnNotFound_WhenRecordNotFound()
        {
            var id = _fixture.Create<int>();
            _serviceMoq.Setup(x => x.DeleteProduct(id)).ReturnsAsync(false);
            var result = await _controller.DeleteProduct(id).ConfigureAwait(false);
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<NotFoundResult>();
        }
        [Fact]
        public async Task DeleteProduct_ShouldReturnBadRequest_WhenRecordIsZero()
        {
            var id = 0;
            _serviceMoq.Setup(x => x.DeleteProduct(id)).ReturnsAsync(false);
            var result = await _controller.DeleteProduct(id).ConfigureAwait(false);
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<BadRequestResult>();
            _serviceMoq.Verify(c => c.DeleteProduct(id), Times.Never());
        }
        [Fact]
        public async Task UpdateProduct_ShouldReturnBadRequest_WhenIdIsZero()
        {
            var request = _fixture.Create<Products>();
            _serviceMoq.Setup(c => c.UpdateProduct(request)).Returns(false);
            var result = await _controller.UpdateProduct(request).ConfigureAwait(false);
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<BadRequestResult>();
            _serviceMoq.Verify(x => x.UpdateProduct(request), Times.Never());
        }
        [Fact]
        public async Task UpdateProduct_ShouldReturnOk_WhenRecordIsUpdated()
        {
            var request = _fixture.Create<Products>();
            _serviceMoq.Setup(c => c.UpdateProduct(request)).Returns(true);
            var result = await _controller.UpdateProduct(request).ConfigureAwait(false);
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<OkObjectResult>();
            _serviceMoq.Verify(x => x.UpdateProduct(request), Times.Never());
        }
        [Fact]
        public async Task UpdateProduct_ShouldReturnNotFound_WhenRecordNotFound()
        {
            var request = _fixture.Create<Products>();
            _serviceMoq.Setup(c => c.UpdateProduct(request)).Returns(false);
            var result = await _controller.UpdateProduct(request).ConfigureAwait(false);
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<NotFoundResult>();
            _serviceMoq.Verify(x => x.UpdateProduct(request), Times.Never());
        }
    }
}