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
using System.IO;

namespace FinalProjectTemplate2
{
    /// <summary>
    /// Interaction logic for Page2.xaml
    /// </summary>

    public partial class Pricing_Management : Window
    {
        List<MembershipType> membershiptypelist = new List<MembershipType>();
        public Pricing_Management()
        {
            InitializeComponent();

            dtgPricingInfo.ItemsSource = membershiptypelist;

            //Data Storage
            string strFilePath = @"..\..\..\Data\MembershipType.txt";
            string strLine = "";
            string[] rawData;

            try
            {
                StreamReader reader = new StreamReader(strFilePath);

                while (!reader.EndOfStream)
                {
                    strLine = reader.ReadLine();

                    rawData = strLine.Split('|');
                    MembershipType CurrentType = new MembershipType(rawData[0], Convert.ToDouble(rawData[1]), rawData[2]);

                    membershiptypelist.Add(CurrentType);
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error in read file: " + ex.Message);
                return;
            }

        }

        private void btnManageDiscount_Click(object sender, RoutedEventArgs e)
        {
            Discount_Management pg = new Discount_Management();
            pg.ShowDialog();
        }

        private void btnAdditionalFeatures_Click(object sender, RoutedEventArgs e)
        {
            Additional_Features pg = new Additional_Features();
            pg.ShowDialog();
        }

        private void btnPMChange_Click(object sender, RoutedEventArgs e)
        {
            //Validation
            if (cbbPMMemberType.Text == "")
            {
                MessageBox.Show("Please choose a valid membership type.");
            }

            if (!Double.TryParse(txtPMCurrentPrice.Text.Trim(), out double douprice) || Convert.ToDouble(txtPMCurrentPrice.Text.Trim()) < 0)
            {
                MessageBox.Show("Please enter a valid price for this membership type.");
                return;
            }

            if (cbbPMAvailability.Text == "")
            {
                MessageBox.Show("Please choose a availability for this membership type.");
            }

            string TypeSelected = cbbPMMemberType.Text;
            string stravailable = cbbPMAvailability.Text;

            //Change information when button is pressed
            foreach (MembershipType m in membershiptypelist)
            {
                if (TypeSelected == m.Type)
                {
                    m.Price = douprice;
                    m.Availability = stravailable;
                }
            }

            dtgPricingInfo.Items.Refresh();

            //Add new information to the database file
            DateTime datNow = DateTime.Now;
            string strFilePath = @"..\..\..\Data\MembershipType.txt";
            string strLine;

            try
            {
                StreamWriter writer = new StreamWriter(strFilePath, false);
                foreach (MembershipType m in membershiptypelist)
                {
                    strLine = String.Format("{0}|{1}|{2}", m.Type, m.Price, m.Availability);
                    writer.WriteLine(strLine);
                }

                writer.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error in read file: " + ex.Message);
                return;
            }

        }
    }
}
