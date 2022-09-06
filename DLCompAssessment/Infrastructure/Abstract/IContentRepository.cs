using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility.Model;

namespace DLCompAssessment.Infrastructure.Abstract
{
    public interface IContentRepository
    {
        List<AssessmentDetails> GetAllAssesment();
        List<CompetencyDetails> GetAllCompetency();
        CreateCaseStudyViewModel GetAllCompetencyAndAssesment();
        string CreateCaseStudy(CreateCaseStudyViewModel caseStudy);
        List<CaseStudyListViewModel> GetAllCaseStudy();
        CaseStudySolutionReviewViewModel GetCaseStudySolnToReview(int caseSdyID);
        string InsertCaseSdyReview(int csid, string reviewComment);
        string CreateAssessment(AssessmentDetails asstDtls);
        AssessmentDetails GetAssessmentByID(int AsstID);
        string UpdateAssessment(AssessmentDetails asstDtls);
        AssessmentUserIDViewModel GetAllAssesmentAndCandidate();
        string AddAssessmentUserMapping(int asstID, List<int> usrToBeMapped, string createdBy);
    }
}
