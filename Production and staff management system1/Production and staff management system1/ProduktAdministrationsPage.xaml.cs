using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Navigation;


namespace Production_and_staff_management_system1
{
    /// <summary>
    /// Interaction logic for ProduktAdministrationsPage.xaml
    /// </summary>
    public partial class ProduktAdministrationsPage : Page
    {

        private List<ProductClass> ProduktKlasse1 = new List<ProductClass>();
        private List<MySteps> Mysteps = new List<MySteps>();
        private List<string> ProductNameList = new List<string>();
        private Api api = new Api();
        public ProduktAdministrationsPage()
        {
            InitializeComponent();
            ProduktKlasse();

        }

        private void ProduktAdministrationsPage_OnLoaded(object Sender, RoutedEventArgs E)
        {
            stepsOnload();
            produktDataOnload();

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(Pages.p2);
        }


        //------------------------------------------------------------------------------------------------------Data colector fra databasen ---------------------------------------------------------------------
        public async Task<List<ProductClass>> ProduktKlasse() // henter alle Prodkuter ved opstart af siden. 
        {
            JArray produkter = await api.Produkt();
            if (produkter == null)
            {


            }
            else
            {
                foreach (JObject items in produkter)
                {
                    string virksomhedNavn = (string)items.GetValue("virksomhedNavn");
                    string virksomhedAdresse = (string)items.GetValue("virksomhedAdresse");
                    int virksomhedTlfnummer = (int)items.GetValue("virksomhedTlfnummer");
                    string virksomhedKontaktPerson = (string)items.GetValue("virksomhedKontaktPerson");
                    string id = (string)items.GetValue("id");
                    string kundeId = (string)items.GetValue("kundeId");
                    string bestilling = (string)items.GetValue("bestilling");
                    string frist = (string)items.GetValue("frist");
                    int antal = (int)items.GetValue("antal");
                    string matriale = (string)items.GetValue("matriale");
                    string karmHojde = (string)items.GetValue("karmHojde");
                    string karmBrede = (string)items.GetValue("karmBrede");
                    string rammeHojde = (string)items.GetValue("rammeHojde");
                    string rammeBrede = (string)items.GetValue("rammeBrede");
                    string begyndt = (string)items.GetValue("begyndt");
                    string afsluttet = (string)items.GetValue("afsluttet");
                    ProduktKlasse1.Add(new ProductClass(id,
                                                            frist,
                                                            matriale,
                                                            antal,
                                                            karmHojde,
                                                            karmBrede,
                                                            rammeHojde,
                                                            rammeBrede,
                                                            begyndt,
                                                            bestilling,
                                                            afsluttet,
                                                            virksomhedNavn,
                                                            virksomhedTlfnummer,
                                                            virksomhedAdresse,
                                                            virksomhedKontaktPerson,
                                                            kundeId));
                    SeeAllProductsCombo.Items.Add(virksomhedNavn); // adder til dropdown på Tap2

                }
            }

            foreach (var VARIABLE in ProduktKlasse1) // adder alt fra databasens produkter til en liste så vi kan arbejde med den i programmet
            {
                ProductNameList.Add(VARIABLE.Virksomhedsnavn);
            }

            SeeAllProductsCombo.ItemsSource = ProductNameList;
            return null;

        }

        // ------------------------------------------------------------------------------------------------------------------------ visibility controler --------------------------------------------------------------
        public void editOn(string on) // venter på en string som styre hvileke elementer skal være synlige griddet skal åbnes eller lukkes
        {
            switch (on)
            {
                case "on":
                    matrialeTxt.Visibility = Visibility.Collapsed;
                    MatrialeCombo.Visibility = Visibility.Visible;
                    ShowDate.Visibility = Visibility.Collapsed;
                    editDate.Visibility = Visibility.Visible;
                    KundeInfoStackVisning.Visibility = Visibility.Collapsed;
                    KundeInfoStackEdit.Visibility = Visibility.Visible;
                    KvantitetGridvising.Visibility = Visibility.Collapsed;
                    KvantitetGridMedTxt.Visibility = Visibility.Visible;
                    break;
                case "off":
                    matrialeTxt.Visibility = Visibility.Visible;
                    MatrialeCombo.Visibility = Visibility.Collapsed;
                    ShowDate.Visibility = Visibility.Visible;
                    editDate.Visibility = Visibility.Collapsed;
                    KundeInfoStackVisning.Visibility = Visibility.Visible;
                    KundeInfoStackEdit.Visibility = Visibility.Collapsed;
                    KvantitetGridvising.Visibility = Visibility.Visible;
                    KvantitetGridMedTxt.Visibility = Visibility.Collapsed;
                    break;
            }


        }
        public enum TextboxNames // enum til at udfylde dynamiske udfyldte textboxe. 
        {
            Kunde_Navn,
            Kundens_Adresse,
            Kunde_Kontakt,
            medarbejder_navn
        }

