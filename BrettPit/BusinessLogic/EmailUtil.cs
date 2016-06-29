using System.Net.Mail;

namespace BrettPit.BusinessLogic
{
    public static class EmailUtil
    {
        public static bool SendPasswordEmail(string resetAddress, string newPassword)
        {
            try
            {
                var email = new MailMessage();

                var sender = new MailAddress("BrettPittServiceMail@gmx.de");
                email.From = sender; // Absender einstellen
                email.To.Add(resetAddress); // Empfänger hinzufügen

                email.Subject = "BrettPit : new user Password"; // Betreff hinzufügen

                email.Body = "Dear Custumer,\nyou requested a new password for your BrettPit account.\nIf you did not request a password change, please contact a BrettPit Service Desk employee!\n\nYour new Password: " + newPassword + "\n\nkind regards\nyour Brettpit Service Crew\n\ndo not reply to this email"; // Nachrichtentext hinzufügen

                const string serverName = "mail.gmx.net";
                const string port = "25";

                var mailClient = new SmtpClient(serverName, int.Parse(port)); // Postausgangsserver definieren
                const string userName = "BrettPittServiceMail@gmx.de";
                const string password = "gyUN9sPMtYQmXIoNFfWj";
                var credentials = new System.Net.NetworkCredential(userName, password);

                mailClient.Credentials = credentials; // Anmeldeinformationen setzen*/

                mailClient.Send(email); // Email senden
                return true;
            }
            catch
            {
                return false;
            }

        }
    }
}