using System;
using System.Collections.Generic;

public class Prostokąt
{
    private double bokA;
    private double bokB;

    public double BokA
    {
        get { return bokA; }
        set
        {
            if (!double.IsFinite(value) || value < 0)
            {
                throw new ArgumentException("Bok A musi być skończoną, nieujemną liczbą.");
            }
            bokA = value;
        }
    }

    public double BokB
    {
        get { return bokB; }
        set
        {
            if (!double.IsFinite(value) || value < 0)
            {
                throw new ArgumentException("Bok B musi być skończoną, nieujemną liczbą.");
            }
            bokB = value;
        }
    }

    public Prostokąt(double bokA, double bokB)
    {
        BokA = bokA;
        BokB = bokB;
    }

    public static Dictionary<char, decimal> wysokośćArkusza0 = new Dictionary<char, decimal>
    {
        ['A'] = 1189m,
        ['B'] = 1414m,
        ['C'] = 1297m
    };

    public static Prostokąt ArkuszPapieru(string format)
    {
        if (string.IsNullOrEmpty(format) || format.Length < 2)
        {
            throw new ArgumentException("Nieprawidłowy format.");
        }

        char szereg = format[0];
        if (!wysokośćArkusza0.ContainsKey(szereg))
        {
            throw new ArgumentException("Nieprawidłowy szereg.");
        }

        if (!byte.TryParse(format.Substring(1), out byte indeks))
        {
            throw new ArgumentException("Nieprawidłowy indeks.");
        }

        decimal bazowaWysokość = wysokośćArkusza0[szereg];
        double pierwiastekZDwóch = Math.Sqrt(2);

        double bokA = (double)(bazowaWysokość / (decimal)Math.Pow(pierwiastekZDwóch, indeks));
        double bokB = bokA / pierwiastekZDwóch;

        return new Prostokąt(bokA, bokB);
    }
}


class Program
{
    static void Main()
    {
        Prostokąt prostokąt1 = new Prostokąt(5.0, 10.0);
        Console.WriteLine($"Bok A: {prostokąt1.BokA}, Bok B: {prostokąt1.BokB}");

        try
        {
            Prostokąt arkusz = Prostokąt.ArkuszPapieru("A4");
            Console.WriteLine($"Arkusz A4 - Bok A: {arkusz.BokA:F2}, Bok B: {arkusz.BokB:F2}");
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine($"Błąd: {ex.Message}");
        }
    }
}