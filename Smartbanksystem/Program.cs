using System;
using System.Collections.Generic;
using System.Linq;

namespace SmartBankSystem
{
    public class InsufficientBalanceException : Exception
    {
        public InsufficientBalanceException(string message) : base(message) { }
    }

    public class MinimumBalanceException : Exception
    {
        public MinimumBalanceException(string message) : base(message) { }
    }

    public class InvalidTransactionException : Exception
    {
        public InvalidTransactionException(string message) : base(message) { }
    }

    public abstract class BankAccount
    {
        public string AccountNumber { get; }
        public string CustomerName { get; }
        public decimal Balance { get; protected set; }
        public List<string> Transactions { get; } = new List<string>();

        protected BankAccount(string accountNumber, string customerName, decimal initialBalance)
        {
            AccountNumber = accountNumber;
            CustomerName = customerName;
            Balance = initialBalance;
            LogTransaction($"Account created with initial balance {Balance:C}");
        }

        public virtual void Deposit(decimal amount)
        {
            if (amount <= 0)
                throw new InvalidTransactionException("Deposit amount must be positive.");

            Balance += amount;
            LogTransaction($"Deposited {amount:C}. New balance: {Balance:C}");
        }

        public virtual void Withdraw(decimal amount)
        {
            if (amount <= 0)
                throw new InvalidTransactionException("Withdrawal amount must be positive.");

            if (amount > Balance)
                throw new InsufficientBalanceException("Withdrawal cannot exceed available balance.");

            Balance -= amount;
            LogTransaction($"Withdrew {amount:C}. New balance: {Balance:C}");
        }

        public void TransferTo(BankAccount target, decimal amount)
        {
            if (target == null)
                throw new InvalidTransactionException("Target account cannot be null.");

            if (ReferenceEquals(this, target))
                throw new InvalidTransactionException("Cannot transfer to the same account.");

            try
            {
                Withdraw(amount);
                target.Deposit(amount);
                LogTransaction($"Transferred {amount:C} to {target.AccountNumber} ({target.CustomerName}).");
                target.LogTransaction($"Received {amount:C} from {AccountNumber} ({CustomerName}).");
            }
            catch
            {
                
                throw;
            }
        }

        public abstract decimal CalculateInterest();

        protected void LogTransaction(string message)
        {
            Transactions.Add($"[{DateTime.Now}] {message}");
        }

        public override string ToString()
        {
            return $"{GetType().Name} - {AccountNumber} - {CustomerName} - Balance: {Balance:C}";
        }
    }

    public class SavingsAccount : BankAccount
    {
        public const decimal MinimumBalance = 1000m;
        public decimal InterestRate { get; } // annual rate, e.g. 0.04m = 4%

        public SavingsAccount(string accountNumber, string customerName, decimal initialBalance, decimal interestRate = 0.04m)
            : base(accountNumber, customerName, initialBalance)
        {
            InterestRate = interestRate;
            if (Balance < MinimumBalance)
            {
                throw new MinimumBalanceException($"Savings account must be opened with at least {MinimumBalance:C}.");
            }
        }

        public override void Withdraw(decimal amount)
        {
            if (amount <= 0)
                throw new InvalidTransactionException("Withdrawal amount must be positive.");

            if (Balance - amount < MinimumBalance)
                throw new MinimumBalanceException($"Withdrawal would breach minimum balance of {MinimumBalance:C}.");

            base.Withdraw(amount);
        }

        public override decimal CalculateInterest()
        {
            var interest = Balance * InterestRate;
            LogTransaction($"Calculated interest {interest:C} at rate {InterestRate:P}.");
            return interest;
        }
    }

    public class CurrentAccount : BankAccount
    {
        public decimal OverdraftLimit { get; }

        public CurrentAccount(string accountNumber, string customerName, decimal initialBalance, decimal overdraftLimit)
            : base(accountNumber, customerName, initialBalance)
        {
            OverdraftLimit = overdraftLimit;
        }

        public override void Withdraw(decimal amount)
        {
            if (amount <= 0)
                throw new InvalidTransactionException("Withdrawal amount must be positive.");

            if (Balance - amount < -OverdraftLimit)
                throw new InsufficientBalanceException($"Withdrawal would exceed overdraft limit of {OverdraftLimit:C}.");

            Balance -= amount;
            LogTransaction($"Withdrew {amount:C} (overdraft allowed). New balance: {Balance:C}");
        }

