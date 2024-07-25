using System.Data;
using Dapper;
using TFLPortal.Entities;
using MySql.Data.MySqlClient;
using TFLPortal.Repositories.Interfaces;

namespace TFLPortal.Repositories
{
    public class ProjectDapperRepository : IProjectRepository
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;

        public ProjectDapperRepository(IConfiguration configuration)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _connectionString = _configuration.GetConnectionString("DefaultConnection")
                                ?? throw new ArgumentNullException("connectionString");
        }

        private IDbConnection CreateConnection()
        {
            return new MySqlConnection(_connectionString);
        }

        public Task<List<Project>> GetProjects()
        {
            using (IDbConnection connection = CreateConnection())
            {
                string query = "SELECT id, title, description, startdate, enddate, status FROM projects";
                var result = connection.Query<Project>(query).AsList();
                return Task.FromResult(result);
            }
        }

        public Project GetProject(int id)
        {
            using (IDbConnection connection = CreateConnection())
            {
                string query = "SELECT id, title, description, startdate, enddate, status FROM projects WHERE id = @Id";
                return connection.QueryFirstOrDefault<Project>(query, new { Id = id });
            }
        }

        public bool Insert(Project project)
        {
            if (project == null)
                throw new ArgumentNullException(nameof(project));

            using (IDbConnection connection = CreateConnection())
            {
                string query = @"INSERT INTO projects (title, description, startdate, enddate, status) 
                                 VALUES (@Title, @Description, @StartDate, @EndDate, @Status)";
                int rowsAffected = connection.Execute(query, project);
                return rowsAffected > 0;
            }
        }

        public bool Update(Project project)
        {
            if (project == null)
                throw new ArgumentNullException(nameof(project));

            using (IDbConnection connection = CreateConnection())
            {
                string query = @"UPDATE projects 
                                 SET title = @Title, description = @Description, startdate = @StartDate, 
                                 enddate = @EndDate, status = @Status 
                                 WHERE id = @Id";
                int rowsAffected = connection.Execute(query, project);
                return rowsAffected > 0;
            }
        }

        public bool Delete(int id)
        {
            using (IDbConnection connection = CreateConnection())
            {
                string query = "DELETE FROM projects WHERE id = @Id";
                int rowsAffected = connection.Execute(query, new { Id = id });
                return rowsAffected > 0;
            }
        }
    }
}
