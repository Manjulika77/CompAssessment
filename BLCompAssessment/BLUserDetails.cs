using System;
using DLCompAssessment.Infrastructure.Abstract;
using DLCompAssessment.Infrastructure.Repository;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility.Model;
using Utility.Utility;

namespace BLCompAssessment
{
    public class BLUserDetails
    {
        IUserRepository objDL = new UserRepository();
        public string InsertUserData(User usr)
        {
            string msg = string.Empty;
            usr.Password = GenarateRandomPassword(8);
            usr.IsActive = true;
            try
            {

                msg = objDL.InsertUserData(usr);

            }
            catch (Exception ex)
            {
                msg = "exception";

                UtilityFeatures utObj = UtilityFeatures.GetInstance();
                utObj.LogErrorMessage(ex.Message + "\n" + ex.StackTrace);
            }

            return msg;
        }

        public string InsertRoleData(Role role)
        {
            string msg = string.Empty;            
            try
            {
                msg = objDL.InsertRoleData(role);                
            }
            catch (Exception ex)
            {
                msg = "exception";

                UtilityFeatures utObj = UtilityFeatures.GetInstance();
                utObj.LogErrorMessage(ex.Message + "\n" + ex.StackTrace);
            }

            return msg;
        }

        public List<Role> GetAllActiveRole()
        {
            List<Role> lstRole = new List<Role>();            

            try
            {
                var tempRole = objDL.GetAllRole();

                if(tempRole.Count > 0)
                {
                    foreach (Role role in tempRole)
                    {
                        if (role.isActive)
                        {
                            Role ObjRole = new Role();
                            ObjRole.RoleID = role.RoleID;
                            ObjRole.RoleName = role.RoleName;
                            lstRole.Add(ObjRole);
                        }
                    }
                }                
            }
            catch (Exception ex)
            {
                UtilityFeatures utObj = UtilityFeatures.GetInstance();
                utObj.LogErrorMessage(ex.Message + "\n" + ex.StackTrace);
            }

            return lstRole;
        }
        public List<Role> GetAllRole()
        {
            List<Role> lstRole = new List<Role>();

            try
            {
                lstRole = objDL.GetAllRole();                
            }
            catch (Exception ex)
            {
                UtilityFeatures utObj = UtilityFeatures.GetInstance();
                utObj.LogErrorMessage(ex.Message + "\n" + ex.StackTrace);
            }

            return lstRole;
        }
        public string GetUserByUserNameAndPassword(string userName, string password)
        {
            string msg = string.Empty;
            RegisteredUserViewModel usr = new RegisteredUserViewModel();
            AssessmentDetails asstDtls = new AssessmentDetails();
            try
            {
                usr = objDL.ValidateUser(userName,password);
                if (!String.IsNullOrEmpty(usr.UserName) && !String.IsNullOrEmpty(usr.RoleName))
                {
                    string usrRole = usr.RoleName;

                    if(usrRole.ToLower() == "candidate")
                    {
                        asstDtls = objDL.GetAssessmentDetailsByUserID(usr.UserID);
                        if (asstDtls.IsActive && (DateTime.Now <= asstDtls.AssessmentDate.AddMinutes(10)))
                        {
                            msg = "candidate_allowed";
                        }
                        else
                        {
                            msg = "not_allowed";
                        }
                    }
                    else
                    {
                        msg = "success";
                    }
                }
                else
                {
                    msg = "failed";
                }

            }
            catch (Exception ex)
            {
                msg = "exception";

                UtilityFeatures utObj = UtilityFeatures.GetInstance();
                utObj.LogErrorMessage(ex.Message + "\n" + ex.StackTrace);
            }

            return msg;
        }
        public string UpdatePassword(string UserName, string oldPwd, string newPwd)
        {
            string msg = string.Empty;
           
            try
            {

                msg = objDL.UpdatePassword(UserName,oldPwd,newPwd);

            }
            catch (Exception ex)
            {
                msg = "exception";

                UtilityFeatures utObj = UtilityFeatures.GetInstance();
                utObj.LogErrorMessage(ex.Message + "\n" + ex.StackTrace);
            }

            return msg;
        }

        public string UpdatePasswordByUserName(string UserName, string newPwd)
        {
            string msg = string.Empty;
            
            try
            {
                msg = objDL.UpdatePasswordByUserName(UserName, newPwd);
            }
            catch (Exception ex)
            {
                msg = "exception";

                UtilityFeatures utObj = UtilityFeatures.GetInstance();
                utObj.LogErrorMessage(ex.Message + "\n" + ex.StackTrace);
            }

            return msg;
        }

        public List<User> GetAllUser()
        {
            List<User> lstUser = new List<User>();
            try
            {
                lstUser = objDL.GetAllUser();
            }
            catch(Exception ex)
            {
                UtilityFeatures utObj = UtilityFeatures.GetInstance();
                utObj.LogErrorMessage(ex.Message + "\n" + ex.StackTrace);
            }
            return lstUser;
        }
        public ValidateUserNameViewModels ValidateUserName(string uname)
        {
            ValidateUserNameViewModels usr = new ValidateUserNameViewModels();
            usr.isValidated = false;

            User tempUsr = new User();
            try
            {
                tempUsr = objDL.ValidateUserName(uname);
                if(!String.IsNullOrEmpty(tempUsr.Email))
                {
                    if (tempUsr.IsActive)
                    {
                        Random rnd = new Random();
                        int otp = rnd.Next(100000, 999999);
                        usr.UserName = tempUsr.UserName;
                        usr.FirstName = tempUsr.FirstName;
                        usr.LastName = tempUsr.LastName;
                        usr.Email = tempUsr.Email;
                        usr.RoleID = tempUsr.RoleID;
                        usr.OTP = otp;
                        usr.LastPassChangeDate = tempUsr.LastPassChangeDate;
                        usr.isValidated = true;

                        //SendOTP(tempUsr.Email,otp);
                    }
                }

            }
            catch (Exception ex)
            {
                UtilityFeatures utObj = UtilityFeatures.GetInstance();
                utObj.LogErrorMessage(ex.Message + "\n" + ex.StackTrace);
            }
            return usr;
        }        

        private string GenarateRandomPassword(int length = 15)
        {
            // Create a string of characters, numbers, special characters that allowed in the password  
            string validChars = "ABCDEFGHJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789!@#$%^&*?_-";
            Random random = new Random();

            // Select one random character at a time from the string  
            // and create an array of chars  
            char[] chars = new char[length];
            for (int i = 0; i < length; i++)
            {
                chars[i] = validChars[random.Next(0, validChars.Length)];
            }
            return new string(chars);
        }

        private void SendOTP(string email, int otp)
        {
            string msg = "<html><body>Dear User,<br> Use this OTP : " + otp + " to reset your password. <br><BR><br>&nbsp;<b>Auto generated, plz don't reply</br></body></html>";
            string sub = "Reset Password Confirmation OTP";

            UtilityFeatures utObj = UtilityFeatures.GetInstance();
            utObj.SendEmail("anglerimi007@gmail.com", email, sub, msg);
        }

        private void SendWelcomeMail(string email, string uname, string pwd)
        {
            string msg = "<html><body>Dear User,<br>Welcome to iSmart Resume Builder. Use this User name : " + uname + " and Password :"+ pwd +" to Login. <br><BR><br>&nbsp;<b>Auto generated, plz don't reply</br></body></html>";
            string sub = "Reset Password Confirmation OTP";

            UtilityFeatures utObj = UtilityFeatures.GetInstance();
            utObj.SendEmail("anglerimi007@gmail.com", email, sub, msg);
        }
    }
}
