using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using System.IO;

namespace Condominio
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // ATTRIBUTI
        private Appartamento[] _tabella;
        public MainWindow()
        {
            InitializeComponent();
            _tabella = new Appartamento[0];
        } // fine costruttore

        // METODI - EVENTI
        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            if (!Esiste(txtCodice.Text))
            {
                try
                {
                    Appartamento nuovo = new Appartamento
                    {
                        Codice = txtCodice.Text,
                        Numero = int.Parse(txtNumero.Text),
                        Nome = txtNome.Text,
                        Valore = decimal.Parse(txtValore.Text),
                        Occupato = (chkOccupato.IsChecked == true ? true : false)
                    };
                    Array.Resize(ref _tabella, _tabella.Length + 1);
                    _tabella[_tabella.Length - 1] = nuovo;
                    UpgradeGUI();
                } // fine try
                catch (Exception errore)
                {
                    MessageBox.Show(errore.Message);
                } // fine catch
            } // fine if
            else
                MessageBox.Show("Codice appartamento esistente");
        } // fine metodo
        private bool Esiste(string codice_ricerca)
        {
            foreach (Appartamento app in _tabella)
                if (app.Codice == codice_ricerca) return true;
            return false;
        } // fine metodo
        private void UpgradeGUI()
        {
            lstCondominio.Items.Clear();
            foreach (Appartamento app in _tabella)
                lstCondominio.Items.Add(app.ToString());
        } // fine metodo

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            using (StreamWriter fileW = new StreamWriter(txtNomeFile.Text, false))
            {
                foreach (Appartamento app in _tabella)
                    fileW.WriteLine(app.ToStringCSV());
            } // fine using
            MessageBox.Show("File CSV salvato con successo");
        } // fine evento

        private void btnLoad_Click(object sender, RoutedEventArgs e)
        {
            string riga_letta;
            Appartamento nuovo;
            _tabella = new Appartamento[0];
            try
            {
                using (StreamReader fileR = new StreamReader(txtNomeFile.Text))
                {
                    while (!fileR.EndOfStream)
                    {
                        riga_letta = fileR.ReadLine();
                        nuovo = new Appartamento();
                        nuovo.FromStringCSV(riga_letta, ';');
                        Array.Resize(ref _tabella, _tabella.Length + 1);
                        _tabella[_tabella.Length - 1] = nuovo;
                    } // fine while
                } // fine using
                UpgradeGUI();
            } // fine try
            catch (Exception errore)
            {
                MessageBox.Show(errore.Message);
            } // fine catch
        } // fine evento

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            int riga;
            riga = lstCondominio.SelectedIndex;
            if (riga != -1)
            {
                for (int i = riga; i < _tabella.Length - 1; i++)
                    _tabella[i] = _tabella[i + 1];
                Array.Resize(ref _tabella, _tabella.Length - 1);
                UpgradeGUI();
            } // fine if
            else
                MessageBox.Show("Devi selezionare una riga nella lista");
        } // fine evento

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            int riga;
            riga = lstCondominio.SelectedIndex;
            if (riga != -1)
            {
                _tabella[riga].Codice = txtCodice.Text;
                _tabella[riga].Numero = int.Parse(txtNumero.Text);
                _tabella[riga].Nome = txtNome.Text;
                _tabella[riga].Valore = decimal.Parse(txtValore.Text);
                _tabella[riga].Occupato = (chkOccupato.IsChecked == true ? true : false);
                UpgradeGUI();
            } // fine if      
        } // fine evento

        private void lstCondominio_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int riga;
            riga = lstCondominio.SelectedIndex;
            if(riga!=-1)
            {
                txtCodice.Text = _tabella[riga].Codice;
                txtNumero.Text = _tabella[riga].Numero.ToString();
                txtNome.Text = _tabella[riga].Nome;
                txtValore.Text = _tabella[riga].Valore.ToString();
                chkOccupato.IsChecked = _tabella[riga].Occupato;
            } // fine if
        } // fine evento

    } // fine classe

} // fine namespace
