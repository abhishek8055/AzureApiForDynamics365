using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Client.Services;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.ServiceModel.Description;
using System.Web;

namespace AzureAPI.Connections
{
    public class DynamicsContext
    {
        private static DynamicsContext dynamicsContext;
        public static DynamicsContext dynamicsContextInstance
        {
            get
            {
                if(dynamicsContext == null)
                {
                    dynamicsContext = new DynamicsContext();
                }
                return dynamicsContext;
            }
        }

        public IOrganizationService Connect()
        {
            IOrganizationService organizationService = null;

            try
            {
                ClientCredentials clientCredentials = new ClientCredentials();
                clientCredentials.UserName.UserName = "abhishek@encore911.onmicrosoft.com";
                clientCredentials.UserName.Password = "encore@no18055";

                // For Dynamics 365 Customer Engagement V9.X, set Security Protocol as TLS12
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

                // Get the URL from CRM, Navigate to Settings -> Customizations -> Developer Resources
                // Copy and Paste Organization Service Endpoint Address URL
                organizationService = (IOrganizationService)new OrganizationServiceProxy(new Uri("https://encore911.api.crm8.dynamics.com/XRMServices/2011/Organization.svc"),
                 null, clientCredentials, null);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception caught-" + ex.Message);
            }
            return organizationService;
        }
    }
}