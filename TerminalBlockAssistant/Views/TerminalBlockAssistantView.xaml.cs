using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace TerminalBlockAssistant.Views
{
    /// <summary>
    /// Interaction logic for TerminalBlockAssistantView.xaml
    /// </summary>
    public partial class TerminalBlockAssistantView : UserControl
    {
        public TerminalBlockAssistantView()
        {
            InitializeComponent();
        }
        private void NumberOnly(object sender, TextCompositionEventArgs e)
        {
            if (!int.TryParse(e.Text, out _))
            {
                e.Handled = true;

                MessageBox.Show("Bitte geben Sie nur Zahlen ein", "Fehler",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
        }
    }
}
