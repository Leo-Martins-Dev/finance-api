using finance_api.Data;
using finance_api.DTO.CustomerAllDtos;
using finance_api.Entities;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace finance_api.EndPoints
{
    public static class CustomerEndPoints
    {
        public static void MapCustomerEndPoints(this WebApplication app)
        {
            var endPointsCustomer = app.MapGroup("Customer");

            endPointsCustomer.MapGet("", async (FinanceDbContext context) =>
            {
                var customers = await context.Customers.ToListAsync();
                return customers;
            });

            endPointsCustomer.MapGet("{Id:guid}", async (Guid Id, FinanceDbContext context) =>
            {
                var customer = await context.Customers.FindAsync(Id);
                return customer;
            });

            endPointsCustomer.MapPost("", async (CustomerAddDto request, FinanceDbContext context) =>
            {
                var customer = new Customer(request.Name, request.Email)
                {
                    Id = Guid.NewGuid()
                };

                await context.Customers.AddAsync(customer);
                await context.SaveChangesAsync();
                return customer;
            });

            endPointsCustomer.MapPut("{Id:guid}", async (Guid Id, CustomerUpdateDto request, FinanceDbContext context) =>
            {
                var customer = await context.Customers.FindAsync(Id);
                if (customer != null)
                {
                    customer.Name = request.Name;
                    customer.Email = request.Email;
                    await context.SaveChangesAsync();
                }
                return customer;

            });

            endPointsCustomer.MapDelete("{Id:guid}", async (Guid Id, FinanceDbContext context) =>
            {
                var customer = await context.Customers.FindAsync(Id);
                if (customer != null)
                {
                    context.Customers.Remove(customer);
                    await context.SaveChangesAsync();
                }
            });
        }
    }
}