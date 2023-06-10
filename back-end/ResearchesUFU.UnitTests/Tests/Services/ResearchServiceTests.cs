using System;
using System.Linq;
using System.Threading.Tasks;
using Atc.Test;
using AutoFixture;
using AutoFixture.Xunit2;
using AutoMapper;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using NSubstitute.ReturnsExtensions;
using ResearchesUFU.API.Controllers;
using ResearchesUFU.API.Models;
using ResearchesUFU.API.Models.DTO.Responses;
using ResearchesUFU.API.Repositories.Interfaces;
using ResearchesUFU.API.Services;
using ResearchesUFU.API.Services.Interfaces;
using ResearchesUFU.API.Utils;
using Xunit;

namespace ResearchesUFU.UnitTests.Tests.Services;

public class ResearchServiceTest
{
    private Fixture _fixture;

    public ResearchServiceTest()
    {
        _fixture = new Fixture();
        _fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList().ForEach(b => _fixture.Behaviors.Remove(b));
        _fixture.Behaviors.Add(new OmitOnRecursionBehavior());
    }

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
        [Frozen] IMapper mapper,
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
        mapper.DidNotReceive().Map<ResearchResponseDTO>(Arg.Any<Research>());
    }
    
    // [Theory, AutoNSubstituteData]
    // public async Task Error(
    //     [Frozen] IBaseRepository<Research> researchRepository,
    //     [Frozen] IMapper mapper,
    //     [Greedy] ResearchService sut
    // )
    // {
    //     // Arrange
    //     var requestId = _fixture.Create<int>();
    //     var exception = _fixture.Create<Exception>();
    //     researchRepository.GetOneAsync(Arg.Any<int>()).ThrowsAsyncForAnyArgs(exception);
    //     
    //     // Act
    //     var response = await sut.GetAsync(requestId);
    //     
    //     // Assert
    //     response.HttpStatusCode.Should().Be(StatusCodes.Status404NotFound);
    //     response.Content.Should().BeNull();
    //     
    //     await researchRepository.Received().GetOneAsync(requestId);
    //     mapper.DidNotReceive().Map<ResearchResponseDTO>(Arg.Any<Research>());
    // }
}