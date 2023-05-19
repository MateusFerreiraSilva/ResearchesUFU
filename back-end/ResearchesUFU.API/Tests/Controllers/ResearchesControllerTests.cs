using Atc.Test;
using AutoFixture.Xunit2;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Xunit;
using NSubstitute;
using ResearchesUFU.API.Controllers;
using ResearchesUFU.API.Models.DTO.Responses;
using ResearchesUFU.API.Services.Interfaces;
using ResearchesUFU.API.Utils;

namespace ResearchesUFU.API.Tests.Controllers;

public class ResearchesControllerTests
{
    private const int RESEARCH_ID = 1;
        
    [Theory, AutoNSubstituteData]
    public async Task Get_AnyId_Success(
        [Frozen] IResearchService researchService,
        [Greedy] ResearchesController researchesController
    )
    {
        #region Arrange

        var expectedServiceResponse = HttpUtils<ResearchResponseDTO>.GenerateHttpSuccessResponse();

        researchService.GetAsync(Arg.Any<int>())
            .ReturnsForAnyArgs(expectedServiceResponse);
        
        #endregion

        #region Act

        var controllerResponse = await researchesController.Get(RESEARCH_ID);

        #endregion

        #region Assert

        controllerResponse.Should().BeAssignableTo<OkObjectResult>();

        #endregion
    }
    
    [Theory, AutoNSubstituteData]
    public async Task Get_AnyId_NotFound(
        [Frozen] IResearchService researchService,
        [Greedy] ResearchesController researchesController
    )
    {
        #region Arrange

        var expectedServiceResponse = HttpUtils<ResearchResponseDTO>.GenerateHttpResponse(StatusCodes.Status404NotFound);

        researchService.GetAsync(Arg.Any<int>())
            .Returns(expectedServiceResponse);
        
        #endregion

        #region Act

        var controllerResponse = await researchesController.Get(RESEARCH_ID);

        #endregion

        #region Assert

        controllerResponse.Should().BeAssignableTo<NotFoundResult>();

        #endregion
    }
}