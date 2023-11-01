using System;

namespace AsyncBasicThread.Utils
{
    public class ConvertUtils
    {
        public static string AutoConvertBytes(decimal bytes,int decimalPlaces = 2)
        {
            string[] sizes = { "B", "KB", "MB", "GB", "TB" };
            int order = 0;
            while(bytes>=1024 && order <sizes.Length -1)
            {
                order++;
                bytes /= 1024;
            }

            string result = $"{Math.Round(bytes, decimalPlaces)}{sizes[order]}";
            return result;
        }
    }
}