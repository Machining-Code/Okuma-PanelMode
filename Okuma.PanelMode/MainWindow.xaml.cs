using Okuma.PanelMode.Common;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;

namespace Okuma.PanelMode
{
    /// <summary>Interaction logic for MainWindow.xaml</summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        /// <summary>Snap-to-edge distance in pixels</summary>
        private const double SNAP_DISTANCE = 20;

        /// <summary>Marlett up-arrow character</summary>
        private const string MARLETT_UP_ARROW = "t";

        /// <summary>Marlett down-arrow character</summary>
        private const string MARLETT_DOWN_ARROW = "u";

        /// <summary>Marlett up-down-arrow character</summary>
        private const string MARLETT_UP_DOWN_ARROW = "v";

        /// <summary>IPanelMode THINC wrapper</summary>
        private readonly IPanelMode _panelMode;

        /// <summary>Current panel mode</summary>
        private Common.PanelMode currentPanelMode;

        /// <summary>Current Panel Mode</summary>
        public Common.PanelMode PanelMode
        {
            get => currentPanelMode;
            set
            {
                currentPanelMode = value;
                NotifyPropertyChanged(
                    nameof(PanelMode),
                    nameof(IsMacMan),
                    nameof(IsToolDataSetup),
                    nameof(IsZeroSetup),
                    nameof(IsParameterSetup),
                    nameof(IsProgramOperation),
                    nameof(IsManual),
                    nameof(IsMDI),
                    nameof(IsAuto),
                    nameof(IsOperation)
                    ); ;
            }
        }

        public string CollapseButtonCaption { get; set; } = MARLETT_UP_ARROW;

        /// <summary>Is MacMan</summary>
        public bool IsMacMan => PanelMode == Common.PanelMode.MacMan;

        /// <summary>Is Tool Data Setup</summary>
        public bool IsToolDataSetup => PanelMode == Common.PanelMode.ToolDataSetup;

        /// <summary>Is Zero Setup</summary>
        public bool IsZeroSetup => PanelMode == Common.PanelMode.ZeroSetup;

        /// <summary>Is Parameter Setup</summary>
        public bool IsParameterSetup => PanelMode == Common.PanelMode.ParameterSetup;

        /// <summary>Is Program Operation</summary>
        public bool IsProgramOperation => PanelMode == Common.PanelMode.ProgramOperation;

        /// <summary>Is Manual Operation</summary>
        public bool IsManual => PanelMode == Common.PanelMode.Manual;

        /// <summary>Is MDI Operation</summary>
        public bool IsMDI => PanelMode == Common.PanelMode.MDI;

        /// <summary>Is Auto Operation</summary>
        public bool IsAuto => PanelMode == Common.PanelMode.Auto;

        /// <summary>Is Operation Mode</summary>
        public bool IsOperation => IsManual || IsMDI || IsAuto;

        /// <summary>Constructor</summary>
        public MainWindow()
        {
            InitializeComponent();
            _panelMode = PanelModeFactory.Instance.CreatePanelMode();
            _panelMode.PanelModeChanged += _panelMode_PanelModeChanged;
        }

        #region INotifyPropertyChanged Implementation
        
        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] string propertyName = null)
        {
            Application.Current.Dispatcher.Invoke(() =>
            PropertyChanged?.Invoke(
                this,
                new PropertyChangedEventArgs(propertyName)
                ));
        }

        private void NotifyPropertyChanged(params string[] propertyNames)
        {
            foreach (string propertyName in propertyNames)
                NotifyPropertyChanged(propertyName);
        }

        #endregion

        /// <summary>Panel mode changed event handler</summary>
        private void _panelMode_PanelModeChanged(object sender, EventArgs e)
            => Dispatcher.Invoke(() => PanelMode = _panelMode.GetPanelMode());

        /// <summary>Operation button click event handler</summary>
        private void btnOperation_Click(object sender, RoutedEventArgs e)
            => _panelMode.ChangeScreen(PanelGroup.OperationMode, "");

        /// <summary>Program button click event handler</summary>
        private void btnProgram_Click(object sender, RoutedEventArgs e)
            => _panelMode.ChangeScreen(PanelGroup.ProgramMode, "");

        /// <summary>Parameter button click event handler</summary>
        private void btnParameter_Click(object sender, RoutedEventArgs e)
            => _panelMode.ChangeScreen(PanelGroup.ParameterMode, "");

        /// <summary>Zero set button click event handler</summary>
        private void btnZeroSet_Click(object sender, RoutedEventArgs e)
            => _panelMode.ChangeScreen(PanelGroup.ZeroSetMode, "");

        /// <summary>Tool data button click event handler</summary>
        private void btnToolData_Click(object sender, RoutedEventArgs e)
            => _panelMode.ChangeScreen(PanelGroup.ToolDataSettingMode, "");

        /// <summary>MacMan button click event handler</summary>
        private void btnMacMan_Click(object sender, RoutedEventArgs e)
            => _panelMode.ChangeScreen(PanelGroup.MacManMode, "");

        /// <summary>Close button click event handler</summary>
        private void btnClose_Click(object sender, RoutedEventArgs e)
            => this.Close();

        /// <summary>Window location changed event handler</summary>
        private void winMain_LocationChanged(object sender, EventArgs e)
        {
            var workArea = SystemParameters.WorkArea;

            if (Math.Abs(workArea.Left - this.Left) <= SNAP_DISTANCE)
                this.Left = workArea.Left;

            if (Math.Abs(workArea.Right - (this.Left + this.Width)) <= SNAP_DISTANCE)
                this.Left = workArea.Right - this.Width;

            if (Math.Abs(workArea.Top - this.Top) <= SNAP_DISTANCE)
                this.Top = workArea.Top;

            if (Math.Abs(workArea.Bottom - (this.Top + this.Height)) <= SNAP_DISTANCE)
                this.Top = workArea.Bottom - this.Height;
        }

        /// <summary>Toggle button click event handler</summary>
        private void btnToggle_Click(object sender, RoutedEventArgs e)
        {
            if(this.Height == 64)
            {
                this.SizeToContent = SizeToContent.WidthAndHeight;
                CollapseButtonCaption = MARLETT_UP_ARROW;
            }
            else
            {
                this.SizeToContent = SizeToContent.Width;
                this.Height = 64;
                CollapseButtonCaption = MARLETT_DOWN_ARROW;
            }
            NotifyPropertyChanged(nameof(CollapseButtonCaption));
        }
    }
}
