using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security;
using System.Runtime.InteropServices;

namespace PlayerRegistrator
{
    /// <summary>
    /// Helpers for <see cref="SecureString"/> class
    /// </summary>
    public static class SecureStringHelpers
    {
        /// <summary>
        /// Unsecures a <see cref="SecureString"/> to string
        /// </summary>
        /// <param name="securePassword">The secure string</param>
        /// <returns></returns>
        public static string Unsecure(this SecureString secureString)
        {
            if (secureString == null)
                return string.Empty;

            var unmanagedString = IntPtr.Zero;

            try
            {
                unmanagedString = Marshal.SecureStringToGlobalAllocUnicode(secureString);
                return Marshal.PtrToStringUni(unmanagedString);
            }
            finally
            {
                // Clean up memore allocation
                Marshal.ZeroFreeGlobalAllocUnicode(unmanagedString);
            }
        }
    }
}
