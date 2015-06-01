using System;
using System.Collections;
using System.Data;
using System.Data.Linq;
using System.Linq;
using System.Threading;
using System.Web;
using Mega.Common.Enum;

namespace Mega.Common.Helpers
{
    public static class DataHelper
    {
        public static string GetNewPassword()
        {
            return Guid.NewGuid().ToString("N").Substring(6, 6);
        }

        //public static DateTime GetPasswordValidDate(AdminDataContext dc)
        //{
        //    try
        //    {
        //        int days = int.Parse(
        //            dc.UDCItems.Where(u => u.Id == Constant.CFG_USER_PSW_VALID_DAYS_UDCITEM_KEY).Single().Optional1);

        //        return DateTime.Today.AddDays(days);
        //    }
        //    catch (Exception ex)
        //    {
        //        Logger.Error("Error en  GetPasswordValidDate. Detalle: " + ex.Message);
        //        return DateTime.Today.AddDays(30);
        //    }
        //}

        public static void FillAuditoryValues(dynamic auditableObjetc, HttpContext currentHttpContext)
        {
            auditableObjetc.ModifiedBy = currentHttpContext.User.Identity.Name;
            auditableObjetc.ModifiedDate = DateTime.Now;
            auditableObjetc.ModifiedOn = currentHttpContext.Request.UserHostName + "-" +
                                         currentHttpContext.Request.UserHostAddress;
        }

        public static CashierClose GetLastCashierClose(AdminDataContext dc, Guid cashierId)
        {
            return dc.CashierCloses.Where(c => c.IsClosed && !c.IsCashierVerification && c.IdCashier == cashierId).OrderByDescending(c => c.AddedDate).Take(1).SingleOrDefault();
        }

        public static MoneyMovement GetLastCashierOpenMovement(AdminDataContext dc, Guid cashierId)
        {
            return dc.MoneyMovements.Where(m =>
                m.IdType == (int)MovementType.CashierOpen && m.IdCashier == cashierId).OrderByDescending(m => m.AddedDate).Take(1).SingleOrDefault();
        }

        public static IQueryable<MoneyMovement> GetUnClosedMoneyMovement(AdminDataContext dc, Guid cashierId)
        {
            return GetUnClosedMoneyMovement(dc, GetLastCashierClose(dc, cashierId), cashierId);
        }

        public static IQueryable<MoneyMovement> GetUnClosedMoneyMovement(AdminDataContext dc,
                CashierClose lastCashierClose, Guid cashierId)
        {
            var mm =  dc.MoneyMovements.Where(
                m =>
                m.IdType != (int)MovementType.CashierClose && m.IdCashier == cashierId);

            return lastCashierClose == null
                       ? mm
                       : mm.Where(m => m.AddedDate > lastCashierClose.FinalDate);
        }

        public static decimal GetCurrentCash(AdminDataContext dc, int currencyId, Guid cashierId)
        {
            DeleteUnClosedCashierClose(dc, cashierId);
            CashierClose lastClose = GetLastCashierClose(dc, cashierId);

            return GetUnClosedMoneyMovement(dc, lastClose, cashierId).Where(
                m => m.IdOperationCurrency == currencyId && m.UDCItem1.Optional3 == "1").Sum(
                    m => m.OperationAmount);
        }

        public static decimal GetCurrentCashFromAllCurrency(AdminDataContext dc, Guid cashierId)
        {
            DeleteUnClosedCashierClose(dc, cashierId);
            CashierClose lastClose = GetLastCashierClose(dc, cashierId);

            var mm = GetUnClosedMoneyMovement(dc, lastClose, cashierId);

            return mm.Where(m => m.UDCItem1.Optional3 == "1").Sum(m => m.Amount);
        }

        public static void FillAuditoryValuesDesktop(dynamic auditableObjetc)
        {
            auditableObjetc.ModifiedBy = string.IsNullOrWhiteSpace(Thread.CurrentPrincipal.Identity.Name) ? null : Thread.CurrentPrincipal.Identity.Name;
            auditableObjetc.ModifiedDate = DateTime.Now;
            auditableObjetc.ModifiedOn = System.Net.Dns.GetHostName();
        }

