using System;

namespace TestApp.Terminal.Helpers
{
    public static class GuardClausesHelper
    {
        public static void IsNotNull(object argumentValue, string argumentName)
        {
            if (argumentValue == null)
            {
                throw new ArgumentNullException(argumentName);
            }
        }

        public static void IsNotNullOrEmpty(string argumentValue, string argumentName)
        {
            IsNotNull(argumentValue, argumentName);

            if (string.IsNullOrEmpty(argumentValue))
            {
                throw new ArgumentException(argumentName);
            }
        }
    }
}
