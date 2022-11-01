using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeliveryApp.Infrastructure.Services
{
    public static class StringFilter
    {
        public static string PhoneTrim(this string str)
        {
            char[] MyChar = { ')', '(', '-', };
            string cCode = "994";
            foreach (char c in MyChar)
            {
                str = str.Replace(c.ToString(), String.Empty);
            }

            str = str.Replace(" ", string.Empty);
            str = str.Substring(1, str.Length - 1);

            return cCode+str;
        }
    }
}
