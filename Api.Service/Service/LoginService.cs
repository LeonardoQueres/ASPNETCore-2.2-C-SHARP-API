using Api.Domain.DTO;
using Api.Domain.Entities;
using Api.Domain.Interfaces.Repository;
using Api.Domain.Interfaces.Service;
using Api.Domain.Security;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;

namespace Api.Service.Service
{
    public class LoginService : ILoginService
    {
        private readonly IUserRepository repository;
        private readonly SigningConfigurations signingConfigurations;
        private readonly TokenConfigurations tokenConfigurations;

        private IConfiguration configuration { get; set; }

        public LoginService(IUserRepository repository,
                SigningConfigurations signingConfigurations,
                TokenConfigurations tokenConfigurations,
                IConfiguration configuration)
        {
            this.repository = repository;
            this.signingConfigurations = signingConfigurations;
            this.tokenConfigurations = tokenConfigurations;
            this.configuration = configuration;
        }

        public async Task<object> FindByLogin(LoginDTO user)
        {
            var baseUser = new UserEntity();

            if (user == null || string.IsNullOrWhiteSpace(user.Email))
            {
                return new
                {
                    authenticated = false,
                    message = "Falha ao autenticar."
                };
            }

            baseUser = await repository.FindByLogin(user.Email);

            if (baseUser == null)
            {
                return new
                {
                    authenticated = false,
                    message = "Falha ao autenticar."
                };
            }

            var identity = new ClaimsIdentity(
                new GenericIdentity(baseUser.Email),
                new[]
                {
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.UniqueName, baseUser.Email)
                }
            );

            var createDate = DateTime.Now;
            var expirationDate = createDate + TimeSpan.FromSeconds(tokenConfigurations.Seconds);

            var handler = new JwtSecurityTokenHandler();
            var token = CreateToken(identity, createDate, expirationDate, handler);
            return SucessObject(createDate, expirationDate, token, user);
        }

        private object SucessObject(DateTime createDate, DateTime expirationDate, string token, LoginDTO user)
        {
            return new
            {
                authenticated = true,
                created = createDate.ToString("yyyy-MM-dd HH:mm:ss"),
                expiration = expirationDate.ToString("yyyy-MM-dd HH:mm:ss"),
                acessToken = token,
                userName = user.Email,
                message = "Usuário Logado com sucesso."
            };
        }

        private string CreateToken(ClaimsIdentity identity, DateTime createDate, DateTime expirationDate, JwtSecurityTokenHandler handler)
        {
            var securityToken = handler.CreateToken(new SecurityTokenDescriptor
            {
                Issuer = tokenConfigurations.Issuer,
                Audience = tokenConfigurations.Audience,
                SigningCredentials = signingConfigurations.SigningCredentials,
                Subject = identity,
                NotBefore = createDate,
                Expires = expirationDate
            });

            var token = handler.WriteToken(securityToken);
            return token;
        }
    } 
}
