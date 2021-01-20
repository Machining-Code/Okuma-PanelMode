using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Okuma.PanelMode.Common
{
    /// <summary>
    /// THINC Panel Mode interface
    /// </summary>
    public interface IPanelMode
    {
        /// <summary>
        /// Raised when the panel mode changes
        /// </summary>
        event EventHandler PanelModeChanged;

        /// <summary>
        /// Changes the screen
        /// </summary>
        /// <param name="panelGroup">The panel group to display</param>
        /// <param name="screenName">The screen name. Pass an empty string for default.</param>
        void ChangeScreen(PanelGroup panelGroup, string screenName);

        /// <summary>
        /// Gets the panel mode
        /// </summary>
        /// <returns></returns>
        PanelMode GetPanelMode();
    }
}
