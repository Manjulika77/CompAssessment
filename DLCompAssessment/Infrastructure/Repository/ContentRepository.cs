using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility.Model;
using Utility.Utility;
using DLCompAssessment.Infrastructure.Abstract;

namespace DLCompAssessment.Infrastructure.Repository
{
    public class ContentRepository : IContentRepository
    {
        SqlConnection con = new SqlConnection(ConfigurationSettings.AppSettings["conString"]);

        public List<AssessmentDetails> GetAllAssesment()
        {
            List<AssessmentDetails> lstAssesmnt = new List<AssessmentDetails>();
            SqlCommand com = new SqlCommand("GetAllAssesment", con);
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
                        AssessmentDetails objAssesmnt = new AssessmentDetails();

                        objAssesmnt.AssessmentID = Convert.ToInt32(eachRow["AssessmentID"]);
                        objAssesmnt.AssessmentName = eachRow["AssessmentName"].ToString();
                        objAssesmnt.AssessmentDate = Convert.ToDateTime(eachRow["AssessmentDate"]);
                        objAssesmnt.CreatedDate = Convert.ToDateTime(eachRow["CreatedDate"]);
                        objAssesmnt.CreatedBy = eachRow["CreatedBy"].ToString();
                        objAssesmnt.DurationMin = Convert.ToInt32(eachRow["DurationMin"]);
                        objAssesmnt.IsActive = Convert.ToBoolean(eachRow["IsActive"]);
                        lstAssesmnt.Add(objAssesmnt);
                    }
                }
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
            SqlCommand com = new SqlCommand("GetAllCompetency", con);
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
                        CompetencyDetails objComp = new CompetencyDetails();

