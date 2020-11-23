using System;
using System.Configuration;

namespace ORMCompare
{
    public static class Constants
    {
        /// <summary>
        /// User need to set value as per number of rows in TeamMember table
        /// </summary>
        public static int TotalTeamMembers = 1000000;

        /// <summary>
        /// User need to set value as per number of rows in Team table
        /// </summary>
        public static int TotalTeams = 1000;

        /// <summary>
        /// User need to set value as per number of rows in Project table
        /// </summary>
        public static int TotalProjects = 500;

        public static int IterationCount = 10;

        public static string ConnectionString = ConfigurationManager.ConnectionStrings["ORMCompare"].ConnectionString;
    }

}