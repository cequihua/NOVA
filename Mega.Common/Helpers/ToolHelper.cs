using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Linq;
using System.Threading;

namespace Mega.Common.Helpers
{
    public class ToolHelper
    {
        public static string ConvetNumberToLetters(decimal nro, string currencyPrefix, string currencySuffix)
        {
            long entero = Convert.ToInt64(Math.Truncate(nro));
            int decimales = Convert.ToInt32(Math.Round((nro - entero) * 100, 2));

            var dec = string.Format(" {0} CON  {1}/100 {2}", currencyPrefix, decimales, currencySuffix);

            return DoubletoText(Convert.ToDouble(entero)) + dec;
        }

        private static string DoubletoText(double value)
        {
            string Num2Text;

            value = Math.Truncate(value);

            if (value == 0) Num2Text = "CERO";
            else if (value == 1) Num2Text = "UNO";
            else if (value == 2) Num2Text = "DOS";
            else if (value == 3) Num2Text = "TRES";
            else if (value == 4) Num2Text = "CUATRO";
            else if (value == 5) Num2Text = "CINCO";
            else if (value == 6) Num2Text = "SEIS";
            else if (value == 7) Num2Text = "SIETE";
            else if (value == 8) Num2Text = "OCHO";
            else if (value == 9) Num2Text = "NUEVE";
            else if (value == 10) Num2Text = "DIEZ";
            else if (value == 11) Num2Text = "ONCE";

            else if (value == 12) Num2Text = "DOCE";

            else if (value == 13) Num2Text = "TRECE";

            else if (value == 14) Num2Text = "CATORCE";

            else if (value == 15) Num2Text = "QUINCE";

            else if (value < 20) Num2Text = "DIECI" + DoubletoText(value - 10);

            else if (value == 20) Num2Text = "VEINTE";

            else if (value < 30) Num2Text = "VEINTI" + DoubletoText(value - 20);

            else if (value == 30) Num2Text = "TREINTA";

            else if (value == 40) Num2Text = "CUARENTA";

            else if (value == 50) Num2Text = "CINCUENTA";

            else if (value == 60) Num2Text = "SESENTA";

            else if (value == 70) Num2Text = "SETENTA";

            else if (value == 80) Num2Text = "OCHENTA";

            else if (value == 90) Num2Text = "NOVENTA";

            else if (value < 100) Num2Text = DoubletoText(Math.Truncate(value / 10) * 10) + " Y " + DoubletoText(value % 10);

            else if (value == 100) Num2Text = "CIEN";

            else if (value < 200) Num2Text = "CIENTO " + DoubletoText(value - 100);

            else if ((value == 200) || (value == 300) || (value == 400) || (value == 600) || (value == 800))
                Num2Text = DoubletoText(Math.Truncate(value / 100)) + "CIENTOS";

            else if (value == 500) Num2Text = "QUINIENTOS";

            else if (value == 700) Num2Text = "SETECIENTOS";

            else if (value == 900) Num2Text = "NOVECIENTOS";

            else if (value < 1000) Num2Text = DoubletoText(Math.Truncate(value / 100) * 100) + " " + DoubletoText(value % 100);

            else if (value == 1000) Num2Text = "MIL";

            else if (value < 2000) Num2Text = "MIL " + DoubletoText(value % 1000);

            else if (value < 1000000)
            {
                Num2Text = DoubletoText(Math.Truncate(value / 1000)) + " MIL";

                if ((value % 1000) > 0) Num2Text = Num2Text + " " + DoubletoText(value % 1000);
            }

            else if (value == 1000000) Num2Text = "UN MILLON";

            else if (value < 2000000) Num2Text = "UN MILLON " + DoubletoText(value % 1000000);

            else if (value < 1000000000000)
            {
                Num2Text = DoubletoText(Math.Truncate(value / 1000000)) + " MILLONES ";

                if ((value - Math.Truncate(value / 1000000) * 1000000) > 0)
                    Num2Text = Num2Text + " " + DoubletoText(value - Math.Truncate(value / 1000000) * 1000000);
            }

            else if (value == 1000000000000) Num2Text = "UN BILLON";

            else if (value < 2000000000000) Num2Text = "UN BILLON " + DoubletoText(value - Math.Truncate(value / 1000000000000) * 1000000000000);

            else
            {
                Num2Text = DoubletoText(Math.Truncate(value / 1000000000000)) + " BILLONES";

                if ((value - Math.Truncate(value / 1000000000000) * 1000000000000) > 0)
                    Num2Text = Num2Text + " " + DoubletoText(value - Math.Truncate(value / 1000000000000) * 1000000000000);
            }

            return Num2Text;
        }

