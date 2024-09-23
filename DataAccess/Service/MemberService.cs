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
        public async Task CreateMemberAsync(Member member)
        {
            using var transaction = await _memberContext.Database.BeginTransactionAsync();
            try
            {
                var newMember = new Member
                {
                    MemberId = member.MemberId,
                    Email = member.Email,
                    Password = HashPassword(member.Password),
                    ConfirmedEmail = false,
                    Addess = member.Addess,
                    Gender = member.Gender,
                    MemberName = member.MemberName,
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
            var memberToDelete = await _memberContext.Members.FirstAsync(p => p.MemberId == id);

            if (memberToDelete == null)
            {
                return false;
            }

            _memberContext.Remove(memberToDelete);
            return await _memberContext.SaveChangesAsync() > 0;
        }

        public async Task<bool> FindMemberAsync(Guid id)
        {
            var memberToFind = await _memberContext.Members.FindAsync(id);

            if (memberToFind == null)
            {
                return false;
            }
            return await _memberContext.SaveChangesAsync() > 0;
        }

        public async Task<Member> GetMemberDetailsAsync(Guid id)
        {
            var member = await _memberContext.Members.FindAsync(id);

            if (member == null)
            {
                return default;
            }
            return member;
        }
        public async Task<IEnumerable<Member>> GetMembersAsync()
        {
            return await _memberContext.Members.ToListAsync();
        }

        public async Task<bool> UpdateMemberAsync(Guid id, Member member)
        {
            var memberToUpdate = await _memberContext.Members.FindAsync(id);

            if (memberToUpdate == null)
            {
                return false;
            }
            memberToUpdate.Addess = member.Addess;
            memberToUpdate.PhoneNumber = member.PhoneNumber;
            memberToUpdate.PhoneNumber2 = member.PhoneNumber2;
            memberToUpdate.Email = member.Email;
            memberToUpdate.Password = member.Password;
            memberToUpdate.Gender = member.Gender;
            _memberContext.Update(memberToUpdate);
            return await _memberContext.SaveChangesAsync() > 0;
        }
        public async Task<Member> GetMemberByEmailAsync(string Email, string password)
        {
            var member = await _memberContext.Members.FirstOrDefaultAsync(m => m.Email == Email && m.Password == password);

            if (member == null)
            {
                return default;
            }
            return member;
        }
        public async Task<Member> GetMemberByEmailOnlyAsync(string Email)
        {
            var member = await _memberContext.Members.FirstOrDefaultAsync(m => m.Email == Email);

            if (member == null)
            {
                return default;
            }
            return member;
        }
        public async Task<bool> CheckPassword(string password, Member member)
        {
            var defaultPassword = await _memberContext.Members.FirstOrDefaultAsync(m => m.Password == HashPassword(password));

            if (defaultPassword == null)
            {
                return false;
            }
            return await _memberContext.SaveChangesAsync() > 0;
        }

        private string HashPassword(string password)
        {
            var sha = SHA256.Create();
            var asByteArray = Encoding.UTF8.GetBytes(password);
            var HashPassword = sha.ComputeHash(asByteArray);
            return Convert.ToBase64String(HashPassword);
        }
    }
}
