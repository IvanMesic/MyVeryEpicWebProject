using Common.BLModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Interfaces
{
    public interface IUserReposi
    {
        IEnumerable<BLUser> GetAll();
        BLUser CreateUser(string username, string firstName, string lastName, string email, string password, string countryName);
        void ConfirmEmail(string email, string securityToken);
        BLUser GetConfirmedUser(string username, string password);
        void ChangePassword(string username, string newPassword);
    }
}
