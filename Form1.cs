using BankAccountApp;

namespace BankAccountAppBasic
{
    public partial class SunCal : Form // partial class indicates that it is part of Form
    {
        List<BankAccount> BankAccounts = new List<BankAccount>();

        public SunCal()
        {
            InitializeComponent();

        }

        private void AccountCreateButton_Click(object sender, EventArgs e)              // calls the particular name of button in form
        {
            if (string.IsNullOrEmpty(OwnerBox.Text))
            {
                MessageBox.Show("Please enter a valid name.");                          // if blank value is detected in textbox, message box prints error message and ensures
                 
                return;
            }
           

            BankAccount bankAccount = new BankAccount(OwnerBox.Text);                  // uses name of object "OwnerBox" and adds new BankAccount object using text
            BankAccounts.Add(bankAccount);                                             // Add method for bankAccount 

            RefreshGrid();
        }

        private void RefreshGrid()
        { 
            BankAccountsGrid.DataSource = null;
            BankAccountsGrid.DataSource = BankAccounts;
        }
    }
}
