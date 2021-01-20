using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Okuma.Scout.Enums;

namespace Okuma.PanelMode.Common
{
    /// <summary>
    /// A factory to create the proper PanelMode interface instance based on machine type.
    /// </summary>
    public class PanelModeFactory
    {

        /// <summary>
        /// The lazy static instance
        /// </summary>
        private static readonly Lazy<PanelModeFactory> __instance = new Lazy<PanelModeFactory>(() => new PanelModeFactory());

        /// <summary>
        /// The factory instance
        /// </summary>
        public static PanelModeFactory Instance => __instance.Value;

        /// <summary>
        /// PanelMode factory method
        /// </summary>
        /// <returns></returns>
        public IPanelMode CreatePanelMode()
        {
            var machineType = Okuma.Scout.Platform.Machine;

            switch(machineType)
            {
                // Lathe
                case MachineType.L:
                case MachineType.NCM_L:
                case MachineType.PCNCM_L:
                    return new Lathe.PanelModeLathe();

                // Machining Center
                case MachineType.M:
                case MachineType.NCM_M:
                case MachineType.PCNCM_M:
                    return new MC.PanelModeMC();

                // Everything else -- simulate.
                default:
                    return new Sim.PanelModeSim();
            }
        }

    }
}
