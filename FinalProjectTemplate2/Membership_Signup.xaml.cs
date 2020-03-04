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
using System.IO;

namespace FinalProjectTemplate2
{
    /// <summary>
    /// Interaction logic for Membership_Signup.xaml
    /// </summary>
    public partial class Membership_Signup : Window
    {
        List<MemberInfo> memlist;
        List<AdditionalFeatures> AFList = new List<AdditionalFeatures>();
        public Membership_Signup()
        {
            InitializeComponent();
            memlist = new List<MemberInfo>();  
            ImportMemberData();

        }

        private void ImportMemberData()
        {
            string strFilePath = @"..\..\..\Data\MemberInfo.txt";  
            string strLine = "";
            string[] rawData;

            try
            {
                StreamReader reader = new StreamReader(strFilePath);  

                while (!reader.EndOfStream)
                {
                    strLine = reader.ReadLine();
                    rawData = strLine.Split('|');  
                    MemberInfo memnew = new MemberInfo(rawData[0], rawData[1], rawData[2], rawData[3], rawData[4], rawData[5], rawData[6], rawData[7], rawData[8], rawData[9], rawData[10], Convert.ToDateTime(rawData[11]), rawData[12], rawData[13], rawData[14]);
                    memlist.Add(memnew);  
                }

                reader.Close();  
            }

            catch (Exception ex)
            {
                MessageBox.Show("Error in read file: " + ex.Message);
                return;
            }


            //AF combo box read data
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
                cobMSUAdditionalFeature.Items.Add(a.FeatureType);
            }


        }




