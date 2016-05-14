using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


using System.Net;
using System.Net.Sockets;

using System.Threading;



namespace WindowsFormsApplication1
{

    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int port = 0; //Variabile in cui verrà immesso il valore della porta aperta per la connessione
            bool avanza = check_porta(port); //Una volta premuto il tasto, vengono effettuati dei controlli per vedere se il numero di porta scelto è valido

            if (avanza)
            {
                object port_obj = (object)port;
                Thread updateThread = new Thread(aggiorna_video); //Vengono fatti partire due thread, uno per aggiornare il video in tempo reale non appena vengono ricevuti nuovi valori
                Thread workerThread = new Thread(Asynchronous.StartListening); //Ed il secondo thread per aprire la socket e mettersi in ascolto
                updateThread.Start();
                workerThread.Start(port_obj);
                Console.WriteLine("main thread: Starting StartListening...");
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            /*string url = "http://checkip.dyndns.org";
            System.Net.WebRequest req = System.Net.WebRequest.Create(url);
            System.Net.WebResponse resp = req.GetResponse();
            System.IO.StreamReader sr = new System.IO.StreamReader(resp.GetResponseStream());
            string response = sr.ReadToEnd().Trim();
            string[] a = response.Split(':');
            string a2 = a[1].Substring(1);
            string[] a3 = a2.Split('<');
            string a4 = a3[0];
            //return a4;*/
            IPHostEntry ipServer = Dns.Resolve(Dns.GetHostName());
            textBox2.Text = ipServer.AddressList[0].ToString();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
        }


        private void textBox1_TextChanged(object sender, EventArgs e)
        {
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                DialogResult result = MessageBox.Show("Do you really want to exit?", "Dialog Title", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    Environment.Exit(0);
                }
                else
                {
                    e.Cancel = true;
                }
            }
            else
            {
                e.Cancel = true;
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }




        public void aggiorna_video() //Questa è la funzione per aggiornare il video in tempo reale utilizzata dal thread
        {
            while (true) //Rimane in polling fino a quando non viene alzato il flag new_parameters e quindi ci sono nuovi dati da mostrare a video
            {
                if (Asynchronous.new_parameters)
                {
                    try //L'aggiornamento dei dati a video viene fatto in un try-catch per evitare che vengano scritti dei dati nulli e vada in crash il programma
                    {
                        textBox3.Text = Asynchronous.parametri[0];
                        textBox4.Text = Asynchronous.parametri[1];
                        textBox5.Text = Asynchronous.parametri[2];
                    }
                    catch (Exception e)
                    {
                        Console.Write(e.ToString());
                    }
                }
            }
        }

        private bool check_porta(int porta)
        {
            int parsedValue;
            if (!int.TryParse(textBox1.Text, out parsedValue))
            {
                MessageBox.Show("Il campo porta deve essere numerico");
                return false;
            }
            porta = int.Parse(textBox1.Text);
            if (porta < 0)
            {
                MessageBox.Show("Numero di porta non valido, immettere un numero di porta > 0");
                return false;
            }
            if (porta > 65535)
            {
                MessageBox.Show("Numero di porta non valido, immettere un numero di porta minore di 65535");
            }
            IPHostEntry ipHost = Dns.Resolve(Dns.GetHostName());
            IPAddress ip = ipHost.AddressList[0];
            try
            {
                TcpListener tcpListener = new TcpListener(ip, porta);
                tcpListener.Start();
            }
            catch (SocketException ex)
            {
                MessageBox.Show(ex.Message + ". Cambiare la porta selezionata", "Errore");
                return false;
            }
            return true;
        }
    }
}