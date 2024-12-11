using EFCore.DbContexts;
using EFCore.Entities;
using EFCore;




using (var context = new EmployeeContext())
{
    EmployeeRepository repo = new EmployeeRepository(context);
    var employees = repo.GetAll();
    foreach (var employee in employees)
    {
        Console.WriteLine(employee.FirstName + "  " + employee.LastName + "  " + employee.Contact + "  " + employee.Email);
    }
}