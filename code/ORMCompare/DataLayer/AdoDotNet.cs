using ORMCompare.Infrastructure;
using ORMCompare.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;

namespace ORMCompare.DataLayer
{
    public class ADONET : IORM
    {
        private string _connectionString { get; set; }

        public ADONET(string connectionString)
        {
            _connectionString = connectionString;
        }

        public TestResult GetTeamMemberByID(int id)
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();
            DataTable table = new DataTable();
            TestResult result = new TestResult();

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                using (SqlDataAdapter adapter = new SqlDataAdapter(@"SELECT * FROM [dbo].[TeamMember] 
                                                                     WHERE [Id] = @ID", conn))
                {
                    adapter.SelectCommand.Parameters.Add(new SqlParameter("@ID", id));
                    adapter.Fill(table);
                }
            }

            _ = new TeamMember()
            {
                ID = Convert.ToInt32(table.Rows[0]["ID"]),
                TeamID = Convert.ToInt32(table.Rows[0]["TeamID"]),
                DateOfBirth = Convert.ToDateTime(table.Rows[0]["DateOfBirth"]),
                FirstName = Convert.ToString(table.Rows[0]["FirstName"]),
                LastName = Convert.ToString(table.Rows[0]["LastName"]),
            };

            watch.Stop();
            result.MemberCount = 1;
            result.Time = watch.ElapsedMilliseconds;
            return result;
        }

        public TestResult GetTeamMemberByTeam(int teamId)
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();
            DataTable table = new DataTable();
            TestResult result = new TestResult();
            List<TeamMember> teamMembers = new List<TeamMember>();

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                using (SqlDataAdapter adapter = new SqlDataAdapter(@"SELECT * FROM [dbo].[TeamMember] 
                                                                     WHERE [TeamID] = @ID 
                                                                     Order By [FirstName]", conn))
                {
                    adapter.SelectCommand.Parameters.Add(new SqlParameter("@ID", teamId));
                    adapter.Fill(table);
                }
            }

            foreach (DataRow row in table.Rows)
            {
                teamMembers.Add(new TeamMember
                {
                    ID = Convert.ToInt32(row["ID"]),
                    FirstName = Convert.ToString(row["FirstName"]),
                    LastName = Convert.ToString(row["LastName"]),
                    TeamID = Convert.ToInt32(row["TeamID"]),
                    DateOfBirth = Convert.ToDateTime(row["DateOfBirth"])
                });
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
            DataTable table = new DataTable();
            TestResult result = new TestResult();
            List<TeamMember> teamMembers = new List<TeamMember>();

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                using (SqlDataAdapter adapter = new SqlDataAdapter(@"SELECT [TM].* 
                                                                     FROM [dbo].[TeamMember] AS [TM] 
                                                                     INNER JOIN [dbo].[Team] AS [T] 
                                                                     ON [TM].[TeamID] = [T].[ID] 
                                                                     WHERE [T].[ProjectID] = @ID 
                                                                     ORDER BY [TM].[FirstName]", conn))
                {
                    adapter.SelectCommand.Parameters.Add(new SqlParameter("@ID", projectId));
                    adapter.Fill(table);
                }
            }

            foreach (DataRow row in table.Rows)
            {
                teamMembers.Add(new TeamMember
                {
                    ID = Convert.ToInt32(row["ID"]),
                    FirstName = Convert.ToString(row["FirstName"]),
                    LastName = Convert.ToString(row["LastName"]),
                    TeamID = Convert.ToInt32(row["TeamID"]),
                    DateOfBirth = Convert.ToDateTime(row["DateOfBirth"])
                });
            }

            watch.Stop();
            result.MemberCount = teamMembers.Count;
            result.Time = watch.ElapsedMilliseconds;
            return result;
        }
    }
}