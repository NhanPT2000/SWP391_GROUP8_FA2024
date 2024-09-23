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
        Task CreateMemberAsync(Member member);
        Task<bool> UpdateMemberAsync(Guid id, Member member);
        Task<bool> DeleteMemberAsync(Guid id);
        Task<Member> GetMemberDetailsAsync(Guid id);
        Task<IEnumerable<Member>> GetMembersAsync();
        Task<bool> FindMemberAsync(Guid id);

        Task<Member>GetMemberByEmailAsync(string Email, string Password);
        Task<Member> GetMemberByEmailOnlyAsync(string Email);
        Task<bool> CheckPassword(string password, Member member);
    }

}
