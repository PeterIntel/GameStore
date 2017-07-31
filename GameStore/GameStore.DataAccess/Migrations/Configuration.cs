using GameStore.DataAccess.Contextes;

namespace GameStore.DataAccess.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using GameStore.DataAccess.Entities;
    using System.Collections.Generic;

    public sealed class Configuration : DbMigrationsConfiguration<GamesSqlContext>
    {
        public Configuration()
        {
        }

        protected override void Seed(GamesSqlContext context)
        {
            context.Database.ExecuteSqlCommand("DBCC CHECKIDENT('GameEntities', RESEED, 0)");
            //  This method will be called after migrating to the latest version.
            Random r = new Random();

            context.PlatformTypes.AddOrUpdate(
                    new PlatformTypeEntity() { Id = 1, TypeName = "Android", IsDeleted = false },
                    new PlatformTypeEntity() { Id = 2, TypeName = "iOS", IsDeleted = false }
                );

            context.Genres.AddOrUpdate(
                new GenreEntity() { Id = 1, ParentGenreId = null, Name = "Strategy", IsDeleted = false },
                new GenreEntity() { Id = 2, ParentGenreId = null, Name = "RPG", IsDeleted = false },
                new GenreEntity() { Id = 3, ParentGenreId = null, Name = "Sports", IsDeleted = false },
                new GenreEntity() { Id = 4, ParentGenreId = null, Name = "Races", IsDeleted = false },
                new GenreEntity() { Id = 5, ParentGenreId = 1, Name = "RTS", IsDeleted = false },
                new GenreEntity() { Id = 6, ParentGenreId = 1, Name = "TBS", IsDeleted = false },
                new GenreEntity() { Id = 7, ParentGenreId = 4, Name = "rally", IsDeleted = false },
                new GenreEntity() { Id = 8, ParentGenreId = 4, Name = "arcade", IsDeleted = false }
                );

            context.SaveChanges();

            context.Publishers.AddOrUpdate(
                new PublisherEntity()
                {
                    Id = 1,
                    CompanyName = "Nale",
                    Description = "info",
                    HomePage = "http://www.vk.com"
                },

                new PublisherEntity()
                {
                    Id = 2,
                    CompanyName = "MicrosoftStudio",
                    Description = "info",
                    HomePage = "https://www.microsoftstudios.com/"
                });

            context.SaveChanges();

            context.Games.AddOrUpdate(
                new GameEntity()
                {
                    Id = 1,
                    Key = "AgeofEmpires",
                    Description = "bla-bla-bla",
                    IsDeleted = false,
                    PlatformTypes = new List<PlatformTypeEntity>() { context.PlatformTypes.First(x => x.Id == 1), context.PlatformTypes.First(x => x.Id == 2) },
                    Genres = new List<GenreEntity> { context.Genres.First(x => x.Id == 5), context.Genres.First(x => x.Id == 7) },
                    Publisher = context.Publishers.First(x => x.Id == 2),
                    Price = 100
                },

                new GameEntity()
                {
                    Id = 2,
                    Key = "CompanyofHeros",
                    Description = "bla-bla-bla",
                    IsDeleted = false,
                    PlatformTypes = new List<PlatformTypeEntity>() { context.PlatformTypes.First(x => x.Id == 1), context.PlatformTypes.First(x => x.Id == 2) },
                    Genres = new List<GenreEntity> { context.Genres.First(x => x.Id == 5), context.Genres.First(x => x.Id == 7) },
                    Price = 120,
                    Publisher = context.Publishers.First(x => x.Id == 2),
                    PublishedDate = new DateTime(2017, 07, 22)
                },

                new GameEntity()
                {
                    Id = 3,
                    Key = "TotalWar",
                    Description = "bla-bla-bla",
                    IsDeleted = false,
                    PlatformTypes = new List<PlatformTypeEntity>() { context.PlatformTypes.Where(x => x.Id == 1).First(), context.PlatformTypes.Where(x => x.Id == 2).First() },
                    Genres = new List<GenreEntity> { context.Genres.Where(x => x.Id == 5).First(), context.Genres.Where(x => x.Id == 6).First() },
                    Price = 400,
                    Publisher = context.Publishers.First(x => x.Id == 2),
                    PublishedDate = new DateTime(2016, 07, 22)
                },

                new GameEntity()
                {
                    Id = 4,
                    Key = "FIFA17",
                    Description = "bla-bla-bla",
                    IsDeleted = false,
                    PlatformTypes = new List<PlatformTypeEntity>() { context.PlatformTypes.Where(x => x.Id == 1).First(), context.PlatformTypes.Where(x => x.Id == 2).First() },
                    Genres = new List<GenreEntity> { context.Genres.Where(x => x.Id == 2).First(), context.Genres.Where(x => x.Id == 3).First() },
                    Price = 330,
                    Publisher = context.Publishers.First(x => x.Id == 1),
                    PublishedDate = new DateTime(2017, 06, 28)
                },

                new GameEntity()
                {
                    Id = 5,
                    Key = "Superracing",
                    Description = "bla-bla-bla",
                    IsDeleted = false,
                    PlatformTypes = new List<PlatformTypeEntity>() { context.PlatformTypes.Where(x => x.Id == 1).First() },
                    Genres = new List<GenreEntity> { context.Genres.Where(x => x.Id == 8).First() },
                    Price = 110,
                    Publisher = context.Publishers.First(x => x.Id == 1),
                    PublishedDate = new DateTime(2017, 07, 21)

                }
                );

            for (int i = 6; i < 100; i++)
            {

                context.Games.AddOrUpdate(
                    new GameEntity()
                    {
                        Id = i,
                        Key = "Game" + i,
                        Description = "Game description",
                        IsDeleted = false,
                        PlatformTypes = new List<PlatformTypeEntity>() { context.PlatformTypes.AsEnumerable().First(x => x.Id == 1), context.PlatformTypes.AsEnumerable().First(x => x.Id == 2) },
                        Genres = new List<GenreEntity> { context.Genres.AsEnumerable().First(x => x.Id == 4), context.Genres.AsEnumerable().First(x => x.Id == 6) },
                        Price = 110 + i,
                        Publisher = r.Next(1, 3) == 1 ? context.Publishers.First(x => x.Id == 1) : context.Publishers.First(x => x.Id == 2),
                        PublishedDate = new DateTime(2017, r.Next(1, 12), 21),
                        UnitsInStock = (short)r.Next(1, 15)
                    }
                    );
            }

            context.SaveChanges();
            context.Comments.AddOrUpdate(
                new CommentEntity()
                {
                    Id = 1,
                    GameId = 1,
                    Name = "Peter",
                    Body = "bla-bla-bla",
                    ParentCommentId = null,
                    IsDeleted = false,
                    Game = context.Games.First(x => x.Key == "AgeofEmpires")
                },
                new CommentEntity()
                {
                    Id = 2,
                    GameId = 1,
                    Name = "Peter",
                    Body = "bla-bla-bla",
                    ParentCommentId = 1,
                    IsDeleted = false,
                    Game = context.Games.First(x => x.Key == "AgeofEmpires")
                },
                new CommentEntity()
                {
                    Id = 3,
                    GameId = 1,
                    Name = "Peter",
                    Body = "bla-bla-bla",
                    ParentCommentId = 2,
                    IsDeleted = false,
                    Game = context.Games.First(x => x.Key == "AgeofEmpires")
                }
                );

            context.GamesInfo.AddOrUpdate(
                new GameInfoEntity()
                {
                    Id = 1,
                    IsDeleted = false,
                    CountOfViews = 2,
                    UploadDate = DateTime.UtcNow.AddDays(-23)
                },
                new GameInfoEntity()
                {
                    Id = 2,
                    IsDeleted = false,
                    CountOfViews = 2,
                    UploadDate = DateTime.UtcNow.AddDays(-23)
                },
                new GameInfoEntity()
                {
                    Id = 3,
                    IsDeleted = false,
                    CountOfViews = 2,
                    UploadDate = DateTime.UtcNow.AddDays(-23)
                },
                new GameInfoEntity()
                {
                    Id = 4,
                    IsDeleted = false,
                    CountOfViews = 2,
                    UploadDate = DateTime.UtcNow.AddDays(-23),
                },
                new GameInfoEntity()
                {
                    Id = 5,
                    IsDeleted = false,
                    CountOfViews = 2,
                    UploadDate = DateTime.UtcNow.AddDays(-23)
                });

            for (int i = 6; i < 100; i++)
            {
                context.GamesInfo.AddOrUpdate(
                    new GameInfoEntity()
                    {
                        Id = i,
                        IsDeleted = false,
                        CountOfViews = i + r.Next(15),
                        UploadDate = new DateTime(2017, r.Next(1,12), 21)
                    }
                );
            }
            context.SaveChanges();

            context.Orders.AddOrUpdate(
                new OrderEntity()
                {
                    Id = 1,
                    CustomerId = 1,
                    IsDeleted = false,
                    OrderDate = DateTime.UtcNow,
                    Status = GameStore.Domain.BusinessObjects.CompletionStatus.Complete
                });

            context.OrderDetails.AddOrUpdate(
                new OrderDetailsEntity()
                {
                    Id = 1,
                    Discount = 10,
                    Price = 120,
                    Quantity = 2,
                    IsDeleted = false,
                    GameId = 1,
                    OrderId = 1
                });
        }
    }
}
