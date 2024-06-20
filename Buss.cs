using den_gula_bussen;
using System;


namespace den_gula_bussen
{
    class Buss
    {
        // Max antal passagerare som bussen kan rymmas
        public int max_antal_passagerare;
        // Passagerare åld
        public int[] passagerare;
        // Passagerarnas kön
        public int[] kön;
        // Index av passagerare inom arrays
        int ordningen_på_passageraren;

        public Buss()
        {
            // Konstruktör
            max_antal_passagerare = 0; // Initialiseras till noll eller läses från användarinmatning senare
            passagerare = new int[max_antal_passagerare]; // Initiera passagerare-array
            kön = new int[max_antal_passagerare]; // Initialize queue array
            ordningen_på_passageraren = 0; // Ordningen-index av passageraren
        }

        public void Run()
        {
            // Välkomna använderan
            Console.WriteLine(new string('*', 50));
            Console.WriteLine(new string('+', 50));
            Console.WriteLine("Welcome to the awesome Buss-simulator");

            // Ska låta användaren bestämma den maximala kapaciteten för passagerare som bussen kan rymma.
            Console.Write("Först ange den maximala kapaciteten för passagerare i bussen\t:");
            max_antal_passagerare = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("I den här simulatorn kan du välja nästa alternativ");
            Console.WriteLine(new string('+', 50));
            Console.WriteLine(new string('*', 50));

            // Initialize passagerare and kön arrays based on max_antal_passagerare
            passagerare = new int[max_antal_passagerare];
            kön = new int[max_antal_passagerare];

            // Meny
            bool running = true;
            while (running)
            {
                Console.WriteLine("\n\n1. Lägg till passagerare");
                Console.WriteLine("2. Lista alla passagerare");
                Console.WriteLine("3. Beräkna passagerarnas totala ålder");
                Console.WriteLine("4. Beräkna genomsnittsåldern för passagerare");
                Console.WriteLine("5. Hitta maxåldern i bussen");
                Console.WriteLine("6. Visa alla positioner med passagerare med en viss ålder");
                Console.WriteLine("7. Sortera bussen efter ålder");
                Console.WriteLine("8. Avsluta programmet\n\n");
                Console.WriteLine("Välj ett alternativ tack: ");
                Console.WriteLine(new string('*', 50));

                switch (Console.ReadLine())
                {
                    case "1":
                        add_passenger();
                        break;

                    case "2":
                        print_buss();
                        break;

                    case "3":
                        Console.WriteLine($"\nPassagerarnas totala ålder => {calc_total_age()}\n");
                        break;

                    case "4":
                        Console.WriteLine($"\nPassagerarnas genomsnittsålder => {calc_average_age()}\n");
                        break;

                    case "5":
                        Console.WriteLine($"\nDän äldsta passageraren är => {max_age()} år\n");
                        break;

                    case "6":
                        find_age();
                        break;

                    case "7":
                        sort_buss();
                        break;

                    case "8":
                        Console.WriteLine("\nTack för att använda appen! Vi ses igen!");
                        running = false;
                        break;

                    default:
                        Console.WriteLine("Invalid option. Please try again.");
                        break;
                }
            }
        }

        // Method för att addera ny passagerare
        public void add_passenger()
        {
            if (ordningen_på_passageraren < max_antal_passagerare)
            {
                // Att tillägga passagerares åld till passagerare array
                Console.Write($"\nAnge passagerares åld \t:");
                passagerare[ordningen_på_passageraren] = Convert.ToInt32(Console.ReadLine());

                // Att tillägga passagereres kön till kön array. 
                // '0' för man ock '1' för kvinna
                Console.Write($"\nAnge passagerares kön - 0 (för män)   1 (för kvinnor) \t:");
                kön[ordningen_på_passageraren] = Convert.ToInt32(Console.ReadLine());
                ordningen_på_passageraren++;
            }
            else
            {
                Console.WriteLine();
                Console.WriteLine(new string('!', 50));
                Console.WriteLine("\nDet finns ingen ledig plats på bussen!!!\n");
                Console.WriteLine(new string('!', 50));
            }
        }

