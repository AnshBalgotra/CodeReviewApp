using Microsoft.AspNetCore.Mvc;
using CodeReviewApp.Models;
using CodeReviewApp.DataAccess;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace CodeReviewApp.Controllers
{
    public class CodeReviewController : Controller
    {
        private readonly ProjectRepository _projectRepository;
         private readonly string connectionString = "Server=127.0.0.1;Database=CodeReviewDB;Uid=root;Pwd=Ansh@2003;";

        public CodeReviewController()
        {
        //    string connectionString = "Server=127.0.0.1;Database=CodeReviewDB;Uid=root;Pwd=Ansh@2003;";
            _projectRepository = new ProjectRepository(connectionString);
        }

        public IActionResult Index()
        {
            var model = new CodeReviewViewModel
            {
                Projects = _projectRepository.GetProjects(),
                Requirements = _projectRepository.GetRequirements(),
                CodeReviews = new List<CodeReview>() 
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult LoadCodeReviews(int projectId, int requirementId)
        {
            var model = new CodeReviewViewModel
            {
                Projects = _projectRepository.GetProjects(),
                Requirements = _projectRepository.GetRequirements(),
                CodeReviews = _projectRepository.GetCodeReviews(projectId, requirementId)
            };

            return View("Index", model); 
        }
        [HttpPost]
public IActionResult UpdateReview(int codeReviewId, string reviewComment)
{
    Console.WriteLine($"Received codeReviewId: {codeReviewId}, reviewComment: {reviewComment}");

    using (MySqlConnection connection = new MySqlConnection(connectionString))
    {
        try
        {
            connection.Open();

            string updateQuery = "UPDATE CodeReviews SET ReviewComment = @ReviewComment WHERE CodeReviewId = @CodeReviewId";
            MySqlCommand cmd = new MySqlCommand(updateQuery, connection);
            
            cmd.Parameters.AddWithValue("@CodeReviewId", codeReviewId);
            cmd.Parameters.AddWithValue("@ReviewComment", reviewComment);

            int rowsAffected = cmd.ExecuteNonQuery();

            if (rowsAffected > 0)
            {
                Console.WriteLine("Update successful.");
                return Json(new { success = true });
            }
            else
            {
                Console.WriteLine("No rows were affected; review ID may not exist.");
                return Json(new { success = false, message = "Review not found" });
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error during update: {ex.Message}");
            return Json(new { success = false, message = ex.Message });
        }
    }
}


    }
}
