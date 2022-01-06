using System;
using System.Collections.Generic;
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
using Newtonsoft.Json.Linq;

namespace Production_and_staff_management_system1
{
    /// <summary>
    /// Interaction logic for MedarbejderAdministrationsPage.xaml
    /// </summary>
    public partial class MedarbejderAdministrationsPage : Page
    {
        private List<MedarbejderClass> medarbejder = new List<MedarbejderClass>();
        private Api api = new Api();

        public MedarbejderAdministrationsPage()
        {
            InitializeComponent();
            _ = getMedarbejder();
            

            
            UserpageDataGrid.ItemsSource = medarbejder;
        }
        // ------------------------------------ ehneter personale data fra databasen ---------------------------------------------------
        public async Task<List<personaleStempel>> getMedarbejder()
        {
            NameTxtB.Clear();
            typeCombo.SelectedIndex = -1;
            TelefonTxtB.Clear();
            KortNummerTxtB.Clear();
            medarbejder.Clear();
            JArray medarbejder1 = await api.getMedarbejder();
            if (medarbejder1 == null)
            {

            }
            else
            {
                foreach (JObject item in medarbejder1)
                {
                    string name = (string)item.GetValue("medarbejderNavn");
                    string kortnummer = (string)item.GetValue("medarbejderKort");
                    string type = (string)item.GetValue("type");
                    string telefonNummer = (string)item.GetValue("telefonNummer");
                    medarbejder.Add(new MedarbejderClass(name, type, telefonNummer, kortnummer));
                }
            }
            UserpageDataGrid.Items.Refresh();
            return null;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(Pages.p2);
        }
        // -------------------------------------------------------------------------------------- first cast på dummy data ------------------------------------------------ 
        public void dummy()
        {
            medarbejder.Add(new MedarbejderClass("Michael", "Admin", "53435135", "bc2569"));
            medarbejder.Add(new MedarbejderClass("Michael", "Admin", "53435135", "bc2569"));
            medarbejder.Add(new MedarbejderClass("Michael", "Admin", "53435135", "bc2569"));
            medarbejder.Add(new MedarbejderClass("Michael", "Admin", "53435135", "bc2569"));
            medarbejder.Add(new MedarbejderClass("Michael", "Admin", "53435135", "bc2569"));
            medarbejder.Add(new MedarbejderClass("Michael", "Admin", "53435135", "bc2569"));
            medarbejder.Add(new MedarbejderClass("Michael", "Admin", "53435135", "bc2569"));


        }

        public int RowIndex;
        string kortnummerForEdit;

        // ------------------------------------------------------------- edit dobeltclick event -------------------------------------------------
        private void UserpageDataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Done.Click -= AddPerson;
            Done.Click += EditPerson;
            delBtn.Visibility = Visibility.Visible;
            if (UserpageDataGrid.SelectedItem == null)
            {
                return;
            }

            var Si = UserpageDataGrid.SelectedItem as MedarbejderClass;
            RowIndex = UserpageDataGrid.Items.IndexOf(UserpageDataGrid.CurrentItem);
            NameTxtB.Text = Si.Name;
            if (Si.Type == "Medarbejder")
                typeCombo.SelectedIndex = 1;
            else
                typeCombo.SelectedIndex = 0;
            TelefonTxtB.Text = Si.Number;
            KortNummerTxtB.Text = Si.KortNr;
            kortnummerForEdit = Si.KortNr;
        }

        //---------------------------------------------------------------tilføjer personale til databasen-----------------------------------------------------------------------------------
        string KortNummer;
        private async void AddPerson(object sender, RoutedEventArgs e) // adder ny poerson til listen og clear alle textboxe på pagen
        {
            string Name = NameTxtB.Text;
            string combo = typeCombo.Text;
            int telefoNumber =  Int32.Parse(TelefonTxtB.Text); 
            JObject add = await api.tilføjMedarbejder(Name, combo, KortNummer, telefoNumber);
            if (add["error"].ToString() == "")
            {
                MessageBox.Show(add["status"].ToString());
            }
            else
            {
                MessageBox.Show(add["error"].ToString());
            }
            

            _ = getMedarbejder();

        }
        //------------------------------------------------------------------- edit personale----------------------------------------------------------------
        private async void EditPerson(object sender, RoutedEventArgs e)
        {
            string Name = NameTxtB.Text;
            string combo = typeCombo.Text;
            int telefonNummer = Int32.Parse(TelefonTxtB.Text);
            if (KortNummer == null)
                KortNummer = "";
            JObject edit = await api.redigerMedarbejder(Name, combo, kortnummerForEdit, telefonNummer, KortNummer);
            if (edit["error"].ToString() == "")
            {
                MessageBox.Show(edit["status"].ToString());
            }
            else
            {
                MessageBox.Show(edit["error"].ToString());
            }
            Done.Click += AddPerson;
            Done.Click -= EditPerson;
            _ = getMedarbejder();
        }
        //----------------------------------------------------------------------------delete personale-----------------------------------------------------
        private async void DelPerson(object sender, RoutedEventArgs e)
        {
            delBtn.Visibility = Visibility.Collapsed;
            Done.Click += AddPerson;
            Done.Click -= EditPerson;
            JObject delete = await api.sletMedarbejder(KortNummerTxtB.Text);
            if (delete["error"].ToString() == "")
            {
                MessageBox.Show(delete["status"].ToString());
            }
            else
            {
                MessageBox.Show(delete["error"].ToString());
            }
            _ = getMedarbejder();
        }

        private void AddCardToPerson(object sender, RoutedEventArgs e)
        {
            loadingscrean("on");
            KortNummerTxtB.Focus();
            

        }
        //--------------------------------------------------------------------------------------- rfid enter click event ----------------------------------------------------------
        private void AddCardEnter(object sender, KeyEventArgs e)
        {
            
            if (e.Key == Key.Enter) // venter på et key press som er enter. rfid scannerne afslutter altid med et enter. 
            {
                KortNummer = KortNummerTxtB.Text;
                loadingscrean("off");
            }
        }
        //---------------------------------------------------------------------------------------Vente skærm og annuller click----------------------------------------------------------------
        public void loadingscrean(string on) // venter på en string som styre om tilføjMedarbejderKortGrid skal åbnes eller lukkes
        {
            switch (on)
            {
                case "on":
                    tilføjMedarbejderKortGrid.Visibility = Visibility.Visible;
                    break;
                case "off":
                    tilføjMedarbejderKortGrid.Visibility = Visibility.Collapsed;
                    break;
            }


        }

        private void annullereBtnAccesLoading(object sender, RoutedEventArgs e)
        {
            loadingscrean("off");
        }
    }
}
