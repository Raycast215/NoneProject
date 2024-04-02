

namespace Template.Utility
{
    public static class Util
    {
        public static int ClampIndex(int index, int min, int max)
        {
            var ret = index;

            if (index < min)
            {
                ret = max;
            }

            if (index > max)
            {
                ret = min;
            }

            return ret;
        }
    }
}