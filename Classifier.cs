using System;

public class Classifier
{
	public Classifier()
    { 
	}


    public int classifica_attività(int value) {
        if (value < 20)
        {
            Console.WriteLine("\n FERMO");
            return 0;
        }
        else if (value >= 20 && value < 50)
        {
            Console.WriteLine("\n LENTO");
            return 1;
        }
        else if (value >= 50 && value < 200)
        {
            Console.WriteLine("\n MODERATO");
            return 2;
        }

        else if (value >= 200 && value < 500)
        {
            Console.WriteLine("\n VELOCE");
            return 3;
        }
        else
        {
            Console.WriteLine("\n Attività Non Classificata!");
            return 10;
        }
    }
}
