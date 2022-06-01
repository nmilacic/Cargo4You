using Cargo4You.Data.Database.Cargo4You.Context;
using Cargo4You.Data.Database.Cargo4You.Model;
using Cargo4You.Services.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IdentityModel.Tokens;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Transactions;
using Cargo4You.Services.Sequrity;

namespace Cargo4You.Services
{
    public class UserService
    {
        private readonly Courier4YouContext _context;
        private readonly PasswordManager _passwordManager;
        public UserService(Courier4YouContext context, PasswordManager passwordManager)
        {
            _context = context;
            _passwordManager = passwordManager;
        }

        public async Task<bool> Registration(RegistrationData registrationData)
        {
            using var scope = CreateTransactionScope(isolationLevel: System.Transactions.IsolationLevel.RepeatableRead);
            var salt = _passwordManager.GenerateSalt();
            var hashedPassword = _passwordManager.Hash(registrationData.Password, salt);

            var user = new User
            {
                Name = registrationData.Username,
                Password = hashedPassword
            };
          
            await _context.AddAsync(user);
            await _context.SaveChangesAsync();

            scope.Complete();
            return true;
        }

        public async Task<LoginData> Login(LoginRequest login)
        {
            var user = await _context.Users.Where(x => x.Name == login.Username &&
                                                x.Password == login.Password)
                                    .FirstOrDefaultAsync();

            if (user == null)
            {
                return null;
            }

            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("nevenanevenanevenanevena"));
            var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

            var tokeOptions = new JwtSecurityToken(
                issuer: "http://localhost:5001",
                audience: "http://localhost:5001",
                claims: new List<Claim>()
                {
                    new Claim("userId", user.Id.ToString())
                },
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: signinCredentials
            );

            var tokenString = new JwtSecurityTokenHandler().WriteToken(tokeOptions);
            return new LoginData { JwtToken = tokenString };
        }

        protected TransactionScope CreateTransactionScope(
            TransactionScopeOption scopeOption = TransactionScopeOption.Required,
            TransactionScopeAsyncFlowOption asyncFlowOption = TransactionScopeAsyncFlowOption.Enabled,
            IsolationLevel isolationLevel = IsolationLevel.ReadUncommitted, TimeSpan? timeout = null)
        {
            var transactionOptions = new TransactionOptions
            {
                IsolationLevel = isolationLevel,
            };

            if (timeout != null)
            {
                transactionOptions.Timeout = (TimeSpan)timeout;
            }

            return new TransactionScope(scopeOption, transactionOptions, asyncFlowOption);
        }
    }
}
