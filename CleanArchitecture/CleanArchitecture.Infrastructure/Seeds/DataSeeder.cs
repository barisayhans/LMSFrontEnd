using Bogus;
using CleanArchitecture.Core.Entities;
using CleanArchitecture.Core.Enums;
using CleanArchitecture.Infrastructure.Contexts;
using CleanArchitecture.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Infrastructure.Seeds
{
    public static class DataSeeder
    {

        public static void Seed(this IHost host)
        {
            using var scope = host.Services.CreateScope();
            using var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            context.Database.Migrate();
            InsertData(context);
        }

        private static void InsertData(ApplicationDbContext context)
        {
            //books
            var bookFaker = new Faker<Book>()
                .RuleFor(b => b.ISBN, f => f.Commerce.Ean8())
                .RuleFor(b => b.Title, f => f.Commerce.ProductName())
                .RuleFor(b => b.Author, f => f.Name.FullName())
                .RuleFor(b => b.Description, f => f.Lorem.Paragraph())
                .RuleFor(b => b.Genre, f => f.Commerce.Categories(1)[0])
                .RuleFor(b => b.PublishDate, f => f.Date.Past(10))
                .RuleFor(b => b.Copies, f => f.Random.Int(1, 30))
                .RuleFor(b => b.Status, f => f.PickRandom<BookStatus>())
                .FinishWith((f, b) =>
                {
                    b.CreatedAt = DateTime.UtcNow;
                    b.CreatedBy = "Seeder";
                    b.LastModifiedAt = DateTime.UtcNow;
                    b.LastModifiedBy = "Seeder";
                });

            var bookCount = context.Books.Count();
            if (bookCount < 50)
            {
                List<Book> generatedBooks = bookFaker.Generate(20);
                foreach (Book book in generatedBooks)
                {
                    context.Books.Add(book);
                    context.SaveChanges();
                }
            }

            

            //borrows
            var borrowFaker = new Faker<Borrow>()
                .RuleFor(b => b.Id, f => Guid.NewGuid())
                .RuleFor(b => b.UserId, f => f.PickRandom(context.Users.OfType<ApplicationUser>().Select(u => u.Id).ToList()))
                .RuleFor(b => b.BookId, f => f.PickRandom(context.Books.Select(b => b.Id).ToList()))
                .RuleFor(b => b.BorrowedAt, f => f.Date.Recent(30))
                .RuleFor(b => b.ReturnedAt, (f, b) => b.BorrowedAt.AddDays(f.Random.Int(1, 14)))
                .RuleFor(b => b.Status, f => f.PickRandom<BorrowStatus>())
                .RuleFor(b => b.DueDate, f => f.Date.Future(30))
                .RuleFor(b => b.LateFee, f => f.Random.Decimal(0, 100))
                .FinishWith((f, b) =>
                {
                    b.CreatedAt = DateTime.UtcNow;
                    b.CreatedBy = "Seeder";
                    b.LastModifiedAt = DateTime.UtcNow;
                    b.LastModifiedBy = "Seeder";
                });

            var borrowCount = context.Borrows.Count();
            if (borrowCount < 50)
            {
                List<Borrow> generatedBorrows = borrowFaker.Generate(30);
                foreach (Borrow borrow in generatedBorrows)
                {
                    context.Borrows.Add(borrow);
                    context.SaveChanges();
                }
            }

            //fines
            var fineFaker = new Faker<Fine>()
                .RuleFor(f => f.Id, f => Guid.NewGuid())
                .RuleFor(b => b.UserId, f => f.PickRandom(context.Users.OfType<ApplicationUser>().Select(u => u.Id).ToList()))
                .RuleFor(f => f.Amount, f => f.Random.Decimal(1, 100))
                .RuleFor(f => f.IsPaid, f => f.Random.Bool())
                .FinishWith((f, b) =>
                {
                    b.CreatedAt = DateTime.UtcNow;
                    b.CreatedBy = "Seeder";
                    b.LastModifiedAt = DateTime.UtcNow;
                    b.LastModifiedBy = "Seeder";
                });

            var fineCount = context.Fines.Count();
            if (fineCount < 50)
            {
                List<Fine> generatedFines = fineFaker.Generate(30);
                foreach (Fine fine in generatedFines)
                {
                    context.Fines.Add(fine);
                    context.SaveChanges();
                }
            }

            //notifications
            var notificationFaker = new Faker<Notification>()
                .RuleFor(n => n.Id, f => Guid.NewGuid())
                .RuleFor(b => b.UserId, f => f.PickRandom(context.Users.OfType<ApplicationUser>().Select(u => u.Id).ToList()))
                .RuleFor(n => n.Message, f => f.Lorem.Sentence())
                .RuleFor(n => n.IsRead, f => f.Random.Bool())
                .FinishWith((f, b) =>
                {
                    b.CreatedAt = DateTime.UtcNow;
                    b.CreatedBy = "Seeder";
                    b.LastModifiedAt = DateTime.UtcNow;
                    b.LastModifiedBy = "Seeder";
                });

            var notificationCount = context.Notifications.Count();
            if (notificationCount < 50)
            {
                List<Notification> generatedNotifications = notificationFaker.Generate(30);
                foreach (Notification notification in generatedNotifications)
                {
                    context.Notifications.Add(notification);
                    context.SaveChanges();
                }
            }

            //reservations
            var reservationFaker = new Faker<Reservation>()
                .RuleFor(r => r.Id, f => Guid.NewGuid())
                .RuleFor(b => b.UserId, f => f.PickRandom(context.Users.OfType<ApplicationUser>().Select(u => u.Id).ToList()))
                .RuleFor(r => r.BookId, f => f.PickRandom(context.Books.Select(b => b.Id).ToList()))
                .RuleFor(r => r.ReservedAt, f => f.Date.Recent(30))
                .RuleFor(r => r.Status, f => f.PickRandom<ReservationStatus>())
                .RuleFor(r => r.ExpiryDate, f => f.Date.Future(30))
                .FinishWith((f, b) =>
                {
                    b.CreatedAt = DateTime.UtcNow;
                    b.CreatedBy = "Seeder";
                    b.LastModifiedAt = DateTime.UtcNow;
                    b.LastModifiedBy = "Seeder";
                });

            var reservationCount = context.Reservations.Count();
            if (reservationCount < 50)
            {
                List<Reservation> generatedReservations = reservationFaker.Generate(30);
                foreach (Reservation reservation in generatedReservations)
                {
                    context.Reservations.Add(reservation);
                    context.SaveChanges();
                }
            }

            //waitlists
            var waitlistFaker = new Faker<Waitlist>()
                .RuleFor(w => w.Id, f => Guid.NewGuid())
                .RuleFor(b => b.UserId, f => f.PickRandom(context.Users.OfType<ApplicationUser>().Select(u => u.Id).ToList()))
                .RuleFor(w => w.BookId, f => f.PickRandom(context.Books.Select(b => b.Id).ToList()))
                .RuleFor(w => w.Position, f => f.Random.Int(1, 100))
                .RuleFor(w => w.Status, f => f.PickRandom<WaitStatus>())
                .FinishWith((f, b) =>
                {
                    b.CreatedAt = DateTime.UtcNow;
                    b.CreatedBy = "Seeder";
                    b.LastModifiedAt = DateTime.UtcNow;
                    b.LastModifiedBy = "Seeder";
                });

            var waitlistCount = context.Waitlists.Count();
            if (waitlistCount < 50)
            {
                List<Waitlist> generatedWaitlists = waitlistFaker.Generate(30);
                foreach (Waitlist waitlist in generatedWaitlists)
                {
                    context.Waitlists.Add(waitlist);
                    context.SaveChanges();
                }
            }
        }
    }
}
