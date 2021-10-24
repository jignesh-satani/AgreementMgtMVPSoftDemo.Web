using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace AgreementMgtMVPSoftDemo.API
{
     public static class JwtMiddleware
     {
		  public static bool ValidateCurrentToken(string token)
		  {			  
			   var tokenHandler = new JwtSecurityTokenHandler();
			   try
			   {
					tokenHandler.ValidateToken(token, new TokenValidationParameters
					{
						 ValidateIssuerSigningKey = true,
						 ValidateIssuer = true,
						 ValidateAudience = true						 
					}, out SecurityToken validatedToken);
			   }
			   catch
			   {
					return false;
			   }
			   return true;
		  }
	 }
}
