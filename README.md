# BankAccountApp

Snapshot of UI
Bank Account # uses Guid key for strengthened security
<img width="1918" height="1007" alt="suns1" src="https://github.com/user-attachments/assets/09070d7c-b8ec-4c92-b7f8-79f994812e73" />



---
## Overview

**BankAccountApp** is a simple Windows Forms application built with C# that allows users to create and manage bank accounts. The app supports:

* Creating multiple bank accounts with unique owners.
* Depositing money into selected accounts.
* Withdrawing money from selected accounts (with balance checks).
* Displaying real-time weather information for a chosen city, with a smooth fade-in animation.

This project demonstrates basic principles of object-oriented programming (OOP), asynchronous API calls, UI event handling, and simple animations in a desktop environment.

---

## Features

* **Account Management:** Create bank accounts, view balances, deposit and withdraw funds.
* **User Input Validation:** Ensures that users enter valid names and amounts.
* **Weather Widget:** Fetches live weather data from the OpenWeatherMap API and displays it with a fade-in effect.
* **Responsive UI:** Uses data binding to dynamically update account lists in the UI.
* **Event Handling Safeguards:** Prevents rapid multiple clicks causing duplicate account creation.

---

## Getting Started

### Prerequisites

* Windows OS
* Visual Studio (recommended) or any C# IDE that supports Windows Forms
* .NET Framework or .NET Core compatible with Windows Forms

### Setup

1. Clone or download this repository.
2. Open the solution file (`.sln`) in Visual Studio.
3. Restore any NuGet packages if prompted.
4. Obtain an API key from [OpenWeatherMap](https://openweathermap.org/api) and replace the placeholder API key in the code (`SunCal.cs`) with your own.
5. Build and run the project.

---

## How to Use

* **Create Account:** Enter a name and click "Create Account." Avoid clicking the button repeatedly as the app disables multiple quick clicks.
* **Deposit:** Select an account from the list, enter an amount, and click "Deposit."
* **Withdraw:** Select an account from the list, enter an amount, and click "Withdraw." Withdrawals are limited by the account balance.
* **Weather:** The app fetches and displays the current weather for Seattle on startup with a fade-in effect.

---

## Code Highlights

* `BankAccount` class encapsulates owner name and balance with deposit/withdraw methods.
* `SunCal` form handles UI, events, and API calls asynchronously.
* Fade-in effect uses a `System.Windows.Forms.Timer` to gradually show weather text.
* Input validation ensures robustness against invalid user inputs.
* Thread-safe mechanisms prevent unintended double account creation.

---

## Future Improvements

* Allow user to select or change weather location dynamically.
* Add account deletion functionality.
* Persist accounts and balances to a database or local storage.
* Enhance UI with custom styling and animations.
* Add unit tests for core functionalities.

---

## License

This project is licensed under the MIT License. See the [LICENSE](LICENSE) file for details.

---

## Acknowledgments

* Weather data powered by [OpenWeatherMap](https://openweathermap.org/).
* Inspired by learning projects focusing on Windows Forms and C#.

---
