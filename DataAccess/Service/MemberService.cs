using DataAccess.Database;
using DataAccess.Repository;
using DataObject;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.Arm;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Service
{
    public class MemberService : IMemberService
    {
        private readonly PetShopContext _memberContext;
        public MemberService(PetShopContext dbContext)
        {
            _memberContext = dbContext;
        }
        public async Task CreateMemberAsync(User member)
        {
            using var transaction = await _memberContext.Database.BeginTransactionAsync();
            try
            {
                var newMember = new User
                {
                    UserId = member.UserId,
                    Email = member.Email,
                    Password = HashPassword(member.Password),
                    ConfirmedEmail = false,
                    Addess = member.Addess,
                    Gender = member.Gender,
                    UserName = member.UserName,
                    PhoneNumber = member.PhoneNumber,
                    PhoneNumber2 = member.PhoneNumber2,
                    RoleId = _memberContext.Roles.FirstOrDefault(m => m.RoleName == "Member")?.RoleId
                };

                await _memberContext.Members.AddAsync(newMember);
                await _memberContext.SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task<bool> DeleteMemberAsync(Guid id)
        {
            var memberToDelete = await _memberContext.Members.FirstAsync(p => p.UserId == id);

            if (memberToDelete == null)
            {
                return false;
            }

            _memberContext.Remove(memberToDelete);
            return await _memberContext.SaveChangesAsync() > 0;
        }

        public async Task<bool> FindMemberAsync(Guid id)
        {
            var memberToFind = await _memberContext.Members
                .Include(m=> m._Role)
                .Include(m => m._Pets)
                .Include(m=> m._PlannedServices)
                .FirstOrDefaultAsync(m => m.UserId == id);

            if (memberToFind == null)
            {
                return false;
            }
            return await _memberContext.SaveChangesAsync() > 0;
        }

        public async Task<User> GetMemberDetailsAsync(Guid id)
        {
            var member = await _memberContext.Members
                .Include(m => m._Role)
                .Include(m => m._Pets)
                .Include(m=> m._PlannedServices)
                .FirstOrDefaultAsync(m => m.UserId == id);

            if (member == null)
            {
                return default;
            }
            return member;
        }
        public async Task<IEnumerable<User>> GetMembersAsync()
        {
            return await _memberContext.Members.Include(u => u._Role).ToListAsync();
        }

        public async Task<bool> UpdateMemberAsync(Guid id, User member)
        {
            var memberToUpdate = await _memberContext.Members.FindAsync(id);

            if (memberToUpdate == null)
            {
                return false;
            }
            memberToUpdate.OnlineTime = DateTime.UtcNow;
            memberToUpdate.UserName = member.UserName;
            memberToUpdate.Profile = member.Profile;
            memberToUpdate.Addess = member.Addess;
            memberToUpdate.PhoneNumber = member.PhoneNumber;
            memberToUpdate.PhoneNumber2 = member.PhoneNumber2;
            memberToUpdate.Email = member.Email;
            memberToUpdate.Password = member.Password;
            memberToUpdate.Gender = member.Gender;
            memberToUpdate.ConfirmedEmail = member.ConfirmedEmail;
            _memberContext.Update(memberToUpdate);
            return await _memberContext.SaveChangesAsync() > 0;
        }
        public async Task<User> GetMemberByEmailAsync(string Email, string password)
        {
            var member = await _memberContext.Members.FirstOrDefaultAsync(m => m.Email == Email && m.Password == HashPassword(password));

            if (member == null)
            {
                return default;
            }
            return member;
        }
        public async Task<User> GetMemberByEmailOnlyAsync(string Email)
        {
            var member = await _memberContext.Members.FirstOrDefaultAsync(m => m.Email == Email);

            if (member == null)
            {
                return default;
            }
            return member;
        }
        public async Task<bool> CheckPassword(string password, User member)
        {
            var defaultPassword = await _memberContext.Members.FirstOrDefaultAsync(m => m.Password == HashPassword(password));

            if (defaultPassword == null)
            {
                return false;
            }
            return true;
        }

        private string HashPassword(string password)
        {
            var sha = SHA256.Create();
            var asByteArray = Encoding.UTF8.GetBytes(password);
            var HashPassword = sha.ComputeHash(asByteArray);
            return Convert.ToBase64String(HashPassword);
        }

        public async Task<bool> ChangePasswordAsync(string oldPassword, string newPassword, Guid Id)
        {
            var member = await _memberContext.Members.FindAsync(Id);
            if (member == null) 
            {
                Console.WriteLine("No member found");
                return false; 
            }
            else if(HashPassword(oldPassword) != member.Password) 
            {
                Console.WriteLine("Old password did not match");
                return false; 
            }
            else
            {
                member.Password = HashPassword(newPassword);
                _memberContext.Update(member);
                return await _memberContext.SaveChangesAsync() > 0;
            }
        }
    }
}
