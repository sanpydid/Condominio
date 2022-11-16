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

namespace Condominio_ver2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // ATTRIBUTI
        private Condominio condominio;
        public MainWindow()
        {
            InitializeComponent();
            condominio = new Condominio();
        } // fine costruttore

        // METODI - EVENTI
        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            if (!condominio.Esiste(txtCodice.Text))
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
                    condominio.Add(nuovo);
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
        
        private void UpgradeGUI()
        {
            lstCondominio.Items.Clear();
            for(int i = 0; i < condominio.Count; i++)
                lstCondominio.Items.Add(condominio[i].ToString());
        } // fine metodo

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            condominio.SaveFileCSV(txtNomeFile.Text);
            MessageBox.Show("File CSV salvato con successo");
        } // fine evento

        private void btnLoad_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                condominio.LoadFileCSV(txtNomeFile.Text);
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
                condominio.Delete(riga);
                UpgradeGUI();
            } // fine if
            else
                MessageBox.Show("Devi selezionare una riga nella lista");
        } // fine evento

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            Appartamento appartamento_modificato;
            int riga;
            riga = lstCondominio.SelectedIndex;
            if (riga != -1)
            {
                appartamento_modificato = new Appartamento() {
                    Codice = txtCodice.Text,
                    Numero = int.Parse(txtNumero.Text),
                    Nome = txtNome.Text,
                    Valore = decimal.Parse(txtValore.Text),
                    Occupato = (chkOccupato.IsChecked == true ? true : false)
                };
                condominio.Update(riga, appartamento_modificato);
                UpgradeGUI();
            } // fine if      
        } // fine evento

        private void lstCondominio_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int riga;
            riga = lstCondominio.SelectedIndex;
            if (riga != -1)
            {
                txtCodice.Text = condominio[riga].Codice;
                txtNumero.Text = condominio[riga].Numero.ToString();
                txtNome.Text = condominio[riga].Nome;
                txtValore.Text = condominio[riga].Valore.ToString();
                chkOccupato.IsChecked = condominio[riga].Occupato;
            } // fine if
        } // fine evento

    } // fine classe

} // fine namespace