        private List<string> plast = new List<string>();
        private List<string> træ = new List<string>();
        // -------------------------------------------------------------------------------------------------------- on load functioner ------------------------------------------------------------------------------------
        public void stepsOnload() // 2 lister til steps indhold
        {
            plast.Add("Afcutter");
            plast.Add("Klips");
            plast.Add("Glas_mont");
            plast.Add("Liste_mont");
            plast.Add("Fejl_eftersyn");
            plast.Add("Ramme_mont");

            træ.Add("Afcutter");
            træ.Add("Fæsser");
            træ.Add("Presser");
            træ.Add("Fejl_eftersyn");
            træ.Add("Maler");
            træ.Add("Glas_mont");
        }
        public void produktDataOnload()
        {
            string matriale = "";
            OversteComboStack.Children.Clear();
            NederestComboStack.Children.Clear();
            foreach (var VARIABLE in ProduktKlasse1) // giver alle textboxe deres indhold
            {
                VirksomhedNavnTxt.Text = VARIABLE.Virksomhedsnavn;
                KundeAdresseTxt.Text = VARIABLE.VirksomhedsAdresse;
                KundeMailEllerTlfTxt.Text = VARIABLE.VirksomhedsTelefonmnummer.ToString();
                KundeKontaktNavnTxt.Text = VARIABLE.VirksomhedsKontaktPerson;

                AntalVisning.Text = VARIABLE.Antal.ToString();
                KBredeVisning.Text = VARIABLE.KarmBrede;
                khojdeVisning.Text = VARIABLE.KarmHojde;
                matrialeVisning.Text = VARIABLE.Matriale;

                BestillingsdatoTB.Text = VARIABLE.Bestilling;
                TidsfristTB.Text = VARIABLE.Frist;
                påBegyndtTB.Text = VARIABLE.Begyndt;
                FærdigTB.Text = VARIABLE.Afsluttet;

                matriale = VARIABLE.Matriale;
            }
            int tæller = 0;
            switch (matriale)
            {

                case "Træ":

                    foreach (string item in plast)
                    {
                        TextBlock textBlock = new TextBlock(); // dynamisk idsat textblock i backend. 
                        textBlock.Text = item;
                        textBlock.Name = item;
                        textBlock.VerticalAlignment = VerticalAlignment.Center;
                        textBlock.FontSize = 36;
                        textBlock.FontWeight = FontWeights.Bold;
                        textBlock.Width = 250;

                        if (tæller < 3) // sætter 3 ind i første child og 3 i næste. 
                        {
                            OversteComboStack.Children.Add(textBlock);
                        }
                        else
                        {
                            NederestComboStack.Children.Add(textBlock);
                        }

                        tæller++;
                    }

                    tæller = 0;
                    break;
                case "Plast":

                    foreach (string item in træ)
                    {
                        TextBlock textBlock = new TextBlock();  // dynamisk idsat textblock i backend.
                        textBlock.Text = item;
                        textBlock.Name = item;
                        textBlock.VerticalAlignment = VerticalAlignment.Center;
                        textBlock.FontSize = 36;
                        textBlock.FontWeight = FontWeights.Bold;
                        textBlock.Width = 250;

                        if (tæller < 3) // sætter 3 ind i første child og 3 i næste. 
                        {
                            OversteComboStack.Children.Add(textBlock);
                        }
                        else
                        {
                            NederestComboStack.Children.Add(textBlock);
                        }

                        tæller++;

                    }

                    tæller = 0;
                    break;
            }


        }




        List<string> KundeIndfo = new List<string>();


        private bool Tjek = true;
        private string date;

        // ----------------------------------------------------------------------------------------------------------  New btn click ----------------------------------------------------------------------------------
        private void NewProduct_Click(object sender, RoutedEventArgs e)
        {
            if (Tjek == true)
            {
                editOn("on");

                var dateTime = DateTime.Now;
                DagsDatoTxt.Text = dateTime.ToShortDateString(); // sltter automatisk datoen på bestilling ved oprettesle af ny ptodukt. 
                date = DateTime.Now.ToString("yyyy-MM-dd");


                NewPruduct.Click -= NewProduct_Click; // fjerner eventhandler fra vores knapper så de ikke kan trykkes på ved et uheld. og chrashe programmet. 
                EditPruduct.Click -= EditBtn_Click;



                Tjek = false;
            }





        }

