using DotNet6Mediator.ApplicationLayer.Services.Interfaces;
using DotNet6Mediator.DomainLayer.Entities;
using DotNet6Mediator.DomainLayer.Helpers;
using DotNet6Mediator.InfrastructureLayer.Services.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace DotNet6Mediator.ApplicationLayer.Services
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _configuration;
        private readonly IAuthenticationService<User,UserCredentials> _authenticationService;

        private const int EXPIRES_TOKEN_MINUTES = 30;  //Durata token

        public TokenService(IConfiguration Configuration, IAuthenticationService<User, UserCredentials> Service)
        {
            this._configuration = Configuration;
            this._authenticationService = Service;
        }

        public async Task<string?> GetToken(UserCredentials UserCredentials)
        {
            User? AuthUser = await this._authenticationService.Authenticate(UserCredentials);
            if (AuthUser == null || AuthUser.UserRole == null) {
                return null;
            }
            string Token = this.GenerateJwtToken(AuthUser);
            return Token;
        }

        public IEnumerable<Claim> ValidateToken(string TokenString)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            tokenHandler.ValidateToken(TokenString, this.GetValidationParameters(), out SecurityToken VALIDATED_TOKEN);
            var token = (JwtSecurityToken)VALIDATED_TOKEN;
            return token.Claims;
        }

        private string GenerateJwtToken(User UserData)
        {
            //Sorgente che emette il token, e pubblico che riceverà il token
            string Issuer = this._configuration["Jwt:Issuer"];
            string Audience = this._configuration["Jwt:Audience"];
            //Genero chiave simmetrica (simmetrica = usata sia per cifrare che per decifrare i dati)
            var Key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this._configuration["Jwt:Key"]));
            //Creo un oggetto-firma contenente gli elementi per cifrare il token. Una chiave simmetrica e un algoritmo di cifratura
            var SignObj = new SigningCredentials(Key, SecurityAlgorithms.HmacSha256);
            //Genero lista di informazioni dell'utente che andranno a costituire il token
            var Claims = new List<Claim>() {
                new Claim("UserID",UserData.Id.ToString()),
                //new Claim(ClaimTypes.Role, UserData.UserRole!.RoleName) //IMPORTANTE: NEL ROLE CLAIMS VIENE ASSOCIATO UN RUOLO ALL'UTENTE
            };
            //Genero data scadenza del token
            DateTime NotBeforeDate = DateTime.Now;
            DateTime ExpiresDate = NotBeforeDate.AddMinutes(EXPIRES_TOKEN_MINUTES);
            var Token = new JwtSecurityToken(Issuer, Audience, Claims, NotBeforeDate, ExpiresDate, SignObj);
            string TokenString = new JwtSecurityTokenHandler().WriteToken(Token);
            return TokenString;
        }

        private TokenValidationParameters GetValidationParameters()
        {
            return new TokenValidationParameters()
            {
                ValidateIssuer = true,           // Controllo la sorgente di emissione del token
                ValidateAudience = true,         // Controllo il pubblico a cui è rivolto il token
                ValidateLifetime = true,         // Controllo che il token non sia scaduto
                ClockSkew = TimeSpan.Zero,       // Inizializzo lo sfasamento dell'inzio del lifetime clock a 0 (default è 5 minuti)
                ValidateIssuerSigningKey = true, // Controllo che la chiave utilizzata per cifrarlo sia corretta
                ValidIssuer = this._configuration["Jwt:Issuer"],     //Imposto un'emittente valida
                ValidAudience = this._configuration["Jwt:Audience"], //Imposto un pubblico valido
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this._configuration["Jwt:Key"]))  //Imposto la chiave corretta con cui confrontare la chiave contenuta nei tokens
            };
        }
    }
}
