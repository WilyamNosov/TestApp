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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TestApplication
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private int groupIndex = 0;
        public void AddGroup(object sender, RoutedEventArgs e)
        {
            TreeViewItem newChild = new TreeViewItem();
            if (groupAndControlView.Items.Count > 0)
            {
                groupIndex = Int32.Parse(groupAndControlView.Items.GetItemAt(groupAndControlView.Items.Count - 1).ToString().Split(' ')[2]) + 1;
            }
            newChild.Header = "Group " + groupIndex;
            groupAndControlView.Items.Add(newChild);

        }
        public void AddControl(object sender, RoutedEventArgs e)
        {
            Mouse.OverrideCursor = Cursors.Cross;
            if (Mouse.LeftButton == MouseButtonState.Pressed)
            {
                Console.WriteLine(Mouse.Captured);
            }

        }
        public MainWindow()
        {
            InitializeComponent();
        }
    }
}
