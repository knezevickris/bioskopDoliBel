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
    /// Interaction logic for ucFilm.xaml
    /// </summary>
    public partial class ucFilm : UserControl
    {
        private string poster, imeFilma, rediteljFilma, drzavaF, zanrFilma, trajanjeFilma;
        private bool triD;
        
        public ucFilm( string zanr, string slika, string ime, string reditelj, string trajanje, string drzava, bool trid )
        {
            InitializeComponent();
            this.poster = slika;
            this.imeFilma = ime;
           this.rediteljFilma = reditelj;
            this.drzavaF = drzava;
            this.triD = trid;
            this.trajanjeFilma = trajanje;
            this.zanrFilma = zanr;

            lblNazivFilma.Content = imeFilma;
            lblReditelj.Content += rediteljFilma;
            lblDrzava.Content += drzavaF;
            lblZanr.Content += zanrFilma;
            lblTrajanje.Content += trajanjeFilma;
            if (triD == true)
                lblD.Content += "3D";
            else
                lblD.Content += "2D";

            imgPoster.Source = new ImageSourceConverter().ConvertFromString(@"slike\" + poster) as ImageSource;

        }
    }
}
