
using Microsoft.AspNetCore.Mvc;
using MyBudgetApp.Models;
using System.Diagnostics;

namespace MyBudgetApp.Controllers
{
    public class HomeController : Controller
    {
       // private readonly ILogger<HomeController> _logger;

        private readonly IExpenseRepo _dapperExpenseRepo;
        //private readonly ExpensesDbContext _context;

        public HomeController( IExpenseRepo dapperExpenseRepo)
        {
           // _logger = logger;
            _dapperExpenseRepo = dapperExpenseRepo;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
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

        public IActionResult CreateEditExpense(int? id)
        {
            if (id != null)
            {
                //editing => load an expense by id

                var expenseInDb = _dapperExpenseRepo.GetExpenseById(id.Value);

                return View(expenseInDb);

               // return RedirectToAction("Expenses");
            }


            return View();
            
        }

        public IActionResult DeleteExpense(int id)
        {
            _dapperExpenseRepo.DeleteExpense(id);

            // Redirect back to the list of expenses after deletion
            return RedirectToAction("Expenses");
           

        }

        // public IActionResult CreateEditExpenseForm(Expense model)
        // {
        //     if(model.Id == 0)
        //     {
        //         // The id will be 0 if it is new, so we will create an id for a new item
        //         _context.Expenses.Add(model);
        //     }
        //     else
        //     {
        //         //Editing
        //         _context.Expenses.Update(model);
        //     }
        //     
        //     _context.SaveChanges();
        //
        //     return RedirectToAction("Expenses");
        //
        // }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
