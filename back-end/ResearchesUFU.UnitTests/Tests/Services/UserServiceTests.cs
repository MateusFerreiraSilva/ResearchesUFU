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
using ResearchesUFU.API.Repositories;
using ResearchesUFU.API.Repositories.Interfaces;
using ResearchesUFU.API.Services;
using Xunit;

namespace ResearchesUFU.UnitTests.Tests.Services;

public class UserServiceTests
{
    private readonly Fixture _fixture;

    private static readonly UserAuthenticationRequestDto UserAuthenticationRequestDto = new()
    {
        Email = "test@test.com",
        PasswordHash = GenerateValidPasswordHash()
    };
    

    public UserServiceTests()
    {
        _fixture = new Fixture();
        _fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList().ForEach(b => _fixture.Behaviors.Remove(b));
        _fixture.Behaviors.Add(new OmitOnRecursionBehavior());
    }

    [Theory, AutoNSubstituteData]
    public async Task AuthenticateUser_Success(
        [Frozen] IBaseRepository<User> userRepository,
        [Greedy] UserService sut
    )
    {
        // Arrange
        var repositoryResponse = new List<User>
        {
            new()
            {
                Email = UserAuthenticationRequestDto.Email,
                PasswordHash = UserAuthenticationRequestDto.PasswordHash
            }
        }.AsQueryable();
        
        userRepository.GetAllAsync().ReturnsForAnyArgs(repositoryResponse);

        // Act
        var response = await sut.AuthenticateUserAsync(UserAuthenticationRequestDto);

        // Assert
        response.HttpStatusCode.Should().Be(StatusCodes.Status200OK);
        response.Content.Should().NotBeNull();
        response.Content.Should().BeAssignableTo<UserAuthenticationResponseDto>();
        
        await userRepository.Received().GetAllAsync();
    }
    
    [Theory, AutoNSubstituteData]
    public async Task AuthenticateUser_BadRequest(
        [Frozen] IBaseRepository<User> userRepository,
        [Greedy] UserService sut
    )
    {
        // Arrange
        var invalidUserAuthenticationRequestDto = new UserAuthenticationRequestDto
        {
            Email = "test.test.com",
            PasswordHash = string.Empty
        };
        
        // Act
        var response = await sut.AuthenticateUserAsync(invalidUserAuthenticationRequestDto);

        // Assert
        response.HttpStatusCode.Should().Be(StatusCodes.Status400BadRequest);
        response.Content.Should().BeNull();

        await userRepository.DidNotReceive().GetAllAsync();
    }
    
    [Theory, AutoNSubstituteData]
    public async Task AuthenticateUser_NotFound(
        [Frozen] IBaseRepository<User> userRepository,
        [Greedy] UserService sut
    )
    {
        // Arrange
        var repositoryResponse = new List<User>
        {
            new()
            {
                Email = "another.email@test.com",
                PasswordHash = GenerateValidPasswordHash()
            }
        }.AsQueryable();
        
        userRepository.GetAllAsync().ReturnsForAnyArgs(repositoryResponse);

        // Act
        var response = await sut.AuthenticateUserAsync(UserAuthenticationRequestDto);

        // Assert
        response.HttpStatusCode.Should().Be(StatusCodes.Status404NotFound);
        response.Content.Should().BeNull();

        await userRepository.Received().GetAllAsync();
    }
    
    [Theory, AutoNSubstituteData]
    public async Task AuthenticateUser_InvalidPassword(
        [Frozen] IBaseRepository<User> userRepository,
        [Greedy] UserService sut
    )
    {
        // Arrange
        var repositoryResponse = new List<User>
        {
            new()
            {
                Email = UserAuthenticationRequestDto.Email,
                PasswordHash = GenerateValidPasswordHash() // different password
            }
        }.AsQueryable();
        
        var failureResponse = new UserAuthenticationResponseDto { UserId = default, Token = string.Empty };
        
        userRepository.GetAllAsync().ReturnsForAnyArgs(repositoryResponse);

        // Act
        var response = await sut.AuthenticateUserAsync(UserAuthenticationRequestDto);

        // Assert
        response.HttpStatusCode.Should().Be(StatusCodes.Status400BadRequest);
        response.Content.Should().BeEquivalentTo(failureResponse);

        await userRepository.Received().GetAllAsync();
    }
    
    [Theory, AutoNSubstituteData]
    public async Task AuthenticateUser_Error(
        [Frozen] IBaseRepository<User> userRepository,
        [Greedy] UserService sut
    )
    {
        // Arrange
        var exception = _fixture.Create<Exception>();
        
        userRepository.GetAllAsync().ThrowsAsync(exception);

        // Act
        var response = await sut.AuthenticateUserAsync(UserAuthenticationRequestDto);

        // Assert
        response.HttpStatusCode.Should().Be(StatusCodes.Status500InternalServerError);
        response.Content.Should().BeNull();

        await userRepository.Received().GetAllAsync();
    }

    private static string GenerateValidPasswordHash()
    {
        const int sha256StringLength = 64;
        var randomString = Guid.NewGuid().ToString() + Guid.NewGuid().ToString();

        return randomString.Substring(default, sha256StringLength);
    }
}