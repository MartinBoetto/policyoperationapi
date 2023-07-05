using PolicyOperation.Infrastructure.AppConfiguration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using static PolicyOperation.Infrastructure.AppConfiguration.AppConfig;
using MediatR;
using PolicyOperation.Core;
using PolicyOperation.Core.Entidad.Policy;
using PolicyOperation.Core.Behavior;

namespace PolicyOperation.Infrastructure.IoC
{
    public static class SettingsExtensions
    {
        public static void BindAppSettings(this IServiceCollection services, IConfiguration configuration)
        {
            var appConfig = new AppConfig();
            var connectionStrings = new AppConfig.ConnectionStrings();
            var logging = new AppConfig.Logging();
            var validateclientcertificates = new AppConfig.Validateclientcertificates();
            var policyCertificateDatail = new AppConfig.PolicyCertificateDatail();
            var profileUserData = new AppConfig.ProfileUserData();
            var intermediariesForUser = new AppConfig.IntermediariesForUser();
            var policiesSummary = new AppConfig.PoliciesSummary();

            //Bindea el .config a la clase de forma automática, segun los nombres de propiedades
            configuration.Bind(nameof(AppConfig.Logging), logging);
            configuration.Bind(nameof(AppConfig.Validateclientcertificates), validateclientcertificates);
            configuration.Bind(nameof(AppConfig.PolicyCertificateDatail), policyCertificateDatail);
            configuration.Bind(nameof(AppConfig.ProfileUserData), profileUserData);
            configuration.Bind(nameof(AppConfig.IntermediariesForUser), intermediariesForUser);
            configuration.Bind(nameof(AppConfig.PoliciesSummary), policiesSummary);

            //inyectar al site
            services.AddSingleton(appConfig);
            services.AddSingleton(logging);
            services.AddSingleton(validateclientcertificates);
            services.AddSingleton(policyCertificateDatail);
            services.AddMemoryCache();
            services.AddSingleton(intermediariesForUser);
            services.AddSingleton(policiesSummary);

            
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LogsBehavior<,>));
            services.AddHttpContextAccessor();
        }
    }
}

