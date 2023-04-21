Account account = new(300);
account.Notify += DisplayMessage;
account.Put(200);
account.Take(150);
void DisplayMessage(Account sender, AccountEventArgs e) {
    Console.WriteLine($"Sum of transaction: {e.Sum}");
    Console.WriteLine(e.Message);
    Console.WriteLine($"You have {sender.Sum} on your bank account");
}
Console.ReadKey();
internal class Account  {   
    public delegate void AccountHandler(Account sender, AccountEventArgs e);
    public event AccountHandler? Notify;

    public int Sum { get; private set; }
    public Account(int sum) => Sum = sum;
    public void Put(int sum) {
        Sum += sum;
        Notify?.Invoke(this, new AccountEventArgs($"{sum} were deposited in your account", sum));
    }
    public void Take(int sum) {
        if (Sum >= sum) {
            Sum -= sum;
            Notify?.Invoke(this, new AccountEventArgs($"{sum} was withdrawn from your account", sum));
        }
        else {            
            Notify?.Invoke(this, new AccountEventArgs($"You don't have enough money on your bank account", sum));
        }
    }
}
internal class AccountEventArgs {
    public string Message { get; }
    public int Sum { get; }
    public AccountEventArgs(string message, int sum) {
        Message = message;
        Sum = sum;
    }
}