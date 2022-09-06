using System;
using System.Collections.Generic;
using Utility.Model;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility.Utility;
using DLCompAssessment.Infrastructure.Abstract;

namespace DLCompAssessment.Infrastructure.Repository
{   
    public class UserRepository : IUserRepository
    {
        SqlConnection con = new SqlConnection(ConfigurationSettings.AppSettings["conString"]);

        public bool FindDuplicateUserName(string userName)
        {
            bool foundDuplicate = false;

            return foundDuplicate;
        }
        public string InsertUserData(User usr)
        {
            string msg = string.Empty;

            SqlCommand com = new SqlCommand("InsertUserData", con);
            com.CommandType = CommandType.StoredProcedure;
            try
            {
                com.Parameters.AddWithValue("@userName", usr.UserName);
                com.Parameters.AddWithValue("@password", usr.Password);
                com.Parameters.AddWithValue("@firstName", usr.FirstName);
                com.Parameters.AddWithValue("@lastName", usr.LastName);
                com.Parameters.AddWithValue("@roleID", usr.RoleID);
                com.Parameters.AddWithValue("@email", usr.Email);
                com.Parameters.AddWithValue("@isActive", usr.IsActive);

                con.Open();
                int num = com.ExecuteNonQuery();
                con.Close();
                if (num > 0)
                {
                    msg = "success";
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

        public string InsertRoleData(Role role)
        {
            string msg = string.Empty;
            SqlCommand com = new SqlCommand("InsertRoleData", con);
            com.CommandType = CommandType.StoredProcedure;
            try
            {
                com.Parameters.AddWithValue("@roleName", role.RoleName);
                com.Parameters.AddWithValue("@isActive", role.isActive);

                con.Open();
                int num = com.ExecuteNonQuery();
                con.Close();
                if (num > 0)
                {
                    msg = "success";
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
            SqlCommand com = new SqlCommand("UpdatePassword", con);
            com.CommandType = CommandType.StoredProcedure;
            try
            {
                com.Parameters.AddWithValue("@userName", UserName);
                com.Parameters.AddWithValue("@oldPassword", oldPwd);
                com.Parameters.AddWithValue("@newPassword", newPwd);

                con.Open();
                int num = com.ExecuteNonQuery();
                con.Close();
                if (num > 0)
                {
                    msg = "success";
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

        public string UpdatePasswordByUserName(string UserName, string newPwd)
        {
            string msg = string.Empty;
            SqlCommand com = new SqlCommand("UpdatePasswordByUserName", con);
            com.CommandType = CommandType.StoredProcedure;
            try
            {
                com.Parameters.AddWithValue("@userName", UserName);                
                com.Parameters.AddWithValue("@newPassword", newPwd);

                con.Open();
                int num = com.ExecuteNonQuery();
                con.Close();
                if (num > 0)
                {
                    msg = "success";
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

        public List<Role> GetAllRole()
        {
            List<Role> lstRole = new List<Role>();
            SqlCommand com = new SqlCommand("GetAllRole", con);
            com.CommandType = CommandType.StoredProcedure;

            DataTable dt = new DataTable();

            try
            {
                SqlDataAdapter da = new SqlDataAdapter(com);
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow eachRow in dt.Rows)
                    {
                        Role obj = new Role();
                        obj.RoleID = Convert.ToInt32(eachRow["RoleID"]);
                        obj.RoleName = eachRow["RoleName"].ToString();
                        obj.isActive = Convert.ToBoolean(eachRow["isActive"]);
                        lstRole.Add(obj);
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

        public string GetUserByUserNameAndPassword(string userName, string password)
        {
            string msg = string.Empty;
            SqlCommand com = new SqlCommand("GetRegisteredUser", con);
            com.CommandType = CommandType.StoredProcedure;

            com.Parameters.AddWithValue("@userName", userName);
            com.Parameters.AddWithValue("@password", password);

            DataTable dt = new DataTable();
            try
            {
                SqlDataAdapter da = new SqlDataAdapter(com);
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    msg = "success";
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
        public RegisteredUserViewModel ValidateUser(string userName, string password)
        {
            RegisteredUserViewModel usr = new RegisteredUserViewModel();
            SqlCommand com = new SqlCommand("GetRegisteredUser", con);
            com.CommandType = CommandType.StoredProcedure;

            com.Parameters.AddWithValue("@userName", userName);
            com.Parameters.AddWithValue("@password", password);

            DataTable dt = new DataTable();
            try
            {
                SqlDataAdapter da = new SqlDataAdapter(com);
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    usr.UserID = Convert.ToInt32(dt.Rows[0]["UserID"]);
                    usr.UserName = dt.Rows[0]["UserName"].ToString();
                    usr.RoleName = dt.Rows[0]["RoleName"].ToString();
                }
            }
            catch (Exception ex)
            {
                UtilityFeatures utObj = UtilityFeatures.GetInstance();
                utObj.LogErrorMessage(ex.Message + "\n" + ex.StackTrace);
            }

            return usr;
        }
        public AssessmentDetails GetAssessmentDetailsByUserID(int uid)
        {
            AssessmentDetails asstDtls = new AssessmentDetails();
            SqlCommand com = new SqlCommand("GetAssessmentDetailsByUserID", con);
            com.CommandType = CommandType.StoredProcedure;

            com.Parameters.AddWithValue("@userID", uid);

            DataTable dt = new DataTable();
            try
            {
                SqlDataAdapter da = new SqlDataAdapter(com);
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    asstDtls.AssessmentID = Convert.ToInt32(dt.Rows[0]["AssessmentID"]);
                    asstDtls.AssessmentName = dt.Rows[0]["AssessmentName"].ToString();
                    asstDtls.AssessmentDate = Convert.ToDateTime(dt.Rows[0]["AssessmentDate"]);
                    asstDtls.CreatedBy = dt.Rows[0]["CreatedBy"].ToString();
                    asstDtls.CreatedDate = Convert.ToDateTime(dt.Rows[0]["CreatedDate"]);
                    asstDtls.DurationMin = Convert.ToInt32(dt.Rows[0]["DurationMin"]);
                    asstDtls.IsActive = true;
                }
            }
            catch(Exception ex)
            {
                UtilityFeatures utObj = UtilityFeatures.GetInstance();
                utObj.LogErrorMessage(ex.Message + "\n" + ex.StackTrace);
            }
            return asstDtls;
        }
        public List<User> GetAllUser()
        {
            List<User> lstUser = new List<User>();
            SqlCommand com = new SqlCommand("GetAllUser", con);
            com.CommandType = CommandType.StoredProcedure;            

            DataTable dt = new DataTable();
            try
            {
                SqlDataAdapter da = new SqlDataAdapter(com);
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow eachRow in dt.Rows)
                    {
                        User objUsr = new User();

                        objUsr.UserID = Convert.ToInt32(eachRow["UserID"]);
                        objUsr.UserName = eachRow["UserName"].ToString();
                        objUsr.FirstName = eachRow["FirstName"].ToString();
                        objUsr.LastName = eachRow["LastName"].ToString();
                        objUsr.Email = eachRow["Email"].ToString();
                        objUsr.RoleID = Convert.ToInt32(eachRow["RoleID"]);
                        objUsr.IsActive = Convert.ToBoolean(eachRow["isActive"]);
                        lstUser.Add(objUsr);
                    }
                }               
            }
            catch (Exception ex)
            {
                UtilityFeatures utObj = UtilityFeatures.GetInstance();
                utObj.LogErrorMessage(ex.Message + "\n" + ex.StackTrace);
            }

            return lstUser;
        }
        public User ValidateUserName(string uname)
        {
            User objusr = new User();
            SqlCommand com = new SqlCommand("GetUserByUserName", con);
            com.CommandType = CommandType.StoredProcedure;

            com.Parameters.AddWithValue("@userName", uname);
            DataTable dt = new DataTable();

            try
            {
                SqlDataAdapter da = new SqlDataAdapter(com);
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow eachRow in dt.Rows)
                    {
                        objusr.UserName = eachRow["UserName"].ToString();
                        objusr.FirstName = eachRow["FirstName"].ToString();
                        objusr.LastName = eachRow["LastName"].ToString();
                        objusr.Email = eachRow["Email"].ToString();
                        objusr.RoleID = Convert.ToInt32(eachRow["RoleID"]);
                        objusr.IsActive = Convert.ToBoolean(eachRow["IsActive"]);
                        objusr.LastPassChangeDate = Convert.ToDateTime(eachRow["LastPassChangeDate"]);
                    }
                }
            }
            catch (Exception ex)
            {
                UtilityFeatures utObj = UtilityFeatures.GetInstance();
                utObj.LogErrorMessage(ex.Message + "\n" + ex.StackTrace);
            }
            return objusr;
        }
        
    }
}
