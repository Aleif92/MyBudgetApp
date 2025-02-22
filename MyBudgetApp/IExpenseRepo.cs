using MyBudgetApp.Models;

namespace MyBudgetApp;

public interface IExpenseRepo
{
    public IEnumerable<Expense> GetAllExpenses();
    
    Expense GetExpenseById(int id);
    Expense DeleteExpense(int id);

    Expense Add(string Description, decimal Value);

    void UpdateExpense(Expense expense);
   // void Add(Expense description);
}