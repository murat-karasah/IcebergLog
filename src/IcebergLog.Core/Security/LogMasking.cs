using System.Text.RegularExpressions;

namespace IcebergLog.Core.Security
{
    public static class LogMasking
    {
        public static string MaskSensitiveData(string logMessage)
        {
            logMessage = Regex.Replace(logMessage, @"\b\d{16}\b", "**** **** **** ****"); // CC
            logMessage = Regex.Replace(logMessage, @"\b\d{3}-\d{2}-\d{4}\b", "***-**-****"); // SSN
            logMessage = Regex.Replace(logMessage, @"\b[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,}\b", "***@***.com"); // E-mail
            return logMessage;
        }
    }
}
