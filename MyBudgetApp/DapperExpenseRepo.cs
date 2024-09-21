using System.Data;
using Dapper;
using MyBudgetApp.Models;

namespace MyBudgetApp;

public class DapperExpenseRepo: IExpenseRepo
{
    
    private readonly IDbConnection _conn;
    
    public DapperExpenseRepo(IDbConnection conn)
    {
        _conn = conn;
    }
    
    public IEnumerable<Expense> GetAllExpenses()
    {
        return _conn.Query<Expense>("SELECT * FROM Expenses");
    }
    public Expense GetExpenseById(int id)
    {
        var query = "SELECT * FROM Expenses WHERE Id = @Id";
        return _conn.QuerySingleOrDefault<Expense>(query, new { Id = id });
    }
    
    public Expense DeleteExpense(int id)
    {
        var expenseToDelete = _conn.QuerySingleOrDefault<Expense>("SELECT * FROM expenses WHERE expenseid = @id", new { id });

        if (expenseToDelete != null)
        {
            // Then, delete the expense
            _conn.Execute("DELETE FROM expenses WHERE expenseid = @id", new { id });
        }

        // Return the deleted expense
        return expenseToDelete;

    }
    
}