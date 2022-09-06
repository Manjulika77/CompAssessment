using System;
using DLCompAssessment.Infrastructure.Abstract;
using DLCompAssessment.Infrastructure.Repository;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility.Model;
using Utility.Utility;
using System.Globalization;

namespace BLCompAssessment
{
    public class BLContentDetails
    {
        IContentRepository objDL = new ContentRepository();

        public List<AssessmentDetails> GetAllAssesment()
        {
            List<AssessmentDetails> lstAssesmnt = new List<AssessmentDetails>();

            try
            {
                lstAssesmnt = objDL.GetAllAssesment();
            }
            catch (Exception ex)
            {
                UtilityFeatures utObj = UtilityFeatures.GetInstance();
                utObj.LogErrorMessage(ex.Message + "\n" + ex.StackTrace);
            }
            return lstAssesmnt;
        }
        public List<CompetencyDetails> GetAllCompetency()
        {
            List<CompetencyDetails> lstComp = new List<CompetencyDetails>();

            try
            {
                lstComp = objDL.GetAllCompetency();
            }
            catch (Exception ex)
            {
                UtilityFeatures utObj = UtilityFeatures.GetInstance();
                utObj.LogErrorMessage(ex.Message + "\n" + ex.StackTrace);
            }
            return lstComp;
        }
        public CreateCaseStudyViewModel GetAllCompetencyAndAssesment()
        {
            CreateCaseStudyViewModel objCompAst = new CreateCaseStudyViewModel();           
            
            try
            {
                objCompAst = objDL.GetAllCompetencyAndAssesment();
            }
            catch (Exception ex)
            {
                UtilityFeatures utObj = UtilityFeatures.GetInstance();
                utObj.LogErrorMessage(ex.Message + "\n" + ex.StackTrace);
            }
            return objCompAst;
        }
        public string CreateCaseStudy(CreateCaseStudyViewModel caseStudy)
        {
            string msg = string.Empty;
            
            try
            {
                msg = objDL.CreateCaseStudy(caseStudy);
            }
            catch (Exception ex)
            {
                msg = "exception";

                UtilityFeatures utObj = UtilityFeatures.GetInstance();
                utObj.LogErrorMessage(ex.Message + "\n" + ex.StackTrace);
            }
            return msg;
        }
        public List<CaseStudyListViewModel> GetAllCaseStudy()
        {
            List<CaseStudyListViewModel> lstCaseSdy = new List<CaseStudyListViewModel>();
            
            try
            {
                var tempObj = objDL.GetAllCaseStudy();

                if(tempObj.Count > 0)
                {
                    foreach(CaseStudyListViewModel item in tempObj)
                    {
                        if (!item.ReviewStatus)
                        {
                            lstCaseSdy.Add(item);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                UtilityFeatures utObj = UtilityFeatures.GetInstance();
                utObj.LogErrorMessage(ex.Message + "\n" + ex.StackTrace);
            }

            return lstCaseSdy;
        }
        public CaseStudySolutionReviewViewModel GetCaseStudySolnToReview(int caseSdyID)
        {
            CaseStudySolutionReviewViewModel objCaseSolnReview = new CaseStudySolutionReviewViewModel();
            
            try
            {
                objCaseSolnReview = objDL.GetCaseStudySolnToReview(caseSdyID);
            }
            catch (Exception ex)
            {
                UtilityFeatures utObj = UtilityFeatures.GetInstance();
                utObj.LogErrorMessage(ex.Message + "\n" + ex.StackTrace);
            }
            return objCaseSolnReview;
        }
        public string InsertCaseSdyReview(int csid, string reviewComment)
        {
            string msg = string.Empty;            
            try
            {
                msg = objDL.InsertCaseSdyReview(csid, reviewComment);
            }
            catch (Exception ex)
            {
                UtilityFeatures utObj = UtilityFeatures.GetInstance();
                utObj.LogErrorMessage(ex.Message + "\n" + ex.StackTrace);
            }
            return msg;
        }
        public string CreateAssessment(CreateAssessmentViewModel asstVM, string currUser)
        {
            string msg = string.Empty;            
            try
            {
                AssessmentDetails asstDtls = new AssessmentDetails();
                DateTime dt = new DateTime();
                if (!String.IsNullOrEmpty(asstVM.AssessmentDate) && !String.IsNullOrEmpty(asstVM.AssessmentTime))
                {
                    string dateTimeStrg = asstVM.AssessmentDate + " " + asstVM.AssessmentTime;
                    dt = DateTime.ParseExact(dateTimeStrg, "yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture);                                      
                }
                asstDtls.AssessmentName = asstVM.AssessmentName;
                asstDtls.CreatedBy = currUser;
                asstDtls.AssessmentDate = dt;
                asstDtls.DurationMin = asstVM.Duration;
                asstDtls.IsActive = asstVM.IsActive;

                msg = objDL.CreateAssessment(asstDtls);
            }
            catch (Exception ex)
            {
                msg = "exception";

                UtilityFeatures utObj = UtilityFeatures.GetInstance();
                utObj.LogErrorMessage(ex.Message + "\n" + ex.StackTrace);
            }
            return msg;
        }
        public CreateAssessmentViewModel GetAssessmentByID(int AsstID)
        {
            CreateAssessmentViewModel asstVM = new CreateAssessmentViewModel();           
            try
            {
                var objAsst = objDL.GetAssessmentByID(AsstID);
                var date = string.Empty;
                var time = string.Empty;
                if (!String.IsNullOrEmpty(objAsst.AssessmentDate.ToString()))
                {
                    date = objAsst.AssessmentDate.ToString("yyyy-MM-dd");
                    time = objAsst.AssessmentDate.ToString("HH:mm");
                }
                asstVM.AssessmentName = objAsst.AssessmentName;
                asstVM.AssessmentDate = date;
                asstVM.AssessmentTime = time;
                asstVM.Duration = objAsst.DurationMin;
                asstVM.IsActive = objAsst.IsActive;
            }
            catch (Exception ex)
            {
                UtilityFeatures utObj = UtilityFeatures.GetInstance();
                utObj.LogErrorMessage(ex.Message + "\n" + ex.StackTrace);
            }
            return asstVM;
        }
        public string UpdateAssessment(CreateAssessmentViewModel asstVM, int id)
        {
            var msg = string.Empty;

            try
            {
                AssessmentDetails asstDtls = new AssessmentDetails();
                DateTime dt = new DateTime();
                if (!String.IsNullOrEmpty(asstVM.AssessmentDate) && !String.IsNullOrEmpty(asstVM.AssessmentTime))
                {
                    string dateTimeStrg = asstVM.AssessmentDate + " " + asstVM.AssessmentTime;
                    dt = DateTime.ParseExact(dateTimeStrg, "yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture);
                }
                asstDtls.AssessmentName = asstVM.AssessmentName;
                asstDtls.AssessmentID = id;
                asstDtls.AssessmentDate = dt;
                asstDtls.DurationMin = asstVM.Duration;
                asstDtls.IsActive = asstVM.IsActive;

                msg = objDL.UpdateAssessment(asstDtls);
            }
            catch (Exception ex)
            {
                msg = "exception";

                UtilityFeatures utObj = UtilityFeatures.GetInstance();
                utObj.LogErrorMessage(ex.Message + "\n" + ex.StackTrace);
            }
            return msg;
        }

        public AssessmentUserIDViewModel GetAllAssesmentAndCandidate()
        {
            AssessmentUserIDViewModel objAstUsr = new AssessmentUserIDViewModel();            
            try
            {
                objAstUsr = objDL.GetAllAssesmentAndCandidate();
            }
            catch (Exception ex)
            {
                UtilityFeatures utObj = UtilityFeatures.GetInstance();
                utObj.LogErrorMessage(ex.Message + "\n" + ex.StackTrace);
            }
            return objAstUsr;
        }
        public string AddAssessmentUserMapping(int asstID, List<int> usrToBeMapped, string createdBy)
        {
            string msg = string.Empty;           
            try
            {
                msg = objDL.AddAssessmentUserMapping(asstID, usrToBeMapped,createdBy);
            }
            catch (Exception ex)
            {
                msg = "exception";

                UtilityFeatures utObj = UtilityFeatures.GetInstance();
                utObj.LogErrorMessage(ex.Message + "\n" + ex.StackTrace);
            }
            return msg;
        }
    }
}
