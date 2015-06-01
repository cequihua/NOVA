namespace Mega.Common
{
    public partial class Dim
    {
        public string FullName
        {
            get { return string.Format("{0} {1} {2}", Name, LastName, MotherMaidenName); }
        }

        public string SpouseFullName
        {
            get { return string.Format("{0} {1} {2}", SpouseName, SpouseLastName, SpouseMotherMaidenName); }
        }

        public UDCItem Bank
        {
            get { return UDCItem; }
            set { UDCItem = value; }
        }

        public string BankName
        {
            get { return UDCItem.Name; }
        }

        public string BankCode
        {
            get { return UDCItem.Code; }
        }

        public UDCItem Country
        {
            get { return UDCItem1; }
            set { UDCItem1 = value; }
        }

        public string CountryName
        {
            get { return UDCItem1.Name; }
        }

        public string IVAToApplyName
        {
            get { return UDCItem2.Name; }
        }

        public UDCItem IVAToApply
        {
            get { return UDCItem2; }
            set { UDCItem2 = value; }
        }


        public string MaritalStatusName
        {
            get { return UDCItem3.Name; }
        }

        public UDCItem MaritalStatus
        {
            get { return UDCItem3; }
            set { UDCItem3 = value; }
        }

        public string PopulationName
        {
            get { return UDCItem4.Name; }
        }

        public UDCItem Population
        {
            get { return UDCItem4; }
            set { UDCItem4 = value; }
        }

        public string SexName
        {
            get { return UDCItem5.Name; }
        }

        public UDCItem Sex
        {
            get { return UDCItem5; }
            set { UDCItem5 = value; }
        }

        public string TypeName
        {
            get { return UDCItem6.Name; }
        }

        public UDCItem Type
        {
            get { return UDCItem6; }
            set { UDCItem6 = value; }
        }

        public string StateName
        {
            get { return UDCItem7.Name; }
        }

        public UDCItem State
        {
            get { return UDCItem7; }
            set { UDCItem7 = value; }
        }

        public UDCItem DiffusionMedia
        {
            get { return UDCItem8; }
            set { UDCItem8 = value; }
        }

        public string FullAddress
        {
            get
            {
                return string.Format("{0}{1}{2} {3}{1}{4}{1}{5} {6}{1}{7}", Address1, " ", Address2, Address3, IdPopulation == Constant.CFG_NULL_ROW_VALUE_UDCITEM_KEY ? string.Empty : PopulationName, IdState == Constant.CFG_NULL_ROW_VALUE_UDCITEM_KEY ? string.Empty : StateName, CountryName, CP);
            }
        }
    }
}
