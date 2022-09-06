using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility.Model;

namespace DLCompAssessment.Infrastructure.Abstract
{
    public interface IUserRepository
    {
        bool FindDuplicateUserName(string userName);
        string InsertUserData(User usr);
        string InsertRoleData(Role role);
        string UpdatePassword(string UserName, string oldPwd, string newPwd);
        string UpdatePasswordByUserName(string UserName, string newPwd);
        List<Role> GetAllRole();
        string GetUserByUserNameAndPassword(string userName, string password);
        RegisteredUserViewModel ValidateUser(string userName, string password);
        AssessmentDetails GetAssessmentDetailsByUserID(int uid);
        List<User> GetAllUser();
        User ValidateUserName(string uname);
    }
}
