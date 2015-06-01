namespace Mega.Common.Enum
{
    public enum SequenceId
    {
        DimIdJDE = 1, 

        SaleConsecutive = 2,
        SaleOfficialConsecutive = 3, 
        SaleCanceledConsecutive = 4,

        ConsignationConsecutive = 5,
        ConsignationOfficialConsecutive = 6,
        ConsignationCanceledConsecutive = 7,

        ConsignationReturnConsecutive = 8,
        ConsignationReturnOfficialConsecutive = 9,
        ConsignationReturnCanceledConsecutive = 10,

        ReceiptOfficialConsecutive = 11, 
        ReceiptCanceledConsecutive = 12, 

        CreditCollectConsecutive = 13,
        
        MoneyMovementConsecutive = 14, 
        CashierVerificationConsecutive = 15,
        CashierCloseConsecutive = 16,

        InventoryMovementConsecutive = 17,
        InventoryMovementOfficialConsecutive = 18,
        InventoryMovementCanceledConsecutive = 19, 

        ResetPointMonthControlValue = 100
    }
}