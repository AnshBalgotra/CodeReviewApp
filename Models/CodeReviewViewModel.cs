using System.Collections.Generic;

namespace CodeReviewApp.Models
{
    public class CodeReviewViewModel
    {
        public List<Project> Projects { get; set; }
        public List<Requirement> Requirements { get; set; }
        public List<CodeReview> CodeReviews { get; set; }
    }
}
