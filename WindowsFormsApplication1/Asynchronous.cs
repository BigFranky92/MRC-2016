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
using MySql.Data.MySqlClient;
using WindowsFormsApplication1;


public class Asynchronous
{
    public static string data = null;
    public static string[] parametri;
    public static string[] parametri_app;
    public static bool new_parameters_env = false; //Segnala la presenza di nuovi dati ambientali
    public static bool new_parameters_activity = false; //Segnala la presenza di dati relativi all'attività fisica
    public static Socket listener;
    // Thread signal.
    public static ManualResetEvent allDone = new ManualResetEvent(false);

    public static void StartListening(object port)
    {
        // Data buffer for incoming data.
        byte[] bytes = new Byte[1024];

        // Establish the local endpoint for the socket.
        // The DNS name of the computer
        // running the listener is "host.contoso.com".
        IPHostEntry ipHostInfo = Dns.Resolve(Dns.GetHostName());
        IPAddress ipAddress = ipHostInfo.AddressList[0];

        Console.WriteLine(ipAddress.ToString());
        int popo = (int)port;
        IPEndPoint localEndPoint = new IPEndPoint(ipAddress, popo);


        // Create a TCP/IP socket.
        Socket listener = new Socket(AddressFamily.InterNetwork,
            SocketType.Stream, ProtocolType.Tcp);

        //Console.WriteLine(ris.ToString());
        // Bind the socket to the local endpoint and listen for incoming connections.
        try
        {
            listener.Bind(localEndPoint);
            listener.Listen(100);
            while (true)
            {
                // Set the event to nonsignaled state.
                allDone.Reset();
                // Start an asynchronous socket to listen for connections.
                Console.WriteLine("Waiting for a connection...");
                listener.BeginAccept(
                    new AsyncCallback(AcceptCallback),
                    listener);
                // Wait until a connection is made before continuing.
                allDone.WaitOne();
            }

        }
        catch (Exception e)
        {
            Console.WriteLine(e.ToString());
        }

        Console.WriteLine("\nPress ENTER to continue...");
        Console.Read();
    }

     public static void AcceptCallback(IAsyncResult ar)
    {
        // Get the socket that handles the client request.
        Socket listener = (Socket) ar.AsyncState;
        Socket handler = listener.EndAccept(ar);

        // Signal the main thread to continue.
        allDone.Set();

        // Create the state object.
        StateObject state = new StateObject();
        state.workSocket = handler;
        Console.WriteLine("Connection Accepted from ... " + handler.RemoteEndPoint.ToString());
        handler.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0,
            new AsyncCallback(ReadCallback), state);
    }

    public static void ReadCallback(IAsyncResult ar)
    {
         String content = String.Empty;
         // Retrieve the state object and the handler socket
         // from the asynchronous state object.
         StateObject state = (StateObject)ar.AsyncState;
         Socket handler = state.workSocket;

        // Read data from the client socket.
        //QUI SI VERIFICA L'ECCEZIONE QUANDO DISCONNETTO I CLIENT
        int bytesRead = handler.EndReceive(ar);
        // There  might be more data, so store the data received so far.
        state.sb.Append(Encoding.ASCII.GetString(state.buffer, 0, bytesRead));
        // Check for end-of-file tag. If it is not there, read 
        // more data.
        content = state.sb.ToString();
        if (content.IndexOf("<EOF>") > -1)
        {
            parametri_app = content.Split('<');
            parametri = parametri_app[0].Split('#');

            //Ricevuti i dati, distingue il tipo di pacchetto (il campo Type è il primo campo del pacchetto)
            if (parametri[0] == "0") //Ho ricevuto dati ambientali
            {
                Console.WriteLine(parametri[0]);
                Console.WriteLine(parametri[1]);
                Console.WriteLine(parametri[2]);
                Console.WriteLine(parametri[3]);
                Console.WriteLine(parametri[4]);

                new_parameters_env = true;
                //Dopo aver ricevuto i dati, salvali in un DB: 
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandText = "INSERT INTO misura_ambientale(pressione, temperatura, umidita, Idsensore_ambientale) VALUES(?pressione, ?temperatura, ?umidita, ?idsensore)";
                cmd.Parameters.Add("?idsensore", MySqlDbType.VarChar).Value = parametri[1];
                cmd.Parameters.Add("?temperatura", MySqlDbType.Float).Value = parametri[2];
                cmd.Parameters.Add("?pressione", MySqlDbType.Int32).Value = parametri[3];
                cmd.Parameters.Add("?umidita", MySqlDbType.Int32).Value = parametri[4];
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();

            }
            else if (parametri[0] == "1") //Ho ricevuto dati relativi all'attività fisica
            {
                Console.WriteLine(parametri[0]);
                Console.WriteLine(parametri[1]);
                Console.WriteLine(parametri[2]);
                Classifier ClAct = new Classifier();
                new_parameters_activity = true;
                //Dopo aver ricevuto i dati, salvali in un DB: 
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = DataBase_Connection.Open_Connection_DB();
                cmd.CommandText = "INSERT INTO misura_actigrafo(indice_attività, idactigrafo) VALUES(?indice_attività, ?id_actigrafo)";
                cmd.Parameters.Add("?id_actigrafo", MySqlDbType.VarChar).Value = parametri[1];
                cmd.Parameters.Add("?indice_attività", MySqlDbType.Int32).Value = ClAct.classifica_attività(Int32.Parse(parametri[2]));
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();
            }
            // Show the data on the console.
            Console.WriteLine("Text received : {0}", content);
        }
        state = new StateObject();
        state.workSocket = handler;
        handler.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0,
                new AsyncCallback(ReadCallback), state);
    }

    private static void Send(Socket handler, String data)
    {
        // Convert the string data to byte data using ASCII encoding.
        byte[] byteData = Encoding.ASCII.GetBytes(data);

        // Begin sending the data to the remote device.
        handler.BeginSend(byteData, 0, byteData.Length, 0,
            new AsyncCallback(SendCallback), handler);
    }

    private static void SendCallback(IAsyncResult ar)
    {
        try
        {
            // Retrieve the socket from the state object.
            Socket handler = (Socket)ar.AsyncState;

            // Complete sending the data to the remote device.
            int bytesSent = handler.EndSend(ar);
            Console.WriteLine("Sent {0} bytes to client.", bytesSent);          
        }
        catch (Exception e)
        {
            Console.WriteLine(e.ToString());
        }
    }


    public static void close_socket() {

        if (listener != null)
        {
            try
            {
                listener.Shutdown(SocketShutdown.Send);
                listener.Close();
            }
            catch (SocketException se)
            {


                listener.Close();
                Console.WriteLine(se.ToString());
            }
        }
    }




}