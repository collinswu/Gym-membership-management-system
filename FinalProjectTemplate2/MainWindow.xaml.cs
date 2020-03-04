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

namespace FinalProjectTemplate2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            // Team member: Wenjie Wu, JieWei Hu, Zi Chai
        }

        private void btn_Membership_Sales_Click(object sender, RoutedEventArgs e)
        {
            Membership_Sales pg = new Membership_Sales();
            pg.ShowDialog();
        }

        private void btn_Pricing_Management_Click(object sender, RoutedEventArgs e)
        {
            Pricing_Management pg = new Pricing_Management();
            pg.ShowDialog();
        }

        private void btn_Member_Information_Click(object sender, RoutedEventArgs e)
        {
            Member_Information pg = new Member_Information();
            pg.ShowDialog();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
