// Rafael Castro  //
//  7-7-25        //
// SunCal Banking App using Windows Forms App and C#, intention of the application is to demonstrate simple UI design using VisualStudio 2022, practicing OOP with C# to build  //
// a functional interactive banking app.                                                                                                                                        //

using BankAccountApp;
using System.Net.Http;
using System.Text.Json;
using Timer = System.Windows.Forms.Timer;

namespace BankAccountAppBasic
{
    public partial class SunCal : Form
    {
        // List to hold all created bank accounts during the app session
        private readonly List<BankAccount> BankAccounts = new();

        // Variables for fade-in animation of the weather text
        private float fadeLevel = 0f;
        private Timer weatherFadeTimer;

        // Lock to prevent multiple quick clicks creating multiple accounts
        private bool isHandlingCreate = false;

        // Constructor runs when form initializes
        public SunCal()
        {
            InitializeComponent();

            // IMPORTANT:
            // Make sure AccountCreateButton, depositBtn, withdrawBtn
            // event handlers are wired only once (preferably in Designer).
            // If wired here in code, comment out Designer wiring to prevent double events.

            // Form Load event handler
            this.Load += SunCal_Load;

            // Increase max deposit/withdraw amount to 100,000
            AmountNum.Maximum = 100000;
        }

        // Runs when the form finishes loading
        private async void SunCal_Load(object sender, EventArgs e)
        {
            // Start with transparent weather text
            WeatherTxt1.ForeColor = Color.FromArgb(0, 0, 0, 0);
            WeatherTxt1.Visible = true;

            // Setup timer to handle fade-in animation for weather text
            weatherFadeTimer = new Timer { Interval = 50 };
            weatherFadeTimer.Tick += WeatherFadeTimer_Tick;
            weatherFadeTimer.Start();

            // Fetch and display weather for default location (Seattle)
            var weather = await GetWeatherAsync("Seattle");

            if (weather is WeatherData w)
            {
                // Convert Celsius to Fahrenheit for display
                float tempF = (w.Temperature * 1.8f) + 32;
                WeatherTxt1.Text = $"Weather: {w.Description}, Temp: {tempF:F1}°F";
            }
            else
            {
                WeatherTxt1.Text = "Weather data unavailable";
            }
        }

        // Timer tick event handler to incrementally fade in the weather text
        private void WeatherFadeTimer_Tick(object sender, EventArgs e)
        {
            if (fadeLevel < 1.0f)
            {
                fadeLevel += 0.05f;
                int alpha = (int)(fadeLevel * 255);
                WeatherTxt1.ForeColor = Color.FromArgb(alpha, 0, 0, 0); // gradually increases opacity
            }
            else
            {
                // Stop and dispose timer once fully visible
                weatherFadeTimer?.Stop();
                weatherFadeTimer?.Dispose();
            }
        }

        // Async method to call OpenWeatherMap API and parse weather data
        private async Task<WeatherData?> GetWeatherAsync(string city)
        {
            // Your API key (keep private in real apps!)
            string apiKey = "e6847d58221af9edaa77860a2de305ca";
            string url = $"https://api.openweathermap.org/data/2.5/weather?q={city}&appid={apiKey}&units=metric";

            using HttpClient client = new();

            try
            {
                var response = await client.GetStringAsync(url);
                using JsonDocument doc = JsonDocument.Parse(response);

                // Parse description and temperature from JSON
                string description = doc.RootElement.GetProperty("weather")[0].GetProperty("description").GetString() ?? "Unknown";
                float temp = doc.RootElement.GetProperty("main").GetProperty("temp").GetSingle();

                return new WeatherData { Description = description, Temperature = temp };
            }
            catch
            {
                // Return null if API call fails or JSON parsing fails
                return null;
            }
        }

        // Event handler for creating a new bank account
        private async void AccountCreateButton_Click(object sender, EventArgs e)
        {
            if (isHandlingCreate) return;  // Prevent double handling on rapid clicks
            isHandlingCreate = true;

            try
            {
                string name = OwnerBox.Text.Trim();

                if (string.IsNullOrWhiteSpace(name))
                {
                    MessageBox.Show("Please enter a valid name.");
                    return;
                }

                // Create and add new BankAccount to list
                BankAccount newAccount = new(name);
                BankAccounts.Add(newAccount);

                // Update the grid UI with new account list
                RefreshGrid();

                MessageBox.Show($"Account created for {name}.");
            }
            finally
            {
                // Small delay to avoid accidental multiple clicks
                await Task.Delay(500);
                isHandlingCreate = false;
            }
        }

        // Event handler to deposit money into the selected account
        private void DepositButton_Click(object sender, EventArgs e)
        {
            // Make sure a valid account is selected in the grid
            if (BankAccountsGrid.CurrentRow?.DataBoundItem is not BankAccount account)
            {
                MessageBox.Show("Please select an account.");
                return;
            }

            // Parse and validate deposit amount input
            if (decimal.TryParse(AmountNum.Text, out decimal amount) && amount > 0)
            {
                account.Deposit(amount);  // Add amount to account balance
                RefreshGrid();            // Refresh UI grid
                MessageBox.Show($"Deposited {amount:C} into {account.Owner}'s account.");
            }
            else
            {
                MessageBox.Show("Enter a valid positive amount.");
            }
        }

        // Event handler to withdraw money from the selected account
        private void WithdrawButton_Click(object sender, EventArgs e)
        {
            // Make sure a valid account is selected in the grid
            if (BankAccountsGrid.CurrentRow?.DataBoundItem is not BankAccount account)
            {
                MessageBox.Show("Please select an account.");
                return;
            }

            // Parse and validate withdrawal amount input
            if (decimal.TryParse(AmountNum.Text, out decimal amount) && amount > 0)
            {
                if (account.Balance >= amount)
                {
                    account.Withdraw(amount);  // Deduct amount from account balance
                    RefreshGrid();             // Refresh UI grid
                    MessageBox.Show($"Withdrew {amount:C} from {account.Owner}'s account.");
                }
                else
                {
                    MessageBox.Show("Insufficient funds.");
                }
            }
            else
            {
                MessageBox.Show("Enter a valid positive amount.");
            }
        }

        // Refresh the data grid view with the current bank accounts list
        private void RefreshGrid()
        {
            BankAccountsGrid.DataSource = null;  // Reset binding to refresh UI
            BankAccountsGrid.DataSource = BankAccounts;
        }
    }

    // Simple POCO class to hold weather info returned from API
    public class WeatherData
    {
        public string Description { get; set; } = "";  // Weather condition description (e.g. "clear sky")
        public float Temperature { get; set; }          // Temperature in Celsius
    }
}