        public static IQueryable<UDCItem> GetUDCRoles(AdminDataContext dc)
        {
            return dc.UDCItems.Where(u => u.IdUDC == Properties.Settings.Default.ROLES_UDC_KEY);
        }

        public static IQueryable<UDCItem> GetUDCCountries(AdminDataContext dc)
        {
            return dc.UDCItems.Where(u => u.IdUDC == Properties.Settings.Default.COUNTRIES_UDC_KEY);
        }

        public static IQueryable<UDCItem> GetUDCPopulation(AdminDataContext dc)
        {
            return dc.UDCItems.Where(u => u.IdUDC == Properties.Settings.Default.POPULATION_UDC_KEY);
        }


        public static IList GetPopulationComboList(AdminDataContext dc)
        {
            return AddNullValueItem(dc, GetUDCPopulation(dc).Select(u => new { u.Id, u.Name }).ToList());
        }

        public static IList GetPopulationComboList(AdminDataContext dc, int stateId)
        {
            var c = GetUDCStates(dc).Where(p => p.Id == stateId).SingleOrDefault();

            if (c != null)
            {
                return AddNullValueItem(dc, GetUDCPopulation(dc).Where(u => u.Optional1 == c.Code).Select(u => new { u.Id, u.Name }).OrderBy(u => u.Name).ToList());
            }

            return AddNullValueItem(dc, new ArrayList());
        }

        public static IQueryable<UDCItem> GetUDCOperationStatus(AdminDataContext dc)
        {
            return dc.UDCItems.Where(u => u.IdUDC == Properties.Settings.Default.OPERATION_STATUS_UDC_KEY);
        }

        public static UDCItem GetUDCWebSynch(AdminDataContext dc)
        {
            return dc.UDCItems.Where(u => u.Id == Constant.CFG_WEB_SYNCH_KEY).Single();
        }

        public static int GetUDCDataGridViewPageSize(AdminDataContext dc)
        {
            var udcDataGridViewPageSize = dc.UDCItems.Where(u => u.Id == Constant.CFG_DATA_GRIDVIEW_PAGE_SIZE).SingleOrDefault();

            int result;
            return udcDataGridViewPageSize != null && !string.IsNullOrEmpty(udcDataGridViewPageSize.Optional1) && int.TryParse(udcDataGridViewPageSize.Optional1, out result) ? result : 20;
        }

        public static int GetUDCGridViewPageSize(AdminDataContext dc)
        {
            var udcDataGridViewPageSize = dc.UDCItems.Where(u => u.Id == Constant.CFG_DATA_GRIDVIEW_PAGE_SIZE).SingleOrDefault();

            int result;
            return udcDataGridViewPageSize != null && !string.IsNullOrEmpty(udcDataGridViewPageSize.Optional2) && int.TryParse(udcDataGridViewPageSize.Optional2, out result) ? result : 50;
        }

        public static UDCItem GeCurrentShopIdFromMonitorAction(AdminDataContext dc)
        {
            return dc.UDCItems.Where(u => u.Id == Constant.CFG_NULL_ROW_VALUE_UDCITEM_KEY).Single();
        }

        public static IList GetOperationStatusComboList(AdminDataContext dc, string nullValueText)
        {
            return AddNullValueItem(dc, GetUDCOperationStatus(dc).Select(u => new { u.Id, u.Name }).ToList(),
                                    nullValueText);
        }

        public static IList GetEmptyList(AdminDataContext dc)
        {
            return AddNullValueItem(dc, new ArrayList());
        }

        public static IQueryable<UDCItem> GetUDCStates(AdminDataContext dc)
        {
            return dc.UDCItems.Where(u => u.IdUDC == Properties.Settings.Default.STATE_UDC_KEY);
        }

        public static IList GetStateComboList(AdminDataContext dc)
        {
            return AddNullValueItem(dc, GetUDCStates(dc).Select(u => new { u.Id, u.Name }).OrderBy(u => u.Name).ToList());
        }

