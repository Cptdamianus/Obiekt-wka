using System;
using System.Collections.Generic;

public class Macierz<T> : IEquatable<Macierz<T>>
{
    private T[,] tablica;

    
    public Macierz(int wiersze, int kolumny)
    {
        if (wiersze <= 0 || kolumny <= 0)
        {
            throw new ArgumentException("Wymiary macierzy muszą być większe od zera.");
        }
        tablica = new T[wiersze, kolumny];
    }

    
    public T this[int wiersz, int kolumna]
    {
        get
        {
            if (wiersz < 0 || wiersz >= tablica.GetLength(0) || kolumna < 0 || kolumna >= tablica.GetLength(1))
            {
                throw new IndexOutOfRangeException("Indeksy są poza zakresem.");
            }
            return tablica[wiersz, kolumna];
        }
        set
        {
            if (wiersz < 0 || wiersz >= tablica.GetLength(0) || kolumna < 0 || kolumna >= tablica.GetLength(1))
            {
                throw new IndexOutOfRangeException("Indeksy są poza zakresem.");
            }
            tablica[wiersz, kolumna] = value;
        }
    }

    
    public static bool operator ==(Macierz<T> lewa, Macierz<T> prawa)
    {
        if (ReferenceEquals(lewa, prawa))
        {
            return true;
        }
        if (lewa is null || prawa is null)
        {
            return false;
        }
        if (lewa.tablica.GetLength(0) != prawa.tablica.GetLength(0) || lewa.tablica.GetLength(1) != prawa.tablica.GetLength(1))
        {
            return false;
        }
        for (int i = 0; i < lewa.tablica.GetLength(0); i++)
        {
            for (int j = 0; j < lewa.tablica.GetLength(1); j++)
            {
                if (!EqualityComparer<T>.Default.Equals(lewa.tablica[i, j], prawa.tablica[i, j]))
                {
                    return false;
                }
            }
        }
        return true;
    }

    
    public static bool operator !=(Macierz<T> lewa, Macierz<T> prawa)
    {
        return !(lewa == prawa);
    }

   
    public bool Equals(Macierz<T> inna)
    {
        return this == inna;
    }

    
    public override bool Equals(object obj)
    {
        if (obj is Macierz<T> macierz)
        {
            return Equals(macierz);
        }
        return false;
    }

    
    public override int GetHashCode()
    {
        int hash = 17;
        for (int i = 0; i < tablica.GetLength(0); i++)
        {
            for (int j = 0; j < tablica.GetLength(1); j++)
            {
                hash = hash * 23 + (tablica[i, j] == null ? 0 : tablica[i, j].GetHashCode());
            }
        }
        return hash;
    }
}


class Program
{
    static void Main()
    {
        var macierz1 = new Macierz<int>(2, 2);
        var macierz2 = new Macierz<int>(2, 2);

        macierz1[0, 0] = 1;
        macierz1[0, 1] = 2;
        macierz1[1, 0] = 3;
        macierz1[1, 1] = 4;

        macierz2[0, 0] = 1;
        macierz2[0, 1] = 2;
        macierz2[1, 0] = 3;
        macierz2[1, 1] = 4;

        Console.WriteLine(macierz1 == macierz2); // True
        Console.WriteLine(macierz1.Equals(macierz2)); // True
        Console.WriteLine(macierz1 != macierz2); // False

        macierz2[1, 1] = 5;
        Console.WriteLine(macierz1 == macierz2); // False
    }
}
