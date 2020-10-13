using System;
using System.IO;
using System.Collections.Generic;

/**
 * @author Joseph Kern
 * class VendingMachine simulates a simple Vending Machine
 * where items may be purchased, and item information is updated
 * and stored.
 */
class VendingMachine
{
  private List<Item> items;

  static void Main(string[] args)
  {
    const string FILE_PATH = @"..\\..\\..\\";
    string fileBaseName = "items";

    VendingMachine machine = new VendingMachine();

    try
    {
      machine.Start(FILE_PATH, fileBaseName + ".txt");
      machine.Run();
      machine.Stop(FILE_PATH, fileBaseName + ".txt");
    }
    catch (Exception exc)
    {
      Console.WriteLine(exc.StackTrace);
    }
  }

  public VendingMachine()
  {
    // Assign a new empty list of Items to the items field.
    // Remember – DO NOT redeclare items.
    items = new List<Item>();
  }

  /**
	* Method Start loads the machine with vending machine data.
	* @param filePath The file location
	* @param fileName The name of the file holding the vending 
	*                  machine data. 
	*/
  public void Start(string filePath, string fileName)
  {
    // Open the file for reading. (Not required to check if it exists)
    StreamReader inFile = new StreamReader(
      new FileStream(filePath + fileName, FileMode.Open));

    // While there are more lines in the file, read each vending machine
    // item from file into the items list
    string record;
    while ((record = inFile.ReadLine()) != null)
    {
      // If the next line (record) was not already read in the loop
      // header, read the next line. Then, split it into an
      // array of strings
      string[] data = record.Split(',');

      // IF the line of data read represents a Soda, build a new
      // Soda object with the name/price/quantity/caffeine information
      // and add it to the items list
      // ELSE build a new Candy object with the name/price/quantity/nuts
      // information and add it to the items list
      if (data[0] == "Soda")
      {
        items.Add(new Soda(data[1], decimal.Parse(data[2]), 
          int.Parse(data[3]), bool.Parse(data[4])));
      }
      else
      {
        items.Add(new Candy(data[1], decimal.Parse(data[2]),
          int.Parse(data[3]), bool.Parse(data[4])));
      }

    } // end while

    // Close the file
    inFile.Close();

  }

  /**
	* Method Lookup performs a case-insensitive linear search to determine
	* if a vending machine item with the given name exists.
	* @param itemName The name of the vending machine item
	* @return A reference to the found item found or null if not found.
	*/
  public Item Lookup(string itemName)
  {
    Item itemFound = null;
    for (int i = 0; i < items.Count; i++)
    {
      if (items[i].Name.ToLower() == itemName.ToLower())
      {
        itemFound = items[i];
      }
    }
    return itemFound;
  } // end method

  /** Method DisplayItems displays each item in the machine */
  public void DisplayItems()
  {
    foreach (Item item in items)
    {
      item.Display();
    }
  } // end method

  /** Method Run allows a user to purchase items from the machine */
  public void Run()
  {
    // Process items until the user quits
    string choice = "Y";
    do
    {
      DisplayItems();

      // Prompt for and retrieve the desired item. 
      // Store the user-entered item into a string named itemName
      Console.Write("\nItem? ");
      string itemName = Console.ReadLine();

      Item item = Lookup(itemName);
      // If item is found, proceed with the purchase
      if (item != null)
      {
        // If there is at least one item, process a purchase
        if (item.Quantity > 0)
        {
          // Prompt for and retrieve the money
          Console.Write("Enter money: $");
          decimal amountPaid = decimal.Parse(Console.ReadLine());

          // Loop until enough money is entered
          while (item.Price > amountPaid)
          {
            decimal totalLeft = item.Price - amountPaid;
            Console.Write($"Enter {totalLeft:C} more: $");
            amountPaid += decimal.Parse(Console.ReadLine());
          }

          // Tell the user to take the product and any remaining change
          Console.WriteLine($"Please take your {itemName}.");
          if (amountPaid > item.Price)
          {
            decimal change = amountPaid - item.Price;
            Console.WriteLine($"PLease take your change ({change:C})");
          }

          // Reduce the quantity in stock of the purchased item by 1
          item.Quantity -= 1;

        }
        else
        {
          Console.WriteLine("Item sold out.");
        }
      }
      else
      {
        Console.WriteLine("Invalid item.");
      }

      // Prompt for and retrieve choice whether the user wants to quit.
      Console.WriteLine("\n===================");
      Console.Write("Quit? (y or no): ");
      choice = Console.ReadLine();
      Console.WriteLine("===================\n");

    } while (choice.ToUpper() != "Y");

  } // end method

  /**
	* Method Stop writes the items to file
	* @param filePath The file location
	* @param fileName The name of the file to which the items are written.
	*/
  public void Stop(string filePath, string fileName)
  {
    StreamWriter outFile = new StreamWriter(
      new FileStream(filePath + fileName, FileMode.Create));

    foreach (Item item in items)
    {
      outFile.WriteLine(item.ToString());
    }

    outFile.Close();
  }
} // end class
