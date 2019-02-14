namespace Microsoft.IoT.ServiceFabric.Common
{
    using System;

    public static class Ensure
    {
        public static void EnsureIsNotNull(this object value, string paramName = null, string message = null)
        {
            if (value == null)
            {
                throw new ArgumentNullException(paramName, message);
            }
        }

        public static void EnsureIsNotNullOrEmpty(this string value, string paramName = null, string message = null)
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new ArgumentNullException(paramName, message);
            }
        }
    }
}
