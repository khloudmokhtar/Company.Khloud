using System.Net;
using System.Net.Mail;

namespace Company.Khloud.PL.Helpers
{
    public static class EmailSettings
    {

        public static bool SendEmail(Email email)
        {

            //Mail Server : Gmail
            //SMTP:

          try
            {

                var client = new SmtpClient("smtp.gmail.com", 587); //Server Name , Port
                client.EnableSsl = true;
                client.Credentials = new NetworkCredential("kmokhtar827@gmail.com", "njwbgnnycqxnfqmb"); //Sender
                client.Send("kmokhtar827@gmail.com", email.To, email.Subject, email.Body);

                return true;

            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}
