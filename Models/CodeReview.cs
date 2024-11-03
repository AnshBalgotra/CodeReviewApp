namespace CodeReviewApp.Models
{
    public class CodeReview
    {
        public int codeReviewId { get; set; }
        public int ProjectId { get; set; }
        public int RequirementId { get; set; }
        public string ReviewComment { get; set; }
        public string DeveloperName { get; set; }
        public string ReviewerName { get; set; }
        public string ProjectName { get; set; }
        public string RequirementName { get; set; }
        public DateTime? ReviewDate { get; set; } // Nullable in case of missing dates
    }
}
