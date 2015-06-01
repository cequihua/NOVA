using System;
using Mega.Common.Enum;

namespace Mega.Common
{
    public partial class Operation_Pay
    {
        public string CurrencyCode
        {
            get { return UDCItem == null ? string.Empty : UDCItem.Code; }
        }

        public string PayCurrencyTypeDetail
        {
            get
            {
                return IdType != (int)MovementType.SaleCurrency && IdType != (int)MovementType.CreditCollectCurrency
                           ? string.Empty
                           : string.Format("->Rec.: {0} {1}, Cambio: {2} {3}",
                                           (ReceivedAmountOperation ?? 0).ToString("N"), CurrencyCode,
                                           (ChangeAmount ?? 0).ToString("N"), Operation != null ? Operation.OperationCurrencyCode : DimCreditCollect.OperationCurrencyCode);
            }
        }

        public UDCItem Currency
        {
            get { return UDCItem; }
            set { UDCItem = value; }
        }

        public string TypeName
        {
            get { return UDCItem1 == null ? string.Empty : UDCItem1.Name; }
        }

        public UDCItem Type
        {
            get { return UDCItem1; }
            set { UDCItem1 = value; }
        }

        public int TypeSign
        {
            get { return UDCItem1 == null ? 0 : Convert.ToInt32(UDCItem1.Optional1); }
        }

        public bool TypeIsManual
        {
            get { return UDCItem1 == null ? false : UDCItem1.Optional2 == "0" ? false : true; }
        }

        public string DimName
        {
            get { return Operation != null ? Operation.DimName : DimCreditCollect.DimName; }
        }

        public int? IdDim
        {
            get { return Operation != null ? Operation.IdDim : DimCreditCollect.IdDim;  }
        }

        public decimal DimMaxCreditAmount
        {
            get { return Operation != null ? Operation.DimMaxCreditAmount : DimCreditCollect.DimMaxCreditAmount; }
        }

        public decimal DimCreditAmount
        {
            get { return Operation != null ? Operation.DimCreditAmount : DimCreditCollect.DimCreditAmount; }
        }

        public string OfficialConsecutive
        {
            get { return Operation != null ? Operation.OfficialConsecutive : DimCreditCollect.Consecutive; }
        }
    }
}
