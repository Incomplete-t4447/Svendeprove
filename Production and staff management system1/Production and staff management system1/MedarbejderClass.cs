using System;
using System.Collections.Generic;
using System.Text;

namespace Production_and_staff_management_system1
{
    public class MedarbejderClass
    {
        private string _name;
        private string _type;
        private string _number;
        private string _kortNr;

        public MedarbejderClass(string Name, string Type, string Number, string KortNr)
        {
            this._name = Name;
            this._type = Type;
            this._number = Number;
            this._kortNr = KortNr;
        }

        public string Name
        {
            get { return _name ;}
            set { _name = value; }
        }
        public string Type
        {
            get { return _type; }
            set { _type = value; }
        }
        public string Number
        {
            get { return _number; }
            set { _number = value; }
        }
        public string KortNr
        {
            get { return _kortNr; }
            set { _kortNr = value; }
        }
    }
}
