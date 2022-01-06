using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Production_and_staff_management_system1
{
    public class MySteps
    {
        private string _produktId;
        private string _navn;
        private string _medarbejderId;
        private string _start;
        private string _slut;
        public MySteps(string Produkt, string Navn, string MedArbjderId, string Start, string Slut)
        {
            this._produktId = Produkt;
            this._navn = Navn;
            this._medarbejderId = MedArbjderId;
            this._start = Start;
            this._slut = Slut;
        }

        public string Produkt
        {
            get { return _produktId; }
            set { _produktId = value; }
        }
        public string Navn
        {
            get { return _navn; }
            set { _navn = value; }
        }

        public string MedArbjderId
        {
            get { return _medarbejderId; }
            set { _medarbejderId = value; }
        }
        public string Start
        {
            get { return _start; }
            set { _start = value; }
        }
        public string Slut
        {
            get { return _slut; }
            set { _slut = value; }
        }

    }

    class personale
    {
        private string _name;
        private string _kortNr;

        public personale(string name, string kortNr)
        {
            this._name = name;
            this._kortNr = kortNr;
        }
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public string KortNr
        {
            get { return _kortNr; }
            set { _kortNr = value; }
        }

    }

    public class personaleStempel
    {
        private string _name;
        private string _kortNr;
        private string _mødetid;
        private string _fyraften;

        public personaleStempel(string name, string kortNr, string mødeTid, string fyrAften)
        {
            this._name = name;
            this._kortNr = kortNr;
            this._mødetid = mødeTid;
            this._fyraften = fyrAften;
        }
        public string name
        {
            get { return _name; }
            set { _name = value; }
        }

        public string kortNr
        {
            get { return _kortNr; }
            set { _kortNr = value; }
        }
        public string mødeTid
        {
            get { return _mødetid; }
            set { _mødetid = value; }
        }
        public string fyrAften
        {
            get { return _fyraften; }
            set { _fyraften = value; }
        }

    }
}
