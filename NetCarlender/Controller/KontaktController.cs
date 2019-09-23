using NetCarlender.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using Umbraco.Web.Mvc;

namespace NetWebkonzepteUmbreco.Controller
{
    public class KontaktController : SurfaceController
    {
        public const string PARTIAL_VIEW_FOLDER = "~/Views/Partials/Kontakt/";

        public ActionResult RenderForm()
        {
            return PartialView(PARTIAL_VIEW_FOLDER + "_Kontakt.cshtml");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SubmitForm(M_Kontakt model)
        {
            if (ModelState.IsValid)
            {
                SendEmail(model);
                TempData["ContactSuccess"] = true;
                return RedirectToCurrentUmbracoPage();
            }
            return CurrentUmbracoPage();
        }

        private void SendEmail(M_Kontakt model)
        {
            MailMessage message = new MailMessage(model.Email, "website@installumbraco.web.local");
            message.Subject = string.Format("Nachricht von {0} {1} - {2}", model.Vorname, model.Nachname, model.Email);
            message.Body = model.Nachname;
            SmtpClient client = new SmtpClient("127.0.0.1", 25);
            //client.Send(message);
            // Mit einem angebunden SMTP Server würde die E-Mail nun versendet werden
        }
    }
}