namespace TaskN4
{
    using System;
    public class Account
    {
        public string Name { get; set; }
        public double Balance { get; set; }

        public Account(string name = "Unnamed Account", double balance = 0.0)
        {
            this.Name = name;
            this.Balance = balance;
        }

        public virtual bool Deposit(double amount)
        {
            if (amount < 0)
                return false;
            else
            {
                Balance += amount;
                return true;
            }
        }

        public virtual bool Withdraw(double amount)
        {
            if (Balance - amount >= 0)
            {
                Balance -= amount;
                return true;
            }
            else
            {
                return false;
            }
        }
    }


    public class SavingsAccount : Account
    {
        public double InterestRate { get; set; }

        public SavingsAccount(
            string name = "Unnamed Savings Account",
            double balance = 0.0,
            double interestRate = 0.0)
            : base(name, balance)
        {
            this.InterestRate = interestRate;
        }

        public override bool Deposit(double amount)
        {
            if (amount < 0)
                return false;

            amount += amount * InterestRate / 100;

            return base.Deposit(amount);
        }
    }


    public class CheckingAccount : Account
    {
        private const double WithdrawalFee = 1.50;

        public CheckingAccount(
            string name = "Unnamed Checking Account",
            double balance = 0.0)
            : base(name, balance)
        {
        }

        public override bool Withdraw(double amount)
        {
            if (amount < 0)
                return false;

            return base.Withdraw(amount + WithdrawalFee);
        }
    }


    public class TrustAccount : Account
    {
        public double InterestRate { get; set; }

        private int withdrawalsCount = 0;

        private const double Bonus = 50.0;
        private const double BonusThreshold = 5000.0;

        public TrustAccount(
            string name = "Unnamed Trust Account",
            double balance = 0.0,
            double interestRate = 0.0)
            : base(name, balance)
        {
            InterestRate = interestRate;
        }

        public override bool Deposit(double amount)
        {
            if (amount < 0)
                return false;

            double bonus = amount >= BonusThreshold ? Bonus : 0;

            amount += amount * InterestRate / 100;
            amount += bonus;

            return base.Deposit(amount);
        }


        public override bool Withdraw(double amount)
        {
            if (withdrawalsCount >= 3)
                return false;

            if (amount < 0 || amount >= Balance * 0.20)
                return false;

            if (base.Withdraw(amount))
            {
                withdrawalsCount++;
                return true;
            }

            return false;
        }
    }




    public static class AccountUtil
    {

        public static void Deposit<T>(List<T> accounts, double amount)
    where T : Account
        {
            Console.WriteLine("\n=== Depositing to Accounts =================================");

            foreach (var acc in accounts)
            {
                if (acc.Deposit(amount))
                    Console.WriteLine($"Deposited {amount} to {acc.Name}");
                else
                    Console.WriteLine($"Failed Deposit of {amount} to {acc.Name}");
            }
        }

        public static void Withdraw<T>(List<T> accounts, double amount)
    where T : Account
        {
            Console.WriteLine("\n=== Withdrawing from Accounts ==============================");
            foreach (var acc in accounts)
            {
                if (acc.Withdraw(amount))
                    Console.WriteLine($"Withdrew {amount} from {acc.Name}");
                else
                    Console.WriteLine($"Failed Withdrawal of {amount} from {acc.Name}");
            }
        }

    }



    internal class Program
    {
        static void Main(string[] args)
        {
            // Accounts
            var accounts = new List<Account>();
            accounts.Add(new Account());
            accounts.Add(new Account("Larry"));
            accounts.Add(new Account("Moe", 2000));
            accounts.Add(new Account("Curly", 5000));

            AccountUtil.Deposit(accounts, 1000);
            AccountUtil.Withdraw(accounts, 2000);

            // Savings
            var savAccounts = new List<SavingsAccount>();
            savAccounts.Add(new SavingsAccount());
            savAccounts.Add(new SavingsAccount("Superman"));
            savAccounts.Add(new SavingsAccount("Batman", 2000));
            savAccounts.Add(new SavingsAccount("Wonderwoman", 5000, 5.0));


            AccountUtil.Deposit(savAccounts, 1000);
            AccountUtil.Withdraw(savAccounts, 2000);

            // Checking
            var checAccounts = new List<CheckingAccount>();
            checAccounts.Add(new CheckingAccount());
            checAccounts.Add(new CheckingAccount("Larry2"));
            checAccounts.Add(new CheckingAccount("Moe2", 2000));
            checAccounts.Add(new CheckingAccount("Curly2", 5000));

            AccountUtil.Deposit(checAccounts, 1000);
            AccountUtil.Withdraw(checAccounts, 2000);
            AccountUtil.Withdraw(checAccounts, 2000);



            // Trust
            var trustAccounts = new List<TrustAccount>();
            trustAccounts.Add(new TrustAccount());
            trustAccounts.Add(new TrustAccount("Superman2"));
            trustAccounts.Add(new TrustAccount("Batman2", 2000));
            trustAccounts.Add(new TrustAccount("Wonderwoman2", 5000, 5.0));

            AccountUtil.Deposit(trustAccounts, 1000);
            AccountUtil.Deposit(trustAccounts, 6000);
            AccountUtil.Withdraw(trustAccounts, 2000);
            AccountUtil.Withdraw(trustAccounts, 3000);
            AccountUtil.Withdraw(trustAccounts, 500);

            Console.WriteLine();
        }

    }
}
