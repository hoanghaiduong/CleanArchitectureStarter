using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Authentication;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MyWebApi.Application.Common.Models;
using MyWebApi.Domain.Entities;
using MyWebApi.Infrastructure.Identity;
using MyWebApi.Web.Services;
#nullable disable
namespace MyWebApi.Web.Endpoints
{
    public class TestEndpoints : EndpointGroupBase
    {
   

        public override void Map(WebApplication app)
        {
            app.MapGroup("Authenticator").MapGet("GetSharedKey", GetAuthenticatorKey);
            app.MapPost("Enable-2FA", Switch2FA);
            app.MapPost("Verify-AuthenticatorCode", VerifyAuthenticatorCode);
        }
        [Authorize]
        private async Task<IResult> VerifyAuthenticatorCode(UserManager<ApplicationUser> userManager, ClaimsPrincipal principal, [FromQuery] string verificationCode)
        {
            try
            {
                var (result, user) = await GetUserAsync(userManager, principal);
                var is2faTokenValid = await userManager.VerifyTwoFactorTokenAsync(user, userManager.Options.Tokens.AuthenticatorTokenProvider, verificationCode);
                if (!is2faTokenValid)
                {
                    return Results.BadRequest("Authenticator token is not valid");
                }
                IEnumerable<string> recoveryCodes = [];
                if (await userManager.CountRecoveryCodesAsync(user) == 0)
                {
                    recoveryCodes = await userManager.GenerateNewTwoFactorRecoveryCodesAsync(user, 10);
                }
                return Results.Ok(new
                {
                    Message = "Authenticator code verified successfully.",
                    RecoveryCodes = recoveryCodes,
                    User = user
                });
            }

            catch (System.Exception ex)
            {
                return Results.BadRequest(ex.Message);
            }

        }
        private async Task<(IResult, ApplicationUser)> GetUserAsync(UserManager<ApplicationUser> _userManager, ClaimsPrincipal claimsPrincipal)
        {
            var userId = claimsPrincipal.FindFirstValue("UserID");
            if (string.IsNullOrEmpty(userId))
            {
                return (Results.BadRequest("User ID not found in Token."), null);
            }

            var user = await _userManager.FindByIdAsync(userId);
            return user == null ? (Results.NotFound("User not found."), null) : (Results.Ok(), user);
        }
        private const string AuthenticatorUriFormat = "otpauth://totp/{0}:{1}?secret={2}&issuer={0}&digits=6";

        [Authorize]
        public async Task<IResult> Switch2FA(UserManager<ApplicationUser> _userManager, ClaimsPrincipal claimsPrincipal, [FromQuery] bool enableF2a)
        {
            try
            {
                var (result, user) = await GetUserAsync(_userManager, claimsPrincipal);

                await _userManager.SetTwoFactorEnabledAsync(user, enableF2a);
                var twoFactorResult = await _userManager.SetTwoFactorEnabledAsync(user, enableF2a);
                return !twoFactorResult.Succeeded
                    ? Results.BadRequest("Failed to switch Two-Factor Authentication.")
                    : Results.Ok(new
                    {
                        Message = enableF2a ? "Two-Factor Authentication enabled." : "Two-Factor Authentication disabled.",
                        user
                    });
            }
            catch (Exception ex)
            {
                return Results.BadRequest(ex.Message);
            }
        }


        [Authorize]
        private async Task<IResult> GetAuthenticatorKey(UserManager<ApplicationUser> _userManager, ClaimsPrincipal claimsPrincipal, UrlEncoder urlEncoder)
        {
            var userId = claimsPrincipal.Claims.First(x => x.Type == "UserID").Value;
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return Results.NotFound();
            }
            var unformattedKey = await _userManager.GetAuthenticatorKeyAsync(user!);
            if (string.IsNullOrEmpty(unformattedKey))
            {
                await _userManager.ResetAuthenticatorKeyAsync(user!);
                unformattedKey = await _userManager.GetAuthenticatorKeyAsync(user!);
            }
            var formatKey = FormatKey(unformattedKey!);
            var qrcode = GenerateQrCodeUri(user.Email!, unformattedKey!, urlEncoder);
            return Results.Ok(new { user, formatKey, qrcode });
        }
        private string GenerateQrCodeUri(string email, string unformattedKey, UrlEncoder urlEncoder)
        {
            return string.Format(
                CultureInfo.InvariantCulture,
                AuthenticatorUriFormat,
                urlEncoder.Encode("Microsoft.AspNetCore.Identity.UI"),
                urlEncoder.Encode(email),
                unformattedKey);
        }
        private string FormatKey(string unformattedKey)
        {
            var result = new StringBuilder();
            int currentPosition = 0;
            while (currentPosition + 4 < unformattedKey.Length)
            {
                result.Append(unformattedKey.AsSpan(currentPosition, 4)).Append(' ');
                currentPosition += 4;
            }
            if (currentPosition < unformattedKey.Length)
            {
                result.Append(unformattedKey.AsSpan(currentPosition));
            }

            return result.ToString().ToLowerInvariant();
        }
    }
}
