using PostSharp.Patterns.Diagnostics;
using System.Diagnostics;
using System.Globalization;
using System.Net;

namespace xyLOGIX.Core.Common
{
    /// <summary>Methods to decide whether certain facts are true.</summary>
    public class IsThis
    {
        /// <summary>
        /// Initializes <see langword="static" /> data or performs actions that
        /// need to be performed once only for the
        /// <see cref="T:xyLOGIX.Core.Common.IsThis" /> class.
        /// </summary>
        /// <remarks>
        /// This constructor is called automatically prior to the first instance
        /// being created or before any <see langword="static" /> members are referenced.
        /// <para />
        /// We've decorated this constructor with the <c>[Log(AttributeExclude = true)]</c>
        /// attribute in order to simplify the logging output.
        /// </remarks>
        [Log(AttributeExclude = true)]
        static IsThis() { }

        /// <summary>
        /// Constructs a new instance of
        /// <see cref="T:xyLOGIX.Core.Common.IsThis" /> and returns a reference to it.
        /// </summary>
        /// <remarks>
        /// This is an empty, <see langword="private" /> constructor to prohibit
        /// direct allocation of this class, as it is a <c>Singleton</c> object accessible
        /// via the <see cref="P:xyLOGIX.Core.Common.IsThis.Instance" /> property.
        /// </remarks>
        [Log(AttributeExclude = true)]
        private IsThis()
        { }

        /// Gets a reference to the one and only instance of
        /// <see cref="T:xyLOGIX.Core.Common.IsThis" />
        /// .
        public static IsThis Machine { [DebuggerStepThrough] get; } = new IsThis();

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
                 * If ANY exception occurs -- it does not matter which exception -- then return
                 * FALSE;
                 */

                result = false;
            }

            return result;
        }
    }
}