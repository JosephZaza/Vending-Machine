using System;
/**
 * @author Joseph Kern
 */

class Item
{
  public string Name { get; set; }

  private decimal price;
  private int quantity;

  public decimal Price
  {
    get
    {
      return price;
    }
    set
    {
      if (value >= 0)
      {
        price = value;
      }
    }
  }
  public int Quantity
  {
    get
    {
      return quantity;
    }
    set
    {
      if (value >= 0)
      {
        quantity = value;
      }
    }
  }

  public Item()
  {
    Price = 0;
    Quantity = 0;
    Name = "";
  }

  public Item(string name, decimal price, int quantity)
  {
    Price = price;
    Quantity = quantity;
    Name = name;
  }

  public virtual void Display()
  {
    Console.Write($"{Name}, {Price:C}, {Quantity} remaining");
  }

  public override string ToString()
  {
    return $"{base.ToString()},{Name},{Price:F},{Quantity}";
  }
}

