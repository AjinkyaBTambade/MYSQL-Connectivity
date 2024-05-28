using MySql.Data.MySqlClient;
using TFLPortal.Entities;
using TFLPortal.Repositories.Interfaces;
namespace TFLPortal.Repositories;
public class ProjectRepository : IProjectRepository
{
    private readonly IConfiguration _configuration;

    private readonly string _connectionString;

    public ProjectRepository(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task<bool> Update(Project project)
    {
        bool status = false;
        using (MySqlConnection connection = new MySqlConnection(_configuration.GetConnectionString("DefaultConnection")))
        {
            try
            {
                await connection.OpenAsync();
                string query = "UPDATE projects SET title = @Title, description = @Description WHERE id = @Id";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@Title", project.Title);
                command.Parameters.AddWithValue("@Description", project.Description);
                command.Parameters.AddWithValue("@Id", project.Id);
                int rowsAffected = await command.ExecuteNonQueryAsync();
                status = rowsAffected > 0;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw e;
            }
        }
        return status;
    }


    public async Task<List<Project>> GetProjects()
    {
        List<Project> projects = new List<Project>();
        using (MySqlConnection connection = new MySqlConnection(_configuration.GetConnectionString("DefaultConnection")))
        {
            try
            {
                await connection.OpenAsync();
                string query = "SELECT id, title, description, startdate, enddate, status FROM projects";
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    using (MySqlDataReader reader = (MySqlDataReader)await command.ExecuteReaderAsync())
                    {

                        {
                            while (await reader.ReadAsync())
                            {
                                int id = reader.GetInt32("id");
                                string title = reader.GetString("title");
                                string description = reader.GetString("description");
                                DateTime startdate = reader.GetDateTime("startdate");
                                DateTime enddate = reader.GetDateTime("enddate");
                                string status = reader.GetString("status");

                                Project project = new Project()
                                {
                                    Id = id,
                                    Title = title,
                                    Description = description,
                                    StartDate = startdate,
                                    EndDate = enddate,
                                    Status = status
                                };
                                projects.Add(project);
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        return projects;
    }

    public Project GetProject(int id)
    {
        Project project = null;
        using (MySqlConnection connection = new MySqlConnection(_configuration.GetConnectionString("DefaultConnection")))
        {
            try
            {
                connection.Open();
                string query = "SELECT * FROM projects WHERE id = @Id";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@Id", id);
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        project = new Project()
                        {
                            Id = reader.GetInt32("id"),
                            Title = reader.GetString("title"),
                            Description = reader.GetString("description"),
                            StartDate = reader.GetDateTime("startdate"),
                            EndDate = reader.GetDateTime("enddate"),
                            Status = reader.GetString("status")
                        };
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw e;
            }
        }
        return project;
    }

    public bool Insert(Project project)
    {
        bool status = false;
        using (MySqlConnection connection = new MySqlConnection(_configuration.GetConnectionString("DefaultConnection")))
        {
            try
            {
                connection.Open();
                string query = "INSERT INTO projects (title, description, startdate, enddate, status) VALUES (@Title, @Description, @StartDate, @EndDate, @Status)";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@Title", project.Title);
                command.Parameters.AddWithValue("@Description", project.Description);
                command.Parameters.AddWithValue("@StartDate", project.StartDate);
                command.Parameters.AddWithValue("@EndDate", project.EndDate);
                command.Parameters.AddWithValue("@Status", project.Status);
                int rowsAffected = command.ExecuteNonQuery();
                status = rowsAffected > 0;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw e;
            }
        }
        return status;
    }

    public bool Delete(int id)
    {
        bool status = false;
        using (MySqlConnection connection = new MySqlConnection(_configuration.GetConnectionString("DefaultConnection")))
        {
            try
            {
                connection.Open();
                string query = "DELETE FROM projects WHERE id = @Id";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@Id", id);
                int rowsAffected = command.ExecuteNonQuery();
                status = rowsAffected > 0;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw e;
            }
        }
        return status;
    }
}

