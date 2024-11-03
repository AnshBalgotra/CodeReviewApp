using System.Collections.Generic;
using System.Data;
using MySql.Data.MySqlClient;
using CodeReviewApp.Models;

namespace CodeReviewApp.DataAccess
{
    public class ProjectRepository
    {
        private readonly string _connectionString;

        public ProjectRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public List<Project> GetProjects()
        {
            List<Project> projects = new List<Project>();

            try
            {
                using (MySqlConnection connection = new MySqlConnection(_connectionString))
                {
                    using (MySqlCommand command = new MySqlCommand("GetProjects", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        connection.Open();

                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                projects.Add(new Project
                                {
                                    Id = reader.GetInt32("ProjectId"),
                                    Name = reader.GetString("ProjectName")
                                });
                            }
                        }
                    }
                }
            }
            catch (MySqlException ex)
            {
                Console.WriteLine($"MySQL Error fetching projects: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching projects: {ex.Message}");
            }

            return projects;
        }

        public List<Requirement> GetRequirements()
        {
            List<Requirement> requirements = new List<Requirement>();

            try
            {
                using (MySqlConnection connection = new MySqlConnection(_connectionString))
                {
                    using (MySqlCommand command = new MySqlCommand("GetRequirements", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        connection.Open();

                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                requirements.Add(new Requirement
                                {
                                    Id = reader.GetInt32("RequirementId"),
                                    Name = reader.GetString("RequirementName")
                                });
                            }
                        }
                    }
                }
            }
            catch (MySqlException ex)
            {
                Console.WriteLine($"MySQL Error fetching requirements: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching requirements: {ex.Message}");
            }

            return requirements;
        }

        public List<CodeReview> GetCodeReviews(int projectId, int requirementId)
{
    List<CodeReview> codeReviews = new List<CodeReview>();

    try
    {
        using (MySqlConnection connection = new MySqlConnection(_connectionString))
        {
            using (MySqlCommand command = new MySqlCommand("GetCodeReviews", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("selectedProjectId", projectId);
                command.Parameters.AddWithValue("selectedRequirementId", requirementId);

                connection.Open();

                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var review = new CodeReview
                        {
                            ProjectId = projectId,
                            RequirementId = requirementId,
                            codeReviewId = reader.GetInt32("CodeReviewId"),
                            ProjectName = reader.GetString("ProjectName"),
                            RequirementName = reader.GetString("RequirementName"),
                            ReviewComment = reader.GetString("ReviewComment"),
                            DeveloperName = reader.GetString("DeveloperName"),
                            ReviewDate = reader.IsDBNull(reader.GetOrdinal("ReviewDate")) 
                                     ? (DateTime?)null 
                                     : reader.GetDateTime("ReviewDate"), 
                            ReviewerName = reader.GetString("ReviewerName"),
                        };
                        Console.WriteLine($"Review found: {review.ReviewComment}");
                        
                        codeReviews.Add(review);
                    }
                }
            }
        }
    }
    catch (MySqlException ex)
    {
        Console.WriteLine($"MySQL Error fetching code reviews: {ex.Message}");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error fetching code reviews: {ex.Message}");
    }

    return codeReviews;
}

    }
}
