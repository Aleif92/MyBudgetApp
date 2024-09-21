using MyBudgetApp.Models;

namespace MyBudgetApp;

public interface IExpenseRepo
{
    public IEnumerable<Expense> GetAllExpenses();
    
    Expense GetExpenseById(int id);
    Expense DeleteExpense(int id);
}