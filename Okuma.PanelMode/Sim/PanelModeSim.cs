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
        private readonly System.Threading.Timer _changeTimer; 
        private Common.PanelMode _panelMode;

        public event EventHandler PanelModeChanged;

        private void ChangeTimer_Tick(object state) => ChangeScreen(PanelGroup.OperationMode, "");

        public PanelModeSim()
        {
            var name = System.Reflection.Assembly.GetEntryAssembly().GetName().Name;
            Console.WriteLine($"Logging name {name}");
            _changeTimer = new System.Threading.Timer(ChangeTimer_Tick, null, TimeSpan.FromSeconds(5), TimeSpan.FromSeconds(5));
        }

        public void ChangeScreen(PanelGroup panelGroup, string screenName)
        {
            Console.WriteLine($"ChangeScreen {panelGroup}, {screenName}");

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
                case PanelGroup.MacManMode:
                    _panelMode = Common.PanelMode.MacMan;
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
