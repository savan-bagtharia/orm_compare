using ORMCompare.Models;

namespace ORMCompare.Infrastructure
{
    public interface IORM
    {
        /// <summary>
        /// Returns Test Result of time taken to fetch a member 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        TestResult GetTeamMemberByID(int id);

        /// <summary>
        /// Returns Test Result of time taken to fetch members of a team 
        /// and their count
        /// </summary>
        /// <param name="teamId"></param>
        /// <returns></returns>
        TestResult GetTeamMemberByTeam(int teamId);

        /// <summary>
        /// Returns Test Result of time taken to fetch members of all team 
        /// of specific project and their count
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
        TestResult GetTeamMemberByProject(int projectId);
    }
}
