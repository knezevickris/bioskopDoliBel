using System;
using System.Collections.Generic;
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

using MySql.Data.MySqlClient;

namespace bioskopDoliBel
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MySqlConnection konekcija = null;
        public MainWindow()
        {
            InitializeComponent();

            string konString = "Server=localhost; Database=tmp; Username=root; Password=;";
            konekcija = new MySqlConnection(konString);
      
            try
            {
                konekcija.Open();
            }
            catch
            {
                MessageBox.Show("Nije moguce uspostaviti vezu sa bazom podataka!", "Greska", MessageBoxButton.OK);
            }

            try
            {
                cmcZanrovi.Items.Add("Svi");
                cmcZanrovi.SelectedIndex = 0;
                ucitajZanrove();
                ucitajFilmove("Svi", "");
            }
            catch 
            {
                MessageBox.Show("Problem pri citanju podataka iz baze!", "Greska", MessageBoxButton.OK);
            }


        }

        public void ucitajFilmove(string trazeniZanr, string uneseniNaziv)
        {
            spFilmovi.Children.Clear();
            string upit = "SELECT * FROM kino WHERE 1";
            
            if (cmcZanrovi.SelectedIndex > 0)
            {
                upit += " AND zanr='" + trazeniZanr.ToString()+"'";
            }
            if (txtNazivFilma.Text != "")
            {
                upit += " AND naziv LIKE '%"+ uneseniNaziv +"%'";
            }

            if (rb3D.IsChecked == true)
            {
                upit += " AND 3d = '1'";
            }
            if (rb2D.IsChecked == true)
            {
                upit += " AND 3d = '0'";
            }

            MySqlCommand komanda = new MySqlCommand(upit, konekcija);
            MySqlDataReader reader = komanda.ExecuteReader();


            while (reader.Read())
            {
                string naziv = reader["naziv"].ToString();
                string reditelj = reader["reditelj"].ToString();
                string zanr = reader["zanr"].ToString();
                string trajanje = reader["trajanje"].ToString();
                string drzava = reader["drzava"].ToString();
                string opis = reader["opis"].ToString();
                bool triD = (bool)reader["3d"];
                string slika = reader["slika"].ToString();

                ucFilm film = new ucFilm(zanr,slika,naziv,reditelj,trajanje,drzava,triD);
                film.ToolTip = opis;
                spFilmovi.Children.Add(film);
            }

            reader.Close();
        }

        public void ucitajZanrove()
        {
            string upit = "SELECT DISTINCT zanr FROM kino ORDER BY zanr ASC";
            MySqlCommand komanda = new MySqlCommand(upit, konekcija);
            MySqlDataReader reader = komanda.ExecuteReader();
            
            while (reader.Read())
            {
                cmcZanrovi.Items.Add(reader["zanr"].ToString());
            }
            reader.Close();

        }

        private void btnPonisti_Click(object sender, RoutedEventArgs e)
        {
            txtNazivFilma.Clear();
            cmcZanrovi.SelectedIndex = 0;
            ucitajFilmove("Svi", "");
        }

        private void btnpretraga_Click(object sender, RoutedEventArgs e)
        {
            string izabraniZanr = cmcZanrovi.SelectedItem.ToString();
            string uneseniNaziv = txtNazivFilma.Text;
            ucitajFilmove(izabraniZanr, uneseniNaziv);
        }
    }
}
