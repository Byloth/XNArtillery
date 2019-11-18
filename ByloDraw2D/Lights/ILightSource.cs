using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ByloDraw2D.Lights
{
    public interface ILightSource
    {
        Light Light
        {
            get;
        }
    }
}
