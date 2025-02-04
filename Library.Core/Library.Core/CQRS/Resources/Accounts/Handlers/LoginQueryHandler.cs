﻿using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using Library.Core.Context;
using Library.Core.CQRS.Abstraction.Queries;
using Library.Core.CQRS.Resources.Accounts.Queries;
using Library.Core.Models.ViewModels.AccountsViewModel;
using Library.Core.Exceptions.Accounts;

namespace Library.Core.CQRS.Resources.Accounts.Handlers
{
    public class LoginQueryHandler : IQueryHandler<LoginQuery, string>
    {
        private readonly IDataBaseContext context;
        private readonly IPasswordHasher<Entities.User> hasher;
        private readonly AuthenticationSettings authenticationSettings;
        public LoginQueryHandler(IDataBaseContext context,
            IPasswordHasher<Entities.User> hasher,
            AuthenticationSettings authenticationSettings)
        {
            this.context = context;
            this.hasher = hasher;
            this.authenticationSettings = authenticationSettings;
        }

        public string Handle(LoginQuery query) 
        {
            var model = new LoginViewModel()
            {
                UUserName = query.Username,
                UPassword = query.Password,
            };

            var token = GenerateJWTToken(model);

            return JsonSerializer.Serialize(token);
        }

        public string GenerateJWTToken(LoginViewModel model)
        {
            var user = context.AllUsers.FirstOrDefault(u => u.UUserName == model.UUserName);

            if (user == null)
                throw new LoginOrUserNotFoundExceptions("Podany login lub hasło jest błędne!");

            var result = hasher.VerifyHashedPassword(user, user.UPassword, model.UPassword);

            if (result == PasswordVerificationResult.Failed)
                throw new LoginOrUserNotFoundExceptions("Podany login lub hasło jest błędne!");

            var userRole = context.Roles.FirstOrDefault(x => x.RID == user.URID)?.RName ?? "User";

            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, user.UGID.ToString()),
                new Claim(ClaimTypes.Name, $"{user.UFirstName} {user.ULastName}"),
                new Claim(ClaimTypes.Role, $"{userRole}"),
                new Claim("UID", user.UID.ToString()),
                new Claim("UGID", user.UGID.ToString()),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authenticationSettings.JWTKey));
            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddDays(authenticationSettings.JWTExpiredDays);

            var token = new JwtSecurityToken(authenticationSettings.JWTIssuer, authenticationSettings.JWTIssuer, claims, expires: expires, signingCredentials: cred);

            var tokenHandler = new JwtSecurityTokenHandler();
            return tokenHandler.WriteToken(token);
        }
    }
}