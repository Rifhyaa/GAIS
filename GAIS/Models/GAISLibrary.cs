using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Reflection;
using System.Web;

namespace GAIS.Models
{
    public class GAISLibrary
    {
        //[System.ComponentModel.DataAnnotations.MetadataType(typeof(VendorMetaData))]
        public static void CopyObject<T>(object sourceObject, ref T destObject)
        {
            //  If either the source, or destination is null, return
            if (sourceObject == null || destObject == null)
                return;

            //  Get the type of each object
            Type sourceType = sourceObject.GetType();
            Type targetType = destObject.GetType();

            //  Loop through the source properties
            foreach (PropertyInfo p in sourceType.GetProperties())
            {
                //  Get the matching property in the destination object
                PropertyInfo targetObj = targetType.GetProperty(p.Name);
                //  If there is none, skip
                if (targetObj == null)
                    continue;

                //  Set the value in the destination
                targetObj.SetValue(destObject, p.GetValue(sourceObject, null), null);
            }

            using (MailMessage mail = new MailMessage())
            {
                mail.From = new MailAddress("gais.staff@gmail.com");
                mail.To.Add("muhrifh@gmail.com");
                mail.Subject = "Bem Polman Astra";
                mail.Body = "<h2>Hello, " + "" +
                    "</h2>Berkaitan dengan website Sistem Informasi Bem, Berikut Terlampir detail informasi akun anda<br>"
                    + "Username : <b>" + "Test" + "</b><br>Password   : <b>" + "test" +
                    "</b><br>Sekian info yang dapat kami sampaikan atas perhatiannya kami ucapkan terimakasih." +
                    "<br>Sekretaris";
                mail.IsBodyHtml = true;

                using (SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587))
                {
                    smtp.Credentials = new NetworkCredential("gais.staff@gmail.com", "Testing1322");
                    smtp.EnableSsl = true;
                    smtp.Send(mail);
                }
            }
        }
        
    }
}