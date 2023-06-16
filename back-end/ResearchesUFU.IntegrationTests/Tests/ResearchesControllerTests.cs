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
    public async Task Post_NewResearch_Success()
    {
        // Act
        var response = await CreateNewResearchAsync();
    
        // Assert
        response.Should().BeAssignableTo<ResearchResponseDto>();
        response.Should().NotBeNull();
    }
    
    [Fact]
    public async Task Get_OneById_Success()
    {
        // Arrange
        var newResearch = await CreateNewResearchAsync();

        // Act
        var researchId = newResearch.Id;
        
        var response = await TestClient.GetAsync(route + researchId);
        var responseStatus = response.StatusCode;
        var responseContent = await response.Content.ReadAsAsync<ResearchResponseDto>();

        // Assert
        responseStatus.Should().Be(HttpStatusCode.OK);
        responseContent.Should().BeAssignableTo<ResearchResponseDto>();
        responseContent.Should().NotBeNull();
        responseContent.Should().BeEquivalentTo(newResearch);
    }
    
    [Fact]
    public async Task Put_UpdateOne_Success()
    {
        // Arrange
        var newResearch = await CreateNewResearchAsync();
        
        var updatedRequest = new ResearchRequestDto
        {
            Title = "Updated Title",
            Summary = "Updated Summary",
            Fields = new List<ResearchFieldRequestDto>
            {
                new()
                {
                    Field = new FieldRequestDto
                    {
                        Id = 2
                    }
                }
            },
            Tags = new List<ResearchTagRequestDto>
            {
                new()
                {
                    Tag = new TagRequestDto()
                    {
                        Id = 1
                    },
                },
                new()
                {
                    Tag = new TagRequestDto()
                    {
                        Id = 2
                    },
                }
            },
            Authors = new List<ResearchAuthorRequestDto>
            {
                new()
                {
                    Author = new AuthorRequestDto()
                    {
                        Id = 1
                    }
                }
            }
        };
    
        // Act
        var researchId = newResearch.Id;

        var putResponse = await TestClient.PutAsJsonAsync(route + researchId, updatedRequest);
        var putResponseStatus = putResponse.StatusCode;
        var putResponseContent = await putResponse.Content.ReadAsAsync<ResearchResponseDto>();
    
        // Assert
        putResponseStatus.Should().Be(HttpStatusCode.OK);
        putResponseContent.Should().BeAssignableTo<ResearchResponseDto>();
        putResponseContent.Should().NotBeNull();
        putResponseContent.Id.Should().Be(researchId);
        putResponseContent.Title.Should().Be(updatedRequest.Title);
        putResponseContent.Summary.Should().Be(updatedRequest.Summary);
        putResponseContent.Fields.Should().BeEquivalentTo(updatedRequest.Fields);
        putResponseContent.Tags.Should().BeEquivalentTo(updatedRequest.Tags);
    }
    
    [Fact]
    public async Task Delete_NewResearch_Success()
    {
        // Arrange
        var newResearch = await CreateNewResearchAsync();
    
        // Act
        var researchId = newResearch.Id;
        
        var deleteResponse = await TestClient.DeleteAsync(route + researchId);
        var deleteResponseStatus = deleteResponse.StatusCode;
    
        // Assert
        deleteResponseStatus.Should().Be(HttpStatusCode.OK);
    }
    
    [Fact]
    public async Task Get_AllResearches_Success()
    {
        // Act
        var response = await TestClient.GetAsync(route);
        var responseStatus = response.StatusCode;
        var responseContent = await response.Content.ReadAsAsync<List<ResearchResponseDto>>();
        
        // Assert
        responseStatus.Should().Be(HttpStatusCode.OK);
        responseContent.Should().BeAssignableTo<List<ResearchResponseDto>>();
        responseContent.Should().NotBeNullOrEmpty();
    }

    private async Task<ResearchResponseDto> CreateNewResearchAsync()
    {
        var genericResearch = new ResearchRequestDto
        {
            Title = "Integration test research",
            Fields = new List<ResearchFieldRequestDto>
            {
                new()
                {
                    Field = new FieldRequestDto
                    {
                        Id = 1
                    }
                }
            },
            Tags = new List<ResearchTagRequestDto>
            {
                new()
                {
                    Tag = new TagRequestDto()
                    {
                        Id = 1
                    }
                }
            },
            Authors = new List<ResearchAuthorRequestDto>
            {
                new()
                {
                    Author = new AuthorRequestDto()
                    {
                        Id = 1
                    }
                }
            }
        };
        
        var response = await TestClient.PostAsJsonAsync(route, genericResearch);
        var responseStatus = response.StatusCode;
        Assert.Equal(HttpStatusCode.OK, responseStatus);
        var responseContent = await response.Content.ReadAsAsync<ResearchResponseDto>();
        Assert.NotNull(responseContent);

        return responseContent;
    }
}