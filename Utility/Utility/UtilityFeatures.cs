using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Utility.Utility
{
    public sealed class UtilityFeatures
    {
        private static UtilityFeatures obj = null;

        public static UtilityFeatures GetInstance()
        {
            if (obj == null)
            {
                obj = new UtilityFeatures();
            }

            return obj;
        }
        public void SendEmail(string from, string to, string subject, string msgBody)
        {

            try
            {
                MailMessage message = new MailMessage(from, to);   // From address has to be GMAIL only             
                message.Subject = subject;
                message.IsBodyHtml = true;
                message.Body = msgBody;

                SmtpClient smtp = new SmtpClient();
                smtp.Port = 587;                        // Free of cost port number by google                                  
                smtp.Host = "smtp.gmail.com";
                smtp.EnableSsl = true;                  // https   
                smtp.UseDefaultCredentials = false;

                smtp.Credentials = new NetworkCredential("fromaddress_abcd1234@gmail.com", "actualpwd_tttttttt1223344");
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;

                smtp.Send(message);
            }
            catch (Exception) { }
        }

        public void LogErrorMessage(string msg)
        {
            string dFileName = string.Empty;
            // When you implement in our project, dynamically the name of file should be today's date ex:  ErrorLog_15112021.txt
            dFileName = "ErrorLog_"+ DateTime.Now.ToString("ddMMyyyy");

            string path = @"E:\personal\Project\ExceptionErrorLog\" + dFileName;     // Put in Web.config            

            using (StreamWriter sw = new StreamWriter(path, append: true))
            {
                sw.WriteLine("\n" + DateTime.Now.ToString() + "\n");
                sw.WriteLine(msg);
                sw.WriteLine("-----------------------------------------------");
            }


        }
    }
}
