using Okuma.PanelMode.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Okuma.PanelMode
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {

        public static readonly DependencyProperty PanelModeProperty = DependencyProperty.Register(nameof(PanelMode), typeof(Common.PanelMode), typeof(MainWindow));

        private readonly IPanelMode _panelMode;

        public event PropertyChangedEventHandler PropertyChanged;

        public Common.PanelMode PanelMode
        {
            get => (Common.PanelMode)GetValue(PanelModeProperty);
            set
            {
                SetValue(PanelModeProperty, value);
                NotifyPropertyChanged(
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

        public bool IsMacMan => PanelMode == Common.PanelMode.MacMan;
        public bool IsToolDataSetup => PanelMode == Common.PanelMode.ToolDataSetup;
        public bool IsZeroSetup => PanelMode == Common.PanelMode.ZeroSetup;
        public bool IsParameterSetup => PanelMode == Common.PanelMode.ParameterSetup;
        public bool IsProgramOperation => PanelMode == Common.PanelMode.ProgramOperation;
        public bool IsManual => PanelMode == Common.PanelMode.Manual;
        public bool IsMDI => PanelMode == Common.PanelMode.MDI;
        public bool IsAuto => PanelMode == Common.PanelMode.Auto;
        public bool IsOperation => IsManual || IsMDI || IsAuto;

        public MainWindow()
        {
            InitializeComponent();
            _panelMode = PanelModeFactory.CreatePanelMode();
            _panelMode.PanelModeChanged += _panelMode_PanelModeChanged;
        }

        private void NotifyPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged.Invoke(
                this,
                new PropertyChangedEventArgs(propertyName)
                );
        }

        private void NotifyPropertyChanged(params string[] propertyNames)
        {
            foreach (string propertyName in propertyNames)
                NotifyPropertyChanged(propertyName);
        }

        private void _panelMode_PanelModeChanged(object sender, EventArgs e)
        {
            PanelMode = _panelMode.GetPanelMode();
        }

        private void btnOperation_Click(object sender, RoutedEventArgs e)
            => _panelMode.ChangeScreen(PanelGroup.OperationMode, "");

        private void btnProgram_Click(object sender, RoutedEventArgs e)
            => _panelMode.ChangeScreen(PanelGroup.ProgramMode, "");

        private void btnParameter_Click(object sender, RoutedEventArgs e)
            => _panelMode.ChangeScreen(PanelGroup.ParameterMode, "");

        private void btnZeroSet_Click(object sender, RoutedEventArgs e)
            => _panelMode.ChangeScreen(PanelGroup.ZeroSetMode, "");

        private void btnToolData_Click(object sender, RoutedEventArgs e)
            => _panelMode.ChangeScreen(PanelGroup.ToolDataSettingMode, "");

        private void btnMacMan_Click(object sender, RoutedEventArgs e)
            => _panelMode.ChangeScreen(PanelGroup.MacManMode, "");
    }
}