        private void btnMSUSubmit_Click(object sender, RoutedEventArgs e)
        {

            string strFilePath = @"..\..\..\Data\MemberInfo.txt";
            string strLine = "";

            //vaildation
            //empty
            string strFirstName = txbMSUFirstName.Text;
            string strLastName = txbMSULastName.Text;
            string strCreditCardType = cobMSUCreditCardType.Text;
            string strCreditCardNumber = txbMSUCCNumber.Text;
            string strPhone = txbMSUPhone.Text;
            string strEmail = txbMSUEmail.Text;
            string strGender = cobMSUGender.Text;
            string strAdditionalFeatures = cobMSUAdditionalFeature.Text;
            string strStartDate = txbMSUStartDate.Text;
            string strMembershipType = cobMSUMembershipType.Text;
            string strPhoneType = cobMSUPhoneType.Text;
            string strAge = txbMSUAge.Text;
            string strWeight = txbMSUWeight.Text;
            string strGoal = cobMSUPersonalGoal.Text;

            if (strFirstName == "")
            {
                MessageBox.Show("First Name is required");
                return;
            }
            if (strLastName == "")
            {
                MessageBox.Show("Last Name is required");
                return;
            }
            if (strCreditCardType == "")
            {
                MessageBox.Show("Credit Card Typee is required");
                return;
            }
            if (strCreditCardNumber == "")
            {
                MessageBox.Show("Credit Card Number is required");
                return;
            }
            if (strPhone == "")
            {
                MessageBox.Show("Phone is required");
                return;
            }
            if (strEmail == "")
            {
                MessageBox.Show("Email is required");
                return;
            }
            if (strGender == "")
            {
                MessageBox.Show("Gender is required");
                return;
            }
            if (strAdditionalFeatures == "")
            {
                MessageBox.Show("Additional Features is required");
                return;
            }
            if (strStartDate == "")
            {
                MessageBox.Show("Start date is required");
                return;
            }
            if (strMembershipType == "")
            {
                MessageBox.Show("Membership type is required");
                return;
            }
            if (strPhoneType == "")
            {
                MessageBox.Show("Phone type is required");
                return;
            }


            //date
            //start date
            DateTime datEndDate = DateTime.Today;
            DateTime datStartDate;
            bool bolDate = DateTime.TryParse(strStartDate, out datStartDate);
            DateTime datToday = DateTime.Today;
            string strEndDate = Convert.ToString(datEndDate);

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
            txbMSUEndDate.Text = strEndDate;


            //card vaildation
            if (strCreditCardType == "Amercian Express")
            {
                if (strCreditCardNumber.Length != 15)
                {
                    MessageBox.Show("Please enter a vaild credit card number");
                    return;
                }
            }
            else
            {
                if (strCreditCardNumber.Length != 16)
                {
                    MessageBox.Show("Please enter a vaild credit card number");
                    return;
                }
            }

            //phone
            if (strPhone.Length != 10)
            {
                MessageBox.Show("Please enter a vaild phone number");
                return;
            }


            //Email Validation
            if (strEmail.IndexOf("@") < 0)
            {
                MessageBox.Show("Please enter a valid email address");
                return;
            }


            int age = 0;
            double weight = 0;

            //Age validation
            if (strAge != "")
            {
                if (!Int32.TryParse(strAge, out age))
                {
                    MessageBox.Show("Please enter a integer for age.");
                    return;
                }
                if (age < 0)
                {
                    MessageBox.Show("Please enter a positive integer for age.");
                    return;
                }
            }

            //Weight validation
            if (strWeight != "")
            {
                if (!Double.TryParse(strWeight, out weight))
                {
                    MessageBox.Show("Please enter a valid number for weight.");
                    return;
                }
                if (weight < 0)
                {
                    MessageBox.Show("Please enter a positive number for weight.");
                    return;
                }
            }


            //phone type

            if (strPhoneType == "IOS")
            {
                MessageBox.Show("Please download our app at https://www.apple.com/ios/app-store/");
            }
            else
            {
                MessageBox.Show("Please download our app at https://store.google.com/");
            }



            MemberInfo custnew = new MemberInfo(txbMSUFirstName.Text, txbMSULastName.Text, cobMSUCreditCardType.Text, txbMSUCCNumber.Text, txbMSUPhone.Text, txbMSUEmail.Text, cobMSUGender.Text, cobMSUPhoneType.Text, cobMSUMembershipType.Text, cobMSUAdditionalFeature.Text, txbMSUStartDate.Text, datEndDate, Convert.ToString(age), Convert.ToString(weight), cobMSUPersonalGoal.Text); 
            memlist.Add(custnew);
            try
            {
                StreamWriter writer = new StreamWriter(strFilePath, false);  

                
                foreach (MemberInfo c in memlist)
                {
                    strLine = String.Format("{0}|{1}|{2}|{3}|{4}|{5}|{6}|{7}|{8}|{9}|{10}|{11}|{12}|{13}|{14}", c.FirstName, c.LastName, c.CreditCardType, c.CreditCardNumber, c.Phone,c.Email,c.Gender,c.PhoneType,c.MembershipType, c.AdditonalFeature, c.StartDate, c.EndDate, c.Age, c.Weight, c.FitnessGoal);
                    writer.WriteLine(strLine);  
                }

                writer.Close();  
            }

            catch (Exception ex)
            {
                MessageBox.Show("Error in export process: " + ex.Message);
            }

            MessageBox.Show("Member Information saved!" + Environment.NewLine);
            
            

        }


        private void btnMSUClear_Click(object sender, RoutedEventArgs e)
        {
            txbMSUFirstName.Text = "";
            txbMSULastName.Text = "";
            cobMSUCreditCardType.Text = "";
            txbMSUCCNumber.Text = "";
            txbMSUPhone.Text = "";
            txbMSUEmail.Text = "";
            cobMSUGender.Text = "";
            txbMSUAge.Text = "";
            txbMSUWeight.Text = "";
            cobMSUPersonalGoal.Text = "";
            cobMSUPhoneType.Text = "";
            cobMSUMembershipType.Text = "";
            cobMSUAdditionalFeature.Text = "";
            txbMSUStartDate.Text = "";
            txbMSUEndDate.Text = "";
            
        }


      
    }
}
