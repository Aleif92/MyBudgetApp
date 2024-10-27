using Microsoft.AspNetCore.Mvc;
using MyBudgetApp;
using MyBudgetApp.Models;

public class HomeController : Controller
{
    private readonly IExpenseRepo _dapperExpenseRepo;
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger, IExpenseRepo dapperExpenseRepo)
    {
        _logger = logger;
        _dapperExpenseRepo = dapperExpenseRepo;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Expenses()
    {
        var allExpenses = _dapperExpenseRepo.GetAllExpenses().ToList();
        var totalExpenses = allExpenses.Sum(x => x.Value);
        ViewBag.Expenses = totalExpenses;

        return View(allExpenses);
    }

    public IActionResult DeleteExpense(int id)
    {
        _dapperExpenseRepo.DeleteExpense(id);
        return RedirectToAction("Expenses");
    }

    // This action shows the form for creating/editing an expense
    public IActionResult CreateEditExpense(int? id)
    {
        if (id == null) // New expense
        {
            return View(new Expense());
        }
    
        // Edit existing expense, load it by Id
        var expenseInDb = _dapperExpenseRepo.GetExpenseById(id.Value);
        if (expenseInDb == null)
        {
            return NotFound();
        }
    
        return View(expenseInDb);
    }

    // Handles the form submission for creating or editing an expense
    [HttpPost]
    public IActionResult CreateEditExpense(Expense model)
    {
        _logger.LogInformation("Form submitted. Model state: {ModelState}", ModelState.IsValid);
        
        if (ModelState.IsValid)
        {
            if (model.Id == 0) // New expense
            {
                _dapperExpenseRepo.Add(model.Description, model.Value);
                _logger.LogInformation("New expense added with description: {Description}, value: {Value}", model.Description, model.Value);
            }
            else // Existing expense, update it
            {
                _dapperExpenseRepo.UpdateExpense(model);
                _logger.LogInformation("Expense updated. Id: {Id}, Description: {Description}, Value: {Value}", model.Id, model.Description, model.Value);
            }

            return RedirectToAction("Expenses"); // Redirect to the list of expenses
        }

        _logger.LogWarning("Model state is invalid. Returning to form.");
        return View(model);
    }
}
