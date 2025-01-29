using System;
using System.Collections.Generic;
using System.Linq;

// Product record 
public record Product(string Name, string Category, decimal Price);

// Storage location struct 
public struct StorageLocation
{
    public int Section;
    public int Shelf;
    public int Position;

    public StorageLocation(int section, int shelf, int position)
    {
        Section = section;
        Shelf = shelf;
        Position = position;
    }

    public override string ToString() => $"{Section}-{Shelf}-{Position}";
}

// Main program 
class Program
{
    //Anger plats för ny produkt
    public static int platsinfosection = 0;
    public static int platsinfoshelf = 0;
    public static int platsinfoposition = 0;

    static void Main(string[] args)
    {
        // Create a list to store products 
        List<(Product Product, StorageLocation Location, int Quantity)> inventory = new();

        // Main menu 
        while (true)
        {
            Console.WriteLine("\nVälj en åtgärd:");
            Console.WriteLine("1. Lägg till produkt");
            Console.WriteLine("2. Visa lager");
            Console.WriteLine("3. Sök produkt");
            Console.WriteLine("4. Uppdatera lager");
            Console.WriteLine("5. Avsluta");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    AddProduct(inventory);
                    break;

                case "2":
                    DisplayInventory(inventory);
                    break;

                case "3":
                    SearchProduct(inventory);
                    break;

                case "4":
                    UpdateInventory(inventory);
                    break;

                case "5":
                    return;

                default:
                    Console.WriteLine("Ogiltigt val. Försök igen.");
                    break;
            }
        }
    }

    static void AddProduct(List<(Product, StorageLocation, int)> inventory)
    {

        Console.Clear(); //Tar bort menyn

        //Tar in information från användaren
        Console.WriteLine("Ange produktnamn:");
        String name = Console.ReadLine().ToLower();

        Console.WriteLine("Ange produktens kategori:");
        String category = Console.ReadLine().ToLower();

        Console.WriteLine("Ange produktens pris:");
        decimal price = Convert.ToDecimal(Console.ReadLine().ToLower());

        Console.WriteLine("Ange kvantitet:");
        int quantity = Convert.ToInt32(Console.ReadLine().ToLower());

        //Anger plats för produkt
        int section = platsinfosection;
        Console.WriteLine("\nSektion: "+ platsinfosection);
        platsinfosection++;

        int shelf = platsinfoshelf;
        Console.WriteLine("Hylla: "+ platsinfoshelf);
        platsinfoshelf++;

        int position = platsinfoposition;
        Console.WriteLine("Ange position: "+ platsinfoposition);
        platsinfoposition++;

        //initierar en product och storageloation
        Product product = new Product(name, category, price);
        StorageLocation location = new StorageLocation(section, shelf, position);

        //lägger till i lager
        inventory.Add((product, location, quantity));

        //meddelar användaren om lyckat genomförande
        Console.WriteLine("Din produkt har lagts till!\n");

        Console.WriteLine("Tryck enter för att komma tillbaka till menyn");
        Console.ReadLine();

    }

    static void DisplayInventory(List<(Product, StorageLocation, int)> inventory) 
    {
        Console.Clear(); //Tar bort menyn
        Console.WriteLine("LAGERINFORMATION\n"); //rubrik

        //kollar igenom lagret
        for (int i = 0; i < inventory.Count; i++)
        {
            //skriver ut lager information
            Console.WriteLine($"Produkt: {inventory[i].Item1.Name}");
            Console.WriteLine($"Hyllplats: {inventory[i].Item2.Section}");
            Console.WriteLine($"Antal: {inventory[i].Item3}\n");
        }

        Console.WriteLine("Tryck enter för att komma tillbaka till menyn");
        Console.ReadLine();
    }

    static void SearchProduct(List<(Product, StorageLocation, int)> inventory) 
    {
        Console.Clear(); //Tar bort menyn

        bool searchResult = false;
        Console.WriteLine("Sök produkt\n");//rubrik
        //hämtar information från användaren
        Console.WriteLine("Skriv in ett sökord");
        String? productSearch = Console.ReadLine();

        //om användaren anger ett null-värde tas ny information in
        while (String.IsNullOrEmpty(productSearch))
        {
            Console.Clear();
            Console.WriteLine("Skriv in ett sökord.Tack");
            productSearch = Console.ReadLine();
        }
        
        if (!String.IsNullOrEmpty(productSearch))
        {
            Console.WriteLine("Resultat:");

            //om vi hittar produkten skriver vi ut produktens namn, plats & antal
            foreach (var product in inventory)
            {
                if (productSearch.ToLower() == product.Item1.Name.ToLower() || product.Item1.Name.Contains(productSearch))
                {
                    Console.WriteLine("Produkt:" + product.Item1.Name);
                    Console.WriteLine($"Hyllplats: {product.Item2}");
                    Console.WriteLine($"Antal: {product.Item3}\n");
                    searchResult = true;
                }
            }

            //om vi inte kan hitta produkten och meddelar användaren
            if (searchResult == false)
            {
                Console.WriteLine("Din sökning gav inget resultat");
            }

            Console.WriteLine("Tryck enter för att komma tillbaka till menyn");
            Console.ReadLine();
        }
    }

    static void UpdateInventory(List<(Product, StorageLocation, int)> inventory)
    {
        Console.Clear(); //Tar bort menyn

        //hämtar info från användaren
        Console.WriteLine("Vad söker du?");
        string productSearch = Console.ReadLine();

        //vi söker genom lagret
        for (int i = 0; i < inventory.Count; i++)
        {
            //om prudukten hittas bes användaren om den nya kvantiteten
            if (inventory[i].Item1.Name.ToLower() == productSearch.ToLower())
            {
                Console.WriteLine($"Nuvarande kvantitet: {inventory[i].Item3} för {inventory[i].Item1.Name}");
                Console.WriteLine("Ange vad kvantiteten ska uppdateras till? ");

                //tidigare kvantitet ersätts mot den nya
                if (int.TryParse(Console.ReadLine(), out int newQuantity))
                {
                    var updatedItem = inventory[i];
                    updatedItem.Item3 = newQuantity;
                    inventory[i] = updatedItem;

                    //användaren uppdateras om den nya kvantiteten
                    Console.WriteLine($"Kvantiteten är uppdaterad till: {inventory[i].Item3} för produkt: {inventory[i].Item1.Name}");
                }
            }
            //om användaren anger ett null-värde
            else if (String.IsNullOrEmpty(productSearch))
            {
                Console.WriteLine("Du har inte sökt på något");
            }
        }
        Console.WriteLine("Tryck enter för att komma tillbaka till menyn");
        Console.ReadLine();
    }
}




