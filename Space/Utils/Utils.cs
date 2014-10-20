using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Space
{
    public static class Utils
    {
        public static float[] ToFloat(this object obj, char splitChar)
        {
            string str = obj.ToString();
            string[] strs = str.Split(splitChar);
            float[] result = new float[strs.Length];
            for (int i = 0; i < strs.Length; i++)
                result[i] = Convert.ToSingle(strs[i]);
            return result;
        }
    }
}
