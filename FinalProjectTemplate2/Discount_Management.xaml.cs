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
    /// Interaction logic for Discount_Management.xaml
    /// </summary>
    public partial class Discount_Management : Window
    {
        List<Discount> discounttypelist = new List<Discount>();
        public Discount_Management()
        {
            InitializeComponent();

            //Define Data Source
            dtgDiscountInfo.ItemsSource = discounttypelist;

            //Data Storage
            string strFilePath = @"..\..\..\Data\Discount.txt";
            string strLine = "";
            string[] rawData;

            try
            {
                StreamReader reader = new StreamReader(strFilePath);

                while (!reader.EndOfStream)
                {
                    strLine = reader.ReadLine();

                    rawData = strLine.Split('|');
                    Discount CurrentDiscount = new Discount(rawData[0], Convert.ToDouble(rawData[1]));

                    discounttypelist.Add(CurrentDiscount);
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error in read file: " + ex.Message);
                return;
            }

            foreach (Discount d in discounttypelist)
            {
                cbbDiscountType.Items.Add(d.DiscountName);
            }
        }

        private void btnAddNewD_Click(object sender, RoutedEventArgs e)
        {
            //Validation
            if (txtNewDName.Text == "")
            {
                MessageBox.Show("Please enter a valid name for the new discount.");
                return;
            }
            if (txtNewDiscount.Text == "" || !Double.TryParse(txtNewDiscount.Text, out double douDiscount))
            {
                MessageBox.Show("Please enter a valid discount for the new discount.");
                return;
            }
            if (douDiscount < 0)
            {
                MessageBox.Show("Please enter a positive number for the new discount.");
                return;
            }

            string NewDiscountName = txtNewDName.Text.Trim();

            //Add object to the list
            Discount discount = new Discount(NewDiscountName, douDiscount);

            foreach (Discount d in discounttypelist)
            {
                if (discount.DiscountName == d.DiscountName)
                {
                    MessageBox.Show("This discount name has been taken, please try another one.");
                    return;
                }
            }

            discounttypelist.Add(discount);
            dtgDiscountInfo.Items.Refresh();
            cbbDiscountType.Items.Clear();
            foreach (Discount d in discounttypelist)
            {
                cbbDiscountType.Items.Add(d.DiscountName);
            }


            //Add new information to the database file
            DateTime datNow = DateTime.Now;
            string strFilePath = @"..\..\..\Data\Discount.txt";
            string strLine;

            try
            {
                StreamWriter writer = new StreamWriter(strFilePath, false);
                foreach (Discount d in discounttypelist)
                {
                    strLine = String.Format("{0}|{1}", d.DiscountName, d.SpecifiedDiscount);
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

        private void btnDeleteDiscount_Click(object sender, RoutedEventArgs e)
        {
            //Validation
            if (cbbDiscountType.Text == "")
            {
                MessageBox.Show("Please select a discount type.");
                return;
            }

            string DeleteDiscountType = cbbDiscountType.Text.Trim();

            foreach (Discount d in discounttypelist.ToList())
            {
                if (d.DiscountName == DeleteDiscountType)
                {
                    discounttypelist.Remove(d);
                }
            }

            dtgDiscountInfo.Items.Refresh();
            cbbDiscountType.Items.Clear();
            foreach (Discount d in discounttypelist)
            {
                cbbDiscountType.Items.Add(d.DiscountName);
            }

            //Update new information to the database file
            DateTime datNow = DateTime.Now;
            string strFilePath = @"..\..\..\Data\Discount.txt";
            string strLine;
            try
            {
                StreamWriter writer = new StreamWriter(strFilePath, false);
                foreach (Discount d in discounttypelist)
                {
                    strLine = String.Format("{0}|{1}", d.DiscountName, d.SpecifiedDiscount);
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

        private void btnChangeDiscount_Click(object sender, RoutedEventArgs e)
        {
            //Validation
            if (cbbDiscountType.Text == "")
            {
                MessageBox.Show("Please select a discount type.");
                return;
            }
            if (txtDiscount.Text == "" || !Double.TryParse(txtDiscount.Text, out double douDiscount))
            {
                MessageBox.Show("Please enter a valid discount for the new discount.");
                return;
            }

            string ChangeDiscountType = cbbDiscountType.Text.Trim();

            foreach (Discount d in discounttypelist)
            {
                if (d.DiscountName == ChangeDiscountType)
                {
                    d.SpecifiedDiscount = douDiscount;
                }
            }

            dtgDiscountInfo.Items.Refresh();

            //Update new information to the database file
            DateTime datNow = DateTime.Now;
            string strFilePath = @"..\..\..\Data\Discount.txt";
            string strLine;
            try
            {
                StreamWriter writer = new StreamWriter(strFilePath, false);
                foreach (Discount d in discounttypelist)
                {
                    strLine = String.Format("{0}|{1}", d.DiscountName, d.SpecifiedDiscount);
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
