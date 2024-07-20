using System.Diagnostics;
using System.Globalization;
using System.Net;

namespace xyLOGIX.Core.Common
{
    /// <summary> Methods to decide whether certain facts are true. </summary>
    public class IsThis
    {
        /// <summary>
        /// Empty, static constructor to prohibit direct allocation of this
        /// class.
        /// </summary>
        static IsThis() { }

        /// Empty, protected constructor to prohibit direct allocation of this class.
        protected IsThis() { }

        /// Gets a reference to the one and only instance of
        /// <see cref="T:xyLOGIX.Core.Common.IsThis" />
        /// .
        public static IsThis Machine { [DebuggerStepThrough] get; } =
            new IsThis();

        /// Determines whether this computer is connected to the Internet.
        /// <returns>
        /// <see langword="true" /> if the machine is connected to the Internet;
        /// <see langword="false" /> otherwise.
        /// </returns>
        public bool ConnectedToTheInternet()
        {
            var result = false;

            try
            {
                var url = string.Empty;

                if (CultureInfo.InstalledUICulture.Name.StartsWith("fa"))
                {
                    // Iran
                    url = "http://www.aparat.com";
                }
                else if (CultureInfo.InstalledUICulture.Name.StartsWith("zh"))
                {
                    url = "http://www.baidu.com";
                }
                else
                {
                    url = "https://www.google.com/";
                }


                using (var client = new WebClient())
                {
                    var response = client.DownloadString(url);
                    result = !string.IsNullOrWhiteSpace(response);
                }
            }
            catch
            {
                /*
                 * If ANY exception occurs -- it does not matter
                 * which exception -- then return FALSE;
                 */

                result = false;
            }

            return result;
        }
    }
}