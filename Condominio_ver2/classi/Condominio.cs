using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;


public class Condominio
{
    // ATTRIBUTI
    private Appartamento[] _tabella;
    // COSTRUTTORE
    public Condominio()
    {
        _tabella = new Appartamento[0];
    } // fine costruttore

    // PROPERTY
    public Appartamento this[int riga]
    {
        get { return _tabella[riga]; }
        set { _tabella[riga] = value; }
    } // fine property

    public int Count    
    { 
        get { return _tabella.Length; } 
    } // fine property
    // METODI
    public bool Esiste(string codice_ricerca)
    {
        foreach (Appartamento app in _tabella)
            if (app.Codice == codice_ricerca) return true;
        return false;
    } // fine metodo

    public void Clear()
    {
        _tabella = new Appartamento[0];
    } // fine metodo
    public void Add(Appartamento nuovo)
    {
        Array.Resize(ref _tabella, _tabella.Length + 1);
        _tabella[_tabella.Length - 1] = nuovo;
    } // fine metodo

    public void Update(int riga, Appartamento new_data)
    {
        _tabella[riga] = new_data;
    } // fine metodo

    public void Delete(int riga)
    {
        for (int i = riga; i < _tabella.Length - 1; i++)
            _tabella[i] = _tabella[i + 1];
        Array.Resize(ref _tabella, _tabella.Length - 1);
    } // fine metodo

    public void LoadFileCSV(string file_name)
    {
        string riga_letta;
        Appartamento nuovo;
        this.Clear();
        using (StreamReader fileR = new StreamReader(file_name))
        {
            while (!fileR.EndOfStream)
            {
                riga_letta = fileR.ReadLine();
                nuovo = new Appartamento();
                nuovo.FromStringCSV(riga_letta, ';');
                this.Add(nuovo);
            } // fine while
        } // fine using
    } // fine metodo

    public void SaveFileCSV(string file_name)
    {
        using (StreamWriter fileW = new StreamWriter(file_name, false))
        {
            foreach (Appartamento app in _tabella)
                fileW.WriteLine(app.ToStringCSV());
        } // fine using
    } // fine metodo

} // fine classe

