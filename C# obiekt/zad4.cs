using System;
using System.Collections.Generic;


public abstract class Produkt
{
    private string nazwa;
    private decimal cenaNetto;
    private string kategoriaVAT;
    private string krajPochodzenia;

    protected static Dictionary<string, decimal> StawkiVAT = new Dictionary<string, decimal>
    {
        {"A", 0.05m},
        {"B", 0.08m},
        {"C", 0.23m},
        {"D", 0.10m}
    };

    protected static HashSet<string> DozwoloneKraje = new HashSet<string>
    {
        "Polska",
        "Niemcy",
        "Francja",
        "Hiszpania"
    };

    public string Nazwa
    {
        get => nazwa;
        set => nazwa = value ?? throw new ArgumentNullException(nameof(Nazwa));
    }

    public decimal CenaNetto
    {
        get => cenaNetto;
        set
        {
            if (value < 0) throw new ArgumentOutOfRangeException(nameof(CenaNetto));
            cenaNetto = value;
        }
    }

    public string KategoriaVAT
    {
        get => kategoriaVAT;
        set
        {
            if (!StawkiVAT.ContainsKey(value)) throw new ArgumentException("Niepoprawna kategoria VAT");
            kategoriaVAT = value;
        }
    }

    public virtual decimal CenaBrutto => CenaNetto * (1 + StawkiVAT[KategoriaVAT]);

    public string KrajPochodzenia
    {
        get => krajPochodzenia;
        set
        {
            if (!DozwoloneKraje.Contains(value)) throw new ArgumentException("Niepoprawny kraj pochodzenia");
            krajPochodzenia = value;
        }
    }
}


public abstract class ProduktSpożywczy<T> : Produkt
{
    private T kalorie;

    protected static HashSet<string> DozwoloneAlergeny = new HashSet<string>
    {
        "Orzechy",
        "Mleko",
        "Jajka",
        "Pszenica"
    };

    public T Kalorie
    {
        get => kalorie;
        set
        {
            if (typeof(T) == typeof(decimal) && Convert.ToDecimal(value) < 0)
                throw new ArgumentOutOfRangeException(nameof(Kalorie));
            kalorie = value;
        }
    }

    public HashSet<string> Alergeny { get; set; } = new HashSet<string>();
}


public class ProduktSpożywczyNaWagę : ProduktSpożywczy<decimal>
{
}


public class ProduktSpożywczyPaczka : ProduktSpożywczy<decimal>
{
    private decimal waga;

    public decimal Waga
    {
        get => waga;
        set
        {
            if (value < 0) throw new ArgumentOutOfRangeException(nameof(Waga));
            waga = value;
        }
    }
}


public class ProduktSpożywczyNapój : ProduktSpożywczyPaczka
{
    private uint objętość;

    public uint Objętość
    {
        get => objętość;
        set => objętość = value;
    }
}


public class Wielopak : Produkt
{
    public Produkt Produkt { get; set; }
    public ushort Ilość { get; set; }

    public override decimal CenaBrutto => CenaNetto * (1 + StawkiVAT[Produkt.KategoriaVAT]);

    public override string KategoriaVAT => Produkt.KategoriaVAT;
    public override string KrajPochodzenia => Produkt.KrajPochodzenia;
}


class Program
{
    static void Main()
    {
        try
        {
            ProduktSpożywczyNaWagę jabłko = new ProduktSpożywczyNaWagę
            {
                Nazwa = "Jabłko",
                CenaNetto = 2.00m,
                KategoriaVAT = "A",
                Kalorie = 52,
                Alergeny = new HashSet<string> { "Brak" },
                KrajPochodzenia = "Polska"
            };

            ProduktSpożywczyPaczka chipsy = new ProduktSpożywczyPaczka
            {
                Nazwa = "Chipsy",
                CenaNetto = 3.50m,
                KategoriaVAT = "B",
                Kalorie = 536,
                Alergeny = new HashSet<string> { "Orzechy", "Mleko" },
                Waga = 150,
                KrajPochodzenia = "Niemcy"
            };

            ProduktSpożywczyNapój sok = new ProduktSpożywczyNapój
            {
                Nazwa = "Sok",
                CenaNetto = 4.00m,
                KategoriaVAT = "C",
                Kalorie = 46,
                Alergeny = new HashSet<string> { "Brak" },
                Waga = 1000,
                Objętość = 1000,
                KrajPochodzenia = "Francja"
            };

            Wielopak wielopakSoków = new Wielopak
            {
                Produkt = sok,
                Ilość = 6,
                CenaNetto = 24.00m // CenaNetto całego wielopaku
            };

            Console.WriteLine($"Produkt: {jabłko.Nazwa}, Cena Brutto: {jabłko.CenaBrutto}");
            Console.WriteLine($"Produkt: {chipsy.Nazwa}, Cena Brutto: {chipsy.CenaBrutto}");
            Console.WriteLine($"Produkt: {sok.Nazwa}, Cena Brutto: {sok.CenaBrutto}");
            Console.WriteLine($"Wielopak: {wielopakSoków.Produkt.Nazwa}, Ilość: {wielopakSoków.Ilość}, Cena Brutto: {wielopakSoków.CenaBrutto}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Błąd: {ex.Message}");
        }
    }
}