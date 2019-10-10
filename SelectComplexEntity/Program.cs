using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DotNetConcepts.EntityFramework.SelectComplexEntity
{
    enum CardType : int
    {
        Own = 1,
        Friend = 2,
    }

    abstract class Card
    {
        public int Id { get; set; }
        public CardType Type { get; set; }
        public string Name { get; set; }
    }

    class MyCard : Card
    {
        public string BookstorePart { get; set; }
    }

    class FriendCard : Card
    {
        public Friend Friend { get; set; }
        public int Friend_Id { get; set; }
    }

    class Friend
    {
        public int Id { get; set; }
        public string GivenName { get; set; }

        public List<FriendCard> Cards { get; set; }
    }

    class CardOut
    {
        public int Id { get; set; }
        public CardType Type { get; set; }
        public string Name { get; set; }

        public MyCardOut MyCard { get; set; }
        public FriendCardOut FriendCard { get; set; }

        public static readonly Expression<Func<Card, CardOut>> Build = x => new CardOut()
        {
            Id = x.Id,
            Type = x.Type,
            Name = x.Name,

            MyCard = !(x is MyCard) ? null : new MyCardOut()    
            {
                BookstorePart = ((MyCard)x).BookstorePart,
            },

            FriendCard = !(x is FriendCard) ? null : new FriendCardOut()
            {
                Friend_Id = ((FriendCard)x).Friend.Id,
                Friend_GivenName = ((FriendCard)x).Friend.GivenName,
            }
        };
    }
    class MyCardOut
    {
        public string BookstorePart { get; set; }
    }
    class FriendCardOut
    {
        public int Friend_Id { get; set; }
        public string Friend_GivenName { get; set; }
    }

    class EntitySelectContext : DbContext
    {
        public EntitySelectContext(DbContextOptions options)
            : base(options)
        {
        }

        public DbSet<Card> Cards { get; set; }
        public DbSet<Friend> Friends { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Card>(entity =>
            {
                entity.HasDiscriminator(x => x.Type)
                    .HasValue<MyCard>(CardType.Own)
                    .HasValue<FriendCard>(CardType.Friend)
                    ;
            });

            modelBuilder.Entity<FriendCard>(entity =>
            {
                entity.HasOne(p => p.Friend)
                    .WithMany(b => b.Cards)
                    .HasForeignKey(p => p.Friend_Id)
                    .IsRequired();
            });
        }
    }

    class Program
    {
        static async Task Main(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<EntitySelectContext>();
            optionsBuilder.UseSqlServer("Server=.;Initial Catalog=concepts.tagfiltering.local;Integrated Security=True");

            Console.WriteLine("Preparing database..");

            using (var context = new EntitySelectContext(optionsBuilder.Options))
            {
                await context.Database.EnsureDeletedAsync();
                await context.Database.EnsureCreatedAsync();
            }

            Console.WriteLine("Inserting data..");

            using (var context = new EntitySelectContext(optionsBuilder.Options))
            using (var transaction = await context.Database.BeginTransactionAsync())
            {
                Friend peter;

                context.Friends.Add(peter = new Friend()
                {
                    GivenName = "Peter"
                });

                await context.SaveChangesAsync();

                Card ragnaros, sylvanas, kelthuzad;

                context.Cards.Add(ragnaros = new MyCard()
                {
                    Name = "Ragnaros",
                    BookstorePart = "Main",
                });
                context.Cards.Add(sylvanas = new MyCard()
                {
                    Name = "Sylvanas Windrunner",
                    BookstorePart = "Second",
                });
                context.Cards.Add(kelthuzad = new FriendCard()
                {
                    Name = "Kel'Thuzad",
                    Friend = peter,
                });

                await context.SaveChangesAsync();

                transaction.Commit();
            }

            Console.WriteLine("Printing filtered cards..");

            using (var context = new EntitySelectContext(optionsBuilder.Options))
            {
                var all = await context.Cards
                    .Select(CardOut.Build)
                    .ToArrayAsync();

                foreach(var card in all)
                {
                    Console.WriteLine($"Type: {card.Type}, Name: {card.Name}, bookstore part: {card.MyCard?.BookstorePart}, friend given name: {card.FriendCard?.Friend_GivenName}");
                }
            }

            Console.ReadKey();
        }
    }
}