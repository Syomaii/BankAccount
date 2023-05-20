/*
    Name: Christian Jay Putol
    Date created: 03/17/2023
*/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BankAccount
{
    public partial class Form1 : Form
    {
        double balance = 0;
        int deposit = 0;
        double totaldeposit = 0;
        int check = 0;
        double totalcheck = 0;
        double servicecharge = 0;

        
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtBoxAmmount.Clear();
            txtBoxBal.Clear();
            balance = 0;
            deposit = 0;
            totaldeposit = 0;
            check = 0;
            totalcheck = 0;
            servicecharge = 0;
        }

        private void btnCalculate_Click(object sender, EventArgs e)
        {
            double transactAmmount = 0;
            try
            {
                transactAmmount = double.Parse(txtBoxAmmount.Text);
            }
            catch(FormatException)
            {
                MessageBox.Show("Invalid transaction. Enter a valid ammount");
            }
            try
            {
                if (rbtnDeposit.Checked)
                {
                    balance += transactAmmount;
                    deposit++;
                    totaldeposit += transactAmmount;
                }
                else if (rbtnCheck.Checked)
                {
                    if (balance < transactAmmount)
                    {
                        throw new InsufficientFundsException();
                    }
                    else
                    {
                        balance -= transactAmmount;
                        check++;
                        totalcheck += transactAmmount;
                    }
                }
                else if (rbtnSCharge.Checked)
                {
                    if (transactAmmount > 0)
                    {
                        balance -= transactAmmount;
                        servicecharge += transactAmmount;
                    }
                    else
                    {
                        MessageBox.Show("Service charge should not be less than zero");
                        return;
                    }
                }
            }
            catch(InsufficientFundsException)
            { 
                MessageBox.Show("Insufficient Funds! A service charge of $10 will be deducted to your account");
                balance -= 10;
            }
            catch(Exception)
            {
                MessageBox.Show("An error occured. Try again later!");
                return;
            }
                txtBoxBal.Text = "$"+ balance.ToString();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSummary_Click(object sender, EventArgs e)
        {
            string summary = "Number of Deposits: " + deposit +
                             "\nTotal Deposits          : " + totaldeposit +
                             "\nNumber of Checks   : " + check +
                             "\nTotal Checks             : " + totalcheck +
                             "\nService Charge         : " + servicecharge +
                             "\nRemaining Balance  : " + balance;
            MessageBox.Show(summary, "Summary of Information");
        }
    }
    public class InsufficientFundsException : Exception 
    { 
        public InsufficientFundsException() { }
        public InsufficientFundsException(string message) : base(message) { }
    }
}
