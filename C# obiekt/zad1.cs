using System;

public class Osoba
{
    private string imię;
    public string Nazwisko { get; private set; }
    public DateTime? DataUrodzenia { get; set; } = null;
    public DateTime? DataŚmierci { get; set; } = null;

    public Osoba(string imięNazwisko)
    {
        ImięNazwisko = imięNazwisko;
    }

    public string Imię
    {
        get { return imię; }
        set
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException("Imię nie może być puste.");
            }
            imię = value;
        }
    }

    public string ImięNazwisko
    {
        get { return $"{Imię} {Nazwisko}"; }
        set
        {
            var parts = value.Split(' ');
            if (parts.Length > 1)
            {
                Imię = parts[0];
                Nazwisko = parts[parts.Length - 1];
            }
            else
            {
                Imię = parts[0];
                Nazwisko = string.Empty;
            }
        }
    }

    public TimeSpan? Wiek
    {
        get
        {
            if (DataUrodzenia == null)
            {
                return null;
            }

            DateTime endDate = DataŚmierci ?? DateTime.Now;
            return endDate - DataUrodzenia;
        }
    }
}


class Program
{
    static void Main()
    {
        Osoba osoba = new Osoba("Jan Kowalski");
        osoba.DataUrodzenia = new DateTime(1990, 1, 1);

        Console.WriteLine($"Imię: {osoba.Imię}");
        Console.WriteLine($"Nazwisko: {osoba.Nazwisko}");
        Console.WriteLine($"Imię i Nazwisko: {osoba.ImięNazwisko}");
        Console.WriteLine($"Wiek: {osoba.Wiek?.TotalDays / 365.25:F2} lat");

        osoba.ImięNazwisko = "Antoni Strauss";
        Console.WriteLine($"Zmienione Imię i Nazwisko: {osoba.ImięNazwisko}");
    }
}