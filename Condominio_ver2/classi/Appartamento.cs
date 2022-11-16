using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Documents;

public class Appartamento
{
    // ATTRIBUTI
    private string _codice;
    public int Numero { get; set; }
    public string Nome { get; set; }
    public decimal Valore { get; set; }
    public bool Occupato { get; set; }
    // PROPERTY
    public string Codice
    {
        get { return _codice; }
        set 
        {
            if (value.Length == 5)
                _codice = value;
            else
                throw new ArgumentException("il codice deve essere alfanumerico di 5 caratteri");      
        } // fine set
    } // fine property
    // METODI
    public override string ToString()
    {
        return $"Appartamento: {_codice} nr. {Numero} di {Nome} valore {Valore} occupato:{Occupato}";
    } // fine metodo

    public string ToStringCSV()
    {
        return $"{_codice};{Numero};{Nome};{Valore};{Occupato}";
    } // fine metodo

    public void FromStringCSV(string strCSV, char carattere = ',')
    {
        string[] campi;
        campi = strCSV.Split(carattere);
        this.Codice = campi[0].Trim();
        this.Numero = int.Parse(campi[1].Trim());
        this.Nome = campi[2].Trim();
        this.Valore = decimal.Parse(campi[3].Trim());
        this.Occupato = bool.Parse(campi[4].Trim());
    } // fine metodo

} // fine classe

