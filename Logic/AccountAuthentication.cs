
using System;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using Buggie.DataProperties;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Buggie.Interface;
using System.Threading.Tasks;
using System.Security.Claims;
using System.Linq;

namespace Buggie.Logic {

    public class AccountAuthentication : IAccountAuthentication {

    
    public async Task<string> GenerateJwtAccessToken(string infoToken)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var securityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes("j79n4hfrug5c9jk1u"));
        var credit =  new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);
            var claims = new[] 
        {
            new Claim("Token",infoToken),
        };
        var token = new SecurityTokenDescriptor
        {
            Issuer = "https://localhost:5501",
            Expires = DateTime.UtcNow.AddHours(10),
            SigningCredentials = credit,
            Subject = new ClaimsIdentity(claims)
        };

        var output = tokenHandler.CreateEncodedJwt(token);

        
        return output;
    }

      public async Task<string> GenerateJwtInfoToken(User user)
    {
        var tokenHanlder = new JwtSecurityTokenHandler();
        var securityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes("j79n4hfrug5c9jk1u"));
        var credit =  new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);
        var claims = new[] 
        {
            new Claim("UserId",user.UserId.ToString()),
            new Claim("CompanyId",user.CompanyId.ToString()),
            new Claim("Role",user.Role)
           
        };
        var token = new SecurityTokenDescriptor
        {
            Issuer = "https://localhost:5501",
            Subject = new ClaimsIdentity(claims),
            SigningCredentials = credit
        };

        var output = tokenHanlder.CreateEncodedJwt(token);

        return output;
    }

    public User ReadJwtAccessToken(string token)
    {
        var handler = new JwtSecurityTokenHandler();
        var JwtST = handler.ReadJwtToken(token);
        User user = new User();
        user.Role = JwtST.Claims.First(c => c.Type == "Role").Value;
        return user;
    }
    public User ReadJwtInfoToken(string token)
    {
        var handler = new JwtSecurityTokenHandler();
        var JwtST = handler.ReadJwtToken(token);
        User user = new User();
        user.UserId = int.Parse(JwtST.Claims.First(c => c.Type == "UserId").Value);
        user.CompanyId =int.Parse(JwtST.Claims.First(c => c.Type == "CompanyId").Value);
        return user;
    }
    public string HashPassword(string password)
    {
        using (var sha256 = SHA256.Create())
        {   

            byte[] value;

            UTF8Encoding encoder = new UTF8Encoding();

            //String hashing
            value = sha256.ComputeHash(encoder.GetBytes(password));


            //putting the encoding into a readble string
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < value.Length; i++)
            {

                builder.Append(value[i].ToString("x2"));
            }
            return builder.ToString();
        }
    }

    public bool PasswordAuthentication(string password, string hashed)
    {

        var hash = HashPassword(password);

        if (hash == hashed)
        {
            return true;
        }
        return false;
    }

    public bool IsUserRoleValid(User user,string[] roles)
    {
       
       foreach(var role in roles)
       {
            if(role == user.Role) return true;

       }
       return false;
    }

    //Gets user from AccessToken token to reveal InfoToken
    public User ReadTokens(ClaimsIdentity identity)
    {
        try
        {
         var infoToken = identity.Claims.First(c => c.Type == "Token").Value;

         var tokenHandler =  new JwtSecurityTokenHandler();

         var readToken = tokenHandler.ReadJwtToken(infoToken);
      
         User user = new User()
         {
            Role = readToken.Claims.First(c => c.Type == "Role").Value,
            UserId = int.Parse(readToken.Claims.First(c => c.Type == "UserId").Value),
            CompanyId = int.Parse(readToken.Claims.First(c => c.Type == "CompanyId").Value)

         };
       
         return user;
        }catch(Exception e)
        {
            Console.WriteLine(e.Message);
        }

        return null;
    }
    }
}