using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Crypton.Extensions
{
    public class HelperFunctions
    {
        public static DateTime UnixTimestampToDateTime(long unixTimeStamp)
        {
            return new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).AddSeconds(unixTimeStamp).ToLocalTime();
        }
    }
}
