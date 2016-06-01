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
            MailMessage message = new MailMessage(from, to);
            message.Subject = "Allerta superamento soglie nel tuo centro";
            message.Body = "Ciao";//@"E' stato rilevato un superamento delle soglie relative alle condiioni ambientali ottimali nel tuo centro. I dati rilevati sono \n"
            //                 + "Temperatura: " + measuredTemp + "\n Pressione: " + measuredPres + "\n Umidità: " + measuredPres;
            SmtpClient client = new SmtpClient("smtp.libero.it", 465);
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.UseDefaultCredentials = false;
            NetworkCredential basicCredential = new NetworkCredential("mrc.gruppo@libero.it", "nuncfermnisciun2016");
            client.Credentials = basicCredential;
            client.EnableSsl = true;

            try
            {
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
