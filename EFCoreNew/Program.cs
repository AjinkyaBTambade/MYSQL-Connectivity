using EFCoreNew.DbContexts;
using EFCoreNew.Repositories;

Console.WriteLine("Welcome, To The World Of Entity Framework!");

var context = new BlogContext();

BlogRepository repository = new BlogRepository(context);
repository.AddNew();
repository.ShowAll();

