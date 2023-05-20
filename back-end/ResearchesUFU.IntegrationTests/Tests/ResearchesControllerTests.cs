using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using ResearchesUFU.API.Models.DTO.Requests;
using ResearchesUFU.API.Models.DTO.Responses;
using Xunit;

namespace ResearchesUFU.IntegrationTests;

public class ResearchesControllerTests : IntegrationTestBase
{
    private const string route = "api/Researches/";
    
    [Fact]
    public async Task Get_OneById_Success()
    {
        // Arrange
        var request = GenerateNewResearchRequest();

        // Act
        var postResponse = await TestClient.PostAsJsonAsync(route, request); // creating new research for test
        var postResponseStatus = postResponse.StatusCode;
        Assert.Equal(HttpStatusCode.OK, postResponseStatus);
        var postResponseContent = await postResponse.Content.ReadAsAsync<ResearchResponseDTO>();

        var researchId = postResponseContent.Id;
        
        var getResponse = await TestClient.GetAsync(route + researchId);
        var getResponseStatus = getResponse.StatusCode;
        var getResponseContent = await getResponse.Content.ReadAsAsync<ResearchResponseDTO>();

        // Assert
        getResponseStatus.Should().Be(HttpStatusCode.OK);
        getResponseContent.Should().BeAssignableTo<ResearchResponseDTO>();
        getResponseContent.Should().NotBeNull();
        getResponseContent.Id.Should().Be(researchId);
        getResponseContent.Title.Should().Be(request.Title);
    }

    [Fact]
    public async Task Get_AllResearches_Success()
    {
        // Act
        var response = await TestClient.GetAsync(route);
        var responseStatus = response.StatusCode;
        var responseContent = await response.Content.ReadAsAsync<List<ResearchResponseDTO>>();
        
        // Assert
        responseStatus.Should().Be(HttpStatusCode.OK);
        responseContent.Should().BeAssignableTo<List<ResearchResponseDTO>>();
        responseContent.Should().NotBeNullOrEmpty();
    }
    
    [Fact]
    public async Task Post_NewResearch_Success()
    {
        // Arrange
        var request = GenerateNewResearchRequest();

        // Act
        var response = await TestClient.PostAsJsonAsync(route, request);
        var responseStatus = response.StatusCode;
        var responseContent = await response.Content.ReadAsAsync<ResearchResponseDTO>();

        // Assert
        responseStatus.Should().Be(HttpStatusCode.OK);
        responseContent.Should().BeAssignableTo<ResearchResponseDTO>();
        responseContent.Should().NotBeNull();
        responseContent.Title.Should().Be(request.Title);
    }
    
    [Fact]
    public async Task Put_UpdateOne_Success()
    {
        // Arrange
        var request = GenerateNewResearchRequest();

        // Act
        var postResponse = await TestClient.PostAsJsonAsync(route, request); // creating new research for test
        var postResponseStatus = postResponse.StatusCode;
        Assert.Equal(HttpStatusCode.OK, postResponseStatus);
        var postResponseContent = await postResponse.Content.ReadAsAsync<ResearchResponseDTO>();

        var researchId = postResponseContent.Id;
        var updatedRequest = request;
        updatedRequest.Title = "Updated Title";
        updatedRequest.Summary = "New Summary";
        updatedRequest.Tags.Add(new ResearchTagRequestDto
        {
            Tag = new TagRequestDTO()
            {
                Id = 2
            }
        });
        
        var putResponse = await TestClient.PutAsJsonAsync(route + researchId, request);
        var putResponseStatus = putResponse.StatusCode;
        var putResponseContent = await putResponse.Content.ReadAsAsync<ResearchResponseDTO>();

        // Assert
        putResponseStatus.Should().Be(HttpStatusCode.OK);
        putResponseContent.Should().BeAssignableTo<ResearchResponseDTO>();
        putResponseContent.Should().NotBeNull();
        putResponseContent.Id.Should().Be(researchId);
        putResponseContent.Title.Should().Be(updatedRequest.Title);
        putResponseContent.Summary.Should().Be(updatedRequest.Summary);
        putResponseContent.Tags.Should().BeEquivalentTo(updatedRequest.Tags);
    }
    
    [Fact]
    public async Task Delete_NewResearch_Success()
    {
        // Arrange
        var request = GenerateNewResearchRequest();

        // Act
        var postResponse = await TestClient.PostAsJsonAsync(route, request); // creating new research for test
        var postResponseStatus = postResponse.StatusCode;
        Assert.Equal(HttpStatusCode.OK, postResponseStatus);
        var postResponseContent = await postResponse.Content.ReadAsAsync<ResearchResponseDTO>();

        var researchId = postResponseContent.Id;
        
        var deleteResponse = await TestClient.DeleteAsync(route + researchId);
        var deleteResponseStatus = deleteResponse.StatusCode;

        // Assert
        deleteResponseStatus.Should().Be(HttpStatusCode.OK);
    }

    private ResearchRequestDTO GenerateNewResearchRequest()
    {
        var research = new ResearchRequestDTO
        {
            Title = "Integration test research",
            Fields = new List<ResearchFieldRequestDto>
            {
                new()
                {
                    Field = new FieldRequestDTO
                    {
                        Id = 1
                    }
                }
            },
            Tags = new List<ResearchTagRequestDto>
            {
                new()
                {
                    Tag = new TagRequestDTO()
                    {
                        Id = 1
                    }
                }
            },
            Authors = new List<ResearchAuthorRequestDto>
            {
                new()
                {
                    Author = new AuthorRequestDTO()
                    {
                        Id = 1
                    }
                }
            }
        };

        return research;
    }
}