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
using ResearchesUFU.API.Models.DTO.Requests;
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
        var mappedResponse = _fixture.Create<ResearchResponseDto>();
        
        researchRepository.GetOneAsync(Arg.Any<int>()).ReturnsForAnyArgs(repositoryResponse);
        mapper.Map<ResearchResponseDto>(Arg.Any<Research>()).ReturnsForAnyArgs(mappedResponse);

        // Act
        var response = await sut.GetAsync(requestId);

        // Assert
        response.HttpStatusCode.Should().Be(StatusCodes.Status200OK);
        response.Content.Should().Be(mappedResponse);
        
        await researchRepository.Received().GetOneAsync(requestId);
        mapper.Received().Map<ResearchResponseDto>(repositoryResponse);
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
        var mappedResponse = _fixture.Create<ResearchResponseDto>();
        
        researchRepository.GetAllAsync().ReturnsForAnyArgs(repositoryResponse);
        mapper.Map<ResearchResponseDto>(Arg.Any<Research>()).ReturnsForAnyArgs(mappedResponse);

        // Act
        var response = await sut.GetAsync();

        // Assert
        response.HttpStatusCode.Should().Be(StatusCodes.Status200OK);
        response.Content.Should().NotBeNull();
        response.Content.Should().AllBeAssignableTo<ResearchResponseDto>();

        await researchRepository.Received().GetAllAsync();
        repositoryResponse.ToList().ForEach(r => mapper.Received().Map<ResearchResponseDto>(r));
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
        researchRepository.GetAllAsync().ThrowsAsync(exception);
        
        // Act
        var response = await sut.GetAsync();
        
        // Assert
        response.HttpStatusCode.Should().Be(StatusCodes.Status500InternalServerError);
        response.Content.Should().BeNull();
        
        await researchRepository.Received().GetAllAsync();
    }
    #endregion

    #region Post Tests
    [Theory, AutoNSubstituteData]
    public async Task Post_NewResearch_Success(
        [Frozen] IBaseRepository<Research> researchRepository,
        [Frozen] IMapper mapper,
        [Greedy] ResearchService sut
    )
    {
        // Arrange
        var newResearchRequestDto = _fixture.Create<ResearchRequestDto>();
        var newResearch = _fixture.Create<Research>();
        var newResearchResponseDto = _fixture.Create<ResearchResponseDto>();

        mapper.Map<Research>(Arg.Any<ResearchRequestDto>()).ReturnsForAnyArgs(newResearch);
        mapper.Map<ResearchResponseDto>(Arg.Any<Research>()).ReturnsForAnyArgs(newResearchResponseDto);

        // Act
        var response = await sut.PostAsync(newResearchRequestDto);

        // Assert
        response.HttpStatusCode.Should().Be(StatusCodes.Status200OK);
        response.Content.Should().Be(newResearchResponseDto);
        
        mapper.Received().Map<Research>(newResearchRequestDto);
        researchRepository.Received().Insert(newResearch);
        await researchRepository.Received().SaveAsync();
        mapper.Received().Map<ResearchResponseDto>(newResearch);
    }
    
    [Theory, AutoNSubstituteData]
    public async Task Post_NewResearch_Error(
        [Frozen] IBaseRepository<Research> researchRepository,
        [Frozen] IMapper mapper,
        [Greedy] ResearchService sut
    )
    {
        // Arrange
        var newResearchRequestDto = _fixture.Create<ResearchRequestDto>();
        var newResearch = _fixture.Create<Research>();
        var exception = _fixture.Create<Exception>();

        mapper.Map<Research>(Arg.Any<ResearchRequestDto>()).ReturnsForAnyArgs(newResearch);
        researchRepository.SaveAsync().ThrowsAsync(exception);

        // Act
        var response = await sut.PostAsync(newResearchRequestDto);

        // Assert
        response.HttpStatusCode.Should().Be(StatusCodes.Status500InternalServerError);
        response.Content.Should().BeNull();
        
        mapper.Received().Map<Research>(newResearchRequestDto);
        researchRepository.Received().Insert(newResearch);
        await researchRepository.Received().SaveAsync();
        mapper.DidNotReceiveWithAnyArgs().Map<ResearchResponseDto>(newResearch);
    }
    #endregion
    
    #region Post Tests
    [Theory, AutoNSubstituteData]
    public async Task Put_NewResearch_Success(
        [Frozen] IBaseRepository<Research> researchRepository,
        [Frozen] IMapper mapper,
        [Greedy] ResearchService sut
    )
    {
        // Arrange
        var newResearchId = _fixture.Create<int>();
        var newResearchRequestDto = _fixture.Create<ResearchRequestDto>();
        var newResearch = _fixture.Create<Research>();
        var newResearchResponseDto = _fixture.Create<ResearchResponseDto>();
        var oldResearch = _fixture.Create<Research>();

        researchRepository.GetOneAsync(Arg.Any<int>()).ReturnsForAnyArgs(oldResearch);

        mapper.Map<Research>(Arg.Any<ResearchRequestDto>()).ReturnsForAnyArgs(newResearch);
        mapper.Map<ResearchResponseDto>(Arg.Any<Research>()).ReturnsForAnyArgs(newResearchResponseDto);
        
        // Act
        var response = await sut.PutAsync(newResearchId, newResearchRequestDto);

        // Assert
        response.HttpStatusCode.Should().Be(StatusCodes.Status200OK);
        response.Content.Should().Be(newResearchResponseDto);
        
        await researchRepository.Received().GetOneAsync(newResearchId);
        mapper.Received().Map<Research>(newResearchRequestDto);
        researchRepository.Received().Update(oldResearch, newResearch);
        await researchRepository.Received().SaveAsync();
        mapper.Received().Map<ResearchResponseDto>(newResearch);
    }
    
    [Theory, AutoNSubstituteData]
    public async Task Put_NewResearch_NotFound(
        [Frozen] IBaseRepository<Research> researchRepository,
        [Frozen] IMapper mapper,
        [Greedy] ResearchService sut
    )
    {
        // Arrange
        var newResearchId = _fixture.Create<int>();
        var newResearchRequestDto = _fixture.Create<ResearchRequestDto>();

        researchRepository.GetOneAsync(Arg.Any<int>()).ReturnsNullForAnyArgs();

        // Act
        var response = await sut.PutAsync(newResearchId, newResearchRequestDto);

        // Assert
        response.HttpStatusCode.Should().Be(StatusCodes.Status404NotFound);
        response.Content.Should().BeNull();
        
        await researchRepository.Received().GetOneAsync(newResearchId);
        mapper.DidNotReceiveWithAnyArgs().Map<Research>(Arg.Any<ResearchRequestDto>());
        researchRepository.DidNotReceiveWithAnyArgs().Update(Arg.Any<Research>(), Arg.Any<Research>());
        await researchRepository.DidNotReceiveWithAnyArgs().SaveAsync();
        mapper.DidNotReceiveWithAnyArgs().Map<ResearchResponseDto>(Arg.Any<Research>());
    }
    
    [Theory, AutoNSubstituteData]
    public async Task Put_NewResearch_Error(
        [Frozen] IBaseRepository<Research> researchRepository,
        [Frozen] IMapper mapper,
        [Greedy] ResearchService sut
    )
    {
        // Arrange
        var newResearchId = _fixture.Create<int>();
        var newResearchRequestDto = _fixture.Create<ResearchRequestDto>();
        var newResearch = _fixture.Create<Research>();
        var newResearchResponseDto = _fixture.Create<ResearchResponseDto>();
        var oldResearch = _fixture.Create<Research>();
        var exception = _fixture.Create<Exception>();

        researchRepository.GetOneAsync(Arg.Any<int>()).ReturnsForAnyArgs(oldResearch);
        
        mapper.Map<Research>(Arg.Any<ResearchRequestDto>()).ReturnsForAnyArgs(newResearch);
        mapper.Map<ResearchResponseDto>(Arg.Any<Research>()).ReturnsForAnyArgs(newResearchResponseDto);

        researchRepository.SaveAsync().ThrowsAsync(exception);

        // Act
        var response = await sut.PutAsync(newResearchId, newResearchRequestDto);

        // Assert
        response.HttpStatusCode.Should().Be(StatusCodes.Status500InternalServerError);
        response.Content.Should().BeNull();
        
        await researchRepository.Received().GetOneAsync(newResearchId);
        mapper.Received().Map<Research>(newResearchRequestDto);
        researchRepository.Received().Update(oldResearch, newResearch);
        await researchRepository.Received().SaveAsync();
        mapper.DidNotReceiveWithAnyArgs().Map<ResearchResponseDto>(Arg.Any<Research>());
    }
    #endregion

    #region Delete Tests
    [Theory, AutoNSubstituteData]
    public async Task Delete_ById_Success(
        [Frozen] IBaseRepository<Research> researchRepository,
        [Greedy] ResearchService sut
    )
    {
        // Arrange
        var researchId = _fixture.Create<int>();
        var research = _fixture.Create<Research>();

        researchRepository.GetOneAsync(Arg.Any<int>()).ReturnsForAnyArgs(research);

        // Act
        var response = await sut.DeleteAsync(researchId);

        // Assert
        response.HttpStatusCode.Should().Be(StatusCodes.Status200OK);
        response.Content.Should().BeNull();
        
        await researchRepository.Received().GetOneAsync(researchId);
        researchRepository.Received().Delete(research);
        await researchRepository.Received().SaveAsync();
    }

    [Theory, AutoNSubstituteData]
    public async Task Delete_ById_NotFound(
        [Frozen] IBaseRepository<Research> researchRepository,
        [Greedy] ResearchService sut
    )
    {
        // Arrange
        var researchId = _fixture.Create<int>();

        researchRepository.GetOneAsync(Arg.Any<int>()).ReturnsNullForAnyArgs();

        // Act
        var response = await sut.DeleteAsync(researchId);

        // Assert
        response.HttpStatusCode.Should().Be(StatusCodes.Status404NotFound);
        response.Content.Should().BeNull();
        
        await researchRepository.Received().GetOneAsync(researchId);
        researchRepository.DidNotReceiveWithAnyArgs().Delete(Arg.Any<Research>());
        await researchRepository.DidNotReceive().SaveAsync();
    }
    
    [Theory, AutoNSubstituteData]
    public async Task Delete_ById_Error(
        [Frozen] IBaseRepository<Research> researchRepository,
        [Greedy] ResearchService sut
    )
    {
        // Arrange
        var researchId = _fixture.Create<int>();
        var research = _fixture.Create<Research>();
        var exception = _fixture.Create<Exception>();

        researchRepository.GetOneAsync(Arg.Any<int>()).ReturnsForAnyArgs(research);

        researchRepository.SaveAsync().ThrowsAsync(exception);

        // Act
        var response = await sut.DeleteAsync(researchId);

        // Assert
        response.HttpStatusCode.Should().Be(StatusCodes.Status500InternalServerError);
        response.Content.Should().BeNull();
        
        await researchRepository.Received().GetOneAsync(researchId);
        researchRepository.Received().Delete(research);
        await researchRepository.Received().SaveAsync();
    }
    #endregion
}