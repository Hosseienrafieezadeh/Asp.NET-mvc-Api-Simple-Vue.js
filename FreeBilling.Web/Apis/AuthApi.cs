using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using FreeBilling.Web.Data.Entitis;
using FreeBilling.Web.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace FreeBilling.Web.Apis
{
    public static class AuthApi
    {
        public static void Register(WebApplication app)
        {
            app.MapPost("/api/auth/token", async (HttpContext context, UserManager<TimeBillUsers> userManager, SignInManager<TimeBillUsers> signInManager, IConfiguration configuration) =>
            {
                var model = await context.Request.ReadFromJsonAsync<TokenModel>();

                if (model != null)
                {
                    var user = await userManager.FindByEmailAsync(model.UserName);
                    if (user != null)
                    {
                        var signInResult = await signInManager.CheckPasswordSignInAsync(user, model.Password, false);
                        if (signInResult.Succeeded)
                        {
                            var tokenKey = configuration["Token:Key"];
                            var issuer = configuration["Token:Issuer"];
                            var audience = configuration["Token:Audience"];

                            if (!string.IsNullOrEmpty(tokenKey) && !string.IsNullOrEmpty(issuer) && !string.IsNullOrEmpty(audience))
                            {
                                var claims = new[]
                                {
                                    new Claim(JwtRegisteredClaimNames.Email, user.Email),
                                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                                    new Claim(JwtRegisteredClaimNames.UniqueName, user.UserName)
                                };

                                var signinKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenKey));
                                var creds = new SigningCredentials(signinKey, SecurityAlgorithms.HmacSha256);

                                var token = new JwtSecurityToken(issuer, audience,
                                    claims: claims,
                                    expires: DateTime.UtcNow.AddMinutes(20),
                                    signingCredentials: creds);

                                var tokenString = new JwtSecurityTokenHandler().WriteToken(token);
                                await context.Response.WriteAsync(tokenString);
                                return;
                            }
                        }
                    }
                }

                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                await context.Response.WriteAsync("Bad username or password");
            });
        }
    }
}
