using MySql.Data.MySqlClient;
using System;
using System.Windows.Forms;

public class DataBase_Connection
{

    static MySql.Data.MySqlClient.MySqlConnection conn;

    public DataBase_Connection()
    {
    }

    public static MySql.Data.MySqlClient.MySqlConnection Open_Connection_DB()
    {

        //Testa l'inserimento di dati nel DB    
        string myConnectionString;

        myConnectionString = "server=localhost;uid=root;" +
            "pwd=000000;database=mrc_db;";

        try
        {
            conn = new MySql.Data.MySqlClient.MySqlConnection();
            conn.ConnectionString = myConnectionString;
            conn.Open();
        }
        catch (MySql.Data.MySqlClient.MySqlException ex)
        {
            MessageBox.Show(ex.Message);
        }

        return conn;
    }

    public static void SELECT ()
    {
        MySqlCommand cmd = conn.CreateCommand();
        cmd.CommandText = "SELECT * from misura_ambientale" ;

        MySqlDataReader reader = cmd.ExecuteReader();

        while (reader.Read())
        {
            Console.Write(reader.GetInt32("idmisura")+" ");
            Console.Write(reader.GetDateTime("timestamp").ToString() + " ");
            Console.Write(reader.GetInt32("pressione") + " ");
            Console.WriteLine(reader.GetFloat("temperatura") + " ");
        }
        
    }
}