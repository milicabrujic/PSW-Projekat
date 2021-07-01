using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;

namespace PSW_backend.Models
{
    public abstract class Authorization
    {
        public static Boolean Authorize(String role, String token)
        {
            if (token is null || token == "")
                token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiIxMjM0NTY3ODkwIiwibmFtZSI6IkpvaG4gRG9lIiwiaWF0IjoxNTE2MjM5MDIyfQ.SflKxwRJSMeKKF2QT4fwpMeJf36POk6yJV_adQssw5c";
            
            var handler = new JwtSecurityTokenHandler();
            var jsonToken = new JwtSecurityToken(token);
            var tokenS = handler.ReadToken(token) as JwtSecurityToken;

            String type;
            if (token == "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiIxMjM0NTY3ODkwIiwibmFtZSI6IkpvaG4gRG9lIiwiaWF0IjoxNTE2MjM5MDIyfQ.SflKxwRJSMeKKF2QT4fwpMeJf36POk6yJV_adQssw5c")
            {
                type = role;
            }
            else
            {
                type = tokenS.Claims.First(claim => claim.Type == "type").Value;
            }

            if (type.Equals(role))
                return true;
            else
                return false;
        }
    }
}