        public static IList GetStateComboList(AdminDataContext dc, string countryCode)
        {
            return AddNullValueItem(dc,
                                    GetUDCStates(dc).Where(s => s.Optional1 == countryCode).Select(
                                        u => new { u.Id, u.Name }).ToList());
        }

        #region ADTECH - Obtener el Id del Tipo de Factura de UDCItem
        public static int GetUDCItemIdTypeInvoiceIndividual(AdminDataContext dc)
        {
            return dc.UDCItems.Where(u => u.Code == Properties.Settings.Default.INVOICE_TYPE_INDIVIDUAL_UDCITEM_CODE).Select(u => u.Id).Single();
        }

        public static int GetUDCItemIdTypeInvoiceGeneral(AdminDataContext dc)
        {
            return dc.UDCItems.Where(u => u.Code == Properties.Settings.Default.INVOICE_TYPE_GENERAL_UDCITEM_CODE).Select(u => u.Id).Single();
        }

        #endregion

        #region ADTECH - Obtener el Id del status de la Factura en XSA de UDCItem
        public static int GetUDCItemIdStatusXSAFacturadoInvoice(AdminDataContext dc)
        {
            return dc.UDCItems.Where(u => u.Code == Properties.Settings.Default.INVOICE_STATUS_FACTURADO_XSA_UDCITEM_CODE).Select(u => u.Id).Single();
        }

        public static int GetUDCItemIdStatusXSAPendienteInvoice(AdminDataContext dc)
        {
            return dc.UDCItems.Where(u => u.Code == Properties.Settings.Default.INVOICE_STATUS_PENDIENTE_XSA_UDCITEM_CODE).Select(u => u.Id).Single();
        }

        //public static int GetUDCItemIdStatusXSASinRespuestaInvoice(AdminDataContext dc)
        //{
        //    return dc.UDCItems.Where(u => u.Code == Properties.Settings.Default.INVOICE_STATUS_SIN_RESPUESTA_XSA_UDCITEM_CODE).Select(u => u.Id).Single();
        //}

        #endregion

        #region ADTECH - Obtener el RFC de la compañia de tabla Company
        public static string GetRFCCompany(AdminDataContext dc)
        {
            return dc.UDCItems.Where(u => u.IdUDC == Properties.Settings.Default.RFC_Company).Select(u => u.Name).Single();
            //return dc.Companies.Where(u => u.LegalCertificate == Properties.Settings.Default.RFC_Company).Select(u => u.LegalCertificate).Single();
        }
        #endregion

        #region ADTECH - Obtener el TOKEN de la compañia de tabla Company
        public static string GetTokenXSA(AdminDataContext dc)
        {
            return dc.UDCItems.Where(U => U.IdUDC == Properties.Settings.Default.TOKEN_XSA_CODE).Select(u => u.Name).Single();
            //return dc.Companies.Where(u => u.LegalCertificate == Properties.Settings.Default.RFC_Company).Select(u => u.String1).Single();
        }
        #endregion

        #region ADTECH - Obtener el Tipo de comprobante Fiscal de la Tienda
        public static string GetTipoComprobanteFiscal(AdminDataContext dc)
        {
            return dc.UDCItems.Where(u => u.Code == Properties.Settings.Default.TIPO_COMPROBANTE_FISCAL).Select(u => u.Name).Single();
        }

        public static string GetSerieInvoice(AdminDataContext dc)
        {
            return dc.UDCs.Where(u => u.Id == Properties.Settings.Default.ID_SERIE_INVOICE).Select(u => u.Name).Single();
        }

        public static UDC GetSerie(AdminDataContext dc)
        {
            return dc.UDCs.Where(u => u.Id == Properties.Settings.Default.ID_SERIE_INVOICE).SingleOrDefault();
        }

        #endregion

        public static IQueryable<UDCItem> GetUDCDimType(AdminDataContext dc)
        {
            return dc.UDCItems.Where(u => u.IdUDC == Properties.Settings.Default.DIMTYPE_UDC_KEY);
        }

