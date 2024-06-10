using System;
using System.Collections.Generic;

public abstract class Produkt
{
    public string Nazwa { get; set; }
    public decimal CenaNetto { get; set; }
    public string KategoriaVAT { get; set; }
    public string KrajPochodzenia { get; set; }

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

    public virtual decimal CenaBrutto => CenaNetto * (1 + StawkiVAT[KategoriaVAT]);
}

public abstract class ProduktSpożywczy<T> : Produkt
{
    public T Kalorie { get; set; }
    public HashSet<string> Alergeny { get; set; } = new HashSet<string>();
}

public class ProduktSpożywczyNaWagę : ProduktSpożywczy<decimal>
{
}

public class ProduktSpożywczyPaczka : ProduktSpożywczy<decimal>
{
    public decimal Waga { get; set; }
}

public class ProduktSpożywczyNapój : ProduktSpożywczyPaczka
{
    public uint Objętość { get; set; }
}

public class Wielopak : Produkt
{
    public Produkt Produkt { get; set; }
    public ushort Ilość { get; set; }

    public override decimal CenaBrutto => Produkt.CenaBrutto * Ilość;
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
                Ilość = 6
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
