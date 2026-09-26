using LogiTech.BuildingBlocks.Results;

namespace LogiTech.Services.Identity.Application.Authentication.Commands.Register;

public record RegisterCommand(
    string FirstName,
    string LastName,
    string Email,
    string Password
);

public record AuthenticationResult(
    Guid UserId,
    string FirstName,
    string LastName,
    string Email,
    string Token,
    string RefreshToken
);