using System;

namespace FixBug.Utils
{
    public static class UriUtils
    {
        public static string Escape(this string uri)
        {
            return Uri.EscapeDataString(uri).CaseReplace("%2f", "/").CaseReplace("%5c", "\\").CaseReplace("%3a", ":");
        }
    }
}
