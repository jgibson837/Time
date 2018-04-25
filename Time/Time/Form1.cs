using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Time
{

    public partial class Form1 : Form
    {
        #region Public String
        public string Quality_Assurance_Engineer = "Quality Assurance Engineer";
        public string Senior_Developer = "Senior Developer";
        public string Junior_Developer = "Junior Developer";
        public string Client_Services = "Client Services";
        public string Configurator = "Configurator";
        //Job roles 

        public string D1 = "Belfast";
        public string D2 = "Singapore";
        public string D3 = "Denver";
        //Departments

        public string E1 = "NiSoft";
        public string E2 = "Nisoft";
        public string E3 = "Nisoft";
        //Employers

        public static string PassingText;
        public static string PassingText2;
        public static string PassingJobRole;
        #endregion

        public Form1()
        {
            InitializeComponent();
        }

        #region LoadForm
        private void Form1_Load(object sender, EventArgs e)
        {
            this.loginDTableTableAdapter1.Fill(this.dataBaseDataSet.LoginDTable);
            timer1.Start();
            textBox2.PasswordChar = '\u25CF';
            comboBox3.Items.Add(Quality_Assurance_Engineer);
            comboBox3.Items.Add(Senior_Developer);
            comboBox3.Items.Add(Junior_Developer);
            comboBox3.Items.Add(Client_Services);
            comboBox3.Items.Add(Configurator);
            comboBox1.Items.Add(D1);
            comboBox1.Items.Add(D2);
            comboBox1.Items.Add(D3);
            //Pulls data to the datagridview and sets all the combobox values and starts the department timer
        }
        #endregion

        #region UsernameTextBoxFieldSearch
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            textBox3.Text = textboxUsername.Text;
            try
            {
                this.loginDTableTableAdapter1.SearchUser(this.dataBaseDataSet.LoginDTable, textboxUsername.Text);
            }
            catch (System.Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }
            PassingText2 = textBox4.Text;
            //When a username is input in the login screen this will search for that username
            //Passes values to the next form 
        }
        #endregion

        #region CreateAccountButton
        private void button2_Click(object sender, EventArgs e)
        {
            if (button2.Text == "Create Account")
            {
                if (panel3.Visible == false)
                {
                   
                    panel3.Visible = true;
                    panel2.Visible = false;
                    full_nameTextBox.Text = "";
                    usernameTextBox1.Text = "";
                    passwordTextBox1.Text = "";
                    comboBox1.Text = "";
                    comboBox3.Text = "";
                    this.loginDTableBindingSource1.AddNew();
                    button2.Text = "Cancel";
                    //Adds a new row to the login data table, clears previous text to allow new input, sets button text from create account to cancel

                    #region RandomString
                    var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
                    var stringChars = new char[2];
                    var random = new Random();

                    for (int i = 0; i < stringChars.Length; i++)
                    {
                        stringChars[i] = chars[random.Next(chars.Length)];
                    }

                    var finalString = new String(stringChars);
                    label6.Text = finalString;
                    #endregion

                    #region RandomString2
                    var chars2 = "0123456789";
                    var stringChars2 = new char[6];
                    var random2 = new Random();

                    for (int i = 0; i < stringChars2.Length; i++)
                    {
                        stringChars2[i] = chars2[random2.Next(chars2.Length)];
                    }

                    var finalString2 = new String(stringChars2);
                    label7.Text = finalString2;
                    #endregion

                    #region RandomString3
                    var chars3 = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
                    var stringChars3 = new char[1];
                    var random3 = new Random();

                    for (int i = 0; i < stringChars3.Length; i++)
                    {
                        stringChars3[i] = chars3[random3.Next(chars3.Length)];
                    }
                    //These 3 random strings are used to set the National Insurance number 

                    var finalString3 = new String(stringChars3);
                    label8.Text = finalString3;
                    
                    #endregion

                    nI_NumberTextBox.Text = label6.Text + label7.Text + label8.Text;
                    //This is the end result
                }
            } else
            {
                if (MessageBox.Show("Are you sure? Cancel.", "Cancel Create Account", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    this.loginDTableBindingSource.RemoveCurrent();
                    loginDTableDataGridView.Update();
                    panel3.Visible = false;
                    panel2.Visible = true;
                    button2.Text = "Create Account";
                    //If the user presses cancel whilst in the create account process this will remove the current data and change the button text back to 
                    //create account
                }
            }
           
        }
        #endregion

        #region LoginButton
        private void button4_Click(object sender, EventArgs e)
        {
            if (textBox2.Text == passwordTextBox.Text && textboxUsername.Text == usernameTextBox.Text)
            {
                PassingText = textBox3.Text;
                Form2 Form2 = new Form2();
                Form2.Show();
                this.Hide();
                //If statement; Checks that the two text fields for username and password match those of the data on the database
                //Passes the username value to the next form 
                //Opens the form2 and hides this current form
            }
            else
            {
                MessageBox.Show("Wrong username or password!");
                //If an incorrect username of password then this message will display
            }
        }
        #endregion

        #region SaveButton
        private void bSaveAccount_Click_1(object sender, EventArgs e)
        {
            try
            {
                if (usernameTextBox1.Text == "" || passwordTextBox1.Text == "" || full_nameTextBox.Text == "" || job_RoleTextBox.Text == "" || departmentTextBox.Text == "")
                {
                    MessageBox.Show("Don't Leave Blank!", "Error");
                    //Data validation, checks that the new user data has all been input correctly 
                }

                else
                {
                    panel3.Visible = false;
                    panel2.Visible = true;
                    this.Validate();
                    this.loginDTableBindingSource1.EndEdit();
                    this.tableAdapterManager1.UpdateAll(this.dataBaseDataSet);
                    //If all data is correct it is then saved to the database
                    //Hides the create user UI
                }
            }
            catch (System.Data.ConstraintException ex)
            {
                MessageBox.Show("Already Present in the Database.", ex.Message);
                panel3.Visible = true;
                panel2.Visible = false;
                //If the new user data is already in the database the message box will display
            }
        }
        #endregion

        #region DepartmentTimer1
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (departmentTextBox.Text == D1 )
            {
                employerTextBox.Text = E1;
            }

            if (departmentTextBox.Text == D2)
            {
                employerTextBox.Text = E2;
            }

            if (departmentTextBox.Text == D3)
            {
                employerTextBox.Text = E3;
            }
            //Sets the employer name to one of the following strings depending on the department 
        }
        #endregion

        #region PassingJobRole
        private void job_RoleTextBox_TextChanged(object sender, EventArgs e)
        {
            PassingJobRole = job_RoleTextBox.Text;
            //Passes the job role to the next form
        }
        #endregion

        #region Combobox1
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            departmentTextBox.Text = comboBox1.Text;
            //Sets the data to be saved from the value of the combox
        }
        #endregion

        #region Combobox2
        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            job_RoleTextBox.Text = comboBox3.Text;
            //Sets the data to be saved from the value of the combox
        }
        #endregion
      
    }
}
