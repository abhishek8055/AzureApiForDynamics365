using AzureAPI.Models;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AzureAPI.Connections
{
    public class AccountAction
    {
        public DynamicsContext dynamicsContext
        {
            get
            {
                return DynamicsContext.dynamicsContextInstance;
            }
        }


        //CREATE ACCOUNT
        public bool CreateAccount(Account account)
        {
            IOrganizationService service = dynamicsContext.Connect();
            bool status = false;
            try
            {
                if(service != null)
                {
                    Entity newAccount = new Entity("account");
                    newAccount.Attributes.Add("name", account.Name);
                    newAccount.Attributes.Add("emailaddress1", account.Email);
                    newAccount.Attributes.Add("telephone1", account.Telephone);
                    newAccount.Attributes.Add("websiteurl", account.WebsiteUrl);
                    var accountId = service.Create(newAccount);
                   
                    if(accountId != null)
                    {
                        status = true;
                    }
                }
            }
            catch(Exception ex)
            {
                //LOG EXCEPTION
            }
            return status;
        }

        //RETRIEVE ACCOUNT
        public Account RetrieveAccount(Account account)
        {
            IOrganizationService service = dynamicsContext.Connect();
            try
            {
                if (service != null)
                {
                    ColumnSet attributes = new ColumnSet("name", "emailaddress1", "telephone1", "websiteurl");
                    Entity dynamicsAccount = service.Retrieve(account.Name, account.Id, attributes);
                    account.Name = dynamicsAccount["name"].ToString();
                    account.WebsiteUrl = dynamicsAccount["websiteurl"].ToString();
                    account.Email = dynamicsAccount["emailaddress1"].ToString();
                    account.Telephone = dynamicsAccount["telephone1"].ToString();
                }
            }
            catch (Exception ex)
            {
                //LOG EXCEPTION
                Console.WriteLine(ex.Message);
            }
            return account;
        }

        //RETRIEVE ALL ACCOUNT
        public IEnumerable<Account> RetrieveAllAccount()
        {
            IOrganizationService service = dynamicsContext.Connect();
            List<Account> accounts = new List<Account>();
            try
            {
                if (service != null)
                {
                    QueryExpression queryExp = new QueryExpression();
                    queryExp.EntityName = "account";
                    queryExp.ColumnSet = new ColumnSet();
                    queryExp.ColumnSet.Columns.Add("name");
                    queryExp.ColumnSet.Columns.Add("emailaddress1");
                    queryExp.ColumnSet.Columns.Add("telephone1");
                    queryExp.ColumnSet.Columns.Add("websiteurl");

                    EntityCollection ec = service.RetrieveMultiple(queryExp);
                    foreach (Entity act in ec.Entities)
                    {
                        accounts.Add(new Account {
                            Id = act.Id,
                            Name = act.Attributes.Contains("name") ? act["name"].ToString() : "",
                            Email =  act.Attributes.Contains("emailaddress1")?act["emailaddress1"].ToString():"",
                            Telephone = act.Attributes.Contains("telephone1") ? act["telephone1"].ToString() : "",
                            WebsiteUrl = act.Attributes.Contains("websiteurl") ? act["websiteurl"].ToString() : ""
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                //LOG EXCEPTION
                Console.WriteLine(ex.Message);
            }
            return accounts;
        }

        //UPDATE ACCOUNT
        public bool UpdateAccount(Account account)
        {
            IOrganizationService service = dynamicsContext.Connect();
            bool status = false;
            try
            {
                if(account == null)
                {
                    return status;
                }

                if (service != null)
                {
                    ColumnSet attributes = new ColumnSet("name", "emailaddress1", "telephone1", "websiteurl");
                    Entity originalAccount = service.Retrieve(account.Name, account.Id, attributes);  
                    originalAccount.Attributes.Add("name", account.Name);
                    originalAccount.Attributes.Add("emailaddress1", account.Email);
                    originalAccount.Attributes.Add("telephone1", account.Telephone);
                    originalAccount.Attributes.Add("websiteurl", account.WebsiteUrl);
                    service.Update(originalAccount);
                    status = true;
                }
            }
            catch (Exception ex)
            {
                //LOG EXCEPTION
                Console.WriteLine(ex.Message);
            }
            return status;
        }

        //DELETE ACCOUNT
        public void DeleteAccount(Account account)
        {
            IOrganizationService service = dynamicsContext.Connect();
            try
            {
                service.Delete("account", account.Id);
            }
            catch (Exception ex)
            {
                //LOG EXCEPTION
                Console.WriteLine(ex.Message);
            }
        }
    }
}