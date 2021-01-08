using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Okuma.PanelMode.Common
{

    public interface IPanelMode
    {
        event EventHandler PanelModeChanged;

        void ChangeScreen(PanelGroup panelGroup, string screenName);
        PanelMode GetPanelMode();
    }
}
