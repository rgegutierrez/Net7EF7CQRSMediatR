﻿using MediatR;
using MediatrExample.ApplicationCore.Common.Exceptions;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MediatrExample.ApplicationCore.Features.Auth;
public class TokenCommand : IRequest<TokenCommandResponse>
{
    public string UserName { get; set; } = default!;
    public string Password { get; set; } = default!;

}


public class TokenCommandHandler : IRequestHandler<TokenCommand, TokenCommandResponse>
{
    private readonly IConfiguration _config;

    public TokenCommandHandler(IConfiguration config)
    {
        _config = config;
    }

    public async Task<TokenCommandResponse> Handle(TokenCommand request, CancellationToken cancellationToken)
    {
        // Generamos un token según los claims
        var claims = new List<Claim> { };
        //var claims = new List<Claim>
        //{
        //    new Claim(ClaimTypes.Sid, user.Id),
        //    new Claim(ClaimTypes.Name, user.UserName)
        //};

        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);
        var tokenDescriptor = new JwtSecurityToken(
            issuer: _config["Jwt:Issuer"],
            audience: _config["Jwt:Audience"],
            claims: claims,
            expires: DateTime.Now.AddMinutes(720),
            signingCredentials: credentials);

        var jwt = new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);

        return new TokenCommandResponse
        {
            AccessToken = jwt
        };
    }
}

public class TokenCommandResponse
{
    public string AccessToken { get; set; } = default!;
}