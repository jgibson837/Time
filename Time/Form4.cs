using System;
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
    public partial class Form4 : Form
    {
        #region Intergers/Floats
        float PAYE;
        float TAX; //12%
        float TAX2; //2%
        

        float TotalPay;
        float Hours;
        float HourlyRate;

        float Deductions;

        float NetPay;

        float OvertimeHours;
        float OvertimePay;
        float TotalovertimePay;

        float StandardPay_Overtimepay;

        int Month;
        int Year;
        int days;

        int PayDay;
        int CurrentDay;
        #endregion

        #region String
        public string Quality_Assurance_Engineer = "Quality Assurance Engineer";
        public string Senior_Developer = "Senior Developer";
        public string Junior_Developer = "Junior Developer";
        public string Client_Services = "Client Services";
        public string Configurator = "Configurator";
        string totalhours;
        string totalovertime;
        #endregion

        public Form4()
        {
            InitializeComponent();
        }

        #region OnFormLoad
        private void Form4_Load(object sender, EventArgs e)
        {
            this.newWorkingHoursTableTableAdapter1.Fill(this.dataBaseDataSet.NewWorkingHoursTable);
         
            this.loginDTableTableAdapter1.Fill(this.dataBaseDataSet.LoginDTable);
        
            this.payTableTableAdapter1.Fill(this.dataBaseDataSet.PayTable);

            label11.Text = Form1.PassingText;
            label24.Text = Form2.GridRowCount.ToString();
            Month = Form2.FormMonth;
            Year = Form2.FormYear;

            timer1.Start();

            textBox15.Text = label11.Text;
            textBox16.Text = label11.Text;

            days = DateTime.DaysInMonth(Year, Month);
            label25.Text = days.ToString();

            PayDay = days - 3;
            label26.Text = PayDay.ToString();

            label27.Text = DateTime.Now.ToString("dd");

            #region PAYDAY
            if (CurrentDay == PayDay)
            {
                this.payTableBindingSource.AddNew();

                #region RandomString
                var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
                var stringChars = new char[8];
                var random = new Random();

                for (int i = 0; i < stringChars.Length; i++)
                {
                    stringChars[i] = chars[random.Next(chars.Length)];
                }

                var finalString = new String(stringChars);
                uniqueNumberTextBox.Text = finalString;
                #endregion

                theDateTextBox.Text = DateTime.Now.ToString("MM/yy");
                monthlyPayTextBox.Text = "£" + NetPay.ToString();

                this.Validate();
                this.payTableBindingSource.EndEdit();
                this.tableAdapterManager2.UpdateAll(this.loginDDataSet4);
            }
            #endregion

            #region Search
            try
            {
                this.loginDTableTableAdapter1.SearchG(this.dataBaseDataSet.LoginDTable, textBox15.Text);
            }
            catch (System.Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }

            try
            {
                this.newWorkingHoursTableTableAdapter1.SearchT(this.dataBaseDataSet.NewWorkingHoursTable, textBox16.Text);
            }
            catch (System.Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }
            try
            {
                this.payTableTableAdapter1.SearchName(this.dataBaseDataSet.PayTable, label11.Text);
            }
            catch (System.Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }

            #endregion
        }
        #endregion

        #region JobRoleHourlyRate
        private void textBox13_TextChanged(object sender, EventArgs e)
        {
            if (textBox13.Text == Quality_Assurance_Engineer)
            {
                HourlyRate = 18;
            }
            if (textBox13.Text == Senior_Developer)
            {
                HourlyRate = 16;
            }
            if (textBox13.Text == Junior_Developer)
            {
                HourlyRate = 14;
            }
            if (textBox13.Text == Client_Services)
            {
                HourlyRate = 12;
            }
            if (textBox13.Text == Configurator)
            {
                HourlyRate = 10;
            }
            textBox9.Text = HourlyRate.ToString();
        }
        #endregion

        #region Timer1
        private void timer1_Tick(object sender, EventArgs e)
        {
            #region Date
            label28.Text = DateTime.Now.ToString("dd/MM/yyyy");
            CurrentDay = Convert.ToInt32(label27.Text);
            textBox3.Text = DateTime.Now.ToShortDateString();
            #endregion

            #region Standard Pay
            /////////////////////////////////////////////////////////////////////////////////
            textBox8.Text = Form2.Total.ToString();
            totalhours = textBox8.Text;
            if (totalhours.Contains("."))
                totalhours = totalhours.Replace(".", ",");
            Hours = System.Convert.ToSingle(totalhours);
            label10.Text = totalhours;

            int length = textBox8.Text.Substring(textBox8.Text.IndexOf("")+1).Length;
            label18.Text = length.ToString(); //Amount of numbers

            switch (length)
            {
                case 1:
                    TotalPay = HourlyRate * Hours;
                    break;
                case 3:
                    TotalPay = HourlyRate * Hours / 10;
                    break;
                case 4:
                    TotalPay = HourlyRate * Hours / 100;
                    break;
            }
          
            label20.Text = "£" + TotalPay.ToString(); //Total Pay Text Box
            //////////////////////////////////////////////////////////////////////////////////
            #endregion

            #region Overtime Pay
            /////////////////////////////////////////////////////////////////////////
            overtimeTextBox.Text = Form2.TotalOverTime.ToString();
            totalovertime = overtimeTextBox.Text; //The string
            if (totalovertime.Contains("."))
                totalovertime = totalovertime.Replace(".", ",");
            OvertimeHours = System.Convert.ToSingle(totalovertime);

            int overlength = overtimeTextBox.Text.Substring(overtimeTextBox.Text.IndexOf("") + 1).Length;
            label22.Text = overlength.ToString(); //volatile Length 

            switch (overlength)
            {
                case 2:
                    OvertimePay = HourlyRate * 1.5f;
                    TotalovertimePay = OvertimePay * OvertimeHours;
                    break;
                case 3:
                    OvertimePay = HourlyRate * 1.5f;
                    TotalovertimePay = OvertimePay * OvertimeHours / 10;
                    break;
                case 4:
                    OvertimePay = HourlyRate * 1.5f;
                    TotalovertimePay = OvertimePay * OvertimeHours / 100;
                    break;
            }
            label19.Text = "£" + TotalovertimePay.ToString(); //Total Pay Text Box
            //////////////////////////////////////////////////////////////////////////
            #endregion

            #region The combined totals of standard pay and overtime pay
            if (OvertimeHours < 0)
            {
                label21.Visible = true;
                StandardPay_Overtimepay = TotalPay;
                textBox10.Text = "£" + TotalPay.ToString();

            } else
            {
                StandardPay_Overtimepay = TotalovertimePay + TotalPay;
                textBox10.Text = "£" + StandardPay_Overtimepay.ToString();
            }
            #endregion

            #region TAX
            PAYE = StandardPay_Overtimepay / 4;
            if (PAYE > 157)
            {
                if (PAYE < 866)
                {
                    PAYE = PAYE - 157;
                    TAX = PAYE / 100;
                    TAX = TAX * 12;
                    TAX = TAX * 4;
                    Deductions = TAX;
                    textBox11.Text = "£" + Deductions.ToString();
                }
                if (PAYE >= 866)
                {
                    PAYE = PAYE - 157;
                    if (PAYE >= 709)
                    {
                        TAX = 709 / 100;
                        TAX = TAX * 12;
                        PAYE = PAYE - 709;
                        TAX2 = PAYE / 100;
                        TAX2 = TAX2 * 2;
                        Deductions = TAX + TAX2;
                        Deductions = Deductions * 4;
                        textBox11.Text = "£" + Deductions.ToString();
                    }
                }
            }
            else
            {
                TAX = 0;
            }
            #endregion

            #region Deductions 
            NetPay = StandardPay_Overtimepay - Deductions;
            textBox12.Text = "£" + NetPay.ToString();
            #endregion
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
            }
        }
        #endregion

        #region HomeButton
        private void button3_Click(object sender, EventArgs e)
        {
            Form2 Form2 = new Form2();
            Form2.Show();
            this.Hide();
        }
        #endregion

        #region CalendarButton
        private void button1_Click(object sender, EventArgs e)
        {
            Form3 Form3 = new Form3();
            Form3.Show();
            this.Hide();
        }
        #endregion

        #region PayslipButton
        private void button2_Click(object sender, EventArgs e)
        {
            Form4 Form4 = new Form4();
            Form4.Show();
            this.Hide();
        }
        #endregion

        #region TestPurposeTextBox
        private void textBox17_TextChanged(object sender, EventArgs e)
        {
            label27.Text = textBox17.Text;
        }

        #region ShowButton
        private void button11_Click(object sender, EventArgs e)
        {
            if (panel15.Visible == false)
            {
                panel15.Visible = true;
                button11.Text = "Hide";
            }
            else
            {
                panel15.Visible = false;
                button11.Text = "Show";
            }
        }
        #endregion

        #region PAYDAY
        private void button4_Click(object sender, EventArgs e)
        {
            if (CurrentDay == PayDay)
            {
                this.payTableBindingSource.AddNew();

                #region RandomString
                var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
                var stringChars = new char[8];
                var random = new Random();

                for (int i = 0; i < stringChars.Length; i++)
                {
                    stringChars[i] = chars[random.Next(chars.Length)];
                }

                var finalString = new String(stringChars);
                uniqueNumberTextBox.Text = finalString;
                #endregion

                theDateTextBox.Text = DateTime.Now.ToString("MMMM/yyyy");
                monthlyPayTextBox.Text = "£" + NetPay.ToString();
                searchNameTextBox.Text = label11.Text;

                this.Validate();
                this.payTableBindingSource.EndEdit();
                this.tableAdapterManager2.UpdateAll(this.loginDDataSet4);
            }
        }

        #endregion

        #endregion

        #region PaymentRecordButton
        private void button5_Click(object sender, EventArgs e)
        {
            if (panel16.Visible == false)
            {
                panel16.Visible = true;
                button5.Text = "Close Records";
            }
            else
            {
                panel16.Visible = false;
                button5.Text = "Payment Records";
            }
        }
        #endregion

    

     

       
    }
}