        // ------------------------------------------------------------------------------------------------------------  Fucos textboxe til kotscan ------------------------------------------------------------------
        public void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox tb = (TextBox)sender;
            tb.Text = string.Empty;
            tb.GotFocus -= TextBox_GotFocus;
        }


        // ------------------------------------------------------------------------------------------------------------- save btn clik og function --------------------------------------------------------------------
        string valgteDato;
        async void saveProduct()
        {
            int rammeH = Int32.Parse(KBredeTxt.Text) - 10;  // rammer er altid 10 mindre end karme. 
            int rammeB = Int32.Parse(KHojdeTxt.Text) - 10;
            valgteDato = datePicker2.SelectedDate.Value.ToString("yyyy-MM-dd");




            if (KundeMailEllerTlfTxtEdit.Text.Length == 8) // ser om telefon nummer er efter danske standarter. 
            {
                try
                {
                    await api.opretBestilling(VirksomhedNavnTxtEdit.Text, KundeAdresseTxtEdit.Text, Int32.Parse(KundeMailEllerTlfTxtEdit.Text), KundeKontaktNavnTxtEdit.Text, date, valgteDato, Int32.Parse(AntalTxt.Text), MatrialeCombo.Text, KHojdeTxt.Text, KBredeTxt.Text, rammeH.ToString(),
                        rammeB.ToString());

                    ProductNameList.Add(VirksomhedNavnTxtEdit.Text);
                    //SeeAllProductsCombo.Items.Clear();
                    SeeAllProductsCombo.Items.Add(VirksomhedNavnTxtEdit.Text);

                    KundeIndfo.Clear();
                    editOn("off");
                    clearall();

                }
                catch
                {
                    MessageBox.Show("Noget er ikke udfyldt rigtigt prøv igen");

                }
            }
            else
            {
                MessageBox.Show("Telefonnummer er ikke indtastet korrekt. Pørv igen");
            }


            //SeeAllProductsCombo.ItemsSource = ProductNameList;
        }

        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            Tjek = true;

