

using System.Data;
using Ardalis.Specification.EntityFrameworkCore;
using InteractiveGallery.Infrastructure.Data;
using InteractiveGallery.SharedKernel.Interfaces;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace InteractiveGallery.Web;
public static class SeedData
{
 /* public static readonly Contributor Contributor1 = new(1,"Ardalis");
  public static readonly Contributor Contributor2 = new(1, "Snowfrog");
  public static readonly Project TestProject1 = new Project(1, "Test Project", PriorityStatus.Backlog);
  public static readonly ToDoItem ToDoItem1 = new ToDoItem(1)
  {
    Title = "Get Sample Working",
    Description = "Try to get the sample to build."
  };
  public static readonly ToDoItem ToDoItem2 = new ToDoItem(1)
  {
    Title = "Review Solution",
    Description = "Review the different projects in the solution and how they relate to one another."
  };
  public static readonly ToDoItem ToDoItem3 = new ToDoItem(1)
  {
    Title = "Run and Review Tests",
    Description = "Make sure all the tests run and review what they are doing."
  };

  */public static async  Task Initialize(IServiceProvider serviceProvider)
  {
    
    var dbContext = new InteractiveGalleryDbContext(serviceProvider.GetRequiredService<DbContextOptions<InteractiveGalleryDbContext>>(), serviceProvider.GetRequiredService<IDomainEventDispatcher>());
    var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
      var roles = new[] { "Admin", "User" };
    
      foreach(var role in roles)
      {
      if (!roleManager.RoleExistsAsync(role).Result)
      {
        await roleManager.CreateAsync(new IdentityRole(role));
        await dbContext.SaveChangesAsync();
      }


    }
    var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
    string email = "diana44@gmail.com";
    string password = "t$$esT123456";

    if(await userManager.FindByEmailAsync(email) == null)
    {
      var user = new ApplicationUser();
      user.Email = email;
      user.UserName = email;
      await userManager.CreateAsync(user, password);
      await dbContext.SaveChangesAsync();
      await userManager.AddToRoleAsync(user, "Admin");
      await dbContext.SaveChangesAsync();
    }
    

    // Look for any TODO items.
    //if (dbContext.ToDoItems.Any())
    //{
    //  return;   // DB has been seeded
    //}

    // PopulateTestData(dbContext);



  }
  public static void PopulateTestData(InteractiveGalleryDbContext dbContext)
  {

    //  foreach (var item in dbContext.Projects)
    //  {
    //    dbContext.Remove(item);
    //  }
    //  foreach (var item in dbContext.ToDoItems)
    //  {
    //    dbContext.Remove(item);
    //  }
    //  foreach (var item in dbContext.Contributors)
    //  {
    //    dbContext.Remove(item);
    //  }
    //  dbContext.SaveChanges();

    //  dbContext.Contributors.Add(Contributor1);
    //  dbContext.Contributors.Add(Contributor2);

    //  dbContext.SaveChanges();

    //  ToDoItem1.AddContributor(Contributor1.Id);
    //  ToDoItem2.AddContributor(Contributor2.Id);
    //  ToDoItem3.AddContributor(Contributor1.Id);

    //  TestProject1.AddItem(ToDoItem1);
    //  TestProject1.AddItem(ToDoItem2);
    //  TestProject1.AddItem(ToDoItem3);
    //  dbContext.Projects.Add(TestProject1);
   

  }
}
