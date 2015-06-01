using System;

namespace Mega.Common.Model
{
    public class CreditSaleToCollect
    {
        public Guid IdOperation { get; set; }
        public string OfficialConsecutive { get; set; }
        public decimal CreditAmount { get; set; }
        public decimal Billed { get; set; }
        public decimal ToBill { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}
