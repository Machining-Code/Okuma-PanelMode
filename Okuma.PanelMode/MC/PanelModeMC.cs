using DataAPI = Okuma.CMDATAPI.DataAPI;
using DataEnum = Okuma.CMDATAPI.Enumerations;
using CmdAPI = Okuma.CMCMDAPI.CommandAPI;
using CmdEnum = Okuma.CMCMDAPI.Enumerations;
using Okuma.PanelMode.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Okuma.PanelMode.MC
{
    public class PanelModeMC : IPanelMode
    {
        private readonly CmdAPI.CViews _views;
        private readonly DataAPI.CMachine _machine;
        private readonly System.Threading.Timer _changeTimer;
        private PanelGroup _lastGroup;

        public event EventHandler PanelModeChanged;

        private void ChangeTimer_Tick(object state)
        {
            var actualMode = _machine.GetPanelMode();
            PanelGroup panelGroup;

            if (actualMode == DataEnum.PanelModeEnum.Auto || actualMode == DataEnum.PanelModeEnum.Manual || actualMode == DataEnum.PanelModeEnum.MDI)
                panelGroup = PanelGroup.OperationMode;
            else if (actualMode == DataEnum.PanelModeEnum.ProgramOperation)
                panelGroup = PanelGroup.ProgramMode;
            else if (actualMode == DataEnum.PanelModeEnum.ParameterSetup)
                panelGroup = PanelGroup.ParameterMode;
            else if (actualMode == DataEnum.PanelModeEnum.ZeroSetup)
                panelGroup = PanelGroup.ZeroSetMode;
            else if (actualMode == DataEnum.PanelModeEnum.ToolDataSetup)
                panelGroup = PanelGroup.ToolDataSettingMode;
            else if (actualMode == DataEnum.PanelModeEnum.MacMan)
                panelGroup = PanelGroup.MacManMode;
            else
                panelGroup = PanelGroup.OperationMode;

            if (panelGroup != _lastGroup)
                PanelModeChanged?.Invoke(this, EventArgs.Empty);
        }

        public PanelModeMC()
        {
            var name = System.Reflection.Assembly.GetEntryAssembly().GetName().Name ;
            _machine = new DataAPI.CMachine(name);
            _machine.Init();
            _views = new CmdAPI.CViews();
            _changeTimer = new System.Threading.Timer(ChangeTimer_Tick, null, TimeSpan.FromSeconds(5), TimeSpan.FromSeconds(5));
        }

        public void ChangeScreen(PanelGroup panelGroup, string screenName)
        {
            var actualGroup = panelGroup.Translate<PanelGroup, CmdEnum.PanelGroupEnum>();
            _views.ChangeScreen(actualGroup, screenName);
            _lastGroup = panelGroup;
            PanelModeChanged?.Invoke(this, EventArgs.Empty);
        }

        public Common.PanelMode GetPanelMode()
        {
            return _machine.GetPanelMode().Translate<DataEnum.PanelModeEnum, Common.PanelMode>();
        }
    }
}
