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
            // Start with transparent weather text for all cities
            WeatherTxt1.ForeColor = Color.FromArgb(0, 0, 0, 0);
            WeatherTxt2.ForeColor = Color.FromArgb(0, 0, 0, 0);
            WeatherTxt3.ForeColor = Color.FromArgb(0, 0, 0, 0);
            WeatherTxt4.ForeColor = Color.FromArgb(0, 0, 0, 0);
            WeatherTxt5.ForeColor = Color.FromArgb(0, 0, 0, 0);

            WeatherTxt1.Visible = true;
            WeatherTxt2.Visible = true;
            WeatherTxt3.Visible = true;
            WeatherTxt4.Visible = true;
            WeatherTxt5.Visible = true;

            // Setup timer to handle fade-in animation for weather text
            weatherFadeTimer = new Timer { Interval = 50 };
            weatherFadeTimer.Tick += WeatherFadeTimer_Tick;
            weatherFadeTimer.Start();

            // Fetch and display weather for all locations
            var seattleTask = GetWeatherAsync("Seattle");
            var portlandTask = GetWeatherAsync("Portland");
            var laTask = GetWeatherAsync("Los Angeles");
            var nyTask = GetWeatherAsync("New York");
            var houstonTask = GetWeatherAsync("Houston");

            await Task.WhenAll(seattleTask, portlandTask, laTask, nyTask, houstonTask);

            // Update weather displays for each city
            if (seattleTask.Result is WeatherData seattleWeather)
            {
                float tempF = (seattleWeather.Temperature * 1.8f) + 32;
                WeatherTxt1.Text = $"Seattle: {seattleWeather.Description}, {tempF:F1}°F";
            }
            else
            {
                WeatherTxt1.Text = "Seattle weather unavailable";
            }

            if (portlandTask.Result is WeatherData portlandWeather)
            {
                float tempF = (portlandWeather.Temperature * 1.8f) + 32;
                WeatherTxt2.Text = $"Portland: {portlandWeather.Description}, {tempF:F1}°F";
            }
            else
            {
                WeatherTxt2.Text = "Portland weather unavailable";
            }

            if (laTask.Result is WeatherData laWeather)
            {
                float tempF = (laWeather.Temperature * 1.8f) + 32;
                WeatherTxt3.Text = $"Los Angeles: {laWeather.Description}, {tempF:F1}°F";
            }
            else
            {
                WeatherTxt3.Text = "LA weather unavailable";
            }

            if (nyTask.Result is WeatherData nyWeather)
            {
                float tempF = (nyWeather.Temperature * 1.8f) + 32;
                WeatherTxt4.Text = $"New York: {nyWeather.Description}, {tempF:F1}°F";
            }
            else
            {
                WeatherTxt4.Text = "NY weather unavailable";
            }

            if (houstonTask.Result is WeatherData houstonWeather)
            {
                float tempF = (houstonWeather.Temperature * 1.8f) + 32;
                WeatherTxt5.Text = $"Houston: {houstonWeather.Description}, {tempF:F1}°F";
            }
            else
            {
                WeatherTxt5.Text = "Houston weather unavailable";
            }
        }

        // Timer tick event handler to incrementally fade in the weather text
        private void WeatherFadeTimer_Tick(object sender, EventArgs e)
        {
            if (fadeLevel < 1.0f)
            {
                fadeLevel += 0.05f;
                int alpha = (int)(fadeLevel * 255);
                WeatherTxt1.ForeColor = Color.FromArgb(alpha, 0, 0, 0);
                WeatherTxt2.ForeColor = Color.FromArgb(alpha, 0, 0, 0);
                WeatherTxt3.ForeColor = Color.FromArgb(alpha, 0, 0, 0);
                WeatherTxt4.ForeColor = Color.FromArgb(alpha, 0, 0, 0);
                WeatherTxt5.ForeColor = Color.FromArgb(alpha, 0, 0, 0);
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

        private void label4_Click(object sender, EventArgs e)
        {
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
        }

        private void label6_Click(object sender, EventArgs e)
        {
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
        }

        private void label7_Click(object sender, EventArgs e)
        {
        }
    }

    // Simple POCO class to hold weather info returned from API
    public class WeatherData
    {
        public string Description { get; set; } = "";  // Weather condition description (e.g. "clear sky")
        public float Temperature { get; set; }          // Temperature in Celsius
    }
}
