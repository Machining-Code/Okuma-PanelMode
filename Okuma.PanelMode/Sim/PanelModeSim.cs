using Okuma.PanelMode.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Okuma.PanelMode.Sim
{
    public class PanelModeSim : IPanelMode
    {
        private Common.PanelMode _panelMode;

        public event EventHandler PanelModeChanged;

        public void ChangeScreen(PanelGroup panelGroup, string screenName)
        {
            switch (panelGroup)
            {
                case PanelGroup.OperationMode:
                    _panelMode = Common.PanelMode.Auto;
                    break;
                case PanelGroup.ProgramMode:
                    _panelMode = Common.PanelMode.ProgramOperation;
                    break;
                case PanelGroup.ParameterMode:
                    _panelMode = Common.PanelMode.ParameterSetup;
                    break;
                case PanelGroup.ZeroSetMode:
                    _panelMode = Common.PanelMode.ZeroSetup;
                    break;
                case PanelGroup.ToolDataSettingMode:
                    _panelMode = Common.PanelMode.ToolDataSetup;
                    break;
                default:
                    _panelMode = Common.PanelMode.Auto;
                    break;
            }

            PanelModeChanged?.Invoke(this, EventArgs.Empty);
        }

        public Common.PanelMode GetPanelMode()
        {
            return _panelMode;
        }
    }
}
