using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Navigation;
using System.Windows.Threading;


namespace Production_and_staff_management_system1
{
    /// <summary>
    /// Interaction logic for MainPage.xaml
    /// </summary>
    public partial class MainPage : Page
    {
        List<MedarbejderClass> personale = new List<MedarbejderClass>();
        List<ProductClass> Products = new List<ProductClass>();
        List<MySteps> MySteps = new List<MySteps>();


        List<MySteps> MySteps2 = new List<MySteps>();
        List<MySteps> MySteps3 = new List<MySteps>();

        List<personaleStempel> personaleStp = new List<personaleStempel>();

        private Api api = new Api();

        string btnSenderClick; // button sender navnet på Page knaperne. 
        
        private DispatcherTimer dispatcherTimer;


        public MainPage()
        {
            InitializeComponent();
            ProduktKlasse();
            personaleNr.Focus();
            getCheckIndUd();
            getMederbejder();
            DispatcherTimer LiveTime = new DispatcherTimer();

            LiveTime.Interval = TimeSpan.FromSeconds(1);
            LiveTime.Tick += timer_Tick;
            LiveTime.Start();

            ProductDataGrid.ItemsSource = Products;
            MedarbejderGrid.ItemsSource = personaleStp;
            
            //Create a timer with interval of 2 secs
            dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 2);


            dummy();
        }


        void timer_Tick(object sender, EventArgs e)
        {
            LiveTimeLabel.Content = DateTime.Now.ToString("dd - MM - yyyy hh: mm:ss");
        }

        public void dummy()
        {

            //personale.Add(new personale("Michael", ""));
            //personale.Add(new personale("Nicklas", ""));

            /*MySteps.Add(new MySteps("Afcutter", false));
            MySteps.Add(new MySteps("Fræser", false));
            MySteps.Add(new MySteps("Presser", false));
            MySteps.Add(new MySteps("Maler", false));
            MySteps.Add(new MySteps("Gennemgang", false));

            MySteps2.Add(new MySteps("tis", false));
            MySteps2.Add(new MySteps("mis", false));
            MySteps2.Add(new MySteps("lis", false));
            MySteps2.Add(new MySteps("fis", false));
            MySteps2.Add(new MySteps("lus", false));

            MySteps3.Add(new MySteps("sdfs", false));
            MySteps3.Add(new MySteps("misfghsfghs", false));
            MySteps3.Add(new MySteps("lsfghfsghis", false));
            MySteps3.Add(new MySteps("fsfghfsghis", false));
            MySteps3.Add(new MySteps("lsfghsfghus", true));*/



            //personale.Add(new MedarbejderClass("Michael", "Admin", "53435135", "27007027B6"));
            //personale.Add(new MedarbejderClass("Nicklas", "Medarbejder", "53435135", "010E553BAA" ));
            //personale.Add(new MedarbejderClass("Morten", "Admin", "53435135", "ED965195"));
            //personale.Add(new MedarbejderClass("Camilla", "Medarbejder", "53435135", "0112D434BD"));
            //personale.Add(new MedarbejderClass("Ole", "Admin", "53435135", "9A9370F7"));


            /*Products.Add(new ProductClass(true, "Tryn AS", 60, 60, 1100, 900, MySteps, "1"));
            Products.Add(new ProductClass(true, "Tryn AS", 60, 60, 1100, 900, MySteps, "1"));
            Products.Add(new ProductClass(false, "Tryn AS", 160, 160, 1100, 900, MySteps, "1"));
            Products.Add(new ProductClass(true, "Tryn AS", 80, 80, 1100, 900, MySteps2, "2"));
            Products.Add(new ProductClass(false, "Tryn AS", 70, 70, 1100, 900, MySteps2, "2"));
            Products.Add(new ProductClass(true, "Tryn AS", 60, 60, 1100, 900, MySteps3, "3"));
            Products.Add(new ProductClass(true, "Tryn AS", 60, 60, 1100, 900, MySteps3, "3"));*/

            //SeeMe.Add(new PictureObject() { PictureFilePath = new Uri("file:///c:\\Windows\\System32\\PerfCenterCpl.ico"), PictureName = "Picture1" });


        }
        // ----------------------------------------------------------- data load fra database-----------------------------------------------------------------------
        public async Task<List<ProductClass>> ProduktKlasse()
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
                    Products.Add(new ProductClass(id,
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

                }
            }
            

