using System.Collections.Generic;

namespace GRBL.Controls
{
    public class G_CODE
    {
        public int ? N { get; set; } = null;
        public List<int> G { get; set; } = new List<int>();
        public float ? X { get; set; } = null;
        public float ? Y { get; set; } = null;
        public float ? Z { get; set; } = null;
        public float ? I { get; set; } = null;
        public float ? J { get; set; } = null;
        public float ? K { get; set; } = null;
        public int ? F { get; set; } = null;

        public override string ToString()
        {
            string s = "N" + N.ToString() + " ";

            foreach (int i in G)
            {
                s += string.Format("G{0} ", i);
            }

            return string.Format("{0} X{1} Y{2} Z{3} F{4} I{5} J{6} K{7}", s, X, Y, Z, F, I, J, K);
        }
    }
}
