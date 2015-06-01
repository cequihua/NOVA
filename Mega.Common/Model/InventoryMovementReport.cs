using Mega.Common.Helpers;

namespace Mega.Common
{
    public class InventoryMovementReport
    {
        public string IdLocation { get; set; }
        public string LocationName { get; set; }
        public string UMCode { get; set; }
        public string ProductName { get; set; }
        public string IdProduct { get; set; }
        public double InitialInventoryCount { get; set; }
        public double Entries { get; set; }
        public double Exits { get; set; }
        public double FinalInventoryCount { get; set; }
        public double OperationCount { get; set; }

    }
}
