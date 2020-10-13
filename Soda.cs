using System;
/**
 * @author Joseph Kern
 */

class Soda : Item
{
  public bool HasCaffeine { get; set; }

  public Soda() :base()
  {
    HasCaffeine = false;
  }

  public Soda(string name, decimal price, int quantity, bool hasCaffeine) : 
    base(name, price, quantity)
  {
    HasCaffeine = hasCaffeine;
  }

  public override void Display()
  {
    if (HasCaffeine == true)
    {
      base.Display();
      Console.WriteLine(", (caffeine)");
    }
    else
    {
      base.Display();
      Console.WriteLine(", (no caffeine)");
    }
  }

  public override string ToString()
  {
    return $"{base.ToString()},{HasCaffeine}";
  }
}

