 using MySql.Data.MySqlClient;
using System.Data;
using Transflower.TFLAssessment.Entities;
using Transflower.TFLAssessment.Repositories.Interfaces;
using Transflower.TFLAssessment.Handler;
using Dapper;

namespace Transflower.TFLAssessment.Repositories;

 
 public async Task <Assessment> GetDetails(int assessmentId) 
    {
            await Task.Delay(100);
            Assessment assessment=new Assessment();   
            string query = @"select * from tests where id=@AssessmentId";
            using (IDbConnection con = new MySqlConnection(_connectionString))
            {
                assessment= con.QuerySingleOrDefault<Assessment>(query, new {assessmentId}) ;
                TimeOnly time  = assessment.Duration;
                Console.WriteLine(time);
                               
            }
            return assessment;
    }