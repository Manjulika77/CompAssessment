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
    public class ContentController : Controller
    {
        BLContentDetails objBL = new BLContentDetails();
        // GET: Content
        public ActionResult Index()
        {
            var ObjCaseStudyList = objBL.GetAllCaseStudy();
            return View(ObjCaseStudyList);
        }

        public ActionResult CaseStudyList()
        {
            var ObjCaseStudyList = objBL.GetAllCaseStudy();
            return View(ObjCaseStudyList);
        }

        public ActionResult CreateCaseSdy()
        {
            CreateCaseStudyViewModel objCaseSdy = new CreateCaseStudyViewModel();
            objCaseSdy = objBL.GetAllCompetencyAndAssesment();          
            return View(objCaseSdy);
        }
        [HttpPost]
        public ActionResult CreateCaseSdy(CreateCaseStudyViewModel objCaseSdy)
        {            
            objCaseSdy.CreatedBy = Session["UserID"].ToString();
            var op = objBL.CreateCaseStudy(objCaseSdy);

            if (String.Equals(op, "failed", StringComparison.InvariantCultureIgnoreCase))
            {
                ViewBag.msg = "Create Case Study Failed! Please try again later";
            }
            else if (String.Equals(op, "exception", StringComparison.InvariantCultureIgnoreCase))
            {
                ViewBag.msg = "Unknown error occured, please contact with your admin..";
            }
            else if (String.Equals(op, "success", StringComparison.InvariantCultureIgnoreCase))
            {
                ViewBag.msg = "Case Study created Successfully!";
            }
            objCaseSdy = objBL.GetAllCompetencyAndAssesment();
            return View(objCaseSdy);
        }

        public ActionResult ReviewCaseSdy(string id)
        {
            var objReviewCase = objBL.GetCaseStudySolnToReview(Convert.ToInt32(id));
            return View(objReviewCase);
        }

        [HttpPost]
        public ActionResult ReviewCaseSdy(string CSID, string ReviewComment, string createdby)
        {
            var objReviewCase = objBL.GetCaseStudySolnToReview(Convert.ToInt32(CSID));

            if (Session["UserID"]!=null && Session["UserID"].ToString() != createdby)
            {
                var op = objBL.InsertCaseSdyReview(Convert.ToInt32(CSID), ReviewComment);

                if (String.Equals(op, "failed", StringComparison.InvariantCultureIgnoreCase))
                {
                    ViewBag.msg = "Case Study Review Failed! Please try again later";
                }
                else if (String.Equals(op, "exception", StringComparison.InvariantCultureIgnoreCase))
                {
                    ViewBag.msg = "Unknown error occured, please contact with your admin..";
                }
                else if (String.Equals(op, "success", StringComparison.InvariantCultureIgnoreCase))
                {
                    ViewBag.msg = "Case Study Reviewed Successfully!";
                }
            }
            else
            {
                ViewBag.msg = "You can't review your own case study!";
            }
            return View(objReviewCase);
        }

        public ActionResult AssessmentList()
        {
            var objAssessment = objBL.GetAllAssesment();
            return View(objAssessment);
        }

        public ActionResult CreateAssessment()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateAssessment(CreateAssessmentViewModel asstVM)
        {
            bool canCreate = true;
            if (asstVM.IsActive)
            {
                var objAssessment = objBL.GetAllAssesment();
                bool isActiveExists = objAssessment.Any(e => e.IsActive);
                if (isActiveExists)
                {
                    canCreate = false;
                    ViewBag.msg = "Please update Existing Assessment as 'Not Active' to insert the current one as 'Active' Assesment!";
                }                
            }
            if (canCreate)
            {
                string currUser = string.Empty;
                if (Session["UserID"] != null)
                    currUser = Session["UserID"].ToString();

                var op = objBL.CreateAssessment(asstVM, currUser);

                if (String.Equals(op, "failed", StringComparison.InvariantCultureIgnoreCase))
                {
                    ViewBag.msg = "Assesment Creation Failed! Please try again later";
                }
                else if (String.Equals(op, "exception", StringComparison.InvariantCultureIgnoreCase))
                {
                    ViewBag.msg = "Unknown error occured, please contact with your admin..";
                }
                else if (String.Equals(op, "success", StringComparison.InvariantCultureIgnoreCase))
                {
                    ViewBag.msg = "Assessment Created Successfully!";
                }
            }
            return View();
        }

        public ActionResult EditAssessment(string id)
        {            
            var objasstVM = objBL.GetAssessmentByID(Convert.ToInt32(id));
            ViewBag.date = objasstVM.AssessmentDate;
            ViewBag.time = objasstVM.AssessmentTime;
            ViewBag.id = id;
            return View(objasstVM);
        }

        [HttpPost]
        public ActionResult EditAssessment(CreateAssessmentViewModel asstVM, string asstid)
        {
            var op = objBL.UpdateAssessment(asstVM, Convert.ToInt32(asstid));
            if (String.Equals(op, "failed", StringComparison.InvariantCultureIgnoreCase))
            {
                ViewBag.msg = "Assesment Update operation Failed! Please try again later";
            }
            else if (String.Equals(op, "exception", StringComparison.InvariantCultureIgnoreCase))
            {
                ViewBag.msg = "Unknown error occured, please contact with your admin..";
            }
            else if (String.Equals(op, "success", StringComparison.InvariantCultureIgnoreCase))
            {
                ViewBag.msg = "Assessment Updated Successfully!";
            }
            return View();
        }

        public ActionResult CreateAssessmentUserMapping()
        {
            AssessmentUserIDViewModel objAsstUsr = new AssessmentUserIDViewModel();
            objAsstUsr = objBL.GetAllAssesmentAndCandidate();
            return View(objAsstUsr);
        }
        [HttpPost]
        public ActionResult CreateAssessmentUserMapping(string AssessmentID, List<string> AreUsrChecked)
        {
            string msg = string.Empty;
            var createdBy = string.Empty;
            var usrToBeMapped = AreUsrChecked.Select(s => Convert.ToInt32(s)).ToList();
            if (Session["UserID"] != null)
                createdBy = Session["UserID"].ToString();
            var op = objBL.AddAssessmentUserMapping(Convert.ToInt32(AssessmentID), usrToBeMapped,createdBy);
            if (String.Equals(op, "failed", StringComparison.InvariantCultureIgnoreCase))
            {
                ViewBag.msg = "User Assesment Mapping operation Failed! Please try again later";
            }
            else if (String.Equals(op, "exception", StringComparison.InvariantCultureIgnoreCase))
            {
                ViewBag.msg = "Unknown error occured, please contact with your admin..";
            }
            else if (String.Equals(op, "success", StringComparison.InvariantCultureIgnoreCase))
            {
                ViewBag.msg = "User Assesment Mapped Successfully!";
            }
            AssessmentUserIDViewModel objAsstUsr = new AssessmentUserIDViewModel();
            objAsstUsr = objBL.GetAllAssesmentAndCandidate();
            return View(objAsstUsr);
        }
    }
}