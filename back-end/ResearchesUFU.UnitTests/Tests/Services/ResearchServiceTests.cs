using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Atc.Test;
using AutoFixture;
using AutoFixture.Xunit2;
using AutoMapper;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using NSubstitute.ReturnsExtensions;
using ResearchesUFU.API.Models;
using ResearchesUFU.API.Models.DTO.Responses;
using ResearchesUFU.API.Repositories.Interfaces;
using ResearchesUFU.API.Services;
using Xunit;

namespace ResearchesUFU.UnitTests.Tests.Services;

public class ResearchServiceTest
{
    private readonly Fixture _fixture;

    public ResearchServiceTest()
    {
        _fixture = new Fixture();
        _fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList().ForEach(b => _fixture.Behaviors.Remove(b));
        _fixture.Behaviors.Add(new OmitOnRecursionBehavior());
    }

    #region Get By Id Tests
    [Theory, AutoNSubstituteData]
    public async Task Get_ById_Success(
        [Frozen] IBaseRepository<Research> researchRepository,
        [Frozen] IMapper mapper,
        [Greedy] ResearchService sut
    )
    {
        // Arrange
        var requestId = _fixture.Create<int>();
        var repositoryResponse = _fixture.Create<Research>();
        var mappedResponse = _fixture.Create<ResearchResponseDTO>();
        
        researchRepository.GetOneAsync(Arg.Any<int>()).ReturnsForAnyArgs(repositoryResponse);
        mapper.Map<ResearchResponseDTO>(Arg.Any<Research>()).ReturnsForAnyArgs(mappedResponse);

        // Act
        var response = await sut.GetAsync(requestId);

        // Assert
        response.HttpStatusCode.Should().Be(StatusCodes.Status200OK);
        response.Content.Should().Be(mappedResponse);
        
        await researchRepository.Received().GetOneAsync(requestId);
        mapper.Received().Map<ResearchResponseDTO>(repositoryResponse);
    }
    
    [Theory, AutoNSubstituteData]
    public async Task Get_ById_NotFound(
        [Frozen] IBaseRepository<Research> researchRepository,
        [Greedy] ResearchService sut
    )
    {
        // Arrange
        var requestId = _fixture.Create<int>();
        researchRepository.GetOneAsync(Arg.Any<int>()).ReturnsNullForAnyArgs();
        
        // Act
        var response = await sut.GetAsync(requestId);
        
        // Assert
        response.HttpStatusCode.Should().Be(StatusCodes.Status404NotFound);
        response.Content.Should().BeNull();
        
        await researchRepository.Received().GetOneAsync(requestId);
    }
    
    [Theory, AutoNSubstituteData]
    public async Task Get_ById_Error(
        [Frozen] IBaseRepository<Research> researchRepository,
        [Greedy] ResearchService sut
    )
    {
        // Arrange
        var requestId = _fixture.Create<int>();
        var exception = _fixture.Create<Exception>();
        researchRepository.GetOneAsync(Arg.Any<int>()).ThrowsAsyncForAnyArgs(exception);
        
        // Act
        var response = await sut.GetAsync(requestId);
        
        // Assert
        response.HttpStatusCode.Should().Be(StatusCodes.Status500InternalServerError);
        response.Content.Should().BeNull();
        
        await researchRepository.Received().GetOneAsync(requestId);
    }
    #endregion

    #region Get All Tests
    [Theory, AutoNSubstituteData]
    public async Task Get_All_Success(
        [Frozen] IBaseRepository<Research> researchRepository,
        [Frozen] IMapper mapper,
        [Greedy] ResearchService sut
    )
    {
        // Arrange
        var repositoryResponse = new List<Research> { _fixture.Create<Research>() }.AsQueryable();
        var mappedResponse = _fixture.Create<ResearchResponseDTO>();
        
        researchRepository.GetAllAsync().ReturnsForAnyArgs(repositoryResponse);
        mapper.Map<ResearchResponseDTO>(Arg.Any<Research>()).ReturnsForAnyArgs(mappedResponse);

        // Act
        var response = await sut.GetAsync();

        // Assert
        response.HttpStatusCode.Should().Be(StatusCodes.Status200OK);
        response.Content.Should().NotBeNull();
        response.Content.Should().AllBeAssignableTo<ResearchResponseDTO>();

        await researchRepository.Received().GetAllAsync();
        repositoryResponse.ToList().ForEach(r => mapper.Received().Map<ResearchResponseDTO>(r));
    }
    
    [Theory, AutoNSubstituteData]
    public async Task Get_All_NotFound(
        [Frozen] IBaseRepository<Research> researchRepository,
        [Greedy] ResearchService sut
    )
    {
        // Arrange
        researchRepository.GetAllAsync().ReturnsNullForAnyArgs();

        // Act
        var response = await sut.GetAsync();

        // Assert
        response.HttpStatusCode.Should().Be(StatusCodes.Status404NotFound);
        response.Content.Should().BeNull();
        
        await researchRepository.Received().GetAllAsync();
    }
    
    [Theory, AutoNSubstituteData]
    public async Task Get_All_Error(
        [Frozen] IBaseRepository<Research> researchRepository,
        [Greedy] ResearchService sut
    )
    {
        // Arrange
        var exception = _fixture.Create<Exception>();
        researchRepository.GetAllAsync().ThrowsAsyncForAnyArgs(exception);
        
        // Act
        var response = await sut.GetAsync();
        
        // Assert
        response.HttpStatusCode.Should().Be(StatusCodes.Status500InternalServerError);
        response.Content.Should().BeNull();
        
        await researchRepository.Received().GetAllAsync();
    }
    #endregion
}