        public static IQueryable<UDCItem> GetUDCOperationType(AdminDataContext dc)
        {
            return dc.UDCItems.Where(u => u.IdUDC == Properties.Settings.Default.OPERATION_TYPE_UDC_KEY);
        }

        public static IQueryable<UDCItem> GetUDCMaritalStatus(AdminDataContext dc)
        {
            return dc.UDCItems.Where(u => u.IdUDC == Properties.Settings.Default.MARITAL_STATUS_UDC_KEY);
        }

        public static IList GetUDCIVAGroupComboList(AdminDataContext dc, string nullValueText)
        {
            return AddNullValueItem(dc, GetUDCIVAGroup(dc).Select(u => new { u.Id, u.Name }).ToList(),
                                    nullValueText);
        }

        public static IList GetUDCOperationSubTypeComboList(AdminDataContext dc, int idType)
        {
            return AddNullValueItem(dc, GetUDCOperationSubType(dc, idType).Select(u => new { u.Id, u.Name }).ToList());
        }

        public static IQueryable<UDCItem> GetUDCOperationSubType(AdminDataContext dc, int idType)
        {
            string codeType = GetUDCItemRow(dc, idType).Code;

            return dc.UDCItems.Where(u =>
                                     u.IdUDC == Properties.Settings.Default.OPERATION_SUBTYPE_UDC_KEY &&
                                     u.Optional1 == codeType);
        }

        public static IList GetUDCDiffusionMediaComboList(AdminDataContext dc)
        {
            return AddNullValueItem(dc, GetUDCDiffusionMedia(dc).Select(u => new { u.Id, u.Name }).ToList());
        }

        public static IQueryable<UDCItem> GetUDCDiffusionMedia(AdminDataContext dc)
        {
            return dc.UDCItems.Where(u => u.IdUDC == Properties.Settings.Default.DIFFUSION_MEDIA_UDC_KEY);
        }

        public static IQueryable<UDCItem> GetUDCIVAType(AdminDataContext dc)
        {
            return dc.UDCItems.Where(u => u.IdUDC == Properties.Settings.Default.IVATYPE_UDC_KEY);
        }

        public static UDCItem GetIVAValueItem(AdminDataContext dc, string typeCode, string groupCode)
        {
            return dc.UDCItems.Where(u => u.IdUDC == Properties.Settings.Default.IVAVALUE_UDC_KEY &&
                                          u.Optional1 == typeCode && u.Optional2 == groupCode).SingleOrDefault();
        }

        public static IQueryable<UDCItem> GetUDCIVAGroup(AdminDataContext dc)
        {
            return dc.UDCItems.Where(u => u.IdUDC == Properties.Settings.Default.IVAGROUP_UDC_KEY);
        }

        public static IQueryable<UDCItem> GetUDCCurrencies(AdminDataContext dc)
        {
            return dc.UDCItems.Where(u => u.IdUDC == Properties.Settings.Default.CURRENCY_UDC_KEY);
        }

        public static IQueryable<UDCItem> GetUDCInventoryMovements(AdminDataContext dc)
        {
            return dc.UDCItems.Where(u => u.IdUDC == Properties.Settings.Default.INVENTORY_OP_TYPE_UDC_KEY && u.Disabled == false &&
                                          u.Id != (int)OperationType.Receipt &&
                                          u.Id != (int)OperationType.Sale &&
                                          u.Id != (int)OperationType.Consignation &&
                                          u.Id != (int)OperationType.ConsignationReturn);
        }

        public static bool IsInventoryMovement(int IdOperationType)
        {
            return IdOperationType != (int)OperationType.Receipt && IdOperationType != (int)OperationType.Sale &&
                   IdOperationType != (int)OperationType.Consignation &&
                   IdOperationType != (int)OperationType.ConsignationReturn;
        }

