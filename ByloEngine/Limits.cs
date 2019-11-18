using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ByloEngine
{
    public class Limits
    {
        public float Minimum;
        public float Maximum;

        public Limits()
        {
            Minimum = 0;
            Maximum = 0;
        }

        public Limits(float minimum, float maximum)
        {
            Minimum = minimum;
            Maximum = maximum;
        }
    }
}