        public override decimal CalculateInterest()
        {
            var interest = 0m;
            LogTransaction("No interest calculated for current account.");
            return interest;
        }
    }

    public class LoanAccount : BankAccount
    {
        public decimal InterestRate { get; }

        public LoanAccount(string accountNumber, string customerName, decimal loanAmount, decimal interestRate = 0.10m)
            : base(accountNumber, customerName, -Math.Abs(loanAmount))
        {
            InterestRate = interestRate;
            LogTransaction($"Loan taken for {loanAmount:C} at rate {InterestRate:P}.");
        }

        public override void Deposit(decimal amount)
        {
            throw new InvalidTransactionException("Deposits are not allowed directly into a loan account.");
        }

        public override void Withdraw(decimal amount)
        {
            throw new InvalidTransactionException("Withdrawals are not allowed from a loan account.");
        }

        public override decimal CalculateInterest()
        {
            var interest = Math.Abs(Balance) * InterestRate;
            LogTransaction($"Calculated loan interest {interest:C} at rate {InterestRate:P}.");
            return interest;
        }
    }

    public static class Bank
    {
        public static List<BankAccount> Accounts { get; } = new List<BankAccount>();

        public static void SeedSampleData()
        {
            if (Accounts.Any())
                return;

            Accounts.Add(new SavingsAccount("SA1001", "Ramesh", 75000m));
            Accounts.Add(new SavingsAccount("SA1002", "Sita", 12000m));
            Accounts.Add(new CurrentAccount("CA2001", "Rahul", 5000m, overdraftLimit: 20000m));
            Accounts.Add(new CurrentAccount("CA2002", "John", 80000m, overdraftLimit: 10000m));
            Accounts.Add(new LoanAccount("LA3001", "Ravi", 250000m));
            Accounts.Add(new SavingsAccount("SA1003", "Rohan", 52000m));
        }

        public static BankAccount FindAccount(string accountNumber)
        {
            return Accounts.FirstOrDefault(a => a.AccountNumber.Equals(accountNumber, StringComparison.OrdinalIgnoreCase));
        }

        public static IEnumerable<BankAccount> GetAccountsWithBalanceGreaterThan(decimal amount)
        {
            return Accounts.Where(a => a.Balance > amount);
        }

        public static decimal GetTotalBankBalance()
        {
            return Accounts.Sum(a => a.Balance);
        }

        public static IEnumerable<BankAccount> GetTopNBalances(int n)
        {
            return Accounts.OrderByDescending(a => a.Balance).Take(n);
        }

        public static ILookup<string, BankAccount> GroupByAccountType()
        {
            return Accounts.ToLookup(a => a.GetType().Name);
        }

        public static IEnumerable<BankAccount> FindCustomersStartingWith(char startsWith)
        {
            return Accounts.Where(a => a.CustomerName.StartsWith(startsWith.ToString(), StringComparison.OrdinalIgnoreCase));
        }
    }

    public class Program
    {
        static void Main()
        {
            Bank.SeedSampleData();
            RunMenu();
        }

