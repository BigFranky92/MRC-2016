using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication1
{
    class EmailSender
    {
        public static void sendEmail(float measuredTemp, int measuredPres, int measureUmid, string email)
        {
            string to = email;
            string from = "mrc.gruppo@libero.it";
            MailAddress toMail = new MailAddress(to);
            MailAddress fromMail = new MailAddress(from);
            MailMessage message = new MailMessage(fromMail.Address, toMail.Address);
            //message.To = toMail;
            //message.Sender = fromMail;
            //message.From = fromMail;
            message.Subject = "Allerta superamento soglie nel tuo centro";
            message.Body = "Ciao"; //@"E' stato rilevato un superamento delle soglie relative alle condiioni ambientali ottimali nel tuo centro. I dati rilevati sono \n"
            //                 + "Temperatura: " + measuredTemp + "\n Pressione: " + measuredPres + "\n Umidità: " + measuredPres;
            try
            {
                SmtpClient client = new SmtpClient();
                client.Host = "smtp.libero.it";
                client.Port = 465;
                //client.Timeout = 10000;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.UseDefaultCredentials = false;
                NetworkCredential basicCredential = new NetworkCredential("mrc.gruppo@libero.it", "nuncfermnisciun2016");
                client.Credentials = basicCredential;
                client.EnableSsl = true;

                client.Send(message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Si è verificata un'eccezione nell'invio email: {0}",
                            ex.ToString());
            }
        }
    }
 }
