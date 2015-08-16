using System.Web.Mvc;
using Microsoft.Practices.Unity;
using Unity.Mvc5;
using System.Configuration;
using Blog.Repository;
using System.Web;

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
            container.RegisterType<IPictureRepository, EFPictureRepository>(new InjectionConstructor(connectionString));
            container.RegisterType<ICommentRepository, EFCommentRepository>(new InjectionConstructor(connectionString));
            container.RegisterType<ISecurityManager, FormsSecurityManager>();

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}