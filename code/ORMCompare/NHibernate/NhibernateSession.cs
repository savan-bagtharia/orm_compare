using NHibernate;
using NHibernate.Cfg;
using System.Web;

namespace ORMCompare.NHibernate
{
    public static class NHibernateHelper
    {
        private static ISessionFactory _sessionFactory;

        private static ISessionFactory SessionFactory
        {
            get
            {
                if (_sessionFactory == null)
                {
                    var configuration = new Configuration();
                    var configurationPath = HttpContext.Current.Server.MapPath(@"~\NHibernate\hibernate.cfg.xml");
                    configuration.SetProperty("connection.connection_string", Constants.ConnectionString);
                    configuration.Configure(configurationPath);

                    var teamMemberConfigurationFile = HttpContext.Current.Server.MapPath(@"~\NHibernate\TeamMember.hbm.xml");
                    configuration.AddFile(teamMemberConfigurationFile);

                    var teamConfigurationFile = HttpContext.Current.Server.MapPath(@"~\NHibernate\Team.hbm.xml");
                    configuration.AddFile(teamConfigurationFile);

                    var projectConfigurationFile = HttpContext.Current.Server.MapPath(@"~\NHibernate\Project.hbm.xml");
                    configuration.AddFile(projectConfigurationFile);

                    _sessionFactory = configuration.BuildSessionFactory();
                    return _sessionFactory;
                };
                return _sessionFactory;
            }
        }

        public static IStatelessSession OpenStatelessSession()
        {
            return SessionFactory.OpenStatelessSession();
        }
    }
}