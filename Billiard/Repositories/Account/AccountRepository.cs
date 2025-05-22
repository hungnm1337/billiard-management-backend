using Microsoft.AspNetCore.Identity;
using Billiard.DTO;
using Billiard.Models;
using Microsoft.EntityFrameworkCore;

namespace Billiard.Repositories.Account
{
    public class AccountRepository : IAccountRepository
    {
        private readonly Prn232ProjectContext _context;
        private readonly PasswordHasher<object> _passwordHasher;
        public AccountRepository(Prn232ProjectContext context)
        {
            _passwordHasher = new PasswordHasher<object>();
            _context = context;   
        }
        public async Task Register(RegisterModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            var acc = await _context.Accounts.FirstOrDefaultAsync(x => x.Username.Equals(model.username));
            if (acc != null) return;

          
            var hashedPassword = _passwordHasher.HashPassword(null, model.password);

            var nacc = new Models.Account
            {
                Username = model.username,
                Password = hashedPassword,
                Status = model.status,
            };

            await _context.Accounts.AddAsync(nacc);
            await _context.SaveChangesAsync();

            var nuser = new Models.User
            {
                Name = model.username,
                Dob = model.dob,
                AccountId = nacc.AccountId,
                RoleId = model.roleid
            };
            await _context.Users.AddAsync(nuser);
            await _context.SaveChangesAsync();
        }
    }

}

