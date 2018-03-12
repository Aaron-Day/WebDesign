using Lab9;
using System;
using System.Web.Http;

namespace PetAPI
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
            DependencyInjectionConfig.Register();
            AppDomain.CurrentDomain.SetData("DataDirectory", @"C:\Users\aaron\Documents\GitHub\WebDesign\Week 9 Lab\Lab9\Lab9\App_Data");
        }
    }
}