        public static IList GetUDCCurrenciesByComboPay(AdminDataContext dc, int currencyToExclude)
        {
            var currencies = dc.UDCItems.Where(
                c =>
                c.IdUDC == Properties.Settings.Default.CURRENCY_UDC_KEY &&
                c.Id != currencyToExclude).Select(c => new { c.Id, Name = c.Code + " " + c.Name });

            if (currencies.Count() > 1)
            {
                return AddNullValueItem(dc, currencies.ToList());
            }

            return currencies.ToList();
        }

        public static IQueryable<UDCItem> GetUDCSexs(AdminDataContext dc)
        {
            return dc.UDCItems.Where(u => u.IdUDC == Properties.Settings.Default.SEX_UDC_KEY);
        }

        public static IQueryable GetSponsorDim(AdminDataContext dc)
        {
            return dc.Dims.Select(u => new { u.Id, FullName = u.Id + " - " + u.FullName });
        }

        public static UDCItem GetUDCItemEmptyValue(AdminDataContext dc)
        {
            return dc.UDCItems.Where(i => i.Id == Properties.Settings.Default.CFG_NULL_ROW_VALUE_UDCITEM_KEY).SingleOrDefault();
        }

        public static IQueryable<UDCItem> GetUDCItemEmptyList(AdminDataContext dc)
        {
            return dc.UDCItems.Where(i => i.Id == Properties.Settings.Default.CFG_NULL_ROW_VALUE_UDCITEM_KEY);
        }

        public static IList AddNullValueItem(AdminDataContext dc, IList list)
        {
            var nullValueItem = GetUDCItemEmptyValue(dc);

            list.Add(new { nullValueItem.Id, nullValueItem.Name });

            return list;
        }

        public static IList AddNullValueItem(AdminDataContext dc, IList list, string nullValueText)
        {
            var nullValueItem = GetUDCItemEmptyValue(dc);

            list.Add(new { nullValueItem.Id, Name = nullValueText });

            return list;
        }

        public static string GetDefaultAddExceptionMessage(Exception ex)
        {
            return (ex.Message.Contains(Properties.Settings.Default.ExceptionMsgDuplicateKeyOnInsert) ||
                   ex.Message.Contains(Properties.Settings.Default.ExceptionMsgDuplicateKeyOnInsert2)
                       ? "Ya existe un elemento con el mismo ID. Detalle: " + ex.Message
                       : "Error inesperado, revise sus datos. Detalle: " + ex.Message).Replace("'", "\"");
        }

        public static int GetOperationSign(AdminDataContext dc, int opId)
        {
            return Convert.ToInt32(dc.UDCItems.Where(u => u.Id == opId).Single().Optional1);
        }

        public static UDCItem GetUDCItemRow(AdminDataContext dc, int id)
        {
            return dc.UDCItems.Where(u => u.Id == id).Single();
        }

        public static Cashier GetCashier(AdminDataContext dc, string id)
        {
            return dc.Cashiers.Where(c => c.Id.ToString() == id).Single();
        }

        public static decimal GetChangeRate(AdminDataContext dc, int idSourceCurrency, string currentShop)
        {
            int idTargetCurrency = GetDefaultCurrencyShop(dc, currentShop);

            if (idSourceCurrency == idTargetCurrency)
            {
                return 1;
            }

            return GetChangeRate(dc, idSourceCurrency, idTargetCurrency);
        }

        public static decimal GetChangeRate(AdminDataContext dc, int idSourceCurrency, int idTargetCurrency)
        {
            string codeSource = GetUDCItemRow(dc, idSourceCurrency).Code;
            string codeTarget = GetUDCItemRow(dc, idTargetCurrency).Code;

            var changeRate =
                dc.UDCItems.Where(
                    u =>
                    u.IdUDC == Properties.Settings.Default.CURRENCY_CHANGE_RATE_UDC_KEY &&
                    u.Optional1 == codeSource && u.Optional2 == codeTarget).Select(
                        u => u.Optional3).SingleOrDefault();

            return changeRate == null ? 0 : Convert.ToDecimal(changeRate);
        }

        public static int GetDefaultCurrencyShop(AdminDataContext dc, string currentShop)
        {
            return dc.Shops.Where(s => s.Id == currentShop).Select(s => s.IdCurrency).Single();
        }

