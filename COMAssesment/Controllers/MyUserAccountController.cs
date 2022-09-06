using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using BLCompAssessment;
using Utility.Model;

namespace COMAssesment.Controllers
{
    public class MyUserAccountController : Controller
    {
        BLUserDetails objBL = new BLUserDetails();

        // GET: UserAccount
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(string uname, string pwd)
        {            
            var op = objBL.GetUserByUserNameAndPassword(uname, pwd);

            if (String.Equals(op, "failed", StringComparison.InvariantCultureIgnoreCase))
            {
                ViewBag.msg = "Login Failed! Worng User Name and Password..";
            }
            else if (String.Equals(op, "exception", StringComparison.InvariantCultureIgnoreCase))
            {
                ViewBag.msg = "Login Failed! Unknown error occured, please contact with your admin..";
            }
            else if (String.Equals(op, "success", StringComparison.InvariantCultureIgnoreCase))
            {
                Session["UserID"] = uname;
                return RedirectToAction("UserHome");
            }
            else if (String.Equals(op, "candidate_allowed", StringComparison.InvariantCultureIgnoreCase))
            {
                Session["UserID"] = uname;
                return RedirectToAction("TestInstruction");
            }
            else if (String.Equals(op, "not_allowed", StringComparison.InvariantCultureIgnoreCase))
            {
                ViewBag.msg = "You are not eligible for any active assessment, please try again later!";
            }
            return View();
        }


        public ActionResult UserHome()
        {
            return View();
        }

        public ActionResult TestInstruction()
        {
            return View();
        }

        public ActionResult UserList()
        {
            List<RegistrationViewModels> lstRegUsr = new List<RegistrationViewModels>();
            var lstUsr = objBL.GetAllUser();
            foreach (User usr in lstUsr)
            {
                RegistrationViewModels regVM = new RegistrationViewModels();

                var config = new MapperConfiguration(cfg => cfg.CreateMap<User, RegistrationViewModels>());
                var mapper = new Mapper(config);
                regVM = mapper.Map<RegistrationViewModels>(usr);

                lstRegUsr.Add(regVM);
            }
            return View(lstRegUsr);
        }

        public ActionResult CreateUser()
        {
            UserRoleViewModels usrRole = new UserRoleViewModels();
            usrRole.UserRole = objBL.GetAllActiveRole();

            ViewBag.Roles = usrRole.UserRole;
            return View();
        }

        [HttpPost]
        public ActionResult CreateUser(RegistrationViewModels usr)
        {
            User accountUser = new User();
            UserRoleViewModels usrRole = new UserRoleViewModels();

            var config = new MapperConfiguration(cfg => cfg.CreateMap<RegistrationViewModels, User>());
            var mapper = new Mapper(config);
            accountUser = mapper.Map<User>(usr);

            var op = objBL.InsertUserData(accountUser);

            if (String.Equals(op, "failed", StringComparison.InvariantCultureIgnoreCase))
            {
                ViewBag.msg = "Create User Failed! Please try again later";
            }
            else if (String.Equals(op, "exception", StringComparison.InvariantCultureIgnoreCase))
            {
                ViewBag.msg = "Unknown error occured, please contact with your admin..";
            }
            else if (String.Equals(op, "success", StringComparison.InvariantCultureIgnoreCase))
            {
                ViewBag.msg = "User created Successfully!";
            }

            usrRole.UserRole = objBL.GetAllActiveRole();
            ViewBag.Roles = usrRole.UserRole;
            return View();
        }

        public ActionResult EditUser()
        {
            return View();
        }

        [HttpPost]
        public ActionResult EditUser(RegistrationViewModels usr)
        {
            return View();
        }

        public ActionResult RoleList()
        {
            List<Role> lstRole = objBL.GetAllRole();
            return View(lstRole);
        }

        public ActionResult CreateRole()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateRole(Role role)
        {
            var op = objBL.InsertRoleData(role);

            if (String.Equals(op, "failed", StringComparison.InvariantCultureIgnoreCase))
            {
                ViewBag.msg = "Create Role Failed! Please try again later";
            }
            else if (String.Equals(op, "exception", StringComparison.InvariantCultureIgnoreCase))
            {
                ViewBag.msg = "Unknown error occured, please contact with your admin..";
            }
            else if (String.Equals(op, "success", StringComparison.InvariantCultureIgnoreCase))
            {
                ViewBag.msg = "Role created Successfully!";
            }

            return View();
        }
        public ActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ChangePassword(ChangePasswordViewModels changePass)
        {
            var op = objBL.UpdatePassword(changePass.UserName, changePass.Old_Pass, changePass.New_Pass);

            if (String.Equals(op, "failed", StringComparison.InvariantCultureIgnoreCase))
            {
                ViewBag.msg = "Reset Password Failed! Please try again later";
            }
            else if (String.Equals(op, "exception", StringComparison.InvariantCultureIgnoreCase))
            {
                ViewBag.msg = "Unknown error occured, please contact with your admin..";
            }
            else if (String.Equals(op, "success", StringComparison.InvariantCultureIgnoreCase))
            {
                ViewBag.msg = "Password Updated Successfully!";
            }
            return View();
        }

        public ActionResult ForgotPwd()
        {
            return View();
        }

        [HttpPost]
        public JsonResult ValidateUname(string uname)
        {
            ValidateUserNameViewModels usr = new ValidateUserNameViewModels();


            usr = objBL.ValidateUserName(uname);
            if (!String.IsNullOrEmpty(usr.UserName))
            {
                Session["userName"] = usr.UserName;
                Session["name"] = usr.FirstName + " " + usr.LastName;
                Session["email"] = usr.Email;
                Session["OTP"] = usr.OTP;
            }
            return Json(usr);
        }
        [HttpPost]
        public ActionResult ForgotPwd(int OTP, string New_Password)
        {
            if (Convert.ToInt32(Session["OTP"]) != OTP)
            {
                ViewBag.msg = "Wrong OTP! Please try again later..";
            }
            else
            {
                var uname = Session["userName"].ToString();
                var op = objBL.UpdatePasswordByUserName(uname, New_Password);

                if (String.Equals(op, "failed", StringComparison.InvariantCultureIgnoreCase))
                {
                    ViewBag.msg = "Reset Password Failed! Please try again later";
                }
                else if (String.Equals(op, "exception", StringComparison.InvariantCultureIgnoreCase))
                {
                    ViewBag.msg = "Unknown error occured, please contact with your admin..";
                }
                else if (String.Equals(op, "success", StringComparison.InvariantCultureIgnoreCase))
                {
                    ViewBag.msg = "Password Updated Successfully!";
                }
            }
            return View();
        }
    }
}