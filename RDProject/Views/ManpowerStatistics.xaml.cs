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
    /// ManpowerStatistics.xaml 的交互逻辑
    /// </summary>
    public partial class ManpowerStatistics : UserControl
    {
        public ManpowerStatistics()
        {
            InitializeComponent();
        }

        private void SimpleButton_Click(object sender, RoutedEventArgs e)
        {
            Report.View.ExportToXlsx(@"D:\0文档\123.xlsx");
        }
    }
}
