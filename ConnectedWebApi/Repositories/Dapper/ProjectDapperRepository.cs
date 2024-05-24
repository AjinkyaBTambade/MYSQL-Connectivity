using System.Data;
using Dapper;
using MySql.Data.MySqlClient;
using TFLPortal.Entities;
using TFLPortal.Repositories.Interfaces;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Threading.Tasks;

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
                try
                {
                    string query = "SELECT id, title, description, startdate, enddate, status FROM projects";
                    return connection.QueryAsync<Project>(query).ContinueWith(task => task.Result.AsList());
                }
                catch (Exception e)
                {
                    // Log the exception here
                    throw;
                }
            }
        }

        public Task<Project> GetProject(int id)
        {
            using (IDbConnection connection = CreateConnection())
            {
                try
                {
                    string query = "SELECT id, title, description, startdate, enddate, status FROM projects WHERE id = @Id";
                    return connection.QueryFirstOrDefaultAsync<Project>(query, new { Id = id });
                }
                catch (Exception e)
                {
                    // Log the exception here
                    throw;
                }
            }
        }

        public Task<bool> Insert(Project project)
        {
            if (project == null)
                throw new ArgumentNullException(nameof(project));

            using (IDbConnection connection = CreateConnection())
            {
                try
                {
                    string query = @"INSERT INTO projects (title, description, startdate, enddate, status) 
                                    VALUES (@Title, @Description, @StartDate, @EndDate, @Status)";
                    return connection.ExecuteAsync(query, project).ContinueWith(task => task.Result > 0);
                }
                catch (Exception e)
                {
                    // Log the exception here
                    throw;
                }
            }
        }

        public Task<bool> Update(Project project)
        {
            if (project == null)
                throw new ArgumentNullException(nameof(project));

            using (IDbConnection connection = CreateConnection())
            {
                try
                {
                    string query = @"UPDATE projects 
                                    SET title = @Title, description = @Description, startdate = @StartDate, 
                                    enddate = @EndDate, status = @Status 
                                    WHERE id = @Id";
                    return connection.ExecuteAsync(query, project).ContinueWith(task => task.Result > 0);
                }
                catch (Exception e)
                {
                    // Log the exception here
                    throw;
                }
            }
        }

        public Task<bool> Delete(int id)
        {
            using (IDbConnection connection = CreateConnection())
            {
                try
                {
                    string query = "DELETE FROM projects WHERE id = @Id";
                    return connection.ExecuteAsync(query, new { Id = id }).ContinueWith(task => task.Result > 0);
                }
                catch (Exception e)
                {
                    // Log the exception here
                    throw;
                }
            }
        }
    }
}