            saveProduct();
            NewPruduct.Click += NewProduct_Click;
            EditPruduct.Click += EditBtn_Click;


        }
        private void SaveEditBtn_Click(object sender, RoutedEventArgs e)
        {
            editProduct();

        }
        // ----------------------------------------------------------------------------------- edit btn click og function ------------------------------------------------------------------------------------------------------
        string VirksomhedsNavn;
        string KundeAdresse;
        string KundeTlf;
        string KontakPerson;
        private void EditBtn_Click(object sender, RoutedEventArgs e)
        {
            foreach (var Variable in ProduktKlasse1)
            {
                if (Variable.Id == OrdreNummerTxt.Text)
                {
                    VirksomhedNavnTxtEdit.Text = Variable.Virksomhedsnavn;
                    KundeAdresseTxtEdit.Text = Variable.VirksomhedsAdresse;
                    KundeMailEllerTlfTxtEdit.Text = Variable.VirksomhedsTelefonmnummer.ToString();
                    KundeKontaktNavnTxtEdit.Text = Variable.VirksomhedsKontaktPerson;

                    AntalVisning.Text = Variable.Antal.ToString();
                    KBredeVisning.Text = Variable.KarmBrede;
                    matrialeVisning.Text = Variable.Matriale;
                    khojdeVisning.Text = Variable.KarmHojde;

                    TidsfristTB.Text = Variable.Frist;


                }
            }
            VirksomhedNavnTxtEdit.GotFocus -= TextBox_GotFocus;
            VirksomhedNavnTxtEdit.IsReadOnly = true;

            KundeAdresseTxtEdit.GotFocus -= TextBox_GotFocus;
            KundeAdresseTxtEdit.IsReadOnly = true;

            KundeMailEllerTlfTxtEdit.GotFocus -= TextBox_GotFocus;
            KundeMailEllerTlfTxtEdit.IsReadOnly = true;

            KundeKontaktNavnTxtEdit.GotFocus -= TextBox_GotFocus;
            KundeKontaktNavnTxtEdit.IsReadOnly = true;

            AntalTxt.Text = AntalVisning.Text;
            KBredeTxt.Text = KBredeVisning.Text;
            int matrialeValg;
            if (matrialeVisning.Text == "Plast")
                matrialeValg = 2;
            else
                matrialeValg = 1;

            MatrialeCombo.SelectedIndex = matrialeValg;
            KHojdeTxt.Text = khojdeVisning.Text;


            VirksomhedsNavn = VirksomhedNavnTxt.Text;
            KundeAdresse = KundeAdresseTxt.Text;
            KundeTlf = KundeMailEllerTlfTxt.Text;
            KontakPerson = KundeKontaktNavnTxt.Text;

            datePicker2.SelectedDate = DateTime.Parse(TidsfristTB.Text);

            KundeIndfo.Add(VirksomhedsNavn);
            KundeIndfo.Add(KundeAdresse);
            KundeIndfo.Add(KundeTlf);
            KundeIndfo.Add(KontakPerson);

            editOn("on");
            NewPruduct.Click -= NewProduct_Click;
            EditPruduct.Click -= EditBtn_Click;
            SavePruduct.Click -= SaveBtn_Click;
            SavePruduct.Click += SaveEditBtn_Click;
        }

        private async void editProduct()
        {
            int rammeH = Int32.Parse(KHojdeTxt.Text) - 10;
            int rammeB = Int32.Parse(KBredeTxt.Text) - 10;
            try
            {
                await api.redigerProdukt(OrdreNummerTxt.Text, TidsfristTB.Text, AntalTxt.Text, MatrialeCombo.Text, KHojdeTxt.Text, KBredeTxt.Text, rammeH.ToString(), rammeB.ToString());
            }
            catch
            {
            }
            SavePruduct.Click += SaveBtn_Click;
            SavePruduct.Click -= SaveEditBtn_Click;
            editOn("off");
            OrdreNummerTxt.Clear();
            await ProduktKlasse();
            await stepAdder(OrdreNummerTxt.Text);
        }
        // ------------------------------------------------------------------------------------ nulstiller alle nødvendige text imputs-------------------------------------------------------------------
        public void clearall()
        {

            AntalTxt.Clear();
            KBredeTxt.Clear();
            //matrialeTxt.Clear();
            KHojdeTxt.Clear();
            VirksomhedNavnTxtEdit.Clear();
            KundeAdresseTxtEdit.Clear();
            KundeMailEllerTlfTxtEdit.Clear();
            KundeKontaktNavnTxtEdit.Clear();
            VirksomhedNavnTxtEdit.Text = "*Kunde Navn Her*";
            KundeAdresseTxtEdit.Text = "*Kunde Adresse her*";
            KundeMailEllerTlfTxtEdit.Text = "*Kunde Kontakt her*";
            KundeKontaktNavnTxtEdit.Text = "*Kunde KontaktPerson*";



        }
        private void Orderenummer_textimput(object sender, TextCompositionEventArgs e)// bliver brug i alle textboxe der tager imod int. 
        {
            e.Handled = new Regex("[^0-9]+").IsMatch(e.Text); // man kan kun tasate fra 0 til og med 9 som int
        }

        //---------------------------------------------------------------------------------------------  Key pres event til manuelt orde indtastning ----------------------------------------------------------------------------

        async void EnterHitKeyId(object sender, System.Windows.Input.KeyEventArgs e)
        {
            bool tjek = false;
            if (e.Key == Key.Enter) // venter på et key press som er enter. så vi kan finde idet på en ordre. 
            {
                OversteComboStack.Children.Clear();
                NederestComboStack.Children.Clear();

                foreach (var Variable in ProduktKlasse1)
                {
                    if (Variable.Id == OrdreNummerTxt.Text)
                    {
                        VirksomhedNavnTxt.Text = Variable.Virksomhedsnavn;
                        KundeAdresseTxt.Text = Variable.VirksomhedsAdresse;
                        KundeMailEllerTlfTxt.Text = Variable.VirksomhedsTelefonmnummer.ToString();
                        KundeKontaktNavnTxt.Text = Variable.VirksomhedsKontaktPerson;

                        AntalVisning.Text = Variable.Antal.ToString();
                        KBredeVisning.Text = Variable.KarmBrede;
                        matrialeVisning.Text = Variable.Matriale;
                        khojdeVisning.Text = Variable.KarmHojde;

                        BestillingsdatoTB.Text = Variable.Bestilling;
                        TidsfristTB.Text = Variable.Frist;
                        påBegyndtTB.Text = Variable.Begyndt;
                        FærdigTB.Text = Variable.Afsluttet;
                        tjek = true;

                        await stepAdder(OrdreNummerTxt.Text);
                        int tæller = 0;
                        foreach (var VARIABLE in Mysteps)
                        {

                            if (VARIABLE.Produkt == OrdreNummerTxt.Text) // ader steps dynamisk i backend. 
                            {
                                TextBlock StepsText = new TextBlock();
                                StepsText.Text = VARIABLE.Navn;
                                StepsText.Name = VARIABLE.Navn;
                                StepsText.VerticalAlignment = VerticalAlignment.Center;
                                StepsText.FontSize = 36;
                                StepsText.FontWeight = FontWeights.Bold;
                                StepsText.Width = 250;

                                if (tæller < 3)
                                {
                                    OversteComboStack.Children.Add(StepsText);
                                }
                                else
                                {
                                    NederestComboStack.Children.Add(StepsText);
                                }

                                tæller++;
                            }

                        }
                        tæller = 0;

                        break;

                    }
                    else
                    {
                        Tjek = false;
                    }
                }
            }
        }
        // -------------------------------------------------------------- henter alle steps ---------------------------------------------------------------
        public async Task<List<MySteps>> stepAdder(string produktId)
        {
            Mysteps.Clear();
            JArray stepadder = await api.Steps(produktId);
            if (stepadder == null)
            {
                MessageBox.Show("Denne ordre har ingen steps");
            }
            else
            {
                foreach (JObject VARIABLE in stepadder)
                {
                    string ID = (string)VARIABLE.GetValue("id");
                    string ProduktId = (string)VARIABLE.GetValue("produktId");
                    string Navn = (string)VARIABLE.GetValue("navn");
                    string MedarbejderId = (string)VARIABLE.GetValue("medarbejderId");
                    string Start = (string)VARIABLE.GetValue("start");
                    string Slut = (string)VARIABLE.GetValue("slut");
                    Mysteps.Add(new MySteps(ProduktId, Navn, MedarbejderId, Start, Slut));
                }
            }
            return null;
        }



        // --------------------------------------------------------------- TAP2--------------------------------------------------------------


        async void ProductDropDownChange(object sender, SelectionChangedEventArgs e) // liste alt fra valgte combobox på siden så det er opstillet
        {
            ForsteStepStackTap2.Children.Clear();
            AndenStepStackTap2.Children.Clear();
            int tæller = 0;
            foreach (var variabel in ProduktKlasse1)
            {
                if (variabel.Virksomhedsnavn == SeeAllProductsCombo.SelectedItem)
                {
                    KundeNavn.Text = variabel.Virksomhedsnavn;
                    KundeAdressse.Text = variabel.VirksomhedsAdresse;
                    KundeTlfNum.Text = variabel.VirksomhedsTelefonmnummer.ToString();
                    KundeKontakt.Text = variabel.VirksomhedsKontaktPerson;

                    KarmeAntal.Text = variabel.Antal.ToString();
                    RammeAntal.Text = variabel.Antal.ToString();
                    KarmBrede.Text = variabel.KarmBrede;
                    KarmHojde.Text = variabel.KarmHojde;

                    int rB = Int32.Parse(variabel.KarmBrede);
                    rB = rB - 10;
                    RammeBrede.Text = rB.ToString();
                    int rH = Int32.Parse(variabel.KarmHojde);
                    rH = rH - 10;
                    RammeHojde.Text = rH.ToString();

                    Matrialevis.Text = variabel.Matriale;
                    FristDato.Text = variabel.Frist;
                    BestillingsDato.Text = variabel.Bestilling;
                    AFstedDato.Text = variabel.Afsluttet;
                    PobegyndtDato.Text = variabel.Begyndt;

                    await stepAdder(variabel.Id);
                    foreach (var VARIABLE in Mysteps)
                    {

                        if (VARIABLE.Produkt == variabel.Id)
                        {
                            TextBlock StepsText = new TextBlock();
                            StepsText.Text = VARIABLE.Navn;
                            StepsText.Name = VARIABLE.Navn + 1;
                            StepsText.VerticalAlignment = VerticalAlignment.Center;
                            StepsText.FontSize = 36;
                            StepsText.FontWeight = FontWeights.Bold;
                            StepsText.Width = 250;

                            if (tæller < 3)
                            {
                                ForsteStepStackTap2.Children.Add(StepsText);
                            }
                            else
                            {
                                AndenStepStackTap2.Children.Add(StepsText);
                            }

                            tæller++;
                        }

                    }
                    tæller = 0;

                }
            }
        }

    }
}
