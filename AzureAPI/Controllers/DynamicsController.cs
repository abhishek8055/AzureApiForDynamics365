using AzureAPI.Connections;
using AzureAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace AzureAPI.Controllers
{
    public class DynamicsController : ApiController
    {
        private readonly AccountAction accountContext;
        public DynamicsController()
        {
            accountContext = new AccountAction();
        }


        //GET 
        //  api/dynamics/GetAccounts
        [HttpGet]
        public IHttpActionResult GetAccounts()
        {
            List<Account> accounts = new List<Account>();
            try
            {
                accounts = accountContext.RetrieveAllAccount().ToList();
                return Ok(accounts);
            }
            catch(Exception ex)
            {
                return NotFound();
            }
        }

        //GET
        //  api/dynamics/GetAccount/{id}
        [HttpGet]
        public IHttpActionResult GetAccount(Account accountDB)
        {
            try
            {
                Account account = new Account();
                account = accountContext.RetrieveAccount(accountDB);
                if(account == null)
                {
                    return NotFound();
                }
                return Ok(account);
            }
            catch (Exception ex)
            {
                return NotFound();
            }
        }

        //POST
        //  api/dynamics/CreateAccount
        [HttpPost]
        public IHttpActionResult CreateAccount(Account account)
        {
            try
            {
                bool status = accountContext.CreateAccount(account);
                return Created(new Uri(Request.RequestUri + "/" + account.Id), account);
            }
            catch (Exception ex)
            {
                return NotFound();
            }
        }

        //POST
        //  api/dynamics/UpdateAccount/{id}
        [HttpPut]
        public IHttpActionResult UpdateAccount(Account account)
        {
            bool status;
            try
            {
                if(account == null)
                {
                    return BadRequest();
                }
                status = accountContext.UpdateAccount(account);
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
            if (status)
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        //DELETE
        //  api/dynamics/DeleteAccount/{id}
        public IHttpActionResult DeleteAccount(Account account)
        {
            if(account == null)
            {
                return BadRequest();
            }
            try
            {
                accountContext.DeleteAccount(account);
                return Ok();
            }
            catch (Exception ex)
            {
                return NotFound();
            }
        }
    }
}
