using System;
using System.Linq;

public class Wektor
{
    protected double[] współrzędne;

    public Wektor(byte wymiar)
    {
        współrzędne = new double[wymiar];
    }

    public Wektor(params double[] współrzędne)
    {
        this.współrzędne = (double[])współrzędne.Clone();
    }

    public double Długość
    {
        get { return Math.Sqrt(IloczynSkalarny(this, this)); }
    }

    public byte Wymiar
    {
        get { return (byte)współrzędne.Length; }
    }

    public double this[byte index]
    {
        get { return współrzędne[index]; }
        set { współrzędne[index] = value; }
    }

    public static double IloczynSkalarny(Wektor V, Wektor W)
    {
        if (V.Wymiar != W.Wymiar)
        {
            throw new ArgumentException("Wektory muszą mieć ten sam wymiar.");
        }

        double iloczyn = 0;
        for (int i = 0; i < V.Wymiar; i++)
        {
            iloczyn += V.współrzędne[i] * W.współrzędne[i];
        }
        return iloczyn;
    }

    public static Wektor Suma(params Wektor[] Wektory)
    {
        if (Wektory.Length == 0)
        {
            throw new ArgumentException("Przynajmniej jeden wektor musi być podany.");
        }

        byte wymiar = Wektory[0].Wymiar;
        if (Wektory.Any(w => w.Wymiar != wymiar))
        {
            throw new ArgumentException("Wszystkie wektory muszą mieć ten sam wymiar.");
        }

        double[] sumaWspółrzędnych = new double[wymiar];
        foreach (var wektor in Wektory)
        {
            for (int i = 0; i < wymiar; i++)
            {
                sumaWspółrzędnych[i] += wektor.współrzędne[i];
            }
        }
        return new Wektor(sumaWspółrzędnych);
    }

    public static Wektor operator +(Wektor V, Wektor W)
    {
        return Suma(V, W);
    }

    public static Wektor operator -(Wektor V, Wektor W)
    {
        if (V.Wymiar != W.Wymiar)
        {
            throw new ArgumentException("Wektory muszą mieć ten sam wymiar.");
        }

        double[] różnicaWspółrzędnych = new double[V.Wymiar];
        for (int i = 0; i < V.Wymiar; i++)
        {
            różnicaWspółrzędnych[i] = V.współrzędne[i] - W.współrzędne[i];
        }
        return new Wektor(różnicaWspółrzędnych);
    }

    public static Wektor operator *(Wektor V, double skalar)
    {
        double[] wynik = new double[V.Wymiar];
        for (int i = 0; i < V.Wymiar; i++)
        {
            wynik[i] = V.współrzędne[i] * skalar;
        }
        return new Wektor(wynik);
    }

    public static Wektor operator *(double skalar, Wektor V)
    {
        return V * skalar;
    }

    public static Wektor operator /(Wektor V, double skalar)
    {
        if (skalar == 0)
        {
            throw new DivideByZeroException("Skalar nie może być zerem.");
        }

        double[] wynik = new double[V.Wymiar];
        for (int i = 0; i < V.Wymiar; i++)
        {
            wynik[i] = V.współrzędne[i] / skalar;
        }
        return new Wektor(wynik);
    }

    public override bool Equals(object obj)
    {
        if (obj is Wektor other && Wymiar == other.Wymiar)
        {
            for (int i = 0; i < Wymiar; i++)
            {
                if (współrzędne[i] != other.współrzędne[i])
                {
                    return false;
                }
            }
            return true;
        }
        return false;
    }

    public override int GetHashCode()
{
    unchecked
    {
        int hash = 17;
        foreach (var coord in współrzędne)
        {
            hash = hash * 23 + coord.GetHashCode();
        }
        return hash;
    }
}

    public static bool operator ==(Wektor V, Wektor W)
    {
        if (ReferenceEquals(V, W)) return true;
        if ((object)V == null || (object)W == null) return false;
        return V.Equals(W);
    }

    public static bool operator !=(Wektor V, Wektor W)
    {
        return !(V == W);
    }
}


class Program
{
    static void Main()
    {
        Wektor v1 = new Wektor(1.0, 2.0, 3.0);
        Wektor v2 = new Wektor(4.0, 5.0, 6.0);

        Wektor suma = v1 + v2;
        Wektor różnica = v1 - v2;
        Wektor iloczynSkalarny = v1 * 2.0;
        Wektor iloczynSkalarny2 = 2.0 * v1;
        Wektor ilorazSkalarny = v1 / 2.0;

        Console.WriteLine($"v1: {string.Join(", ", v1.współrzędne)}");
        Console.WriteLine($"v2: {string.Join(", ", v2.współrzędne)}");
        Console.WriteLine($"Suma: {string.Join(", ", suma.współrzędne)}");
        Console.WriteLine($"Różnica: {string.Join(", ", różnica.współrzędne)}");
        Console.WriteLine($"Iloczyn skalarny (v1 * 2.0): {string.Join(", ", iloczynSkalarny.współrzędne)}");
        Console.WriteLine($"Iloczyn skalarny (2.0 * v1): {string.Join(", ", iloczynSkalarny2.współrzędne)}");
        Console.WriteLine($"Iloraz skalarny (v1 / 2.0): {string.Join(", ", ilorazSkalarny.współrzędne)}");
    }
}
