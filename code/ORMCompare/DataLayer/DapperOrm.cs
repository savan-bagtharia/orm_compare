using Dapper;
using ORMCompare.Infrastructure;
using ORMCompare.Models;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;

namespace ORMCompare.DataLayer
{
    public class DapperOrm : IORM
    {
        private string _connectionString { get; set; }

        public DapperOrm(string ConnectionString)
        {
            _connectionString = ConnectionString;
        }

        public TestResult GetTeamMemberByID(int id)
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();
            TestResult result = new TestResult();

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                _ = conn.Query<TeamMember>(@"SELECT * 
                                             FROM [dbo].[TeamMember] 
                                             WHERE [Id] = @ID", new { ID = id })
                                        .FirstOrDefault();
            }

            watch.Stop();
            result.MemberCount = 1;
            result.Time = watch.ElapsedMilliseconds;
            return result;
        }

        public TestResult GetTeamMemberByTeam(int teamId)
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();
            List<TeamMember> teamMembers = new List<TeamMember>();
            TestResult result = new TestResult();

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                teamMembers = conn.Query<TeamMember>(@"SELECT * 
                                                       FROM [dbo].[TeamMember] 
                                                       WHERE [TeamID] = @ID 
                                                       ORDER BY [FirstName]", new { ID = teamId })
                                                     .ToList();
            }

            watch.Stop();
            result.MemberCount = teamMembers.Count;
            result.Time = watch.ElapsedMilliseconds;
            return result;
        }

        public TestResult GetTeamMemberByProject(int projectId)
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();
            List<TeamMember> teamMembers = new List<TeamMember>();
            TestResult result = new TestResult();

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                teamMembers = conn.Query<TeamMember>(@"SELECT [TM].* 
                                                       FROM [dbo].[TeamMember] AS [TM] 
                                                       INNER JOIN [dbo].[Team] AS [T] 
                                                       ON [TM].[TeamID] = [T].[ID] 
                                                       WHERE [T].[ProjectID] = @ID 
                                                       ORDER BY [TM].[FirstName]", new { ID = projectId })
                                                     .ToList();
            }

            watch.Stop();
            result.MemberCount = teamMembers.Count;
            result.Time = watch.ElapsedMilliseconds;
            return result;
        }
    }
}
