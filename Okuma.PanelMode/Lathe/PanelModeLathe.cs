using DataAPI = Okuma.CLDATAPI.DataAPI;
using DataEnum = Okuma.CLDATAPI.Enumerations;
using CmdAPI = Okuma.CLCMDAPI.CommandAPI;
using CmdEnum = Okuma.CLCMDAPI.Enumerations;
using Okuma.PanelMode.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Okuma.PanelMode.Lathe
{
    public class PanelModeLathe : IPanelMode
    {
        private readonly CmdAPI.CViews _views;
        private readonly DataAPI.CMachine _machine;

        public event EventHandler PanelModeChanged;

        public PanelModeLathe()
        {
            _machine = new DataAPI.CMachine();
            _machine.Init();
            _views = new CmdAPI.CViews();
        }

        public void ChangeScreen(PanelGroup panelGroup, string screenName)
        {
            var actualGroup = panelGroup.Translate<PanelGroup, CmdEnum.PanelGroupEnum>();
            _views.ChangeScreen(actualGroup, screenName);
        }

        public Common.PanelMode GetPanelMode()
        {
            return _machine.GetPanelMode().Translate<DataEnum.PanelModeEnum, Common.PanelMode>();
        }
    }
}
