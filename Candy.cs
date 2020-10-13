using System;
/**
 * @author Joseph Kern
 */

class Candy : Item
{
  public bool HasNuts { get; set; }

  public Candy() : base()
  {
    HasNuts = false;
  }

  public Candy(string name, decimal price, int quantity, bool hasNuts) : 
    base(name, price, quantity)
  {
    HasNuts = hasNuts;
  }

  public override void Display()
  {
    if (HasNuts == true)
    {
      base.Display();
      Console.WriteLine(", (nuts)");
    }
    else
    {
      base.Display();
      Console.WriteLine(", (no nuts)");
    }
  }

  public override string ToString()
  {
    return $"{base.ToString()},{HasNuts}";
  }
}

