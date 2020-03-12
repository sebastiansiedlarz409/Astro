using System;
using System.Collections.Generic;
using System.Text;

namespace Astro.BLL.Tools
{
    public class DateFormater
    {
        public string FormatEPIC(string date)
        {
            if (date.Length >= 10)
            {
                string temp = date.Substring(0, 10);
                string result = temp.Replace('-', '/');
                if(result.Split("/").Length != 3)
                    return "0000/00/00";
                return result;
            }
            return "0000/00/00";
        }
    }
}
