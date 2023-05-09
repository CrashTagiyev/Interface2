
using Microsoft.Win32.SafeHandles;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;

//Some Usefull Methods
public static class Sum
{
    static public string? GetShortGUID()
    {
        Guid tempGuid = Guid.NewGuid();
        string? shortGuid = null;
        for (int i = 0; i < 8; i++)
        {
            shortGuid += tempGuid.ToString()[i];
        }
        return shortGuid;
    }

    static public int GetAge(DateTime BirthdayYear)
    {
        return DateTime.Now.Year - BirthdayYear.Year;
    }
}

public abstract class Person
{
    protected Person(string? name, string? surname, DateTime BirthdayTime)
    {
        Name = name;
        Surname = surname;
        Age = Sum.GetAge(BirthdayTime);
    }
    public string? ID { get; set; } = Sum.GetShortGUID();
    public string? Name { get; set; }
    public string? Surname { get; set; }
    public int Age { get; set; }

    public override string ToString()
    {
        return $"Id:{ID}\nName:{Name}\nSurname:{Surname}\nAge:{Age}\n";
    }
}

interface IWorking
{
    public string? Position { get; set; }
    public int Salary { get; set; }
    public string? StartTime { get; set; }
    public string? EndTime { get; set; }

}
public abstract class Worker : Person, IWorking
{

    public Worker(string? name, string? surname, DateTime BirthdayTime, string? position, int salary, string? startTime, string? endTime) : base(name, surname, BirthdayTime)
    {
        Position = position;
        Salary = salary;
        StartTime = startTime;
        EndTime = endTime;
    }
    public string? Position { get; set; }
    public int Salary { get; set; }
    public string? StartTime { get; set; }
    public string? EndTime { get; set; }

    public override string ToString()
    {
        return base.ToString() + $"Position:{Position}\nSalary:{Salary}$\nWorktimes:{StartTime} {EndTime}";
    }

}
public class Client : Person
{
    public List<Credit> credits { get; set; } = new();
    public Client(string? name, string? surname, DateTime BirthdayTime) : base(name, surname, BirthdayTime)
    {
    }
    public void AddCredit(Credit newCredit)
    {
        credits.Add(newCredit);
    }
}
public class Manager : Worker
{
    public Manager(string? name, string? surname, DateTime BirthdayTime, string? position, int salary, string? startTime, string? endTime) : base(name, surname, BirthdayTime, position, salary, startTime, endTime)
    {
    }
    public void Organize(Client client, DateTime OrganizeTime)
    {
        Console.WriteLine($"{Name} {Surname} oganized meeting with {client.Name} {client.Surname} at {OrganizeTime}");
    }

}
public class Ceo : Worker
{
    public Ceo(string? name, string? surname, DateTime BirthdayTime, string? position, int salary, string? startTime, string? endTime) : base(name, surname, BirthdayTime, position, salary, startTime, endTime)
    {
    }
    public void Control()
    {
        Console.WriteLine("I`m controlling this bank");
    }
    public void makeMeeting(Client client, DateTime OrganizeTime)
    {
        Console.WriteLine($"{Name} {Surname} oganized meeting with {client.Name} {client.Surname} at {OrganizeTime}");
    }

}
public class Credit
{
    public Credit(string? creditName, int amount, int percent, int months)
    {
        CreditName = creditName;
        Amount = amount;
        Percent = percent;
        Months = months;
    }
    public string? CreditName { get; set; }
    public string? Id { get; set; } = Sum.GetShortGUID();
    public int Amount { get; set; }
    public int Percent { get; set; }
    public int Months { get; set; }

    public override string ToString()
    {
        return $"Amount:{Amount}\nPercent:{Percent}\nMonth:{Months}";
    }
}

public class StepBank
{
    public string? StepBankName { get; set; }
    public Ceo? CeoName { get; set; }
    public List<Manager> Managers { get; set; } = new();
    public List<Client> Clients { get; set; } = new();
    public List<Client> ClientsCredits { get; set; } = new();


    public StepBank(string? stepBankName, Ceo? ceoName, List<Manager> managers, List<Client> clients)
    {
        StepBankName = stepBankName;
        CeoName = ceoName;
        Managers = managers;
        Clients = clients;
    }

    public void MakeACredit(Client client, string? creditName, int amount, int percent, int months)
    {
        client.AddCredit(new Credit(creditName, amount, percent, months));
        Console.WriteLine($"A {client.Name} {client.Surname} maked a credit for {months} motnh");
        ClientsCredits.Add(client);
    }

    public void ShowCLientsThatMakedCredit()
    {
        foreach (var item in ClientsCredits)
        {
            Console.WriteLine(item);
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        Ceo ceo = new Ceo("Samil", "Tagiyev", new DateTime(1994, 6, 17), "Junior web developer", 500, "9:00", "16:00");

        Manager w1 = new Manager("Samil", "Tagiyev", new DateTime(1994, 6, 17), "Junior web developer", 500, "9:00", "16:00");
        Manager w2 = new Manager("Cmil", "Tagiyev", new DateTime(1994, 6, 17), "Junior web developer", 500, "9:00", "16:00");
        Manager w3 = new Manager("Tamil", "Tagiyev", new DateTime(1994, 6, 17), "Junior web developer", 500, "9:00", "16:00");

        Client c1 = new Client("Arif", "Huseynov", new DateTime(1992, 5, 13));
        Client c2 = new Client("Sabir", "Huseynov", new DateTime(1992, 5, 13));

        List<Manager> workers = new List<Manager> { w1, w2, w3 };
        List<Client> clients = new List<Client> { c1 };

        StepBank stepBank = new StepBank("StepBank", ceo, workers, clients);

        stepBank.MakeACredit(c2, "Teserrufat", 600, 15, 6);

    }
}