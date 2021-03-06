﻿using System;
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
using System.IO;
//using System.Data.SqlClient;

namespace WindowsFormsApplication1
{

    public partial class Form1 : Form
    {
        public delegate void aggiorna_videoCallBack();

        private static FileStream log;
        private static StreamWriter sw;
        private Thread workerThread;

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int port=0;
            try
            {
                port = int.Parse(portBox.Text); ; //Variabile in cui verrà immesso il valore della porta aperta per la connessione
            } 
            catch(Exception ex)
            {
                MessageBox.Show("Numero di porta non valido, immettere un numero di porta");
                return;
            }

            connectBtn.Enabled = false;
            bool avanza = check_porta(port); //Una volta premuto il tasto, vengono effettuati dei controlli per vedere se il numero di porta scelto è valido
                    
            Console.WriteLine(avanza);
            
            if (avanza == true)
            {
                object port_obj = (object)port;
                Thread updateThread = new Thread(aggiorna_video); //Vengono fatti partire due thread, uno per aggiornare il video in tempo reale non appena vengono ricevuti nuovi valori
                Console.WriteLine("1");
                workerThread = new Thread(Asynchronous.StartListening); //Ed il secondo thread per aprire la socket e mettersi in ascolto
                Console.WriteLine("2");
                updateThread.Start();
                Console.WriteLine("3");
                workerThread.Start(port_obj);
                Console.WriteLine("4");
                Console.WriteLine("Main thread: Starting StartListening...");
            }

        }

        private void Form1_Load(object sender, EventArgs e)
        {

            DataBase_Connection.Open_Connection_DB();

            IPHostEntry ipServer = Dns.Resolve(Dns.GetHostName());
            ipBox.Text = ipServer.AddressList[0].ToString();
            logBox.AppendText("Server connesso: IP = " + ipServer.AddressList[0].ToString());


            //log = new FileStream("MeasureLog.txt", FileMode.Append);
            //sw = new StreamWriter(log);
            //Console.SetOut(sw);
            //Console.WriteLine("prova scrittura su file");
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
                if (Asynchronous.new_parameters_env)
                {
                    
                    try //L'aggiornamento dei dati a video viene fatto in un try-catch per evitare che vengano scritti dei dati nulli e vada in crash il programma
                    {
                        if (idBox.InvokeRequired) //Viene utilizzato il metodo InvokeRequired in quanto il thread che va ad aggiornare le textBox non è lo stesso che le ha create, e quindi non ha un vero e proprio controllo su di esse
                        {
                            idBox.Invoke((MethodInvoker)delegate { idBox.Text = Asynchronous.parametri[1]; });
                            tempBox.Invoke((MethodInvoker)delegate { tempBox.Text = Asynchronous.parametri[2]; });
                            presBox.Invoke((MethodInvoker)delegate { presBox.Text = Asynchronous.parametri[3]; });
                            humBox.Invoke((MethodInvoker)delegate { humBox.Text = Asynchronous.parametri[4]; });

                            logBox.Invoke((MethodInvoker)delegate {
                                logBox.AppendText("\r\n => Ricevuti dati da sensore ambientale:  ");
                                logBox.AppendText("\r\n     ID = " + Asynchronous.parametri[1] + "\r\n     Temperatura = " + Asynchronous.parametri[2] + "\r\n     Pressione = " + Asynchronous.parametri[3] + "\r\n     Umidità = " + Asynchronous.parametri[4]);
                            });
                        }                       
                        else
                        {
                            idBox.Text = Asynchronous.parametri[1];
                            tempBox.Text = Asynchronous.parametri[2];
                            presBox.Text = Asynchronous.parametri[3];
                            humBox.Text = Asynchronous.parametri[4];
                            logBox.AppendText("\r\n => Ricevuti dati da sensore ambientale:  ");
                            logBox.AppendText("\r\n     ID = " + Asynchronous.parametri[1] + "\r\n     Temperatura = " + Asynchronous.parametri[2] + "\r\n     Pressione = " + Asynchronous.parametri[3] + "\r\n     Umidità = " + Asynchronous.parametri[4]);
                        }
                    }
                    catch (Exception e)
                    {
                        Console.Write(e.ToString());
                        Application.Exit();
                    }

                    Asynchronous.new_parameters_env = false;
                }
              
                else if (Asynchronous.new_parameters_activity)
                {

                    try //L'aggiornamento dei dati a video viene fatto in un try-catch per evitare che vengano scritti dei dati nulli e vada in crash il programma
                    {

                        if (idBox.InvokeRequired) //Viene utilizzato il metodo InvokeRequired in quanto il thread che va ad aggiornare le textBox non è lo stesso che le ha create, e quindi non ha un vero e proprio controllo su di esse
                        {
                            personBox.Invoke((MethodInvoker)delegate { personBox.Text = Asynchronous.parametri[1]; });
                            activityIndex.Invoke((MethodInvoker)delegate { activityIndex.Text = Asynchronous.parametri[2]; });
                        
                            logBox.Invoke((MethodInvoker)delegate
                             {
                                 logBox.AppendText("\r\n => Ricevuti dati da actigrafo:  ");
                                 logBox.AppendText("\r\n     ID = " + Asynchronous.parametri[1] + "\r\n     Deviazione standard = " + Asynchronous.parametri[2]);
                             });
                        }
                        else
                        {
                            personBox.Text = Asynchronous.parametri[1];
                            activityIndex.Text = Asynchronous.parametri[2];
                            logBox.AppendText("\r\n => Ricevuti dati da actigrafo:  ");
                            logBox.AppendText("\r\n     ID = " + Asynchronous.parametri[1] + "\r\n     Deviazione standard = " + Asynchronous.parametri[2]);
                        }
                    }
                    catch (Exception e)
                    {
                        Console.Write(e.ToString());
                        Application.Exit();
                    }

                    Asynchronous.new_parameters_activity = false;
                }
                else if(Asynchronous.new_client)
                {
                    try
                    {
                        if (idBox.InvokeRequired) //Viene utilizzato il metodo InvokeRequired in quanto il thread che va ad aggiornare le textBox non è lo stesso che le ha create, e quindi non ha un vero e proprio controllo su di esse
                        {
                            logBox.Invoke((MethodInvoker)delegate {
                                logBox.AppendText("\r\nNuova connessione accettata da: " + Asynchronous.new_client_IP + "\r\n" );
                            });
                        }
                        else
                        {
                            logBox.AppendText("\r\nNuova connessione accettata da: " + Asynchronous.new_client_IP + "\r\n");
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.Write(ex.ToString());
                    }
                    Asynchronous.new_client = false;
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
                else if (porta > 65535)//Ma anche minore di 2^16 -1
                {
                    MessageBox.Show("Numero di porta non valido, immettere un numero di porta minore di 65535");
                    return false;
                }

                else return true;         

        }

        public void aggiornaLog(String update)
        {
            logBox.AppendText(update);
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
                Asynchronous.close_socket();
                workerThread.Abort();
                Application.Exit();
            }
            catch (Exception ecc)
            {
                Console.WriteLine(ecc.ToString());
            }

        }
    }
}