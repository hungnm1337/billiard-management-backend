using Microsoft.AspNetCore.Identity;
using Billiard.DTO;
using Billiard.Models;
using Microsoft.EntityFrameworkCore;
using Azure.Messaging;

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

        public async Task<bool> changeStatusAccount(int accountId)
        {
            try
            {
                var acc = await _context.Accounts.Include(x => x.Users).FirstOrDefaultAsync(x => x.AccountId == accountId);
                if (acc == null) { return false; }
                if (acc.Status.Equals("BAN"))
                {
                    acc.Status = "ACTIVE";
                }
                else
                {
                    acc.Status = "BAN";
                }
                
                _context.Accounts.Update(acc);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }

        public async Task<IEnumerable<Models.Account>> GetAccounts()
        {
            var accounts = await _context.Accounts.ToListAsync();
            return accounts;
        }

        public async Task<bool> Register(RegisterModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            var acc = await _context.Accounts.FirstOrDefaultAsync(x => x.Username.Equals(model.username));
            if (acc != null) return false;

          
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
                Name = model.name,
                Dob = model.dob,
                AccountId = nacc.AccountId,
                RoleId = model.roleid
            };
            await _context.Users.AddAsync(nuser);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<string> resetPassword(int accountId)
        {
            try
            {
                var account = await _context.Accounts.FindAsync(accountId);
                if (account == null)
                {
                    return null;
                }
                string realPass = this.genaratePassword();

                var hashedPassword = _passwordHasher.HashPassword(null, realPass);

                account.Password = hashedPassword;
                _context.Accounts.Update(account);
                await _context.SaveChangesAsync();
                return realPass;
            }
            catch (Exception ex)
            {
                return null;
            }

        }

        private string genaratePassword()
        {
            const string characters = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            Random random = new Random();
            char[] password = new char[8];

            for (int i = 0; i < 8; i++)
            {
                password[i] = characters[random.Next(characters.Length)];
            }

            return new string(password);

        }
    }

}

