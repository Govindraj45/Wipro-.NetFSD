namespace DigitalWallet.Core.Models;

public sealed class Wallet
{
    public Wallet(int userId, string ownerName, decimal balance)
    {
        if (userId <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(userId), "User id must be positive.");
        }

        if (string.IsNullOrWhiteSpace(ownerName))
        {
            throw new ArgumentException("Owner name is required.", nameof(ownerName));
        }

        if (balance < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(balance), "Opening balance cannot be negative.");
        }

        UserId = userId;
        OwnerName = ownerName;
        Balance = balance;
    }

    public int UserId { get; }

    public string OwnerName { get; }

    public decimal Balance { get; private set; }

    public void AddMoney(decimal amount)
    {
        ValidatePositiveAmount(amount);

        Balance += amount;
    }

    public void Deduct(decimal amount)
    {
        ValidatePositiveAmount(amount);

        if (amount > Balance)
        {
            throw new InvalidOperationException("Insufficient wallet balance.");
        }

        Balance -= amount;
    }

    private static void ValidatePositiveAmount(decimal amount)
    {
        if (amount <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(amount), "Amount must be greater than zero.");
        }
    }
}
