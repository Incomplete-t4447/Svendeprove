using System.Collections.Generic;

namespace Production_and_staff_management_system1
{
    public class ProductClass
    {
        private string _virksomhedNavn;
        private string _virksomhedAdresse;
        private int _virksomhedTlfnummer;
        private string _virksomhedKontaktPerson;
        private string _id;
        private string _kundeId;
        private string _bestilling;
        private string _frist;
        private int _antal;
        private string _matriale;
        private string _karmHojde;
        private string _karmBrede;
        private string _rammeHojde;
        private string _rammeBrede;
        private string _begyndt;
        private string _afsluttet;


        public ProductClass( string Id, string Frist, string Matriale, int Antal,  string KarmHojde, string KarmBrede, string RammeHojde, string RammeBrede, string Begyndt, string Bestilling, string Afsluttet, string Virksomhedsnavn, int VirksomhedsTelefonmnummer, string VirksomhedsAdresse, string VirksomhedsKontaktPerson,  string KundeId )
        {
            this._virksomhedNavn = Virksomhedsnavn;
            this._virksomhedAdresse = VirksomhedsAdresse;
            this._virksomhedTlfnummer = VirksomhedsTelefonmnummer;
            this._virksomhedKontaktPerson = VirksomhedsKontaktPerson;
            this._id = Id;
            this._kundeId = KundeId;
            this._bestilling = Bestilling;
            this._frist = Frist;
            this._antal = Antal;
            this._matriale = Matriale;
            this._karmHojde = KarmHojde;
            this._karmBrede = KarmBrede;
            this._rammeHojde = RammeHojde;
            this._rammeBrede = RammeBrede;
            this._begyndt = Begyndt;
            this._afsluttet = Afsluttet;
           




        }

        public string Id
        {
            get { return _id; }
            set { _id = value; }
        }
        public string Frist
        {
            get { return _frist; }
            set { _frist = value; }
        }
        public string Matriale
        {
            get { return _matriale; }
            set { _matriale = value; }
        }
        public int Antal
        {
            get { return _antal; }
            set { _antal = value; }
        }
        public string KarmHojde
        {
            get { return _karmHojde; }
            set { _karmHojde = value; }
        }

        public string KarmBrede
        {
            get { return _karmBrede; }
            set { _karmBrede = value; }
        }
        public string RammeHojde
        {
            get { return _rammeHojde; }
            set { _rammeHojde = value; }
        }
        public string RammeBrede
        {
            get { return _rammeBrede; }
            set { _rammeBrede = value; }
        }
        public string Begyndt
        {
            get { return _begyndt; }
            set { _begyndt = value; }
        }
        public string Bestilling
        {
            get { return _bestilling; }
            set { _bestilling = value; }
        }
        public string Afsluttet
        {
            get { return _afsluttet; }
            set { _afsluttet = value; }
        }
        public string Virksomhedsnavn
        {
            get { return _virksomhedNavn; }
            set { _virksomhedNavn = value; }
        }

        public string VirksomhedsAdresse
        {
            get { return _virksomhedAdresse; }
            set { _virksomhedAdresse = value; }
        }
       
        public int VirksomhedsTelefonmnummer
        {
            get { return _virksomhedTlfnummer; }
            set { _virksomhedTlfnummer = value; }
        }
        public string VirksomhedsKontaktPerson
        {
            get { return _virksomhedKontaktPerson; }
            set { _virksomhedKontaktPerson = value; }
        }
        
        public string KundeId
        {
            get { return _kundeId; }
            set { _kundeId = value; }
        }
       
       

       

        
       
        

    }
    
}
