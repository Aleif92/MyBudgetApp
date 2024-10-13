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

    // public Expense Add(string Description, decimal Value)
    // {
    //    // var query = "SELECT * FROM Expenses WHERE Id = @Id";
    //     var query = "INSERT INTO Expenses (Description, Value) VALUES (@Description, @Value)";
    //     return _conn.Execute(query, new { Description = Description, Value = Value });
    //
    // }
    
   

    
    public Expense DeleteExpense(int id)
    {
        var expenseToDelete = _conn.QuerySingleOrDefault<Expense>("SELECT * FROM Expenses WHERE Id = @id", new { id });

        if (expenseToDelete != null)
        {
            // Then, delete the expense
            _conn.Execute("DELETE FROM Expenses WHERE Id = @id", new { id });
        }

        // Return the deleted expense
        return expenseToDelete;
    }

    
    public Expense Add(string description, decimal value)
    {
        var query = "INSERT INTO Expenses (Description, Value) VALUES (@Description, @Value); SELECT LAST_INSERT_ID();";

        // Execute the command and retrieve the new Id
        var id = _conn.ExecuteScalar<int>(query, new { Description = description, Value = value });

        // Now retrieve the complete Expense record using the new Id
        var newExpenseQuery = "SELECT * FROM Expenses WHERE Id = @Id";
        return _conn.QuerySingleOrDefault<Expense>(newExpenseQuery, new { Id = id });
    }
    
    public void UpdateExpense(Expense expense)
    {
        var query = "UPDATE Expenses SET Description = @Description, Value = @Value WHERE Id = @Id";
        _conn.Execute(query, new { expense.Description, expense.Value, expense.Id });
    }

    public void Add(Expense description)
    {
        throw new NotImplementedException();
    }
}