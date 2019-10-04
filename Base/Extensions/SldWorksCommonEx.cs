using CodeStack.SwEx.Common.Enums;

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
        /// <returns>Major version of the application</returns>
        public static SwVersion_e GetVersion(this ISldWorks app, out int minor)
        {
            var rev = app.RevisionNumber().Split('.');
            var majorRev = int.Parse(rev[0]);
            minor = int.Parse(rev[1]);

            return (SwVersion_e)majorRev;
        }

        /// <inheritdoc cref="GetVersion(ISldWorks, out int)"/>
        public static SwVersion_e GetVersion(this ISldWorks app)
        {
            int minor;
            return app.GetVersion(out minor);
        }
    }
}
