﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Time
{
    public partial class Form2 : Form
    {
        #region StringNames
        string Minus = "";
        #endregion

        #region JobRoleValues
        public string Quality_Assurance_Engineer = "Quality Assurance Engineer"; 
        public string Senior_Developer = "Senior Developer";
        public string Junior_Developer = "Junior_Developer";
        public string Client_Services = "Client_Services";
        public string Configurator = "Configurator";
        //Job roles 
        #endregion

        #region DateTime
        DateTime dt1;
        DateTime dt2;
        DateTime dt3;
        DateTime dt4;
        #endregion

        #region TimeSpan
        TimeSpan ts3;
        #endregion

        #region TestPurposeValues
        int TSHOUR;
        int TSMINUTE;

        int TS2HOUR;
        int TS2MINUTE;
        #endregion

        #region TotalHoursWorkedTally
        public static decimal Total;
        #endregion

        #region TotalOvertime
        public static decimal TotalOverTime;
        #endregion

        #region PayDate
        public static int FormMonth;
        public static int FormYear;

        public static int GridRowCount;
        #endregion

        public Form2()
        {
            InitializeComponent();
        }

        #region Timer
        private void timer1_Tick(object sender, EventArgs e)
        {
            label1.Text = DateTime.Now.ToString();
            label16.Text = DateTime.Now.ToString("MM");
            label17.Text = DateTime.Now.ToString("yyyy");
            label8.Text = newWorkingHoursTableDataGridView.RowCount.ToString();
            GridRowCount = Convert.ToInt32(label8.Text);
            FormMonth = Convert.ToInt32(label16.Text);
            FormYear = Convert.ToInt32(label17.Text);
            //This is timer one it updates every second to set time and dates 
        }
        #endregion

        #region OnFormLoad
        private void Form2_Load(object sender, EventArgs e)
        {
            this.newWorkingHoursTableTableAdapter1.Fill(this.dataBaseDataSet.NewWorkingHoursTable);
            label18.Text = Form1.PassingText2;
            label18.Text = Form1.PassingText2;
            label5.Text = Form1.PassingJobRole;
            textBox1.Text = Form1.PassingText;
            categoriesTextBox.Text = textBox1.Text;
            label2.Text = textBox1.Text;
            timer1.Start();

            try
            {
                this.newWorkingHoursTableTableAdapter1.SearchMe(this.dataBaseDataSet.NewWorkingHoursTable, label2.Text);
            }
            catch (System.Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }
            //Fills the newworking hours datagridview
            //Brings over the text values from the login page 
            //Searchs the database so that the working hours displayed is that of the current logged in user 
        }
        #endregion

        #region StartDayButton - Add
        private void button4_Click(object sender, EventArgs e)
        {
            this.newWorkingHoursTableBindingSource1.AddNew();
            startTimeTextBox.Text = DateTime.Now.ToString("HH:mm");
            dateTextBox.Text = DateTime.Now.ToShortDateString();
            jobTextBox.Text = textBox2.Text;
            categoriesTextBox.Text = textBox1.Text;
            dt1 = DateTime.Now;

            #region RandomString
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var stringChars = new char[8];
            var random = new Random();

            for (int i = 0; i < stringChars.Length; i++)
            {
                stringChars[i] = chars[random.Next(chars.Length)];
            }

            var finalString = new String(stringChars);
            randomTextBox.Text = finalString;
            //Adds a new row to the new working hours 
            //Sets start day time 
            //Creates a random unique value for the row in the datatable, always best to have a unique field 
            #endregion
        }
        #endregion

        #region StartLunchButton
        private void button7_Click(object sender, EventArgs e)
        {
            startLunchTextBox.Text = DateTime.Now.ToString("HH:mm");
            dt3 = DateTime.Now;
            //Sets start lunch time 
        }
        #endregion

        #region EndLunchButton
        private void button6_Click(object sender, EventArgs e)
        {
            endLunchTextBox.Text = DateTime.Now.ToString("HH:mm");
            dt4 = DateTime.Now;  
            //Sets end lunch time 
        }
        #endregion

        #region EndDayButton - Save
        private void button5_Click(object sender, EventArgs e)
        {
            endTimeTextBox.Text = DateTime.Now.ToString("HH:mm");
            dt2 = DateTime.Now;
            TimeSpan ts = (dt2 - dt1);
            label9.Text = ts.ToString(@"hh\:mm");

            TimeSpan ts2 = (dt4 - dt3);
            label10.Text = ts2.ToString(@"hh\:mm");

            ts3 = (ts - ts2);
            label11.Text = ts3.ToString(@"hh\:mm");
            hoursWorkedTextBox.Text = ts3.TotalHours.ToString("N2");

            TimeSpan ts4 = new TimeSpan(7, 30, 0);

            TimeSpan ts5 = ts3.Subtract(ts4);

            overTimeTextBox.Text = ts5.TotalHours.ToString("N2");

            label12.Text = TotalOverTime.ToString();

            this.Validate();
            this.newWorkingHoursTableBindingSource1.EndEdit();
            this.tableAdapterManager2.UpdateAll(this.dataBaseDataSet);
            //Sets end day time 
            //This saves all of the times for the working day to the database
        }
        #endregion

        #region CalanderButton - obs
        private void button1_Click(object sender, EventArgs e)
        {
            Total = 0;

            for (int i = 0; i < newWorkingHoursTableDataGridView.Rows.Count; ++i)
            {
               Total += Convert.ToDecimal(newWorkingHoursTableDataGridView.Rows[i].Cells[7].Value);
            }

            TotalOverTime = 0;

            for (int i = 0; i < newWorkingHoursTableDataGridView.Rows.Count; ++i)
            {
                TotalOverTime += Convert.ToDecimal(newWorkingHoursTableDataGridView.Rows[i].Cells[8].Value);
            }
            Form3 Form3 = new Form3();
            Form3.Show();
            this.Hide();
            //Adds up the hours worked and over time hours on the datagridview
            //Opens a new form and closes the current 
        }
        #endregion

        #region PaySlipButton - obs
        private void button2_Click(object sender, EventArgs e)
        {
            Total = 0;

            for (int i = 0; i < newWorkingHoursTableDataGridView.Rows.Count; ++i)
            {
                Total += Convert.ToDecimal(newWorkingHoursTableDataGridView.Rows[i].Cells[7].Value);
            }

            TotalOverTime = 0;

            for (int i = 0; i < newWorkingHoursTableDataGridView.Rows.Count; ++i)
            {
                TotalOverTime += Convert.ToDecimal(newWorkingHoursTableDataGridView.Rows[i].Cells[8].Value);
            }
            Form4 Form4 = new Form4();
            Form4.Show();
            this.Hide();
            //Adds up the hours worked and over time hours on the datagridview
            //Opens a new form and closes the current 
        }
        #endregion

        #region HomeButton - obs
        private void button3_Click(object sender, EventArgs e)
        {
            Form2 Form2 = new Form2();
            Form2.Show();
            this.Hide();
            //Opens a new form and closes the current
        }
        #endregion

        #region LogoutButton
        private void button8_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure?", "Logout", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Form1 Form1 = new Form1();
                Form1.Show();
                this.Hide();
                //Logout button displays a message box with a yes or no with no closing the message box and yes causing the current form to hide 
                //and the login screen to display
            } 
        }
        #endregion

        #region TestPurposeControls
        private void button9_Click(object sender, EventArgs e)
        {
            TimeSpan ts8 = new TimeSpan(TSHOUR, TSMINUTE, 0);
            hoursWorkedTextBox.Text = ts8.TotalHours.ToString();
            this.Validate();
            this.newWorkingHoursTableBindingSource1.EndEdit();
            this.tableAdapterManager2.UpdateAll(this.dataBaseDataSet);
            //Updates the new hoursworked data to the database 
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            TSHOUR = Convert.ToInt32(textBox3.Text);
            //Converts the time
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            TSMINUTE = Convert.ToInt32(textBox4.Text);
            //Converts the time
        }

        private void button10_Click(object sender, EventArgs e)
        {
            TimeSpan ts9 = new TimeSpan(TS2HOUR, TS2MINUTE, 0);
            overTimeTextBox.Text = ts9.TotalHours.ToString();
            this.Validate();
            this.newWorkingHoursTableBindingSource1.EndEdit();
            this.tableAdapterManager2.UpdateAll(this.dataBaseDataSet);
            //Updates the new overtime data to the database 
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {
            TS2HOUR = Convert.ToInt32(textBox6.Text);
            //Converts the time
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            TS2MINUTE = Convert.ToInt32(textBox5.Text);
            //Converts the time
        }
        #endregion

        #region ShowButton - Test purpose only 
        private void button11_Click(object sender, EventArgs e)
        {
            if (panel2.Visible == false)
            {
                panel2.Visible = true;
                button11.Text = "Hide";
            } else
            {
                panel2.Visible = false;
                button11.Text = "Show";
            }
            //This is in place to hide/show the panel for setting hours worked and overtime
        }
        #endregion

        #region CalanderPictureBoxButton 
        private void pictureBox3_Click(object sender, EventArgs e)
        {
                Total = 0;

                for (int i = 0; i < newWorkingHoursTableDataGridView.Rows.Count; ++i)
                {
                    Total += Convert.ToDecimal(newWorkingHoursTableDataGridView.Rows[i].Cells[7].Value);
                }

                TotalOverTime = 0;

                for (int i = 0; i < newWorkingHoursTableDataGridView.Rows.Count; ++i)
                {
                    TotalOverTime += Convert.ToDecimal(newWorkingHoursTableDataGridView.Rows[i].Cells[8].Value);
                }
                Form3 Form3 = new Form3();
                Form3.Show();
                this.Hide();
            //Adds up the hours worked and over time hours on the datagridview
            //Opens a new form and closes the current 
        }
        #endregion

        #region PictureBoxHomeButton 
        private void pictureBox4_Click(object sender, EventArgs e)
        {
                Form2 Form2 = new Form2();
                Form2.Show();
                this.Hide();
            //Opens a new form and hides the current one
        }
        #endregion

        #region PictureBoxPaySlipButton 
        private void pictureBox2_Click(object sender, EventArgs e)
        {
                Total = 0;

                for (int i = 0; i < newWorkingHoursTableDataGridView.Rows.Count; ++i)
                {
                    Total += Convert.ToDecimal(newWorkingHoursTableDataGridView.Rows[i].Cells[7].Value);
                }

                TotalOverTime = 0;

                for (int i = 0; i < newWorkingHoursTableDataGridView.Rows.Count; ++i)
                {
                    TotalOverTime += Convert.ToDecimal(newWorkingHoursTableDataGridView.Rows[i].Cells[8].Value);
                }
                Form4 Form4 = new Form4();
                Form4.Show();
                this.Hide();
               //Adds up the hours worked and over time hours on the datagridview
               //Opens a new form and closes the current 
        }
        #endregion

    }
}