        private static void RunMenu()
        {
            while (true)
            {
                Console.WriteLine();
                Console.WriteLine("======== SMART BANKING SYSTEM ========");
                Console.WriteLine("1. List all accounts");
                Console.WriteLine("2. Deposit");
                Console.WriteLine("3. Withdraw");
                Console.WriteLine("4. Calculate interest (polymorphic)");
                Console.WriteLine("5. Transfer between accounts");
                Console.WriteLine("6. Show transaction history");
                Console.WriteLine("7. LINQ: Accounts with balance > 50,000");
                Console.WriteLine("8. LINQ: Total bank balance");
                Console.WriteLine("9. LINQ: Top 3 highest balance accounts");
                Console.WriteLine("10. LINQ: Group accounts by type");
                Console.WriteLine("11. LINQ: Customers starting with 'R'");
                Console.WriteLine("0. Exit");
                Console.Write("Select an option: ");

                var input = Console.ReadLine();
                Console.WriteLine();

                if (input == "0")
                    break;

                try
                {
                    switch (input)
                    {
                        case "1":
                            ListAllAccounts();
                            break;
                        case "2":
                            DoDeposit();
                            break;
                        case "3":
                            DoWithdraw();
                            break;
                        case "4":
                            DoCalculateInterest();
                            break;
                        case "5":
                            DoTransfer();
                            break;
                        case "6":
                            ShowTransactionHistory();
                            break;
                        case "7":
                            ShowAccountsWithHighBalance();
                            break;
                        case "8":
                            ShowTotalBankBalance();
                            break;
                        case "9":
                            ShowTopThreeAccounts();
                            break;
                        case "10":
                            ShowGroupedByType();
                            break;
                        case "11":
                            ShowCustomersStartingWithR();
                            break;
                        default:
                            Console.WriteLine("Invalid option. Please try again.");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
            }
        }

        private static void ListAllAccounts()
        {
            Console.WriteLine("All Accounts:");
            foreach (var acc in Bank.Accounts)
            {
                Console.WriteLine(acc);
            }
        }

        private static BankAccount PromptForAccount(string promptText)
        {
            Console.Write(promptText);
            var accNo = Console.ReadLine();
            var account = Bank.FindAccount(accNo ?? string.Empty);
            if (account == null)
                throw new InvalidTransactionException("Account not found.");
            return account;
        }

        private static decimal PromptForAmount(string promptText)
        {
            Console.Write(promptText);
            if (!decimal.TryParse(Console.ReadLine(), out var amount) || amount <= 0)
                throw new InvalidTransactionException("Invalid amount entered.");
            return amount;
        }

        private static void DoDeposit()
        {
            var account = PromptForAccount("Enter account number to deposit into: ");
            var amount = PromptForAmount("Enter deposit amount: ");
            account.Deposit(amount);
            Console.WriteLine("Deposit successful.");
        }

        private static void DoWithdraw()
        {
            var account = PromptForAccount("Enter account number to withdraw from: ");
            var amount = PromptForAmount("Enter withdrawal amount: ");
            account.Withdraw(amount);
            Console.WriteLine("Withdrawal successful.");
        }

        private static void DoCalculateInterest()
        {
            var account = PromptForAccount("Enter account number to calculate interest for: ");
            var interest = account.CalculateInterest(); // polymorphic call
            Console.WriteLine($"Calculated interest for {account.AccountNumber}: {interest:C}");
        }

        private static void DoTransfer()
        {
            var from = PromptForAccount("Enter FROM account number: ");
            var to = PromptForAccount("Enter TO account number: ");
            var amount = PromptForAmount("Enter transfer amount: ");

            from.TransferTo(to, amount);
            Console.WriteLine("Transfer successful.");
        }

        private static void ShowTransactionHistory()
        {
            var account = PromptForAccount("Enter account number to view history: ");
            Console.WriteLine($"Transaction history for {account.AccountNumber} ({account.CustomerName}):");
            if (!account.Transactions.Any())
            {
                Console.WriteLine("No transactions found.");
                return;
            }
            foreach (var t in account.Transactions)
            {
                Console.WriteLine(t);
            }
        }

        private static void ShowAccountsWithHighBalance()
        {
            Console.WriteLine("Accounts with balance > 50,000:");
            var result = Bank.GetAccountsWithBalanceGreaterThan(50000m);
            foreach (var acc in result)
                Console.WriteLine(acc);
        }

        private static void ShowTotalBankBalance()
        {
            var total = Bank.GetTotalBankBalance();
            Console.WriteLine($"Total bank balance: {total:C}");
        }

        private static void ShowTopThreeAccounts()
        {
            Console.WriteLine("Top 3 accounts by balance:");
            var result = Bank.GetTopNBalances(3);
            foreach (var acc in result)
                Console.WriteLine(acc);
        }

        private static void ShowGroupedByType()
        {
            Console.WriteLine("Accounts grouped by type:");
            var groups = Bank.GroupByAccountType();
            foreach (var group in groups)
            {
                Console.WriteLine($"== {group.Key} ==");
                foreach (var acc in group)
                    Console.WriteLine(acc);
            }
        }

        private static void ShowCustomersStartingWithR()
        {
            Console.WriteLine("Customers whose name starts with 'R':");
            var result = Bank.FindCustomersStartingWith('R');
            foreach (var acc in result)
                Console.WriteLine(acc);
        }
    }
}

