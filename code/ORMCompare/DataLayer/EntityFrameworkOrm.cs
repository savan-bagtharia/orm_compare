using ORMCompare.Infrastructure;
using ORMCompare.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;

namespace ORMCompare.DataLayer
{
    public class EntityFrameworkOrm : IORM
    {
        private string _connectionString { get; set; }

        public EntityFrameworkOrm(string ConnectionString)
        {
            _connectionString = ConnectionString;
        }

        public TestResult GetTeamMemberByID(int id)
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();
            TestResult result = new TestResult();

            using (EfDbContext context = new EfDbContext(_connectionString))
            {
                var teamMemberEF = context.TeamMembers.Find(id);
                _ = new TeamMember()
                {
                    ID = teamMemberEF.ID,
                    DateOfBirth = teamMemberEF.DateOfBirth,
                    FirstName = teamMemberEF.FirstName,
                    LastName = teamMemberEF.LastName,
                    TeamID = teamMemberEF.TeamID
                };
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

            using (EfDbContext context = new EfDbContext(_connectionString))
            {
                teamMembers = context.TeamMembers
                                .AsNoTracking().Where(x => x.TeamID == teamId)
                                .Select(x => new TeamMember
                                {
                                    ID = x.ID,
                                    TeamID = x.TeamID,
                                    LastName = x.LastName,
                                    FirstName = x.FirstName,
                                    DateOfBirth = x.DateOfBirth
                                }).OrderBy(x => x.FirstName).ToList();
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

            using (EfDbContext context = new EfDbContext(_connectionString))
            {
                teamMembers = context.Teams.AsNoTracking()
                                .Include(x => x.TeamMembers)
                                .Where(x => x.ProjectID == projectId)
                                .SelectMany(x => x.TeamMembers)
                                .Select(x => new TeamMember
                                {
                                    ID = x.ID,
                                    DateOfBirth = x.DateOfBirth,
                                    FirstName = x.FirstName,
                                    LastName = x.LastName,
                                    TeamID = x.TeamID
                                }).OrderBy(x => x.FirstName).ToList();
            }

            watch.Stop();
            result.MemberCount = teamMembers.Count;
            result.Time = watch.ElapsedMilliseconds;
            return result;
        }
    }
}
