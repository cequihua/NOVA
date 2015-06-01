using Mega.Common.Enum;

namespace Mega.Common
{
    public class Constant
    {
        private Constant() { }

        public static int CFG_SYSTEM_ADMIN_ROLE_UDCITEM_KEY = Properties.Settings.Default.CFG_SYSTEM_ADMIN_ROLE_UDCITEM_KEY;
        public static int CFG_USER_PSW_VALID_DAYS_UDCITEM_KEY = Properties.Settings.Default.CFG_USER_PSW_VALID_DAYS_UDCITEM_KEY;
        public static int CFG_NULL_ROW_VALUE_UDCITEM_KEY = Properties.Settings.Default.CFG_NULL_ROW_VALUE_UDCITEM_KEY;
        public static int CFG_BARCODE_MAX_SIZE_UDCITEM_KEY = Properties.Settings.Default.CFG_BARCODE_MAX_SIZE_UDCITEM_KEY;
        public static int CFG_DIM_MAX_SIZE_UDCITEM_KEY = Properties.Settings.Default.CFG_DIM_MAX_SIZE_UDCITEM_KEY;
        public static int CFG_INVOICE_NAME_IN_TICKET_UDCITEM_KEY = Properties.Settings.Default.CFG_INVOICE_NAME_IN_TICKET_UDCITEM_KEY;
        public static int MAX_DAYS_RETURN_CONSIGNATION_UDC_KEY = Properties.Settings.Default.MAX_DAYS_RETURN_CONSIGNATION_UDC_KEY;
        public static int CFG_WEB_SYNCH_KEY = Properties.Settings.Default.WEB_SYNCH_KEY;
        public static int CFG_DATA_GRIDVIEW_PAGE_SIZE = Properties.Settings.Default.DATA_GRIDVIEW_PAGE_SIZE;
        
        public static string CFG_NOT_LOT_CODE = "SIN_LOTE";

        public static readonly int[] SupervisorOrMore = new[] { (int)Roles.Supervisor, (int)Roles.PosAdmin, (int)Roles.SystemAdmin };
        public static readonly int[] PosAdminOrMore = new[] { (int)Roles.PosAdmin, (int)Roles.SystemAdmin };
    }
}