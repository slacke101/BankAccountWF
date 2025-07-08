using BankAccountApp;

namespace BankAccountAppBasic
{
    public partial class Form1 : Form // partial class indicates that it is part of Form
    {
        public Form1()
        {
            InitializeComponent();

            BankAccount bankAccount = new BankAccount(); // creates a bank account, uses Guid
            bankAccount.Owner = "Rafael Castro";
            bankAccount.AccountNumber = Guid.NewGuid();
            bankAccount.Balance = 250;



            List<BankAccount> bankAccounts = new List<BankAccount>();
            bankAccounts.Add(bankAccount);

            BankAccountsGrid.DataSource = bankAccounts; // datasource uses bankaccountsgrid (name of objects in interface) to display bankAccounts data list
        }

        private void label1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Label clicked!");
        }




    }
}