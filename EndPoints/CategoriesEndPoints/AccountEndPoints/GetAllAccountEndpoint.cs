using FinanceApi.Data;
using Microsoft.EntityFrameworkCore;

namespace FinanceApi.EndPoints.CategoriesEndPoints.AccountEndPoints;

public static class GetAllAccountEndpoint
{
  public static void MapGetAllAccountEndpoint(this WebApplication app)
  {
    var endPointsAccount = app.MapGroup("Account");

    endPointsAccount.MapGet("", async (FinanceDbContext context) =>
    {
      var accounts = await context.Accounts.ToListAsync();
      return Results.Ok(accounts);
    });
  }
}