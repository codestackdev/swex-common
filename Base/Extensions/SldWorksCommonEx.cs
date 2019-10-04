using CodeStack.SwEx.Common.Enums;
using System;

namespace SolidWorks.Interop.sldworks
{
    /// <summary>
    /// Collection of common extension methods to use in the SwEx framework
    /// </summary>
    public static partial class SldWorksCommonEx
    {
        /// <summary>
        /// Returns the major version of SOLIDWORKS application
        /// </summary>
        /// <param name="app">Pointer to application to return version from</param>
        /// <param name="servicePack">Version of Service Pack</param>
        /// <param name="servicePackRev">Revision of Service Pack</param>
        /// <returns>Major version of the application</returns>
        public static SwVersion_e GetVersion(this ISldWorks app, out int servicePack, out int servicePackRev)
        {
            var rev = app.RevisionNumber().Split('.');
            var majorRev = int.Parse(rev[0]);
            servicePack = int.Parse(rev[1]);
            servicePackRev = int.Parse(rev[2]);

            return (SwVersion_e)majorRev;
        }

        /// <inheritdoc cref="GetVersion(ISldWorks, out int, out int)"/>
        public static SwVersion_e GetVersion(this ISldWorks app)
        {
            int sp;
            int spRev;
            return app.GetVersion(out sp, out spRev);
        }

        /// <summary>
        /// Checks if the version of the SOLIDWORKS is newer or equal to the specified parameters
        /// </summary>
        /// <param name="app">Current SOLIDWORKS application</param>
        /// <param name="version">Target minimum supported version of SOLIDWORKS</param>
        /// <param name="servicePack">Target minimum service pack version or null to ignore</param>
        /// <param name="servicePackRev">Target minimum revision of service pack version or null to ignore</param>
        /// <returns>True of version of the SOLIDWORKS is the same or newer</returns>
        public static bool IsVersionNewerOrEqual(this ISldWorks app, SwVersion_e version, int? servicePack = null, int? servicePackRev = null)
        {
            if (!servicePack.HasValue && servicePackRev.HasValue)
            {
                throw new ArgumentException($"{nameof(servicePack)} must be specified when {nameof(servicePackRev)} is specified");
            }

            int curSp;
            int curSpRev;
            var curVers = GetVersion(app, out curSp, out curSpRev);

            if (curVers >= version)
            {
                if (servicePack.HasValue && curVers == version)
                {
                    if (curSp >= servicePack.Value)
                    {
                        if (servicePackRev.HasValue)
                        {
                            return curSpRev >= servicePackRev.Value;
                        }
                        else
                        {
                            return true;
                        }
                    }
                }
                else
                {
                    return true;
                }
            }

            return false;
        }
    }
}
