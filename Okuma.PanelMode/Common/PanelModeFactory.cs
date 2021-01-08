using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Okuma.PanelMode.Common
{
    public class PanelModeFactory
    {
        public static IPanelMode CreatePanelMode()
        {
            return new Sim.PanelModeSim();    //TODO: implement for lathe, mc, grinder
        }

    }
}
