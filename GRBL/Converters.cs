namespace GRBL
{
    public static class Converters
    {
        public static string DotToGRBL(string value)
        {
            return value.Replace(',', '.');
        }

        public static string DotToGRBL(float input)
        {
            return input.ToString().Replace(',', '.');
        }

        public static string DotToFloat(string input)
        {
            return input.Replace('.', ',');
        }

        public static string DotToFloat(float input)
        {
            return input.ToString().Replace('.', ',');
        }
    }
}
