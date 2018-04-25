using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;
using System.IO;

namespace Time
{
    public partial class Form3 : Form
    {
        #region Integers
        int value;
        #endregion

        public Form3()
        {
            InitializeComponent();
        }

        #region MonthlyCalander
        private void monthCalendar1_DateChanged(object sender, DateRangeEventArgs e)
        {
            textBox1.Text = label2.Text + monthCalendar1.SelectionRange.Start.ToShortDateString();
            textBox3.Text = monthCalendar1.SelectionRange.Start.ToShortDateString();
            //When a user clicks on a date on the calander it sets textbox data to that of the current date
        }
        #endregion

        #region OnFormLoad
        private void Form3_Load(object sender, EventArgs e)
        {
            this.calenderTableTableAdapter1.Fill(this.dataBaseDataSet.CalenderTable);
            richTextBox1.Enabled = false;
            clientTextBox.Enabled = false;
            textBox2.Visible = false;
            label4.Text = Form1.PassingText2;
            label2.Text = Form1.PassingText;
            timer1.Start();
            //This fills the datagridview for the calander table and grabs values from form 1 such as username and fullname

            #region LoadingCombobox
            StreamReader sr = new StreamReader(@"Combox\cat.txt");
            string line = sr.ReadLine();
            while (line != null)
            {
                comboBox2.Items.Add(line);
                line = sr.ReadLine();
            }
            sr.Close();
            //This loads the text from the .txt file from the folder called combox for client names 
            #endregion
        }
        #endregion

        #region TextBoxSearchDate
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            try
            {
                this.calenderTableTableAdapter1.SearchNOW(this.dataBaseDataSet.CalenderTable, textBox1.Text);
            }
            catch (System.Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }
            //When the date is changed this will search for that row in the datatable in the same way it does for the 
            //login table to find username and password
        }
        #endregion

        #region SaveNoteButton
        private void button1_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.calenderTableBindingSource1.EndEdit();
            this.tableAdapterManager1.UpdateAll(this.dataBaseDataSet);
            richTextBox1.Enabled = false;
            clientTextBox.Enabled = false;
            comboBox2.Visible = false;
            clientTextBox.Visible = true;
            //Saves the note, date, client and user that created the row to the database 
        }
        #endregion

        #region AddNoteButton
        private void button2_Click(object sender, EventArgs e)
        {
            switch (value)
            {
                case 1:
                    this.calenderTableBindingSource1.AddNew();
                    dateTextBox.Text = textBox1.Text;
                    richTextBox1.Enabled = true;
                    clientTextBox.Enabled = true;
                    userTextBox.Text = label2.Text;
                    //Adds a new row to the database 
                    //Enables the text box so that it will accept input 
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
                    button2.Enabled = false;
                    //Adds a unique value to the database, always useful 
                    #endregion
                    break;
                case 2:
                    richTextBox1.Enabled = true;
                    clientTextBox.Enabled = true;
                    break;
            }

        }
        #endregion

        #region DeleteNoteButton
        private void button3_Click(object sender, EventArgs e)
        {
            this.calenderTableBindingSource.RemoveCurrent();
            this.Validate();
            this.calenderTableBindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.loginDDataSet2);
            richTextBox1.Enabled = false;
            clientTextBox.Enabled = false;
            comboBox2.Visible = false;
            clientTextBox.Visible = true;
            //This will delete the currently selected row on the database 
        }
        #endregion

        #region HomeButton
        private void button4_Click(object sender, EventArgs e)
        {
            Form2 Form2 = new Form2();
            Form2.Show();
            this.Hide();
            //Opens a new form and closes the current 
        }
        #endregion

        #region CalendarButton
        private void button6_Click(object sender, EventArgs e)
        {
            Form3 Form3 = new Form3();
            Form3.Show();
            this.Hide();
            //Opens a new form and closes the current 
        }
        #endregion

        #region PaySlipButton
        private void button5_Click(object sender, EventArgs e)
        {
            Form4 Form4 = new Form4();
            Form4.Show();
            this.Hide();
            //Opens a new form and closes the current 
        }
        #endregion

        #region Timer1
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (richTextBox1.Text != "")
            {
                button2.Enabled = true;
                button2.Text = "Edit note";
                button3.Enabled = true;
                value = 2;
            } else
            {
                button2.Text = "Add note";
                button3.Enabled = false;
                value = 1;
            }
            //This detects the change to the rich text box input, if nothing then the add note button will remain the same, but if there is text in the rich text box the add note button text changes to "edit note"
        }
        #endregion

        #region AddNewClientButton
        private void button8_Click(object sender, EventArgs e)
        {
            if (button8.Text == "Add New Client")
            {
                textBox2.Visible = true;
                comboBox2.Visible = false;
                clientTextBox.Visible = false;
                button8.Text = "Save New Client";
                //Changes the button text and allows the user to add a new client into a textbox
            }
            else
            {
                string namestr = textBox2.Text;

                if (!comboBox2.Items.Contains(namestr))
                {
                    comboBox2.Items.Add(namestr);
                }

                StreamWriter OutFile = new StreamWriter(@"C:\Users\Jessica\Documents\Time\Time\bin\Debug\Combox\cat.txt");

                foreach (object L in comboBox2.Items)
                {
                    OutFile.WriteLine(L.ToString());
                }
                OutFile.Close();
                button8.Text = "Add New Client";
                textBox2.Visible = false;
                comboBox2.Visible = true;
                //This saves the new client to the txt file and changes the button text from save new client to add new client 
            }          
        }
        #endregion

        #region DeleteClientButton
        private void button7_Click(object sender, EventArgs e)
        {
            comboBox2.Items.Remove(comboBox2.SelectedItem);
            //This will remove the currently selected value from the combobox
        }
        #endregion

        #region Combobox2
        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            clientTextBox.Text = comboBox2.Text;
            //Sets row value to be saved to the combobox value 
        }
        #endregion

        #region LogoutButton
        private void button9_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure?", "Logout", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Form1 Form1 = new Form1();
                Form1.Show();
                this.Hide();
                //Logout button displays a message box with a yes or no with no closing the message box and yes causing the current form to hide and the login screen to display 
        }
        }
        #endregion

        #region PictureBoxHomeButton
        private void pictureBox4_Click(object sender, EventArgs e)
        {
                Form2 Form2 = new Form2();
                Form2.Show();
                this.Hide();
                //opens a new form and hides the current 
        }
        #endregion

        #region PictureBoxCalendarButton
        private void pictureBox3_Click(object sender, EventArgs e)
        {
                Form3 Form3 = new Form3();
                Form3.Show();
                this.Hide();
                //opens a new form and hides the current 
        }
        #endregion

        #region PictureBoxPaySlipButton
        private void pictureBox2_Click(object sender, EventArgs e)
        {
                Form4 Form4 = new Form4();
                Form4.Show();
                this.Hide();
                //opens a new form and hides the current 
        }
        #endregion
 
    }
}
