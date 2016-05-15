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
using System.Net.NetworkInformation;
using System.Net.Sockets;

using System.Threading;
using System.Data.SqlClient;

namespace WindowsFormsApplication1
{

    public partial class Form1 : Form
    {
        public delegate void aggiorna_videoCallBack();
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int port = int.Parse(portBox.Text); ; //Variabile in cui verrà immesso il valore della porta aperta per la connessione
            bool avanza = check_porta(port); //Una volta premuto il tasto, vengono effettuati dei controlli per vedere se il numero di porta scelto è valido

            Console.WriteLine(avanza);
            if (avanza == true)
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
            /*string url = "http://checkip.dyndns.org";                               //Questo codice ci è servito per mostrare a video l'indirizzo IP esterno della centrale, ma non siamo ancora riusciti ad aprire la porta anche sull'indirizzo IP esterno
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
            ipBox.Text = ipServer.AddressList[0].ToString();

            //Testa l'inserimento di dati nel DB
            try
            {
                using (SqlConnection conn = new SqlConnection())
                {
                    // Crea la connection string per connettersi ad un db locale MySql chiamato dbsensori (già creato)
                    // Trusted_Connection is used to denote the connection uses Windows Authentication
                    //conn.ConnectionString = "root@localhost:3306;Database=dbsensori;Trusted_Connection=true";
                    /*conn.ConnectionString =
                    "Data Source=(local);" +
                    "Initial Catalog=dbsensori;" +
                    "User id=root;" +
                    "Password=root;"; */
                    conn.ConnectionString = "User ID = root; Password = root; Server = 127.0.0.1; Database = dbsensori";

                    conn.Open();
                    //Crea un comando per l'inserimento di una nuova riga con i dati ricevuti
                    SqlCommand insertCommand = new SqlCommand("INSERT INTO dbsensori (id, temperatura, pressione, umidità) VALUES (@0, @1, @2, @3)", conn);

                    // In the command, there are some parameters denoted by @, you can 
                    // change their value on a condition, in my code they're hardcoded.

                    insertCommand.Parameters.Add(new SqlParameter("0", 1));
                    insertCommand.Parameters.Add(new SqlParameter("1", 10));
                    insertCommand.Parameters.Add(new SqlParameter("2", 10));
                    insertCommand.Parameters.Add(new SqlParameter("3", 10));

                    // Execute the command, and print the values of the columns affected through
                    // the command executed.

                    Console.WriteLine("Commands executed! Total rows affected are " + insertCommand.ExecuteNonQuery());
                    Console.WriteLine("Done! Press enter to move to the next step");
                }
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex.Message.ToString());
            }
            catch (SqlException ex)
            {
                Console.Write(ex.Message.ToString());
            }

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




        private void aggiorna_video() //Questa è la funzione per aggiornare il video in tempo reale utilizzata dal thread
        {
            while (true) //Rimane in polling fino a quando non viene alzato il flag new_parameters e quindi ci sono nuovi dati da mostrare a video
            {
                if (Asynchronous.new_parameters)
                {
                    try //L'aggiornamento dei dati a video viene fatto in un try-catch per evitare che vengano scritti dei dati nulli e vada in crash il programma
                    {
                        if (idBox.InvokeRequired) //Viene utilizzato il metodo InvokeRequired in quanto il thread che va ad aggiornare le textBox non è lo stesso che le ha create, e quindi non ha un vero e proprio controllo su di esse
                        {
                            idBox.Invoke((MethodInvoker)delegate { idBox.Text = Asynchronous.parametri[0]; });
                            tempBox.Invoke((MethodInvoker)delegate { tempBox.Text = Asynchronous.parametri[1]; });
                            humBox.Invoke((MethodInvoker)delegate { humBox.Text = Asynchronous.parametri[2]; });
                            presBox.Invoke((MethodInvoker)delegate { presBox.Text = Asynchronous.parametri[3]; });
                        }
                        else
                        {
                            idBox.Text = Asynchronous.parametri[0];
                            tempBox.Text = Asynchronous.parametri[1];
                            humBox.Text = Asynchronous.parametri[2];
                            presBox.Text = Asynchronous.parametri[3];
                        }
                    }
                    catch (Exception e)
                    {
                        Console.Write(e.ToString());
                    }
                }
            }
        }

       

        private bool check_porta(int porta) //Qui vengono fatti tutti i controlli di routine sulla porta scelta per far mettere in ascolto il server
        {
            int parsedValue;
            porta = int.Parse(portBox.Text);
            if (!int.TryParse(portBox.Text, out parsedValue)) //Controlliamo che il campo inserito sia numerico
            {
                MessageBox.Show("Il campo porta deve essere numerico");
                return false;
            }
            else if (porta < 0) //Che rientri nel range delle porte TCP/IP, quindi positivo
            {
                MessageBox.Show("Numero di porta non valido, immettere un numero di porta > 0");
                return false;
            }
            else if (porta > 65535)// Ma anche minore di 2^16 -1
            {
                MessageBox.Show("Numero di porta non valido, immettere un numero di porta minore di 65535");
                return false;
            }
            /*IPHostEntry ipServer = Dns.Resolve(Dns.GetHostName());
            /*try
            {
                TcpListener tcpListener = new TcpListener(ipServer.AddressList[0], porta);
                tcpListener.Start();
                return true;
            }
            catch (SocketException ex)
            {
                MessageBox.Show(ex.Message, "kaboom");
                return false;
            }*/
            else return true;
            

        }
    }
}