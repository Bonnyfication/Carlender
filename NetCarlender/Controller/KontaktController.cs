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
                if (SendEmail(model))
                {
                    TempData["ContactSuccess"] = true;
                }
                else
                {
                    TempData["ContactSuccess"] = false;
                }
                return RedirectToCurrentUmbracoPage();
            }
            return CurrentUmbracoPage();
        }

        private bool SendEmail(M_Kontakt model)
        {
            try
            {
                MailMessage message = new MailMessage("carlender@bonnyfication.com", model.Email);
                message.Subject = string.Format("Nachricht von {0} {1} - {2}", model.Vorname, model.Nachname, model.Email);
                message.Body = model.Nachname;
                SmtpClient client = new SmtpClient();
                client.Send(message);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

            // Mit einem angebunden SMTP Server würde die E-Mail nun versendet werden
        }
    }
}