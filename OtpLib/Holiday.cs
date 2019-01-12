using System;
using System.Collections.Generic;
using System.Text;

namespace OtpLib
{
    public class Holiday
    {
        public string AskIsTodayXmas()
        {
            if (GetDateTime().Day == 25 && GetDateTime().Month == 12)
            {
                return "Merry Xmas";
            }

            return "Today is not Xmas";
        }

        protected virtual DateTime GetDateTime()
        {
            return DateTime.Now;
        }
    }
}
