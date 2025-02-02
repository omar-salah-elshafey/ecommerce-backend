﻿using Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Security.Cryptography;

namespace Application.Features.TokenManagement.GenerateRefreshToken
{
    public class GenerateRefreshTokenCommandHandler(ILogger<GenerateRefreshTokenCommandHandler> _logger)
        : IRequestHandler<GenerateRefreshTokenCommand, RefreshToken>
    {
        public async Task<RefreshToken> Handle(GenerateRefreshTokenCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Generating a new refresh token...");
            var randomNumber = new byte[32];
            using var generator = new RNGCryptoServiceProvider();
            generator.GetBytes(randomNumber);

            var refreshToken = new RefreshToken
            {
                CreatedOn = DateTime.UtcNow,
                ExpiresOn = DateTime.UtcNow.AddDays(1),
                Token = Convert.ToBase64String(randomNumber)
            };

            _logger.LogInformation("Refresh token generated successfully. Expires on: {ExpiresOn}", refreshToken.ExpiresOn);
            return refreshToken;
        }
    }
}
