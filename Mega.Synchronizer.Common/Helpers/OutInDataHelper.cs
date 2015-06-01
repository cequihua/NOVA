using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using log4net;
using Mega.Common;
using Mega.Synchronizer.Common.Data;

namespace Mega.Synchronizer.Common.Helpers
{
    public class OutInDataHelper
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(OutInDataHelper));

        private string ConnectionString;

        public const string XML_IN_FILENAME = "WebExport.xml";
        public const string XML_OUT_FILENAME = "PosExport.xml";

        const string STM_SELECT = "select * from ";

        #region AD TECH espeficica si es exportacion de central para no exportar la tabla DeniedProducts

        private bool isExportCentral;
        public bool IsExportCentral { get { return this.isExportCentral; } set { this.isExportCentral = value; } }

        #endregion

        //public const string SPP_POS_OUT_FILL_TABLES = "PosOut_FillTmpTables";
        //public const string SPP_POS_OUT_DROP_TABLES = "PosOut_DropTmpTables";
        //public const string SPP_POS_OUT_CREATE_TABLES = "PosOut_CreateTmpTables";
        //public const string SPP_POS_OUT_SYNCHRONIZE_TABLES = "PosOut_synchronizeTables";

        //public const string SPP_POS_IN_FILL_TABLES = "PosIn_FillTmpTables";
        //public const string SPP_POS_IN_DROP_TABLES = "PosIn_DropTmpTables";
        //public const string SPP_POS_IN_CREATE_TABLES = "PosIn_CreateTmpTables";
        //public const string SPP_POS_IN_SYNCHRONIZE_TABLES = "PosIn_synchronizeTables";

        string[] posOutTables = new[]{
			"Xml_CashierClose",
			"Xml_CashierCloseDetail", 
			"Xml_CashierCloseMoney", 
			"Xml_Dim", 
			"Xml_DimCreditCollect", 
			"Xml_Dim_CreditSaleCollected",
			"Xml_Inventory",
			"Xml_Kardex",
			"Xml_MoneyMovement",
			"Xml_Operation",
			"Xml_OperationDetail",
			"Xml_Operation_Pay", 
            "Xml_Synchronization", 
            "Xml_ExecutedScript",
            "Xml_DeniedProducts",
            "Xml_Invoices",
            "Xml_DistributionData",
		};

        string[] posInTables = new[]{
			"Xml_UDC",
			"Xml_UDCItem", 
			"Xml_Users", 
			"Xml_User_Rol", 
            "Xml_Shop", 
			"Xml_User_Shop", 
			"Xml_Company",
			"Xml_Cashier",
            "Xml_Location",
			"Xml_Dim",
			"Xml_Dim_CreditSaleCollected",
            "Xml_Product",
			"Xml_Product_Price",
			"Xml_ProductComposition", 
            "Xml_Operation",
            "Xml_OperationDetail", 
            "Xml_Synchronization",
            "Xml_DeniedProducts",
            "Xml_Invoices",
            "Xml_DistributionData",
		};


        public static string GetValue(DataRow row, string column)
        {
            object value = row[column];

            return (value is DBNull) ? null : (string)value;
        }

        public static bool GetBoolValue(DataRow row, string column)
        {
            object value = row[column];

            return (value is DBNull) || string.IsNullOrEmpty(value.ToString()) ? false : bool.Parse(value.ToString());
        }

        public static int? GetIntValue(DataRow row, string column)
        {
            object value = row[column];

            return (value is DBNull) || string.IsNullOrEmpty(value.ToString()) ? null : (int?)int.Parse(value.ToString());
        }

        public static DateTime? GetDateTimeValue(DataRow row, string column)
        {
            object value = row[column];

            return (value is DBNull) || string.IsNullOrEmpty(value.ToString()) ? null : (DateTime?)DateTime.Parse(value.ToString());
        }


        public OutInDataHelper(string connectionString)
        {
            ConnectionString = connectionString;
        }

        public int ExecuteSimpleScript(string simpleScript, string connString)
        {
            using (SqlConnection sqlConn = new SqlConnection(connString))
            {
                sqlConn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = sqlConn;

                cmd.CommandText = simpleScript;

                int valueRet = cmd.ExecuteNonQuery();
                sqlConn.Close();

                return valueRet;
            }
        }

        //public void PosExportFillTablesInDb(string initialdate)
        //{
        //    //Garantizar el formato de fecha en el servidor antes
        //    string strScript = "SET DATEFORMAT mdy exec " + SPP_POS_OUT_FILL_TABLES + " '" + initialdate + "'";

        //    ExecuteSimpleScript(strScript, ConnectionString);
        //}

        //public void WebExportFillTablesInDb(string initialdate, string shopId)
        //{
        //    //Garantizar el formato de fecha en el servidor antes
        //    string strScript = "SET DATEFORMAT mdy exec " + SPP_POS_IN_FILL_TABLES + " '" + initialdate + "', '" + shopId + "'";

        //    ExecuteSimpleScript(strScript, ConnectionString);
        //}

        //public DataTable GetShops()
        //{
        //    //SqlCommand cmd = new SqlCommand("select Id, Name from dbo.Shop where Disabled = 0", cnn);

        //    SqlConnection cnn = new SqlConnection(ConnectionString);
        //    SqlDataAdapter adapter = new SqlDataAdapter("select Id, Name from dbo.Shop where Disabled = 0 order by Name", cnn);

        //    DataTable tbl = new DataTable();
        //    adapter.Fill(tbl);

        //    return tbl;
        //}

        public PosOutDataSet PosExportLoadDataSet()
        {
            //PosExportFillTablesInDb(initialDate);

            SqlDataAdapter adapter;
            PosOutDataSet ds = new PosOutDataSet();
            SqlConnection cnn = new SqlConnection(ConnectionString);

            foreach (string t in posOutTables)
            {
                adapter = new SqlDataAdapter(STM_SELECT + t, cnn);
                adapter.Fill(ds, t);
            }

            return ds;
        }

        public PosInDataSet WebExportLoadDataSet()
        {
            //WebExportFillTablesInDb(initialDate, shopId);

            SqlDataAdapter adapter;
            PosInDataSet ds = new PosInDataSet();
            SqlConnection cnn = new SqlConnection(ConnectionString);

            foreach (string t in posInTables)
            {
                if (IsExportCentral && t == "Xml_DeniedProducts")
                    continue;
                else if (IsExportCentral && t == "Xml_Invoices")
                    continue;
                else if (IsExportCentral && t == "Xml_DistributionData")
                    continue;

                adapter = new SqlDataAdapter(STM_SELECT + t, cnn);
                adapter.Fill(ds, t);
            }

            return ds;
        }

        //public void PosExportDropTablesInDb()
        //{
        //    string strScript = "exec " + SPP_POS_OUT_DROP_TABLES;
        //    ExecuteSimpleScript(strScript, ConnectionString);
        //}

        //public void WebExportDropTablesInDb()
        //{
        //    string strScript = "exec " + SPP_POS_IN_DROP_TABLES;
        //    ExecuteSimpleScript(strScript, ConnectionString);
        //}

        //public void WebImportCreateTablesInDb()
        //{
        //    string strScript = "exec " + SPP_POS_OUT_CREATE_TABLES;
        //    ExecuteSimpleScript(strScript, ConnectionString);
        //}

        public void WebImportFillTablesInDb(PosOutDataSet ds)
        {
            //WebImportCreateTablesInDb();

            SqlDataAdapter adapter;

            using (SqlConnection cnn = new SqlConnection(ConnectionString))
            {
                cnn.Open();

                foreach (string t in posOutTables)
                {
                    adapter = new SqlDataAdapter(STM_SELECT + t, cnn);

                    //El command builder le genera al ASdapter los demas SQLCommand
                    //para las restantes operaciones de Insert, Update, Delete
                    SqlCommandBuilder cmdBld = new SqlCommandBuilder(adapter);

                    adapter.Update(ds, t);
                }
            }
        }

        //public void WebImportDropTablesInDb()
        //{
        //    string strScript = "exec " + SPP_POS_OUT_DROP_TABLES;
        //    ExecuteSimpleScript(strScript, ConnectionString);
        //}

        //public void WebImportSynchonizeTables()
        //{
        //    string strScript = "exec " + SPP_POS_OUT_SYNCHRONIZE_TABLES;
        //    ExecuteSimpleScript(strScript, ConnectionString);

        //    WebImportDropTablesInDb();
        //}

        public void PosImportFillTablesInDb(PosInDataSet ds)
        {
            //PosImportCreateTablesInDb();

            SqlDataAdapter adapter;

            using (SqlConnection cnn = new SqlConnection(ConnectionString))
            {
                cnn.Open();

                foreach (string t in posInTables)
                {
                    adapter = new SqlDataAdapter(STM_SELECT + t, cnn);

                    //El command builder le genera al ASdapter los demas SQLCommand
                    //para las restantes operaciones de Insert, Update, Delete
                    SqlCommandBuilder cmdBld = new SqlCommandBuilder(adapter);

                    adapter.Update(ds, t);
                }
            }
        }

        //public void PosImportCreateTablesInDb()
        //{
        //    string strScript = "exec " + SPP_POS_IN_CREATE_TABLES;
        //    ExecuteSimpleScript(strScript, ConnectionString);
        //}

        //public void PosImportSynchonizeTables()
        //{
        //    string strScript = "exec " + SPP_POS_IN_SYNCHRONIZE_TABLES;
        //    ExecuteSimpleScript(strScript, ConnectionString);

        //    PosImportDropTablesInDb();
        //}

        //public void PosImportDropTablesInDb()
        //{
        //    string strScript = "exec " + SPP_POS_IN_DROP_TABLES;
        //    ExecuteSimpleScript(strScript, ConnectionString);
        //}

        public static Synchronization EnsureSynchronizationConfiguration(AdminDataContext dataContext, Shop shop, string defaultHoursPlanIn, string defaultHoursPlanOut)
        {
            var synchronization = new Synchronization
                                      {
                                          Shop = shop,
                                          DaysPlanIn = "1,1,1,1,1,1,1",
                                          HoursPlanIn = defaultHoursPlanIn,
                                          DaysPlanOut = "1,1,1,1,1,1,1",
                                          HoursPlanOut = defaultHoursPlanOut
                                      };

            dataContext.Synchronizations.InsertOnSubmit(synchronization);

            dataContext.SubmitChanges();

            return synchronization;
        }

        public static IList<string> EnsureHourConfiguration(string[] hours)
        {
            var result = new List<string>();

            foreach (var hour in hours)
            {
                var tempHour = hour.Split(':').Length > 1 ? hour : string.Format("{0}:00", hour);

                int hourInt;
                int minInt;

                if (!int.TryParse(tempHour.Split(':')[0], out hourInt) || !int.TryParse(tempHour.Split(':')[1], out minInt) || hourInt > 23 || hourInt > 59)
                {
                    Logger.Error(string.Format("Error! Hora de sincronización incorrecta: [{0}].", hour));
                }
                else
                {
                    result.Add(tempHour);
                }
            }

            return result;
        }

        public void UpdateShopToExecuteScript(PosOutDataSet outDataSet, string currentShopId)
        {
            foreach (PosOutDataSet.Xml_ExecutedScriptRow r in outDataSet.Xml_ExecutedScript.Rows)
            {
                r.IdShop = currentShopId;
            }
        }

    }


}
