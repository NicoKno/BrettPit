using System.Net.Mail;

namespace BrettPit.BusinessLogic
{
    public static class EmailUtil
    {
        public static bool SendPwEmail(string ResAddress, string newPassword)
        {
            try
            {
                MailMessage Email = new MailMessage();

                MailAddress Sender = new MailAddress("BrettPittServiceMail@gmx.de");
                Email.From = Sender; // Absender einstellen
                Email.To.Add(ResAddress); // Empfänger hinzufügen

                Email.Subject = "BrettPit : new user Password"; // Betreff hinzufügen

                Email.Body = "Dear Custumer,\nyoou requested a new password for your BrettPit account.\nIf you did not request a password change, please contact an BrettPit Support Employee!\n\nYour new Password: " + newPassword + "\n\nkind regards\nyour Brettpit Service Crew\n\ndo not reply to this email"; // Nachrichtentext hinzufügen

                string ServerName = "mail.gmx.net";
                string Port = "25";

                SmtpClient MailClient = new SmtpClient(ServerName, int.Parse(Port)); // Postausgangsserver definieren

                bool LoginNecessary = true;
                if (LoginNecessary)
                {
                    string UserName = "BrettPittServiceMail@gmx.de";
                    string Password = "gyUN9sPMtYQmXIoNFfWj";
                    System.Net.NetworkCredential Credentials = new System.Net.NetworkCredential(UserName, Password);

                    MailClient.Credentials = Credentials; // Anmeldeinformationen setzen*/
                }

                MailClient.Send(Email); // Email senden
                return true;
            }
            catch
            {
                return false;
            }

        }
    }
}