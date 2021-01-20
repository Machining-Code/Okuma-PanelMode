using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Okuma.Scout.Enums;

namespace Okuma.PanelMode.Common
{

    public class PanelModeFactory
    {
        public static IPanelMode CreatePanelMode()
        {
            var machineType = Okuma.Scout.Platform.Machine;

            switch(machineType)
            {
                case MachineType.L:
                case MachineType.NCM_L:
                case MachineType.PCNCM_L:
                    return new Lathe.PanelModeLathe();
                case MachineType.M:
                case MachineType.NCM_M:
                case MachineType.PCNCM_M:
                    return new MC.PanelModeMC();
                default:
                    return new Sim.PanelModeSim();
            }
        }

    }
}
