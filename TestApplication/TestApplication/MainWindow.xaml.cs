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
using TestApplication.Models;

namespace TestApplication
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
     

    public partial class MainWindow : Window
    {
        List<MyControl> myControls = new List<MyControl>();
        public MainWindow()
        {
            InitializeComponent();

            this.PreviewKeyDown += new KeyEventHandler(HandleEsc);
            this.MouseLeftButtonDown += new MouseButtonEventHandler(DefaultArrovValueSet);
            this.MouseLeftButtonDown += new MouseButtonEventHandler(UnsetItemsOnTree);
        }
        private void AddGroupButton_Click(object sender, RoutedEventArgs e)
        {
            TreeViewItem treeViewItemChild = new TreeViewItem();

            if (TreeViewElement.Items.Count == 0)
            {
                treeViewItemChild.Header = "Group 1";
                treeViewItemChild.MouseLeftButtonUp += TreeViewItem_MouseLeftButtonUp;
            } else
            {
                TreeViewItem child = (TreeViewItem)TreeViewElement.Items.GetItemAt(TreeViewElement.Items.Count - 1);
                treeViewItemChild.Header = "Group " + (Int32.Parse(child.Header.ToString().Split(' ')[1]) + 1).ToString();
                treeViewItemChild.MouseLeftButtonUp += TreeViewItem_MouseLeftButtonUp;
            }

            TreeViewElement.Items.Add(treeViewItemChild);
        }
        private void AddControlButton_Click(object sender, RoutedEventArgs e)
        {
            Mouse.OverrideCursor = Cursors.Cross;
        }
        private void DefaultArrovValueSet(object sender, MouseButtonEventArgs e)
        {
            Mouse.OverrideCursor = Cursors.Arrow;
        }
        private void UnsetItemsOnTree(object sender, MouseButtonEventArgs e)
        {
            TreeViewItem item = (TreeViewItem)TreeViewElement.SelectedItem;
            item.IsSelected = false;
        }
        private void HandleEsc(object sender, KeyEventArgs e)
        {
            Mouse.OverrideCursor = Cursors.Arrow;
        }
        private void TreeViewItem_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (Mouse.OverrideCursor == Cursors.Cross)
            {
                MyControl myControl = CreateControlForAdd();
                TreeViewItem addItem = new TreeViewItem();

                addItem.Header = myControl.ControlName + $" ({myControl.name}, {myControl.role}, {myControl.value})";
                TreeViewItem item = (TreeViewItem)TreeViewElement.SelectedItem;
                item.Items.Add(addItem);
            }

            Mouse.OverrideCursor = Cursors.Arrow;
        }
        private void Remove_Click(object sender, RoutedEventArgs e)
        {
            TreeViewItem item = (TreeViewItem)TreeViewElement.SelectedItem;

            if (item != null)
            {
                if (item.Header.ToString().Split(' ')[0] == "Group")
                {
                    TreeViewElement.Items.Remove(item);
                }
                else
                {
                    foreach (var child in TreeViewElement.Items)
                    {
                        TreeViewItem childItem = (TreeViewItem)child;
                        RemoveControl(item);
                        childItem.Items.Remove(item);
                    }
                }
            }
        }
        private void Highlight_Click(object sender, RoutedEventArgs e)
        {
            TreeViewItem item = (TreeViewItem)TreeViewElement.SelectedItem;

            if (item != null)
            {
                if (item.BorderThickness.ToString() == "0,0,0,0")
                {
                    item.BorderThickness = new Thickness(2, 2, 2, 2);
                    item.BorderBrush = new SolidColorBrush(Colors.Green);
                }
                else
                {
                    item.BorderThickness = new Thickness(0, 0, 0, 0);
                    item.BorderBrush = new SolidColorBrush(Colors.White);
                }
            }
        }
        private MyControl CreateControlForAdd()
        {
            MyControl myControl = new MyControl() { ControlName = "Control 1", name = "name", role = "role", value = 100 };

            if (myControls.Count != 0)
            {
                myControl.ControlName = "Control " + (Int32.Parse(myControls[myControls.Count - 1].ControlName.Split(' ')[1]) + 1).ToString();
            }
            myControls.Add(myControl);

            return myControl;
        }
        private void RemoveControl(TreeViewItem item)
        {
            string controlName = item.Header.ToString().Split('(')[0];
            foreach (MyControl control in myControls)
            {
                if (control.ControlName == controlName.Remove(controlName.Length - 1))
                {
                    myControls.Remove(control);
                    break;
                }
            }
        }
    }
}