        public static int GetCountryShop(AdminDataContext dc, string currentShop)
        {
            return dc.Shops.Where(s => s.Id == currentShop).Select(s => s.IdCountry).Single();
        }

        public static IQueryable<UDCItem> GetUDCMovementType(AdminDataContext dc)
        {
            return dc.UDCItems.Where(u => u.IdUDC == Properties.Settings.Default.MOV_PAY_TYPE_UDC_KEY);
        }

        public static IQueryable<UDCItem> GetUDCManualMovementType(AdminDataContext dc)
        {
            return dc.UDCItems.Where(u => u.IdUDC == Properties.Settings.Default.MOV_PAY_TYPE_UDC_KEY && u.Optional2 == 1.ToString());
        }

        public static IQueryable<UDCItem> GetUDCBanks(AdminDataContext dc)
        {
            return dc.UDCItems.Where(u => u.IdUDC == Properties.Settings.Default.BANK_UDC_KEY);
        }

        public static IList GetBankComboList(AdminDataContext dc)
        {
            return AddNullValueItem(dc, GetUDCBanks(dc).Select(u => new { u.Id, Name = u.Code + " " + u.Name }).ToList());
        }

        public static bool CashierIsClosed(AdminDataContext dc, string currentCashier)
        {
            var mov = dc.MoneyMovements.Where(m => m.IdCashier.ToString() == currentCashier).OrderByDescending(m => m.AddedDate).
                Take(1).SingleOrDefault();

            return mov == null || mov.IdType == (int)MovementType.CashierClose;
        }

        public static bool IsActiveRoundByFive(AdminDataContext dc, int idOperationCurrency)
        {
            return GetUDCItemRow(dc, idOperationCurrency).Optional1 == "1";
        }

        public static void DeleteUnClosedCashierClose(AdminDataContext dc, Guid cashierId)
        {
            dc.CashierCloses.DeleteAllOnSubmit(dc.CashierCloses.Where(c => !c.IsClosed && c.IdCashier == cashierId));
            dc.SubmitChanges();
        }

        //public static void ClearCache(this AdminDataContext context)
        //{
        //    const BindingFlags flags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;
        //    var method = context.GetType().GetMethod("ClearCache", flags);
        //    method.Invoke(context, null);
        //}

        public static IQueryable<T> GetPage<T>(this IQueryable<T> queryable, int page, int records, out long total)
        {
            total = queryable.Count();
            return queryable.Skip((page - 1) * records).Take(records);
        }

        public static int GetNextSequence(AdminDataContext dc, SequenceId sequenceId)
        {
            dc.Refresh(RefreshMode.OverwriteCurrentValues, dc.Sequences);

            var seq = dc.Sequences.Where(s => s.IdSequence == (int) sequenceId).SingleOrDefault();

            if (seq != null)
            {
                seq.SequenceValue++;
            }
            else
            {
                seq = new Sequence();
                seq.IdSequence = (int)sequenceId;
                seq.SequenceValue = 1;

                dc.Sequences.InsertOnSubmit(seq);
            }

            dc.SubmitChanges();
            return seq.SequenceValue;
        }

        public static IDbTransaction BeginTransaction(AdminDataContext dataContext)
        {
            if (dataContext.Connection.State == ConnectionState.Closed)
            {
                dataContext.Connection.Open();
            }

            dataContext.Transaction = dataContext.Connection.BeginTransaction();

            return dataContext.Transaction;
        }

        public static void CommitTransaction(AdminDataContext dataContext)
        {
            if (dataContext.Transaction != null)
            {
                dataContext.Transaction.Commit();
            }
        }

        public static void RollbackTransaction(AdminDataContext dataContext)
        {
            if (dataContext.Transaction != null)
            {
                dataContext.Transaction.Rollback();
            }
        }

        public static DateTime GetServerDateTime(AdminDataContext dataContext)
        {
            return dataContext.ExecuteQuery<DateTime>("select getdate()").First();
        }
    }
}