﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Destroyer
{
    internal static class Resources
    {
        internal static readonly Image Plugin_Destroying;
        private const string plugin_destroying = "iVBORw0KGgoAAAANSUhEUgAAABAAAAAQCAMAAAAoLQ9TAAAAAXNSR0IArs4c6QAAAARnQU1BAACxjwv8YQUAAAMAUExURQACAAMCAQMDAwMCBQEEAgQCAwQFAwUGBwcJBwYJCQgBBAoECQwHDQkICAwICAwMCREEABIGAh8JCB8LDhMPERIPFBgPEhgRDxQSFBQSFxkWGhwYHiIDAiYGCiUKCCkKATAGBjMLBzYJBDUMCTYPCDgIAzgMBDsNBz4JAzsNCzsODD4NCz4RByoeIi8cJjAbIyYlJzIjKTs5PEIJBUALBUALB0ILC0MMDkcVBEIWCEQUCEoVB0oVCk4ZBkkQEU0eHlEOB1UPClkKBFESC1ETDFEVClYVClQVC1cVDFAdD1caC1UZDlYaDVcaD14VB1oQD1gVClgWDF4SC10QDF8VDFwVD1geBVoYC10ZCV4cDFUTFFMbEVsYE1MgFVcgGEIlL0IlNUwrN0YzPks9PmQPDWoPDGMdBmUdBmMeDm4VB20TDGgZCmseDWUVFGMZEGAZEWIbFmIfE2MdFGMfF2UeEm8cE3YVB3EZDnEdEHMdFnAeFHQYE3AfGnYeGXQfHXkWEXgdEHwfEn8fFWoiD2UhFGshFG8iEm0lHHgiDHchEHcjF3YjGHUjG3kgE3sjEHgjFHoiFn8hFn0kEHolGn4lGn0lHH8nHX8oHH8rHXkwH2Y1P2M4O2k9R0xJSllEQlxLT29JT4sfCoAcEoIfE4YaEoInG44mE4ghGo0mGYoqGokpH4suHY0uHJUjHJIsHJkhEpsgFZknGZ0vGZUwFo86Loc1MIY6P4w3N5YuJZ4zJpk3LZ4yNKYlFaYkH6YrFKgrHK03F7AkG70qFbA0FLI1H6A2JKk2IKo2KaA2MqE+Pqg5OKs4ObE4Jbc6J7A9LrI4MoA7RaY4R6dBNrBBOr9DOpNERK1HT7FESLNIQ71LQb5WSrJfbcI9Icc5McdEN81LNM9KNcJHQ8RJRMtOWtdNTNBRUNZRU8RPYt9dZdhfctpfd9xdesBgcOJWYuNcbeBbceZiYuRqaPBqb9tojOZSgeVZgeVag+ZumOt4j+h9rYuKjK6kpd6dtOWGo+Ggw+zJ1lBjO1sAAAAJcEhZcwAACxIAAAsSAdLdfvwAAAAadEVYdFNvZnR3YXJlAFBhaW50Lk5FVCB2My41LjEwMPRyoQAAARtJREFUKFMBEAHv/gAbCgEAAg4JDA8IBgcVnTANAAQFAwstoJxhYF9iY577+jIAFy8uMZvq9fP5/vzZ2v+fGgA/2+zVtO709vf91Nzx+JoUAEnX5M2y6Ovn1s7A3/Lv0BgAOX3exMXTyKWIs8Pg8O22GQAqOoBmVme45dKszKa65rcWACNKqpRrg5jL48mQPUfKtRMANbHH4auOuYpDVFk7PFiHHQAgvuLp2Kl6OFeVhVBFTG82AHu9wbvRVSxNhpdyW0Z5KBAAgWmvjXxdXoySqGh0gqNtPgBksGqRxplLfoSTj4l4bjMpACRCUt3Cz3AmJTSnbB83IREAlq1Rf6F2U3FzRK5OHCInHgCLpEhlv7yiQFx1d09aQSsSQJJ/gb8xS7IAAAAASUVORK5CYII=";

        static Resources()
        {
            using (MemoryStream memoryStream = new MemoryStream(Convert.FromBase64String(plugin_destroying)))
            {
                Plugin_Destroying = Image.FromStream(memoryStream);
            }
        }

    }
}
