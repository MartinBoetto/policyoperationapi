using Microsoft.Extensions.DependencyInjection;
using PolicyOperation.ExternalServices;
using PolicyOperation.ExternalServices.Service;
using PolicyOperation.ExternalServices.Interface;
using System.ServiceModel;
using System;
using PolicyOperation.Infrastructure.AppConfiguration;

namespace PolicyOperation.Infrastructure.IoC
{
    public static class ExternalServicesExtensions
    {
        /// <summary>
        /// Registra los servicios externos en el inyector de dependencias
        /// </summary>
        /// <param name="services"></param>
        public static void RegisterExternalServices(this IServiceCollection services)
        {
            //services.AddTransient<INTERFAZ_SERVICIO>(provider => new CLASE_CLIENTE_SERVICIO(new ComplexHttpBinding(),
            //    new EndpointAddress(new Uri(URL_SERVICIO))));

            services.AddTransient<IPolicyForBupIdValidation>(provider => new PolicyForBupIdValidation(
               new EndpointAddress(new Uri(AppConfig.Validateclientcertificates.BaseUrl))));

            services.AddTransient<IPolicyCertificateDatail>(provider => new PolicyCertificateDatail(
               new EndpointAddress(new Uri(AppConfig.PolicyCertificateDatail.BaseUrl))));

            services.AddTransient<IProfileUserData>(provider => new ProfileUserData(
               new EndpointAddress(new Uri(AppConfig.ProfileUserData.BaseUrl))));

            services.AddTransient<IIntermediariesForUser>(provider => new IntermediariesForUser(
               new EndpointAddress(new Uri(AppConfig.IntermediariesForUser.BaseUrl))));

            services.AddTransient<IPoliciesSummary>(provider => new PoliciesSummary(
              new EndpointAddress(new Uri(AppConfig.PoliciesSummary.BaseUrl))));



        }
    }
}
