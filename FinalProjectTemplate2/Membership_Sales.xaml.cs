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
    /// Interaction logic for Page1.xaml
    /// </summary>
    public partial class Membership_Sales : Window
    {
        List<MembershipType> membershiptypelist = new List<MembershipType>();
        List<Discount> discountList = new List<Discount>();
        List<AdditionalFeatures> AFList = new List<AdditionalFeatures>();
        public Membership_Sales()
        {
            InitializeComponent();
            
            //Data Storage for membership 
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


            //Read data, define data source for discount
            string strFilePathDiscount = @"..\..\..\Data\Discount.txt";
            string strLineDiscount = "";
            string[] rawDataDiscount;

            try
            {
                StreamReader reader = new StreamReader(strFilePathDiscount);

                while (!reader.EndOfStream)
                {
                    strLineDiscount = reader.ReadLine();

                    rawDataDiscount = strLineDiscount.Split('|');
                    Discount CurrentDiscount = new Discount(rawDataDiscount[0], Convert.ToDouble(rawDataDiscount[1]));

                    discountList.Add(CurrentDiscount);
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error in read file: " + ex.Message);
                return;
            }


            string strFilePathAF = @"..\..\..\Data\Additional Features.txt";
            string strLineAF = "";
            string[] rawDataAF;

            try
            {
                StreamReader reader = new StreamReader(strFilePathAF);

                while (!reader.EndOfStream)
                {
                    strLineAF = reader.ReadLine();

                    rawDataAF = strLineAF.Split('|');
                    AdditionalFeatures CurrentAF = new AdditionalFeatures(rawDataAF[0]);

                    AFList.Add(CurrentAF);
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error in read file: " + ex.Message);
                return;
            }

            foreach (AdditionalFeatures a in AFList)
            {
                cobMSAdditionalFeature.Items.Add(a.FeatureType);
            }

            foreach (Discount d in discountList)
            {
                cobMSDiscountType.Items.Add(d.DiscountName);
            }

            foreach (MembershipType m in membershiptypelist)
            {
                cobMSMembershipType.Items.Add(m.Type);
            }

        }

        private void btnMSPurchase_Click(object sender, RoutedEventArgs e)
        {
            // vaild availability
            string strAvailability = txtMSAvailability.Text;
            if (strAvailability == "No")
            {
                MessageBox.Show("This membership is not available");
                return;
            }
            else if (strAvailability == "")
            {
                MessageBox.Show("Please check the availability first");
                return;
            }
            else
            {
                Membership_Signup pg = new Membership_Signup();
                pg.ShowDialog();
            }


           
        }

        private void btnMSSubmit_Click(object sender, RoutedEventArgs e)
        {
            //inputs
            //variables
            string strMembershipType = cobMSMembershipType.Text;
            string strStartDate = txbMSStartDate.Text;
            DateTime datStartDate;
            string strDiscountType = cobMSDiscountType.Text;
            string strAdditionalFeatures = cobMSAdditionalFeature.Text;

            // vaildation

            //start date
            bool bolDate = DateTime.TryParse(strStartDate, out datStartDate);
            DateTime datToday = DateTime.Today;
            if (bolDate == false)
            {
                MessageBox.Show("Please enter a vaild date");
                return;
            }
            if (datStartDate < datToday)
            {
                MessageBox.Show("Start date must be greater than today");
                return;
            }

            // empty
            if (strMembershipType == "")
            {
                MessageBox.Show("Membership type is required");
                return;
            }
            if (strStartDate == "")
            {
                MessageBox.Show("Start date is required");
                return;
            }
            if (strAdditionalFeatures == "")
            {
                MessageBox.Show("Additional Features is required");
                return;
            }
           
            //membership cost & date
            string strMembershipCost = "";
            DateTime datEndDate = DateTime.Today;
            double dMembershipCost = 0;
            string strEndDate;
            
            foreach(MembershipType m in membershiptypelist)
            {
                cobMSAdditionalFeature.Items.Add(m.Type);
                if (m.Type == strMembershipType)
                {
                    dMembershipCost = m.Price;
                }
            }

            if (strMembershipType == "Individual 3 Months")
            {

                datEndDate = datStartDate.AddMonths(3);
            }
            else if (strMembershipType == "Individual 12 Months")
            {

                datEndDate = datStartDate.AddMonths(12);
            }
            else if (strMembershipType == "Two Person 3 Months")
            {

                datEndDate = datStartDate.AddMonths(3);
            }
            else if (strMembershipType == "Two Person 12 Months")
            {

                datEndDate = datStartDate.AddMonths(12);
            }
            else if (strMembershipType == "Family 3 Months")
            {

                datEndDate = datStartDate.AddMonths(3);
            }
            else
            {

                datEndDate = datStartDate.AddMonths(12);
            }


            strEndDate = datEndDate.ToString("MM/dd/yyyy");
            strStartDate = datStartDate.ToString("MM/dd/yyyy");
            strMembershipCost = dMembershipCost.ToString();

            //discount 
            string strDiscountInfo = "";
            double dDiscountRate = 0;
            double dTotalDiscount = 0;

            foreach (Discount d in discountList)
            {
                //cobMSDiscountType.Items.Add(d.DiscountName);
                if (d.DiscountName == strDiscountType)
                {

                    dDiscountRate = d.SpecifiedDiscount;
                    dTotalDiscount = dMembershipCost * (1 - dDiscountRate);
                    strDiscountInfo = d.DiscountName + "- ($" + dTotalDiscount + ")";
                }
            }
                


            //total cost
            double dTotalCost = dMembershipCost  - dTotalDiscount;

            //availability
            string strAvailability = "No";
            foreach (MembershipType m in membershiptypelist)
            {

                if (m.Type == strMembershipType) 
                {
                    strAvailability = m.Availability;
                }
            }
            txtMSAvailability.Text = strAvailability;

            

            //results
            txbMSMembershipInfo.Text = strMembershipType;
            txbMSDateInfo.Text = strStartDate.ToString() + " - " + strEndDate;
            txbMSMembershipCostInfo.Text = "$" + strMembershipCost;
            txbMSFeatureInfo.Text = strAdditionalFeatures;
            txbMSDiscountInfo.Text = strDiscountInfo;
            txbMSTotalInfo.Text = "$" + dTotalCost.ToString();
        }

        private void btnMSClear_Click(object sender, RoutedEventArgs e)
        {
            //clear
            txbMSDateInfo.Text = "";
            txbMSDiscountInfo.Text = "";
            txbMSFeatureInfo.Text = "";
            txbMSMembershipInfo.Text = "";
            txbMSTotalInfo.Text = "";
            txbMSMembershipCostInfo.Text = "";
            cobMSMembershipType.Text = "";
            txbMSStartDate.Text = "";
            cobMSDiscountType.Text = "";
            cobMSAdditionalFeature.Text = "";
           

        }

        private void btnMSCancel_Click(object sender, RoutedEventArgs e)
        {
            //clear
            txbMSDateInfo.Text = "";
            txbMSDiscountInfo.Text = "";
            txbMSFeatureInfo.Text = "";
            txbMSMembershipInfo.Text = "";
            txbMSTotalInfo.Text = "";
            txbMSMembershipCostInfo.Text = "";
            cobMSMembershipType.Text = "";
            txbMSStartDate.Text = "";
            cobMSDiscountType.Text = "";
            txtMSAvailability.Text = "";
            cobMSAdditionalFeature.Text = "";
        }

    }
}
