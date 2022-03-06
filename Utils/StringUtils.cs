namespace FixBug.Utils
{
    public static class StringUtils
    {
        public static string CaseReplace(this string str, string oldValue, string newValue)
        {
            return str.Replace(oldValue, newValue).Replace(oldValue.ToLower(), newValue).Replace(oldValue.ToUpper(), newValue);
        }

        public static string CaseReplace(this string str, char oldChar, char newChar)
        {
            return str.Replace(oldChar, newChar).Replace(char.ToLower(oldChar), newChar).Replace(char.ToUpper(oldChar), newChar);
        }
    }
}
