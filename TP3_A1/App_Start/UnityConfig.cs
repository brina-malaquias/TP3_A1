using System;
using System.Web.Mvc;
using TP3_A1.Models;
using Unity;
using Unity.AspNet.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using System.Web;
using Unity.Injection;

namespace TP3_A1
{
    /// <summary>
    /// Specifies the Unity configuration for the main container.
    /// </summary>
    public static class UnityConfig
    {
        #region Unity Container
        private static Lazy<IUnityContainer> container =
          new Lazy<IUnityContainer>(() =>
          {
              var container = new UnityContainer();
              RegisterTypes(container);
              return container;
          });

        /// <summary>
        /// Configured Unity Container.
        /// </summary>
        public static IUnityContainer Container => container.Value;
        #endregion

        /// <summary>
        /// Registers the type mappings with the Unity container.
        /// </summary>
        /// <param name="container">The unity container to configure.</param>
        /// <remarks>
        /// There is no need to register concrete types such as controllers or
        /// API controllers (unless you want to change the defaults), as Unity
        /// allows resolving a concrete type even if it was not previously
        /// registered.
        /// </remarks>
        public static void RegisterTypes(IUnityContainer container)
        {
            // NOTE: To load from web.config uncomment the line below.
            // Make sure to add a Unity.Configuration to the using statements.
            // container.LoadConfiguration();

            // TODO: Register your type's mappings here.
            // container.RegisterType<IProductRepository, ProductRepository>();
        }

        public static void RegisterComponents()
        {
            var container = new UnityContainer();

            // Registre o ApplicationDbContext para injeção de dependência
            container.RegisterType<ApplicationDbContext>();

            // Registre todos os controllers
            DependencyResolver.SetResolver(new UnityDependencyResolver(container));//

            // Registrar o UserStore e UserManager para o ASP.NET Identity
            container.RegisterType<IUserStore<ApplicationUser>, UserStore<ApplicationUser>>(new InjectionConstructor(typeof(ApplicationDbContext)));
            container.RegisterType<UserManager<ApplicationUser>>(new InjectionConstructor(typeof(IUserStore<ApplicationUser>)));

            // Registrar SignInManager
            //container.RegisterType<SignInManager<ApplicationUser, string>>(new InjectionConstructor(
            //   typeof(UserManager<ApplicationUser>),
            //   typeof(IAuthenticationManager)));

            // Registrar o IAuthenticationManager para autenticação no OWIN
            container.RegisterFactory<IUserStore<ApplicationUser>>(c =>
            {
                return new UserStore<ApplicationUser>(new ApplicationDbContext());
            });

            // Registrando IAuthenticationManager
            container.RegisterType<IAuthenticationManager>(new InjectionFactory(c =>
                HttpContext.Current.GetOwinContext().Authentication));

            // Configuração do Dependency Resolver
            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}

