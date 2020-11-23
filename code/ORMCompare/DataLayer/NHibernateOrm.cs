using NHibernate;
using ORMCompare.Infrastructure;
using ORMCompare.Models;
using ORMCompare.NHibernate;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace ORMCompare.DataLayer
{
    public class NHibernateOrm : IORM
    {
        public TestResult GetTeamMemberByID(int id)
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();
            TestResult result = new TestResult();

            using (IStatelessSession session = NHibernateHelper.OpenStatelessSession())
            {
                NHibernateModel.TeamMember teamMember =
                    session.Get<NHibernateModel.TeamMember>(id);
                _ = new TeamMember()
                {
                    ID = teamMember.Id,
                    DateOfBirth = teamMember.DateOfBirth,
                    FirstName = teamMember.FirstName,
                    LastName = teamMember.LastName,
                    TeamID = teamMember.TeamID
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
            TestResult result = new TestResult();
            List<TeamMember> teamMembers = new List<TeamMember>();

            using (IStatelessSession session = NHibernateHelper.OpenStatelessSession())
            {
                teamMembers = session.QueryOver<NHibernateModel.TeamMember>()
                                .Where(x => x.TeamID == teamId).List()
                                .Select(x => new TeamMember()
                                {
                                    ID = x.Id,
                                    FirstName = x.FirstName,
                                    LastName = x.LastName,
                                    DateOfBirth = x.DateOfBirth,
                                    TeamID = x.TeamID
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
            TestResult result = new TestResult();
            List<TeamMember> teamMembers = new List<TeamMember>();

            using (IStatelessSession session = NHibernateHelper.OpenStatelessSession())
            {
                teamMembers = session.QueryOver<NHibernateModel.TeamMember>()
                                .JoinQueryOver(x => x.Team)
                                .Where(x => x.ProjectID == projectId).List()
                                .Select(x => new TeamMember()
                                {
                                    ID = x.Id,
                                    FirstName = x.FirstName,
                                    LastName = x.LastName,
                                    DateOfBirth = x.DateOfBirth,
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