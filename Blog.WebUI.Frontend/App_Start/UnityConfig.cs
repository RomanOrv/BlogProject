using System.Web.Mvc;
using Microsoft.Practices.Unity;
using Unity.Mvc5;
using System.Configuration;
using Blog.Repository;

namespace Blog.WebUI.Frontend
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();
            string connectionString = ConfigurationManager.ConnectionStrings["BlogEntities"].ConnectionString;
            container.RegisterType<IArticleRepository, EFArticleRepository>(new InjectionConstructor(connectionString));
            container.RegisterType<IUserRepository, EFUserRepository>(new InjectionConstructor(connectionString));
            
            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}