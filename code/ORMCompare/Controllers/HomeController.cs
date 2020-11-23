using ORMCompare.DataLayer;
using ORMCompare.Infrastructure;
using ORMCompare.Models;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace ORMCompare.Controllers
{
    public class HomeController : Controller
    {
        #region -- Action Methods --

        /// <summary>
        /// Landing page
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Returns comparison for different ORMs like Entity Framework, 
        /// Entity Framework Core, Dapper, NHibernate and ADO.Net
        /// </summary>
        /// <returns></returns>
        public JsonResult GetComparsionDetails()
        {
            try
            {
                Random random = new Random();

                int teamMemberID = 0, teamID = 0, projectID = 0;

                ADONET adoDotNet = new ADONET(Constants.ConnectionString);
                DapperOrm dapperOrm = new DapperOrm(Constants.ConnectionString);
                EntityFrameworkCoreOrm efCoreOrm = new EntityFrameworkCoreOrm(Constants.ConnectionString);
                EntityFrameworkOrm efOrm = new EntityFrameworkOrm(Constants.ConnectionString);
                NHibernateOrm nHIbernateOrm = new NHibernateOrm();

                List<string[]> dataByMember = new List<string[]>();
                List<string[]> dataByTeam = new List<string[]>();
                List<string[]> dataByProject = new List<string[]>();

                int totalTeamMemberCountByTeamMember = 0,
                    totlaTeamMemberCountByTeam = 0,
                    totalTeamMemberCountByProject = 0;

                // Get test result of single team member fetch by id
                // from different ORMs for x iterations
                for (var i = 1; i <= Constants.IterationCount; i++)
                {
                    teamMemberID = random.Next(1, Constants.TotalTeamMembers);
                    string[] data = new string[6];

                    TestResult efResult = GetTestResultsByMember(efOrm, teamMemberID);

                    data[0] = i.ToString();
                    totalTeamMemberCountByTeamMember = efResult.MemberCount;
                    data[1] = efResult.Time.ToString();

                    TestResult efCoreResult = GetTestResultsByMember(efCoreOrm, teamMemberID);
                    data[2] = efCoreResult.Time.ToString();

                    TestResult dapperResult = GetTestResultsByMember(dapperOrm, teamMemberID);
                    data[3] = dapperResult.Time.ToString();

                    TestResult nHIbernateResult = GetTestResultsByMember(nHIbernateOrm, teamMemberID);
                    data[4] = nHIbernateResult.Time.ToString();

                    TestResult adoResult = GetTestResultsByMember(adoDotNet, teamMemberID);
                    data[5] = adoResult.Time.ToString();

                    dataByMember.Add(data);
                }

                // Get test results of team members fetch of a single team by team id
                // from different ORMs for x iterations
                for (var i = 1; i <= Constants.IterationCount; i++)
                {
                    teamID = random.Next(1, Constants.TotalTeams);
                    string[] data = new string[6];

                    TestResult efResult = GetTestResultsByTeam(efOrm, teamID);

                    data[0] = i.ToString();
                    totlaTeamMemberCountByTeam = efResult.MemberCount;
                    data[1] = efResult.Time.ToString();

                    TestResult efCoreResult = GetTestResultsByTeam(efCoreOrm, teamID);
                    data[2] = efCoreResult.Time.ToString();

                    TestResult dapperResult = GetTestResultsByTeam(dapperOrm, teamID);
                    data[3] = dapperResult.Time.ToString();

                    TestResult nHIbernateResult = GetTestResultsByTeam(nHIbernateOrm, teamID);
                    data[4] = nHIbernateResult.Time.ToString();

                    TestResult adoResult = GetTestResultsByTeam(adoDotNet, teamID);
                    data[5] = adoResult.Time.ToString();

                    dataByTeam.Add(data);
                }

                // Get test results of all team member fetch of all teams which are working on 
                // specific project by project id for different ORMs for x iterations
                for (var i = 1; i <= Constants.IterationCount; i++)
                {
                    projectID = random.Next(1, Constants.TotalProjects);
                    string[] data = new string[6];

                    TestResult efResult = GetTestResultsByProject(efOrm, projectID);

                    data[0] = i.ToString();
                    totalTeamMemberCountByProject = efResult.MemberCount;

                    data[1] = efResult.Time.ToString();

                    TestResult efCoreResult = GetTestResultsByProject(efCoreOrm, projectID);
                    data[2] = efCoreResult.Time.ToString();

                    TestResult dapperResult = GetTestResultsByProject(dapperOrm, projectID);
                    data[3] = dapperResult.Time.ToString();

                    TestResult nhibernateResult = GetTestResultsByProject(nHIbernateOrm, projectID);
                    data[4] = nhibernateResult.Time.ToString();

                    TestResult adoResult = GetTestResultsByProject(adoDotNet, projectID);
                    data[5] = adoResult.Time.ToString();

                    dataByProject.Add(data);
                }

                var result = new
                {
                    tableByMember = dataByMember,
                    tableByTeam = dataByTeam,
                    tableByProject = dataByProject,
                    totalProjectCount = Constants.TotalProjects,
                    totalTeamCount = Constants.TotalTeams,
                    totalTeamMemberCount = Constants.TotalTeamMembers,
                    totalMemberFetchedByMember = totalTeamMemberCountByTeamMember,
                    totalMemberFetchedByTeam = totlaTeamMemberCountByTeam,
                    totalMemberFetchedByProject = totalTeamMemberCountByProject
                };

                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(ex.Message, JsonRequestBehavior.AllowGet);
            }
        }

        #endregion

        #region -- Private Methods --

        private TestResult GetTestResultsByMember(IORM ormObj, int teamMemberID)
        {
            return ormObj.GetTeamMemberByID(teamMemberID);
        }

        private TestResult GetTestResultsByTeam(IORM ormObj, int teamID)
        {
            return ormObj.GetTeamMemberByTeam(teamID);
        }

        private TestResult GetTestResultsByProject(IORM ormObj, int projectID)
        {
            return ormObj.GetTeamMemberByProject(projectID);
        }

        #endregion
    }
}