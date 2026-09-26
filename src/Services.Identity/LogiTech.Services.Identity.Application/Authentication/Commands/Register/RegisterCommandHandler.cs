using System;
using System.Threading;
using System.Threading.Tasks;
using LogiTech.BuildingBlocks.Results;
using LogiTech.Services.Identity.Application.Authentication.Common;
using LogiTech.Services.Identity.Application.Common.Interfaces;
using LogiTech.Services.Identity.Domain.Entities;

namespace LogiTech.Services.Identity.Application.Authentication.Commands.Register;

public class RegisterCommandHandler
{
    private readonly IPasswordHasher _passwordHasher;
    private readonly IJwtTokenGenerator _jwtTokenGenerator;

    public RegisterCommandHandler(IPasswordHasher passwordHasher, IJwtTokenGenerator jwtTokenGenerator)
    {
        _passwordHasher = passwordHasher;
        _jwtTokenGenerator = jwtTokenGenerator;
    }

    public async Task<Result<AuthenticationResult>> Handle(RegisterCommand command, CancellationToken cancellationToken = default)
    {
        var hashedPassword = _passwordHasher.HashPassword(command.Password);
        
        var user = new User(
            Guid.NewGuid(),
            command.FirstName,
            command.LastName,
            command.Email,
            hashedPassword,
            string.Empty,
            UserRole.Customer
        );
        
        var token = _jwtTokenGenerator.GenerateAccessToken(user);
        var refreshToken = _jwtTokenGenerator.GenerateRefreshToken();

        var result = new AuthenticationResult(
            user.Id,
            user.FirstName,
            user.LastName,
            user.Email,
            token,
            refreshToken
        );

        return Result<AuthenticationResult>.Success(result);
    }
}