        // Method för att skriva ut alla passagerarna
        public void print_buss()
        {
            int i = 0;
            foreach (var ålder in passagerare)
            {
                Console.Write($"\nPassagerare {i + 1}. ålder = {ålder}   ");
                if (kön[i] == 0)
                {
                    Console.Write("kön = Man");
                }
                else
                {
                    Console.Write("kön = Kvinna");
                }
                i++;
            }

        }

        // Method för att beräkna total ålder
        public int calc_total_age()
        {
            int total = 0;
            foreach (var ålder in passagerare)
            {
                total += ålder;
            }
            return total;
        }

        // Method för att beräkna genomsnitt såld
        public double calc_average_age()
        {
            double genomsnitt = calc_total_age() / (double)ordningen_på_passageraren; // genomsnitt = total / antal passagerere
            return Math.Round(genomsnitt, 1); // Se till att det bara finns en siffra efter kommatecken
        }

        // Method för att ta fram den passagerare med högst ålder.
        public int max_age()
        {
            // Den äldsta i bussen
            int gubbe = passagerare[0];
            // För att undvika ett indexfel när du jämför varje element med det nästa, loopa upp till ett mindre än arrayens längd.
            for (int i = 0; i < ordningen_på_passageraren - 1; i++)
            {
                if (gubbe > passagerare[i + 1])
                {
                    ; // Den nuvarande gubben är den äldsta så gör ingenting
                }
                else
                {
                    gubbe = passagerare[i + 1]; // Nästa index är äldre, så uppdatera gubben
                }
            }
            return gubbe;
        }


        public void find_age()
        {
            // Uppmanar användaren att mata in minvärde för intervall
            Console.Write("\nAnge den yngste åldersgruppen\t:");
            int minValue = Convert.ToInt32(Console.ReadLine());

            // Uppmanar användaren att mata in maxvärde för intervall
            Console.Write("\nAnge den äldste åldersgruppen\t:");
            int maxValue = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("\nPassagerare i det givna intervallet sitter i dessa säten\n");

            // Looping to check each value in the passagere vector
            for (int i = 0; i < passagerare.Length; i++)
            {
                if (passagerare[i] >= minValue && passagerare[i] <= maxValue)
                {
                    Console.WriteLine("Sittplats " + i + "\t");
                }
            }
        }

        public void sort_buss()
        {
            // Antal passagerare
            int n = passagerare.Length;

            // Skapa en ny clone array för att inte förstöra original array
            int[] passagerare_clone = (int[])passagerare.Clone();

            // Temporär variabel för att använda under bubble-sort
            int temp = 0;

            // Yttre loop, eftersom två siffror jämförs med varandra
            // Det räcker med (n-1) iterationer för att undvika ett fel i den sista loopen
            for (int i = 0; i < n - 1; i++)
            {
                for (int j = 0; j < n - 1 - i; j++)
                {
                    // Ändra position om den föregående är mindre än den andra
                    if (passagerare_clone[j] < passagerare_clone[j + 1])
                    {
                        temp = passagerare_clone[j]; // Lagra föregående in temp 
                        passagerare_clone[j] = passagerare_clone[j + 1]; // Lagra andra in föregående 
                        passagerare_clone[j + 1] = temp; // Lagra föregående in andra
                    }

                }
            }

            Console.WriteLine("Detta är sorterad version av buss angående ålder i fallande ordning.");
            Console.WriteLine(string.Join(", ", passagerare_clone));
        }
    }
}


class Program
{
    public static void Main(string[] args)
    {
        //Följande rad skapar en buss:
        var minbuss = new Buss();

        //Följande rad anropar metoden Run() som finns i vårt nya buss-objekt.
        minbuss.Run();

        //När metoden Run() tar slut så kommer koden fortsätta här. Då är programmet slut
        Console.Write("Press any key to continue . . . ");
        Console.ReadKey(true);
    }
}