        public static int RoundByFiveByExcess(decimal amount)
        {
            int roundOpAmount = (int)Math.Ceiling(amount);
            int modulus = roundOpAmount % 5;
            roundOpAmount += (5 - modulus);

            return roundOpAmount;
        }

        public static int RoundByFiveByDefault(decimal amount)
        {
            int roundOpAmount = (int)Math.Floor(amount);
            int modulus = roundOpAmount % 5;
            roundOpAmount -= modulus;

            return roundOpAmount;
        }

        public static string GetInvalidHours(IEnumerable<string> hours)
        {
            var sb = new StringBuilder();

            foreach (var hour in hours)
            {
                var tempHour = hour.Split(':').Length > 1 ? hour : string.Format("{0}:00", hour);

                int hourInt;
                int minInt;

                if (!int.TryParse(tempHour.Split(':')[0], out hourInt) || !int.TryParse(tempHour.Split(':')[1], out minInt) || hourInt > 23 || minInt > 59)
                {
                    sb.Append(hour + ",");
                }
            }

            return sb.ToString().TrimEnd(',');
        }
        
        public static string GetDaysInConfig(string days)
        {
            var result = new List<string>();

            for (var i = 1; i <= days.Split(',').Length; i++)
            {
                if (!days.Split(',')[i - 1].Equals("1")) continue;

                var cultureInfo = new CultureInfo("es-ES");
                var dtfi = cultureInfo.DateTimeFormat;

                result.Add(dtfi.DayNames[i - 1]);
            }

            return string.Join(", ", result.Select(r => r));
        }

        public static string GetDateFormat()
        {
            DateTime tmp;
            return DateTime.TryParse("31/12/2011", out tmp) ? "dd/mm/aaaa" : "mm/dd/aaaa";
        }

        public static string GetConvertionDateFormat()
        {
            DateTime tmp;
            return DateTime.TryParse("31/12/2011", out tmp) ? "dd/MM/yyyy" : "MM/dd/yyyy";
        }

        public static string GetDateTimeFormat()
        {
            DateTime tmp;
            return DateTime.TryParse("31/12/2011", out tmp) ? "dd/mm/aaaa hh:mm am/pm " : "mm/dd/aaaa hh:mm am/pm";
        }

        public static string GetConvertionDateTimeFormat()
        {
            DateTime tmp;
            return DateTime.TryParse("31/12/2011", out tmp) ? "dd/MM/yyyy hh:mm tt" : "MM/dd/yyyy hh:mm tt";
        }

        public static string GetJSConvertionDateFormat()
        {
            DateTime tmp;
            return DateTime.TryParse("31/12/2011", out tmp) ? "dd/mm/yy" : "mm/dd/yy";
        }

        public static string GetJSConvertionDateTimeFormat()
        {
            DateTime tmp;
            return DateTime.TryParse("31/12/2011", out tmp) ? "dd/mm/yy hh:mm tt" : "mm/dd/yy hh:mm tt";
        }

        public static string TrucateString(string message, int maxSize)
        {
            return message.Length > maxSize ? message.Substring(0, maxSize) : message;
        }
    }
}
