using DevExpress.Xpf.Editors;
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

namespace RDProject.Views
{
    /// <summary>
    /// ChangePwdDialog.xaml 的交互逻辑
    /// </summary>
    public partial class ChangePwdDialog : UserControl
    {
        public ChangePwdDialog()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            Window win = ((UserControl)sender).Parent as Window;
            if (win != null)
            {
                win.ResizeMode = ResizeMode.NoResize;
            }
        }
    }
}
