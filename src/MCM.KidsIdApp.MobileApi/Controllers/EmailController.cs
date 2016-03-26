﻿using MCM.KidsIdApp.MobileApp.DataObjects;
using Microsoft.Azure.Mobile.Server.Config;
using SendGrid;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Web.Http;
using System.Threading.Tasks;
using System.Configuration;

namespace MCM.KidsIdApp.MobileApp.Controllers
{
    [MobileAppController]
    public class EmailController : ApiController
    {
        // POST: api/Email
        public HttpResponseMessage Post([FromBody]dynamic value)
        {
            var myMessage = new SendGridMessage();
            myMessage.AddTo(value.emailAddress.ToString());
            myMessage.From = new MailAddress(
                ConfigurationManager.AppSettings["EmailFromAddress"],
                ConfigurationManager.AppSettings["EmailFromName"]
                );
            myMessage.Subject = "Your Details";
            myMessage.Text = value.profile.ToString();

            var transportWeb = new Web(ConfigurationManager.AppSettings["SendGridApiKey"]);
            transportWeb.DeliverAsync(myMessage);
            //this seems awfully happy-path to me, y'know
            HttpResponseMessage m = new HttpResponseMessage(HttpStatusCode.OK);
            return m;            
        }
    }
}
