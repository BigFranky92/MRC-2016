using System;

public class Classifier
{
	public Classifier()
    { 
	}


    public int classifica_attività(int value) {
        if (value < 80)
        {
            Console.WriteLine("\n FERMO");
            return 1;
        }
        else if (value >= 80 && value < 120)
        {
            Console.WriteLine("\n LENTO");
            return 2;
        }
        else if (value >= 120 && value < 280)
        {
            Console.WriteLine("\n MODERATO");
            return 3;
        }

        else if (value >= 280 && value < 380)
        {
            Console.WriteLine("\n VELOCE");
            return 4;
        }

        else if (value >= 380)
        {
            Console.WriteLine("\n VIGOROSO");
            return 5;
        }
        else
        {
            Console.WriteLine("\n ERRORE");
            return 6;
        }
    }
}
