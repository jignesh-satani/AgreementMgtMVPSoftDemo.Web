using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace AgreementMgtMVPSoftDemo.API
{
     public static class JwtMiddleware
     {
		  public static bool ValidateCurrentToken(string token)
		  {
			   var mySecret = "JSPDv7DrqznYL6nv7DrqzjnQSoftYO9JxIsWdcjnQYL6nu0fMVP";
			   var mySecurityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(mySecret));

			   //var myIssuer = "http://mysite.com";
			   //var myAudience = "http://myaudience.com";

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