            return null;
        }

        public async Task<List<MySteps>> stepAdder(string produktId)
        {
            MySteps.Clear();
            JArray stepadder = await api.Steps(produktId);
            foreach (JObject VARIABLE in stepadder)
            {
                string ID = (string)VARIABLE.GetValue("id");
                string ProduktId = (string)VARIABLE.GetValue("produktId");
                string Navn = (string)VARIABLE.GetValue("navn");
                string MedarbejderId = (string)VARIABLE.GetValue("medarbejderId");
                string Start = (string)VARIABLE.GetValue("start");
                string Slut = (string)VARIABLE.GetValue("slut");
                MySteps.Add(new MySteps(ProduktId, Navn, MedarbejderId, Start, Slut));
            }
            StepGrid.Items.Refresh();
            return null;
        }



        private void dispatcherTimer_Tick(object sender, EventArgs e) //afslutter Timer til Login text og collapser tjektindud TextBlocken
        {
            tjekIndUd.Visibility = Visibility.Collapsed;
            //Disable the timer
            dispatcherTimer.IsEnabled = false;
        }

        // --------------------------------------------------------------------------------------- personale data ----------------------------------------------------------------
        public async Task<List<MedarbejderClass>> getMederbejder()
        {
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
                    personale.Add(new MedarbejderClass(name, type, telefonNummer, kortnummer));
                }
            }

            return null;
        }
        // ..............................................................................................Acces til administrations sider og loading ...................................................
        public void loadingscrean(string on) // venter på en string som styre om Acces griddet skal åbnes eller lukkes
        {
            switch (on)
            {
                case "on":
                    AccesControlGrid.Visibility = Visibility.Visible;
                    break;
                case "off":
                    AccesControlGrid.Visibility = Visibility.Collapsed;
                    break;
            }


        }
        private void enterHitAcces_Click(object sender, KeyEventArgs e)// hvis enter trykker i vores stempelind boks.
        {
            if (e.Key == Key.Enter) // venter på et key press som er enter. rfid scannerne afslutter altid med et enter. 
            {
                getAccesToAdminPages(btnSenderClick);
            }

        }


        //----------------------------------------------------------------------------------- acces kontrol til administrations sider---------------------------------------------------------------------------------------------
        public void getAccesToAdminPages(string btnContet) // holder det scannede rfid kort content op med kortnr i listen.  modtager også en sktirng sm er navnet fra en button sender for at se hvad buttons was pressed
        {

            foreach (var VARIABLE in personale)
            {
                if (VARIABLE.KortNr == AccesTextBox.Text)
                {
                    if (VARIABLE.Type == "Admin")
                    {
                        switch (btnContet)
                        {
                            case "ProductBtn":
                                NavigationService.Navigate(Pages.p3);
                                loadingscrean("off");
                                AccesTextBox.Clear();
                                LoadingText.Text = "Scan Medarbejder kort for adgang!";
                                break;

                            case "EmplyBtn":
                                NavigationService.Navigate(Pages.p4);
                                loadingscrean("off");
                                AccesTextBox.Clear();
                                LoadingText.Text = "Scan Medarbejder kort for adgang!";
                                break;

                        }

                    }
                    else
                    {
                        LoadingText.Text = "Kort ikke genkendt" + Environment.NewLine + "Pørv igen eller kontakt administration";
                        AccesTextBox.Clear();
                    }
                }
            }

            //loadingscrean("off");
        }
        private void annullereBtnAccesLoading(object sender, RoutedEventArgs e)
        {
            loadingscrean("off");
            LoadingText.Text = "Scan Medarbejder kort for adgang!";
        }
        // .............................................................knapper i menuen........................................................................................

        private void ProductBtn_Click(object sender, RoutedEventArgs e)
        {
            string btnContent = (sender as Button).Name.ToString();
            loadingscrean("on");
            AccesTextBox.Focus();

            btnSenderClick = btnContent;

        }

        private void EmplyBtn_Click(object sender, RoutedEventArgs e)
        {
            string btnContent = (sender as Button).Name.ToString();
            loadingscrean("on");
            AccesTextBox.Focus();

            btnSenderClick = btnContent;

        }

        // ............................................................................................ personale stempel  -------------------------------------------------
        public async Task<List<personaleStempel>> getCheckIndUd()
        {
            personaleStp.Clear();
            JArray checkIndUd = await api.getCheckIndUd();
            if (checkIndUd == null)
            {

            }
            else
            {
                foreach (JObject item in checkIndUd)
                {
                    string name = (string)item.GetValue("medarbejderNavn");
                    string kortnummer = (string)item.GetValue("medarbejderKort");
                    string mødetid = (string)item.GetValue("checkInd");
                    string fyreaften = (string)item.GetValue("checkUd");
                    personaleStp.Add(new personaleStempel(name, kortnummer, mødetid, fyreaften));
                }
            }
            MedarbejderGrid.Items.Refresh();
            return null;
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(Pages.p4);
            MessageBox.Show("Scan Medarbejder kort");
        }





        public async void StempelMedarbejder()
        {
            personaleNr.Focus();
            string kortNr = personaleNr.Text;
            JObject check = await api.checkIndUd(kortNr);
            if (check["error"].ToString() == "")
            {
                tjekIndUd.Text = check["status"].ToString();
                tjekIndUd.Visibility = Visibility.Visible;
                dispatcherTimer.Start();
            }
            else
            {
                MessageBox.Show(check["error"].ToString());
            }

            personaleNr.Clear();
            _ = getCheckIndUd();

        }

        private void ManueltScan_Click_1(object sender, RoutedEventArgs e)
        {
            StempelMedarbejder();
        }
        //----------------------------------------------------------------------------------------------------------------produkt valg events
        private void DataGridCell_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var dataGridCellTarget = (DataGridCell)sender; // sender indholdet i den clickede celle i datagriddet.
            string CellData = dataGridCellTarget.ToString();
            CellData = CellData.Replace("System.Windows.Controls.DataGridCell: ", ""); // fjerner en del af stringen så den passer med selve dataet vi vil have frem.

            //MessageBox.Show(CellData);
            string stepsID;
            foreach (var VARIABLE in Products)
            {
                if (CellData == VARIABLE.Id)
                {
                    stepsID = VARIABLE.Id;
                    foreach (var item in MySteps)
                    {
                        if (item.Produkt == stepsID)
                        {
                            StepGrid.ItemsSource = MySteps;
                        }
                    }
                    
                }
            }

            personaleNr.Focus();
        }

        private async void ProductDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e) // ved klik på produkt sættes steps datagrid med liste i produktet.
        {
            try
            {
                
                var row_list = (ProductClass)ProductDataGrid.SelectedItem;
                //listblock1.Content = "You Selected: " + row_list.FirstName + " " + row_list.LastName;
                //MessageBox.Show(row_list.Id);
                await stepAdder(row_list.Id);
                foreach (var VARIABLE in MySteps)
                {
                    if (VARIABLE.Produkt == row_list.Id)
                    {
                        StepGrid.ItemsSource = MySteps;
                    }
                }
                
                personaleNr.Focus();
                

            }
            catch
            {
            }
        }

        string step;
        string produktId;
        private void MedarbejderDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                var step_list = (MySteps)StepGrid.SelectedItem;
                //listblock1.Content = "You Selected: " + row_list.FirstName + " " + row_list.LastName;
                if (step_list != null)
                {
                    step = step_list.Navn;
                    produktId = step_list.Produkt;
                    //MessageBox.Show(step+ " " + produktId);
                    stepScanloadingscrean("on");
                }
                else
                    StepGrid.SelectedItem = null;
                //StepGrid.ItemsSource = row_list.Steps;


                StepTextBox.Focus();

            }
            catch
            {
            }
        }
        // ----------------------------------------------------------------------------------------- acces kontol til steps ----------------------------------------------------
        public void stepScanloadingscrean(string on) // venter på en string som styre om Acces griddet skal åbnes eller lukkes
        {
            StepTextBox.Clear();
            switch (on)
            {
                case "on":
                    ScanOnStepGrid.Visibility = Visibility.Visible;
                    break;
                case "off":
                    ScanOnStepGrid.Visibility = Visibility.Collapsed;
                    break;
            }


        }
        public async void medarbejderOnStep(string kortnummer, string step, string produktId)
        {
            foreach (var VARIABLE in personale)
            {
                if (VARIABLE.KortNr == kortnummer)
                {
                    //MessageBox.Show(VARIABLE.Name + " du er tjekket ind på " + step);
                    await api.CheckInUdStep(produktId, kortnummer, step);
                    await stepAdder(produktId);
                    StepScanLoadingText.Text = "Scan Medarbejder kort";
                    stepScanloadingscrean("off");
                }
                else
                {
                    StepScanLoadingText.Text = "Kort ikke genkendt" + Environment.NewLine + "Pørv igen eller kontakt administration";
                    StepTextBox.Clear();
                }
            }
            StepScanLoadingText.Text = "Scan Medarbejder kort";
        }
        //----------------------------------------------------------------------------------mauelt kort nummer indtaastning ---------------------------------------------------------------------
        private void enterHitScanStep_Click(object sender, KeyEventArgs e)// hvis enter trykker i vores stempelind boks.
        {
            string kortnummer = StepTextBox.Text;
            if (e.Key == Key.Enter)
            {
                medarbejderOnStep(kortnummer, step, produktId);
            }

        }

        private void EnterHit_Click(object sender, KeyEventArgs e)// hvis enter trykker i vores stempelind boks.
        {
            if (e.Key == Key.Enter)
            {
                StempelMedarbejder();
            }

        }
        
        
        private void annullereBtnScanStepLoading(object sender, RoutedEventArgs e)
        {
            stepScanloadingscrean("off");
            personaleNr.Focus();
        }
    }
}
