using System.Threading.Tasks;
using Atc.Test;
using AutoFixture.Xunit2;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using ResearchesUFU.API.Controllers;
using ResearchesUFU.API.Models.DTO.Responses;
using ResearchesUFU.API.Services.Interfaces;
using ResearchesUFU.API.Utils;
using Xunit;

namespace ResearchesUFU.UnitTests.Tests.Controllers;

public class ResearchesControllerTests
{
    private const int RESEARCH_ID = 1;
        
    [Theory, AutoNSubstituteData]
    public async Task Get_AnyId_Success(
        [Frozen] IResearchService researchService,
        [Greedy] ResearchesController researchesController
    )
    {
        // Arrange
        var expectedServiceResponse = HttpUtils<ResearchResponseDTO>.GenerateHttpSuccessResponse();

        researchService.GetAsync(Arg.Any<int>())
            .ReturnsForAnyArgs(expectedServiceResponse);

        // Act
        var controllerResponse = await researchesController.Get(RESEARCH_ID);

        // Assert
        controllerResponse.Should().BeAssignableTo<OkObjectResult>();
    }
    
    [Theory, AutoNSubstituteData]
    public async Task Get_AnyId_NotFound(
        [Frozen] IResearchService researchService,
        [Greedy] ResearchesController researchesController
    )
    { 
        // Arrange
        var expectedServiceResponse = HttpUtils<ResearchResponseDTO>.GenerateHttpResponse(StatusCodes.Status404NotFound);

        researchService.GetAsync(Arg.Any<int>())
            .Returns(expectedServiceResponse);

        // Act
        var controllerResponse = await researchesController.Get(RESEARCH_ID);

        // Assert
        controllerResponse.Should().BeAssignableTo<NotFoundResult>();
    }
}