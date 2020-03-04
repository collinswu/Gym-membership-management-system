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
    /// Interaction logic for Page3.xaml
    /// </summary>
    public partial class Member_Information : Window
    {
        List<MemberInfo> memlist;
        List<MemberInfo> searchlist;
        public Member_Information()
        {
            InitializeComponent();
            memlist = new List<MemberInfo>();
            searchlist = new List<MemberInfo>();
            dtgResult.ItemsSource = searchlist;
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



        }



        private void btnSearchMI_Click(object sender, RoutedEventArgs e)
        {
            //vaildate at least one input
            searchlist.Clear();
            dtgResult.ItemsSource = searchlist;

            bool IsAllEmpty = false;
            if (!String.IsNullOrEmpty(txtMIEmail.Text))
            {
                IsAllEmpty = true;
            }
            if (!String.IsNullOrEmpty(txtMILastName.Text))
            {
                IsAllEmpty = true;
            }
            if (!String.IsNullOrEmpty(txtMIPhone.Text))
            {
                IsAllEmpty = true;
            }


            if (IsAllEmpty == false)
            {
                MessageBox.Show("At least one box needed to be filled.");
                return;
            }

            //show result in datagrid
            //Search by Last Name Button

            string strName = Convert.ToString(txtMILastName.Text);
            string strPhone = Convert.ToString(txtMIPhone.Text);
            string strEmail = Convert.ToString(txtMIEmail.Text);
            bool IsMatch = false;


            //search using three input(whichever is filled)
            foreach (MemberInfo c in memlist)
            {
                c.LastName = c.LastName.ToLower();   //input case sensitive
                strName = strName.ToLower();

                if (txtMILastName.Text != "" && txtMIEmail.Text != "" && txtMIPhone.Text != "")  //three input = exact 
                {
                    if (c.LastName == strName && c.Email == strEmail && c.Phone == strPhone)
                    {
                        IsMatch = true;
                        searchlist.Add(c);
                    }
                }

                else if (txtMILastName.Text != "" && txtMIEmail.Text != "" && txtMIPhone.Text == "")  //two input  
                {
                    if (c.LastName == strName && c.Email == strEmail)
                    {
                        IsMatch = true;
                        searchlist.Add(c);
                    }
                }

                else if (txtMILastName.Text != "" && txtMIEmail.Text == "" && txtMIPhone.Text != "") //two input
                {
                    if (c.LastName == strName && c.Phone == strPhone)
                    {
                        IsMatch = true;
                        searchlist.Add(c);
                    }
                }

                else if (txtMILastName.Text != "" && txtMIEmail.Text == "" && txtMIPhone.Text == "") //one input
                {
                    if (c.LastName == strName)
                    {
                        IsMatch = true;
                        searchlist.Add(c);
                    }
                }

                else if (txtMILastName.Text == "" && txtMIEmail.Text != "" && txtMIPhone.Text != "") //two input
                {
                    if (c.Email == strEmail && c.Phone == strPhone)
                    {
                        IsMatch = true;
                        searchlist.Add(c);
                    }
                }

                else if (txtMILastName.Text == "" && txtMIEmail.Text != "" && txtMIPhone.Text == "") //one input
                {
                    if (c.Email == strEmail)
                    {
                        IsMatch = true;
                        searchlist.Add(c);
                    }
                }

                else if (txtMILastName.Text == "" && txtMIEmail.Text == "" && txtMIPhone.Text != "") //one input
                {
                    if (c.Phone == strPhone)
                    {
                        IsMatch = true;
                        searchlist.Add(c);
                    }
                }
            }

            if (IsMatch == false)
            {
                MessageBox.Show("No Record Founded.");
            }

            dtgResult.Items.Refresh();
        }

        private void btnClearMI_Click(object sender, RoutedEventArgs e)
        {
            txtMIEmail.Text = "";
            txtMILastName.Text = "";
            txtMIPhone.Text = "";

            searchlist.Clear();
            dtgResult.Items.Refresh();
            return;
        }
    }



        
    }