                        objComp.CompID = Convert.ToInt32(eachRow["CompID"]);
                        objComp.CompName = eachRow["CompName"].ToString();
                        objComp.CreatedDate = Convert.ToDateTime(eachRow["CreatedDate"]);
                        lstComp.Add(objComp);
                    }
                }
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
            List<CompetencyDetails> lstComp = new List<CompetencyDetails>();
            List<AssessmentDetails> lstAst = new List<AssessmentDetails>();

            SqlCommand com = new SqlCommand("GetAssesmentCompetency", con);
            com.CommandType = CommandType.StoredProcedure;

            DataSet dsData = new DataSet();
            try
            {
                SqlDataAdapter da = new SqlDataAdapter(com);
                da.Fill(dsData);

                if (dsData.Tables.Count == 2)
                {
                    foreach (DataRow eachAstRow in dsData.Tables[0].Rows)
                    {
                        AssessmentDetails objAssesmnt = new AssessmentDetails();

                        objAssesmnt.AssessmentID = Convert.ToInt32(eachAstRow["AssessmentID"]);
                        objAssesmnt.AssessmentName = eachAstRow["AssessmentName"].ToString();
                        objAssesmnt.AssessmentDate = Convert.ToDateTime(eachAstRow["AssessmentDate"]);
                        objAssesmnt.CreatedDate = Convert.ToDateTime(eachAstRow["CreatedDate"].ToString());
                        lstAst.Add(objAssesmnt);
                    }
                    foreach (DataRow eachCompRow in dsData.Tables[1].Rows)
                    {
                        CompetencyDetails objComp = new CompetencyDetails();

                        objComp.CompID = Convert.ToInt32(eachCompRow["CompID"]);
                        objComp.CompName = eachCompRow["CompName"].ToString();
                        objComp.CreatedDate = Convert.ToDateTime(eachCompRow["CreatedDate"]);
                        lstComp.Add(objComp);
                    }

                    objCompAst.assessmentDetails = lstAst;
                    objCompAst.competencyDetails = lstComp;
                }
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
            SqlCommand com = new SqlCommand("InsertCaseStudy", con);
            com.CommandType = CommandType.StoredProcedure;
            try
            {
                com.Parameters.AddWithValue("@AssessmentID", caseStudy.AssessmentID);
                com.Parameters.AddWithValue("@caseStudy", caseStudy.caseStudy);
                com.Parameters.AddWithValue("@caseStudySolutions_1", caseStudy.caseStudySolutions_1);
                com.Parameters.AddWithValue("@caseStudySolutions_2", caseStudy.caseStudySolutions_2);
                com.Parameters.AddWithValue("@caseStudySolutions_3", caseStudy.caseStudySolutions_3);
                com.Parameters.AddWithValue("@caseStudySolutions_4", caseStudy.caseStudySolutions_4);
                com.Parameters.AddWithValue("@CompID_1", caseStudy.CompID_1);
                com.Parameters.AddWithValue("@CompID_2", caseStudy.CompID_2);
                com.Parameters.AddWithValue("@CompID_3", caseStudy.CompID_3);
                com.Parameters.AddWithValue("@CompID_4", caseStudy.CompID_4);
                com.Parameters.AddWithValue("@CreatedBy", caseStudy.CreatedBy);

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
        public List<CaseStudyListViewModel> GetAllCaseStudy()
        {
            List<CaseStudyListViewModel> lstCaseSdy = new List<CaseStudyListViewModel>();

            SqlCommand com = new SqlCommand("GetAllCaseStudy", con);
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
                        CaseStudyListViewModel objCaseSdy = new CaseStudyListViewModel();

                        objCaseSdy.CSID = Convert.ToInt32(eachRow["CSID"]);
                        objCaseSdy.CaseStudy = eachRow["CaseStudy"].ToString();
                        objCaseSdy.AssessmentName = eachRow["AssessmentName"].ToString(); ;
                        objCaseSdy.ReviewStatus = Convert.ToBoolean(eachRow["ReviewStatus"]);
                        objCaseSdy.CreatedBy = eachRow["CreatedBy"].ToString();
                        objCaseSdy.CreatedDate = Convert.ToDateTime(eachRow["CreatedDate"]);
                        lstCaseSdy.Add(objCaseSdy);
                    }
                }
            }
            catch(Exception ex)
            {
                UtilityFeatures utObj = UtilityFeatures.GetInstance();
                utObj.LogErrorMessage(ex.Message + "\n" + ex.StackTrace);
            }

            return lstCaseSdy;
        }

        public CaseStudySolutionReviewViewModel GetCaseStudySolnToReview(int caseSdyID)
        {
            CaseStudySolutionReviewViewModel objCaseSolnReview = new CaseStudySolutionReviewViewModel();
            List<CaseStudySolutionComptViewModel> lstCaseCompt = new List<CaseStudySolutionComptViewModel>();
            SqlCommand com = new SqlCommand("GetCaseStudySolnById", con);
            com.CommandType = CommandType.StoredProcedure;

            com.Parameters.AddWithValue("@cSID", caseSdyID);

            DataSet dsData = new DataSet();
            try
            {
                SqlDataAdapter da = new SqlDataAdapter(com);
                da.Fill(dsData);

                if (dsData.Tables.Count == 2)
                {
                    foreach (DataRow eachSolnRevRow in dsData.Tables[0].Rows)
                    {
                        objCaseSolnReview.CaseStudy = eachSolnRevRow["CaseStudy"].ToString();
                        objCaseSolnReview.CreatedBy = eachSolnRevRow["CreatedBy"].ToString();
                    }
                    foreach (DataRow eachCompRow in dsData.Tables[1].Rows)
                    {
                        CaseStudySolutionComptViewModel objSolnCompt = new CaseStudySolutionComptViewModel();

                        objSolnCompt.CSSID = Convert.ToInt32(eachCompRow["CSSID"]);
                        objSolnCompt.Solution = eachCompRow["Solution"].ToString();
                        objSolnCompt.CompName = eachCompRow["CompName"].ToString();
                        objSolnCompt.CreatedDate = Convert.ToDateTime(eachCompRow["CreatedDate"]);
                        lstCaseCompt.Add(objSolnCompt);
                    }

                    objCaseSolnReview.CSID = caseSdyID;
                    objCaseSolnReview.CaseSdyCompt = lstCaseCompt;
                }
            }
            catch(Exception ex)
            {
                UtilityFeatures utObj = UtilityFeatures.GetInstance();
                utObj.LogErrorMessage(ex.Message + "\n" + ex.StackTrace);
            }
            return objCaseSolnReview;
        }

        public string InsertCaseSdyReview(int csid, string reviewComment)
        {
            string msg = string.Empty;
            SqlCommand com = new SqlCommand("InsertCaseStudyReview", con);
            com.CommandType = CommandType.StoredProcedure;
            try
            {
                com.Parameters.AddWithValue("@cSID", csid);
                com.Parameters.AddWithValue("@reviewComment", reviewComment);

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
                UtilityFeatures utObj = UtilityFeatures.GetInstance();
                utObj.LogErrorMessage(ex.Message + "\n" + ex.StackTrace);
            }
            return msg;
        }

        public string CreateAssessment(AssessmentDetails asstDtls)
        {
            string msg = string.Empty;
            SqlCommand com = new SqlCommand("InsertAssessment", con);
            com.CommandType = CommandType.StoredProcedure;
            try
            {
                com.Parameters.AddWithValue("@assessmentName", asstDtls.AssessmentName);
                com.Parameters.AddWithValue("@assessmentDate", asstDtls.AssessmentDate);
                com.Parameters.AddWithValue("@createdBy", asstDtls.CreatedBy);
                com.Parameters.AddWithValue("@durationMin", asstDtls.DurationMin);
                com.Parameters.AddWithValue("@isActive", asstDtls.IsActive);

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

        public AssessmentDetails GetAssessmentByID(int AsstID)
        {
            AssessmentDetails ObjAsst = new AssessmentDetails();
            SqlCommand com = new SqlCommand("GetAssessmentbyID", con);
            com.CommandType = CommandType.StoredProcedure;

            com.Parameters.AddWithValue("@assessmentID", AsstID);
            DataTable dt = new DataTable();
            try
            {
                SqlDataAdapter da = new SqlDataAdapter(com);
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    ObjAsst.AssessmentName = dt.Rows[0]["AssessmentName"].ToString();
                    ObjAsst.AssessmentDate = Convert.ToDateTime(dt.Rows[0]["AssessmentDate"]);
                    ObjAsst.DurationMin = Convert.ToInt32(dt.Rows[0]["DurationMin"]);
                    ObjAsst.IsActive = Convert.ToBoolean(dt.Rows[0]["IsActive"]);
                    ObjAsst.CreatedBy = dt.Rows[0]["CreatedBy"].ToString();
                }
            }
            catch(Exception ex)
            {
                UtilityFeatures utObj = UtilityFeatures.GetInstance();
                utObj.LogErrorMessage(ex.Message + "\n" + ex.StackTrace);
            }
            return ObjAsst;
        }

        public string UpdateAssessment(AssessmentDetails asstDtls)
        {
            var msg = string.Empty;
            SqlCommand com = new SqlCommand("UpdateAssessment", con);
            com.CommandType = CommandType.StoredProcedure;
            try
            {
                com.Parameters.AddWithValue("@assessmentID", asstDtls.AssessmentID);
                com.Parameters.AddWithValue("@assessmentName", asstDtls.AssessmentName);
                com.Parameters.AddWithValue("@assessmentDate", asstDtls.AssessmentDate);
                com.Parameters.AddWithValue("@durationMin", asstDtls.DurationMin);
                com.Parameters.AddWithValue("@isActive", asstDtls.IsActive);

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
            catch(Exception ex)
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
            List<AssessmentDetails> lstAst = new List<AssessmentDetails>();
            List<UserIDViewModel> lstUsr = new List<UserIDViewModel>();

            SqlCommand com = new SqlCommand("GetAssessmentUser", con);
            com.CommandType = CommandType.StoredProcedure;

            DataSet dsData = new DataSet();
            try
            {
                SqlDataAdapter da = new SqlDataAdapter(com);
                da.Fill(dsData);

                if (dsData.Tables.Count == 2)
                {
                    foreach (DataRow eachAstRow in dsData.Tables[0].Rows)
                    {
                        AssessmentDetails objAssesmnt = new AssessmentDetails();

                        objAssesmnt.AssessmentID = Convert.ToInt32(eachAstRow["AssessmentID"]);
                        objAssesmnt.AssessmentName = eachAstRow["AssessmentName"].ToString();
                        objAssesmnt.AssessmentDate = Convert.ToDateTime(eachAstRow["AssessmentDate"]);
                        objAssesmnt.CreatedDate = Convert.ToDateTime(eachAstRow["CreatedDate"].ToString());
                        lstAst.Add(objAssesmnt);
                    }
                    foreach (DataRow eachCompRow in dsData.Tables[1].Rows)
                    {
                        UserIDViewModel objUsr = new UserIDViewModel();
                        
                        objUsr.userID = Convert.ToInt32(eachCompRow["userID"]);
                        objUsr.userName = eachCompRow["userName"].ToString();
                        
                        lstUsr.Add(objUsr);
                    }

                    objAstUsr.assessmentList = lstAst;
                    objAstUsr.userIDList = lstUsr;
                }
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
                int num = 0;                
                foreach (int i in usrToBeMapped)
                {
                    SqlCommand com = new SqlCommand("InsertAssessmentUserMapping", con);
                    com.CommandType = CommandType.StoredProcedure;
                    com.Parameters.AddWithValue("@assmtID", asstID);
                    com.Parameters.AddWithValue("@createdBy", createdBy);
                    com.Parameters.AddWithValue("@userID", i);
                    con.Open();
                    num = com.ExecuteNonQuery();
                    con.Close();
                }                
                if (num > 0)
                    msg = "success";
                else
                    msg = "failed";
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
