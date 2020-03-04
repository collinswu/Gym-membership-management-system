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
    /// Interaction logic for Additional_Features.xaml
    /// </summary>
    public partial class Additional_Features : Window
    {
        List<AdditionalFeatures> AFlist = new List<AdditionalFeatures>();
        public Additional_Features()
        {
            InitializeComponent();

            //Define Data Source
            dtgAFInfo.ItemsSource = AFlist;


            //Data Storage
            string strFilePath = @"..\..\..\Data\Additional Features.txt";
            string strLine = "";
            string[] rawData;

            try
            {
                StreamReader reader = new StreamReader(strFilePath);

                while (!reader.EndOfStream)
                {
                    strLine = reader.ReadLine();

                    rawData = strLine.Split('|');
                    AdditionalFeatures CurrentAF = new AdditionalFeatures(rawData[0]);

                    AFlist.Add(CurrentAF);
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error in read file: " + ex.Message);
                return;
            }

            foreach (AdditionalFeatures a in AFlist)
            {
                cbbFeatureType.Items.Add(a.FeatureType);
            }
        }

        private void btnAddNewFeature_Click(object sender, RoutedEventArgs e)
        {
            //Validation
            if (txtNewAFName.Text == "")
            {
                MessageBox.Show("Please enter a valid name for the new feature.");
                return;
            }
            

            string NewFeatureName = txtNewAFName.Text.Trim();

            //Add object to the list
            AdditionalFeatures AF = new AdditionalFeatures(NewFeatureName);
            foreach (AdditionalFeatures a in AFlist)
            {
                if (AF.FeatureType == a.FeatureType)
                {
                    MessageBox.Show("This feature name has been taken, please try another one.");
                    return;
                }
            }

            AFlist.Add(AF);
            dtgAFInfo.Items.Refresh();
            cbbFeatureType.Items.Clear();
            foreach (AdditionalFeatures a in AFlist)
            {
                cbbFeatureType.Items.Add(a.FeatureType);
            }


            //Add new information to the database file
            DateTime datNow = DateTime.Now;
            string strFilePath = @"..\..\..\Data\Additional Features.txt";
            string strLine;

            try
            {
                StreamWriter writer = new StreamWriter(strFilePath, false);
                foreach (AdditionalFeatures a in AFlist)
                {
                    strLine = String.Format("{0}", a.FeatureType);
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

        private void btnDeleteAF_Click(object sender, RoutedEventArgs e)
        {
            //Validation
            if (cbbFeatureType.Text == "")
            {
                MessageBox.Show("Please select an additional feature type.");
                return;
            }

            string DeleteFeatureType = cbbFeatureType.Text.Trim();

            foreach (AdditionalFeatures a in AFlist.ToList())
            {
                if (a.FeatureType == DeleteFeatureType)
                {
                    AFlist.Remove(a);
                }
            }

            dtgAFInfo.Items.Refresh();
            cbbFeatureType.Items.Clear();
            foreach (AdditionalFeatures a in AFlist)
            {
                cbbFeatureType.Items.Add(a.FeatureType);
            }

            //Update information to the database file
            DateTime datNow = DateTime.Now;
            string strFilePath = @"..\..\..\Data\Additional Features.txt";
            string strLine;

            try
            {
                StreamWriter writer = new StreamWriter(strFilePath, false);
                foreach (AdditionalFeatures a in AFlist)
                {
                    strLine = String.Format("{0}", a.FeatureType);
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
