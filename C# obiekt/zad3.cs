using System;
using System.Linq;

public class Wektor
{
    
    private double[] współrzędne;

    
    public Wektor(byte wymiar)
    {
        if (wymiar <= 0)
        {
            throw new ArgumentException("Wymiar musi być dodatni.");
        }
        współrzędne = new double[wymiar];
    }

    
    public Wektor(params double[] współrzędne)
    {
        if (współrzędne == null || współrzędne.Length == 0)
        {
            throw new ArgumentException("Współrzędne nie mogą być puste.");
        }
        this.współrzędne = (double[])współrzędne.Clone();
    }

    
    public double Długość
    {
        get
        {
            return Math.Sqrt(IloczynSkalarny(this, this));
        }
    }

    
    public byte Wymiar
    {
        get
        {
            return (byte)współrzędne.Length;
        }
    }

    
    public double this[byte index]
    {
        get
        {
            return współrzędne[index];
        }
        set
        {
            współrzędne[index] = value;
        }
    }

    
    public static double IloczynSkalarny(Wektor V, Wektor W)
    {
        if (V.Wymiar != W.Wymiar)
        {
            throw new ArgumentException("Wektory muszą mieć ten sam wymiar.");
        }

        double suma = 0;
        for (int i = 0; i < V.Wymiar; i++)
        {
            suma += V[(byte)i] * W[(byte)i];
        }
        return suma;
    }

    
    public static Wektor Suma(params Wektor[] wektory)
    {
        if (wektory == null || wektory.Length == 0)
        {
            throw new ArgumentException("Nie podano żadnych wektorów.");
        }

        byte wymiar = wektory[0].Wymiar;
        if (wektory.Any(w => w.Wymiar != wymiar))
        {
            throw new ArgumentException("Wszystkie wektory muszą mieć ten sam wymiar.");
        }

        double[] sumaWspółrzędnych = new double[wymiar];
        foreach (var wektor in wektory)
        {
            for (int i = 0; i < wymiar; i++)
            {
                sumaWspółrzędnych[i] += wektor[(byte)i];
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
        double[] różnicaWspółrzędnych = new double[V.Wymiar];
        for (int i = 0; i < V.Wymiar; i++)
        {
            różnicaWspółrzędnych[i] = V[(byte)i] - W[(byte)i];
        }
        return new Wektor(różnicaWspółrzędnych);
    }

    
    public static Wektor operator *(Wektor V, double skalar)
    {
        double[] noweWspółrzędne = V.współrzędne.Select(x => x * skalar).ToArray();
        return new Wektor(noweWspółrzędne);
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
        double[] noweWspółrzędne = V.współrzędne.Select(x => x / skalar).ToArray();
        return new Wektor(noweWspółrzędne);
    }

    
    public static void Main()
    {
        try
        {
            Wektor v1 = new Wektor(1, 2, 3);
            Wektor v2 = new Wektor(4, 5, 6);

            Wektor suma = v1 + v2;
            Wektor różnica = v1 - v2;
            Wektor mnożenie = v1 * 2;
            Wektor dzielenie = v2 / 2;

            Console.WriteLine("v1: {0}", string.Join(", ", v1.współrzędne));
            Console.WriteLine("v2: {0}", string.Join(", ", v2.współrzędne));
            Console.WriteLine("Suma: {0}", string.Join(", ", suma.współrzędne));
            Console.WriteLine("Różnica: {0}", string.Join(", ", różnica.współrzędne));
            Console.WriteLine("Mnożenie przez skalar: {0}", string.Join(", ", mnożenie.współrzędne));
            Console.WriteLine("Dzielenie przez skalar: {0}", string.Join(", ", dzielenie.współrzędne));
            Console.WriteLine("Iloczyn skalarny v1 * v2: {0}", Wektor.IloczynSkalarny(v1, v2));
            Console.WriteLine("Długość v1: {0}", v1.Długość);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Błąd: {ex.Message}");
        }
    }
}

