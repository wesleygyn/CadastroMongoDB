using Microsoft.AspNetCore.Identity;
using System.Globalization;
using System.Text.RegularExpressions;

namespace CadastroMongoDB.Utils
{
    public static class Utils
    {
        public static DateTime? ValidaData(string data)
        {
            DateTime valor;
            try
            {
                if (DateTime.TryParseExact(data, "yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out valor))
                    return valor;
                else
                    return null;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static string GeneratePassword(PasswordOptions opts = null)
        {
            if (opts == null) opts = new PasswordOptions()
            {
                RequiredLength = 30,
                RequiredUniqueChars = 4,
                RequireDigit = true,
                RequireLowercase = true,
                RequireNonAlphanumeric = true,
                RequireUppercase = true
            };

            string[] randomChars = new[] {
            "ABCDEFGHJKLMNOPQRSTUVWXYZ",    // uppercase 
            "abcdefghijkmnopqrstuvwxyz",    // lowercase
            "0123456789",                   // digits
            "!@$?_-"                        // non-alphanumeric
        };

            Random rand = new Random(Environment.TickCount);
            List<char> chars = new List<char>();

            if (opts.RequireUppercase)
                chars.Insert(rand.Next(0, chars.Count),
                    randomChars[0][rand.Next(0, randomChars[0].Length)]);

            if (opts.RequireLowercase)
                chars.Insert(rand.Next(0, chars.Count),
                    randomChars[1][rand.Next(0, randomChars[1].Length)]);

            if (opts.RequireDigit)
                chars.Insert(rand.Next(0, chars.Count),
                    randomChars[2][rand.Next(0, randomChars[2].Length)]);

            if (opts.RequireNonAlphanumeric)
                chars.Insert(rand.Next(0, chars.Count),
                    randomChars[3][rand.Next(0, randomChars[3].Length)]);

            for (int i = chars.Count; i < opts.RequiredLength
                || chars.Distinct().Count() < opts.RequiredUniqueChars; i++)
            {
                string rcs = randomChars[rand.Next(0, randomChars.Length)];
                chars.Insert(rand.Next(0, chars.Count),
                    rcs[rand.Next(0, rcs.Length)]);
            }

            return new string(chars.ToArray());
        }

        public static string FormatCNPJCPF(string vCad)
        {
            if (vCad.Length == 14)
            {
                return Convert.ToUInt64(vCad).ToString(@"00\.000\.000\/0000\-00");
            }
            else if (vCad.Length == 11)
            {
                return Convert.ToUInt64(vCad).ToString(@"000\.000\.000\-00");
            }
            else
            {
                return "Documento inválido";
            }
        }

        public static string FormatDocument(string doc)
        {
            string result = SemFormatacao(doc);

            if (result.Length == 14)
            {
                return Convert.ToUInt64(result).ToString(@"00\.000\.000\/0000\-00");
            }
            else if (doc.Length == 11)
            {
                return Convert.ToUInt64(result).ToString(@"000\.000\.000\-00");
            }
            else
            {
                return "Documento inválido";
            }
        }

        public static string FormatCEP(string vCad)
        {
            //00000 - 000
            if (vCad.Length == 8)
            {
                return Convert.ToUInt64(vCad).ToString(@"00\.000\-000");
            }
            else
            {
                return "Documento inválido";
            }
        }

        public static string SemFormatacao(string Item)
        {
            string pattern = "[^0-9]";
            string replacement = "";
            string result = Regex.Replace(Item, pattern, replacement);

            return result;
        }

        public static DateTime? ConvertUTCToLocalTime(DateTime? dateTime)
        {
            if (dateTime == null)
            {
                return null;
            }
            TimeZoneInfo zone = TimeZoneInfo.FindSystemTimeZoneById("America/Sao_Paulo");

            return TimeZoneInfo.ConvertTimeFromUtc(dateTime.GetValueOrDefault(), zone);
        }
        public static string ConvertDate(DateTime? dt)
        {
            var date = dt.ToString().Split("-")[0] + "/" + dt.ToString().Split("-")[1] + "/" + dt.ToString().Split("-")[2];
            //date = date[0]
            return date;
        }

        public static decimal TruncateDecimalPlaces(decimal val, int places)
        {
            if (places <= 0)
                return Math.Truncate(val);

            var dv = Math.Pow(10, places);
            decimal part = (val % 1) * Convert.ToDecimal(dv);
            val -= (part % 1) / Convert.ToDecimal(dv);
            return val;
        }
    }
}