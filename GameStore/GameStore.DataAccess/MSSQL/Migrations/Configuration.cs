using GameStore.DataAccess.MSSQL.Entities;

namespace GameStore.DataAccess.MSSQL.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using System.Collections.Generic;

    public sealed class Configuration : DbMigrationsConfiguration<GamesSqlContext>
    {
        public Configuration()
        {
        }

        protected override void Seed(GamesSqlContext context)
        {
            //context.Database.ExecuteSqlCommand("DBCC CHECKIDENT('GameEntities', RESEED, 0)");
            //This method will be called after migrating to the latest version.
            Random r = new Random();

            context.PlatformTypes.AddOrUpdate(
                    new PlatformTypeEntity() { Id = new Guid("d6224e00-2078-4243-aed5-7e31b76a9941").ToString(), TypeName = "Android", IsDeleted = false, IsSqlEntity = true },
                    new PlatformTypeEntity() { Id = new Guid("d6224e00-2078-4243-aed5-7e31b76a9942").ToString(), TypeName = "iOS", IsDeleted = false, IsSqlEntity = true }
                );

            context.SaveChanges();

            context.Genres.AddOrUpdate(
                new GenreEntity() { Id = new Guid("d6224e00-2078-4243-aed5-7e31b76a9941").ToString(), ParentGenreId = null, Name = "Strategy", IsDeleted = false, IsSqlEntity = true },
                new GenreEntity() { Id = new Guid("d6224e00-2078-4243-aed5-7e31b76a9942").ToString(), ParentGenreId = null, Name = "RPG", IsDeleted = false, IsSqlEntity = true },
                new GenreEntity() { Id = new Guid("d6224e00-2078-4243-aed5-7e31b76a9943").ToString(), ParentGenreId = null, Name = "Sports", IsDeleted = false, IsSqlEntity = true },
                new GenreEntity() { Id = new Guid("d6224e00-2078-4243-aed5-7e31b76a9944").ToString(), ParentGenreId = null, Name = "Races", IsDeleted = false, IsSqlEntity = true });
            context.SaveChanges();
            context.Genres.AddOrUpdate(
                new GenreEntity() { Id = new Guid("d6224e00-2078-4243-aed5-7e31b76a9945").ToString(), ParentGenreId = context.Genres.First(x => x.Name == "RPG").Id, Name = "RTS", IsDeleted = false, IsSqlEntity = true },
                new GenreEntity() { Id = new Guid("d6224e00-2078-4243-aed5-7e31b76a9946").ToString(), ParentGenreId = context.Genres.First(x => x.Name == "Sports").Id, Name = "TBS", IsDeleted = false, IsSqlEntity = true },
                new GenreEntity() { Id = new Guid("d6224e00-2078-4243-aed5-7e31b76a9947").ToString(), ParentGenreId = context.Genres.First(x => x.Name == "RPG").Id, Name = "rally", IsDeleted = false, IsSqlEntity = true },
                new GenreEntity() { Id = new Guid("d6224e00-2078-4243-aed5-7e31b76a9948").ToString(), ParentGenreId = context.Genres.First(x => x.Name == "Races").Id, Name = "arcade", IsDeleted = false, IsSqlEntity = true }
                );

            context.SaveChanges();

            context.Publishers.AddOrUpdate(
                new PublisherEntity()
                {
                    Id = new Guid("d6224e00-2078-4243-aed5-7e31b76a9941").ToString(),
                    CompanyName = "Nale",
                    Description = "info",
                    HomePage = "http://www.vk.com",
                    IsSqlEntity = true
                },

                new PublisherEntity()
                {
                    Id = new Guid("d6224e00-2078-4243-aed5-7e31b76a9942").ToString(),
                    CompanyName = "MicrosoftStudio",
                    Description = "info",
                    HomePage = "https://www.microsoftstudios.com/",
                    IsSqlEntity = true
                });

            context.SaveChanges();

            context.Games.AddOrUpdate(
                new GameEntity()
                {
                    Id = new Guid("d6224e00-2078-4243-aed5-7e31b76a9901").ToString(),
                    Key = "AgeofEmpires",
                    Description = "bla-bla-bla",
                    IsDeleted = false,
                    PlatformTypes = new List<PlatformTypeEntity>() { context.PlatformTypes.First(x => x.TypeName == "Android"), context.PlatformTypes.First(x => x.TypeName == "iOS") },
                    Genres = new List<GenreEntity> { context.Genres.First(x => x.Name == "RTS"), context.Genres.First(x => x.Name == "Sports") },
                    Publisher = context.Publishers.First(x => x.CompanyName == "MicrosoftStudio"),
                    Price = 100,
                    IsSqlEntity = true
                },

                new GameEntity()
                {
                    Id = new Guid("d6224e00-2078-4243-aed5-7e31b76a9902").ToString(),
                    Key = "CompanyofHeros",
                    Description = "bla-bla-bla",
                    IsDeleted = false,
                    PlatformTypes = new List<PlatformTypeEntity>() { context.PlatformTypes.First(x => x.TypeName == "Android"), context.PlatformTypes.First(x => x.TypeName == "iOS") },
                    Genres = new List<GenreEntity> { context.Genres.First(x => x.Name == "RTS"), context.Genres.First(x => x.Name == "Races") },
                    Price = 120,
                    Publisher = context.Publishers.First(x => x.CompanyName == "MicrosoftStudio"),
                    PublishedDate = new DateTime(2017, 07, 22),
                    IsSqlEntity = true
                },

                new GameEntity()
                {
                    Id = new Guid("d6224e00-2078-4243-aed5-7e31b76a9903").ToString(),
                    Key = "TotalWar",
                    Description = "bla-bla-bla",
                    IsDeleted = false,
                    PlatformTypes = new List<PlatformTypeEntity>() { context.PlatformTypes.First(x => x.TypeName == "Android"), context.PlatformTypes.First(x => x.TypeName == "iOS") },
                    Genres = new List<GenreEntity> { context.Genres.First(x => x.Name == "Strategy"), context.Genres.First(x => x.Name == "RTS") },
                    Price = 400,
                    Publisher = context.Publishers.First(x => x.CompanyName == "Nale"),
                    PublishedDate = new DateTime(2016, 07, 22),
                    IsSqlEntity = true
                },

                new GameEntity()
                {
                    Id = new Guid("d6224e00-2078-4243-aed5-7e31b76a9904").ToString(),
                    Key = "FIFA17",
                    Description = "bla-bla-bla",
                    IsDeleted = false,
                    PlatformTypes = new List<PlatformTypeEntity>() { context.PlatformTypes.First(x => x.TypeName == "Android") },
                    Genres = new List<GenreEntity> { context.Genres.First(x => x.Name == "Strategy"), context.Genres.First(x => x.Name == "RTS") },
                    Price = 330,
                    Publisher = context.Publishers.First(x => x.CompanyName == "Nale"),
                    PublishedDate = new DateTime(2017, 06, 28),
                    IsSqlEntity = true
                },

                new GameEntity()
                {
                    Id = new Guid("d6224e00-2078-4243-aed5-7e31b76a9905").ToString(),
                    Key = "Superracing",
                    Description = "bla-bla-bla",
                    IsDeleted = false,
                    PlatformTypes = new List<PlatformTypeEntity>() { context.PlatformTypes.First(x => x.TypeName == "Android"), context.PlatformTypes.First(x => x.TypeName == "iOS") },
                    Genres = new List<GenreEntity> { context.Genres.First(x => x.Name == "Strategy") },
                    Price = 110,
                    Publisher = context.Publishers.First(x => x.CompanyName == "Nale"),
                    PublishedDate = new DateTime(2017, 07, 21),
                    IsSqlEntity = true

                }
                );
            context.SaveChanges();

            for (int i = 6; i < 100; i++)
            {
                context.Games.AddOrUpdate(
                    new GameEntity()
                    {
                        Id = new Guid("d6224e00-2078-4243-aed5-7e31b76a99" + i.ToString("D2")).ToString(),
                        Key = "Game" + i,
                        Description = "Game description",
                        IsDeleted = false,
                        PlatformTypes = new List<PlatformTypeEntity>() { context.PlatformTypes.AsEnumerable().First(x => x.TypeName == "Android"), context.PlatformTypes.AsEnumerable().First(x => x.TypeName == "iOS") },
                        Genres = new List<GenreEntity> { context.Genres.AsEnumerable().First(x => x.Name == "Strategy"), context.Genres.AsEnumerable().First(x => x.Name == "rally") },
                        Price = 110 + i,
                        Publisher = r.Next(1, 3) == 1 ? context.Publishers.First(x => x.CompanyName == "Nale") : context.Publishers.First(x => x.CompanyName == "MicrosoftStudio"),
                        PublishedDate = new DateTime(2017, r.Next(1, 12), 21),
                        UnitsInStock = (short)r.Next(1, 15),
                        IsSqlEntity = true
                    }
                    );
            }
            context.SaveChanges();

            context.Comments.AddOrUpdate(
                new CommentEntity()
                {
                    Id = new Guid("d6224e00-2078-4243-aed5-7e31b76a9901").ToString(),
                    GameId = context.Games.AsEnumerable().First(x => x.Key == "AgeofEmpires").Id,
                    Name = "Peter",
                    Body = "bla-bla-bla",
                    ParentCommentId = null,
                    IsDeleted = false,
                    IsSqlEntity = true
                });
            context.SaveChanges();
            context.Comments.AddOrUpdate(
                new CommentEntity()
                {
                    Id = new Guid("d6224e00-2078-4243-aed5-7e31b76a9902").ToString(),
                    GameId = context.Games.AsEnumerable().First(x => x.Key == "AgeofEmpires").Id,
                    Name = "Peter",
                    Body = "bla-bla-bla",
                    ParentCommentId = context.Comments.First().Id,
                    IsDeleted = false,
                    IsSqlEntity = true
                },
                new CommentEntity()
                {
                    Id = new Guid("d6224e00-2078-4243-aed5-7e31b76a9903").ToString(),
                    GameId = context.Games.AsEnumerable().First(x => x.Key == "AgeofEmpires").Id,
                    Name = "Peter",
                    Body = "bla-bla-bla",
                    ParentCommentId = context.Comments.First().Id,
                    IsDeleted = false,
                    IsSqlEntity = true
                }
                );

            context.GamesInfo.AddOrUpdate(
                new GameInfoEntity()
                {
                    Id = context.Games.ToList()[0].Id,
                    IsDeleted = false,
                    CountOfViews = 2,
                    UploadDate = DateTime.UtcNow.AddDays(-23),
                    IsSqlEntity = true
                },
                new GameInfoEntity()
                {
                    Id = context.Games.ToList()[1].Id,
                    IsDeleted = false,
                    CountOfViews = 2,
                    UploadDate = DateTime.UtcNow.AddDays(-23),
                    IsSqlEntity = true
                },
                new GameInfoEntity()
                {
                    Id = context.Games.ToList()[2].Id,
                    IsDeleted = false,
                    CountOfViews = 2,
                    UploadDate = DateTime.UtcNow.AddDays(-23),
                    IsSqlEntity = true
                },
                new GameInfoEntity()
                {
                    Id = context.Games.ToList()[3].Id,
                    IsDeleted = false,
                    CountOfViews = 2,
                    UploadDate = DateTime.UtcNow.AddDays(-23),
                    IsSqlEntity = true
                },
                new GameInfoEntity()
                {
                    Id = context.Games.ToList()[4].Id,
                    IsDeleted = false,
                    CountOfViews = 2,
                    UploadDate = DateTime.UtcNow.AddDays(-23),
                    IsSqlEntity = true
                });

            for (int i = 6; i < 100; i++)
            {
                context.GamesInfo.AddOrUpdate(
                    new GameInfoEntity()
                    {
                        Id = context.Games.ToList()[i - 1].Id,
                        IsDeleted = false,
                        CountOfViews = i + r.Next(15),
                        UploadDate = new DateTime(2017, r.Next(1, 7), 21),
                        IsSqlEntity = true
                    }
                );
            }
            context.SaveChanges();

            context.Users.AddOrUpdate(new UserEntity()
            {
                Id = "1",
                FirstName = "Ivan",
                LastName = "Ivanow",
                Login = "ivan",
                Password = "customer1",
                BirthDay = DateTime.UtcNow.AddYears(20),
                IsDeleted = false,
                IsSqlEntity = true
            });

            context.Orders.AddOrUpdate(
                new OrderEntity()
                {
                    Id = new Guid("d6224e00-2078-4243-aed5-7e31b76a9941").ToString(),
                    CustomerId = "1",
                    IsDeleted = false,
                    OrderDate = DateTime.UtcNow,
                    Status = GameStore.Domain.BusinessObjects.CompletionStatus.Complete,
                    IsSqlEntity = true
                });
            context.SaveChanges();
            context.OrderDetails.AddOrUpdate(
                new OrderDetailsEntity()
                {
                    Id = new Guid("d6224e00-2078-4243-aed5-7e31b76a9941").ToString(),
                    Discount = 10,
                    Price = 120,
                    Quantity = 2,
                    IsDeleted = false,
                    GameId = context.Games.First(x => x.Key == "AgeofEmpires").Id,
                    OrderId = context.Orders.First().Id,
                    IsSqlEntity = true
                });
        }
    }
}
