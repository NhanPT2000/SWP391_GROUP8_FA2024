using DataObject;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public interface IMemberService
    {
        Task CreateMemberAsync(User member);
        Task<bool> UpdateMemberAsync(Guid id, User member);
        Task<bool> DeleteMemberAsync(Guid id);
        Task<User> GetMemberDetailsAsync(Guid id);
        Task<IEnumerable<User>> GetMembersAsync();
        Task<bool> FindMemberAsync(Guid id);

        Task<User>GetMemberByEmailAsync(string Email, string Password);
        Task<User> GetMemberByEmailOnlyAsync(string Email);
        Task<bool> CheckPassword(string password, User member);
        Task<bool> ChangePasswordAsync(string oldPassword, string newPassword, Guid Id);
    }

}
