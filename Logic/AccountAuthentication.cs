
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

    
    public async Task<string> GenerateJwtAccessToken(User user)
    {
        var tokenHanlder = new JwtSecurityTokenHandler();
        var securityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes("j79n4hfrug5c9jk1u"));
        var credit =  new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);

        var token = new SecurityTokenDescriptor
        {
            Issuer = "",
            Expires = DateTime.UtcNow.AddHours(10),
            SigningCredentials = credit
        };

        var output = tokenHanlder.CreateEncodedJwt(token);

        return output;
    }

      public async Task<string> GenerateJwtInfoToken(User user)
    {
        var tokenHanlder = new JwtSecurityTokenHandler();
        var securityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes("j79n4hfrug5c9jk1u"));
        var credit =  new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);
        var claims = new[] 
        {
            new Claim("UserId",user.UserId),
            new Claim("CompanyId",user.CompanyId)
        };
        var token = new SecurityTokenDescriptor
        {
            Issuer = "",
            Expires = DateTime.UtcNow.AddHours(10),
            Subject = new ClaimsIdentity(claims),
            SigningCredentials = credit,
        };

        var output = tokenHanlder.CreateEncodedJwt(token);

        return output;
    }

    public User ReadJwtToken(string token)
    {
        var handler = new JwtSecurityTokenHandler();
        var JwtST = handler.ReadJwtToken(token);
        User user = new User();
        user.UserId = JwtST.Claims.First(c => c.Type == "UserId").Value;
        user.CompanyId = JwtST.Claims.First(c => c.Type == "CompanyId").Value;
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

    }
}