using LogiTech.Services.Identity.Domain.Entities;

namespace LogiTech.Services.Identity.Application.Common.Interfaces;

public interface IJwtTokenGenerator
{
    string GenerateAccessToken(User user);
    string GenerateRefreshToken();
}