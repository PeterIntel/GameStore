using System.Runtime.InteropServices;
using GameStore.DataAccess.MSSQL.Entities;
using GameStore.DataAccess.MSSQL.Entities.Localization;
using GameStore.Domain.BusinessObjects;

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

            context.Cultures.AddOrUpdate(
                new CultureEntity() { Id = new Guid("d6224e00-2078-4243-aed5-7e31b76a9941").ToString(), IsDeleted = false, Title = "English", Code = "en" },
                new CultureEntity() { Id = new Guid("d6224e00-2078-4243-aed5-7e31b76a9942").ToString(), IsDeleted = false, Title = "Руский", Code = "ru" }
                );

            context.SaveChanges();

            context.PlatformTypes.AddOrUpdate(
                    new PlatformTypeEntity()
                    {
                        Id = new Guid("d6224e00-2078-4243-aed5-7e31b76a9941").ToString(),
                        IsDeleted = false,
                        IsSqlEntity = true,
                        Locals = new List<PlatformTypeLocalEntity>()
                        {
                            new PlatformTypeLocalEntity()
                            {
                                Id = Guid.NewGuid().ToString(),
                                TypeName = "Android",
                                Culture = context.Cultures.First(c=>c.Code == "en")
                            }
                        }
                    },
                    new PlatformTypeEntity()
                    {
                        Id = new Guid("d6224e00-2078-4243-aed5-7e31b76a9942").ToString(),
                        IsDeleted = false,
                        IsSqlEntity = true,
                        Locals = new List<PlatformTypeLocalEntity>()
                        {
                            new PlatformTypeLocalEntity()
                            {
                                Id = Guid.NewGuid().ToString(),
                                TypeName = "iOS",
                                Culture = context.Cultures.First(c=>c.Code == "en")
                            }
                        }
                    }
                );

            context.SaveChanges();

            context.Genres.AddOrUpdate(
                new GenreEntity()
                {
                    Id = new Guid("d6224e00-2078-4243-aed5-7e31b76a9941").ToString(),
                    ParentGenreId = null,
                    IsDeleted = false,
                    IsSqlEntity = true,
                    Locals = new List<GenreLocalEntity>()
                    {
                        new GenreLocalEntity()
                        {
                            Id = Guid.NewGuid().ToString(),
                            Name = "Strategy",
                            Culture = context.Cultures.First(c=>c.Code == "en")
                        },
                        new GenreLocalEntity()
                        {
                            Id = Guid.NewGuid().ToString(),
                            Name = "Стратегия",
                            Culture = context.Cultures.First(c=>c.Code == "ru")
                        }
                    }
                },
                new GenreEntity()
                {
                    Id = new Guid("d6224e00-2078-4243-aed5-7e31b76a9942").ToString(),
                    ParentGenreId = null,
                    IsDeleted = false,
                    IsSqlEntity = true,
                    Locals = new List<GenreLocalEntity>()
                    {
                        new GenreLocalEntity()
                        {
                            Id = Guid.NewGuid().ToString(),
                            Name = "RPG",
                            Culture = context.Cultures.First(c=>c.Code == "en")
                        }
                    }
                },
                new GenreEntity()
                {
                    Id = new Guid("d6224e00-2078-4243-aed5-7e31b76a9943").ToString(),
                    ParentGenreId = null,
                    IsDeleted = false,
                    IsSqlEntity = true,
                    Locals = new List<GenreLocalEntity>()
                    {
                        new GenreLocalEntity()
                        {
                            Id = Guid.NewGuid().ToString(),
                            Name = "Sports",
                            Culture = context.Cultures.First(c=>c.Code == "en")
                        }
                    }
                },
                new GenreEntity()
                {
                    Id = new Guid("d6224e00-2078-4243-aed5-7e31b76a9944").ToString(),
                    ParentGenreId = null,
                    IsDeleted = false,
                    IsSqlEntity = true,
                    Locals = new List<GenreLocalEntity>()
                    {
                        new GenreLocalEntity()
                        {
                            Id = Guid.NewGuid().ToString(),
                            Name = "Races",
                            Culture = context.Cultures.First(c=>c.Code == "en")
                        }
                    }
                },
                new GenreEntity()
                {
                    Id = new Guid("d6224e00-2078-4243-aed5-7e31b76a9945").ToString(),
                    ParentGenreId = null,
                    IsDeleted = false,
                    IsSqlEntity = true,
                    Locals = new List<GenreLocalEntity>()
                    {
                        new GenreLocalEntity()
                        {
                            Id = Guid.NewGuid().ToString(),
                            Name = "RTS",
                            Culture = context.Cultures.First(c=>c.Code == "en")
                        },
                        new GenreLocalEntity()
                        {
                            Id = Guid.NewGuid().ToString(),
                            Name = "РТС",
                            Culture = context.Cultures.First(c=>c.Code == "ru")
                        }
                    }
                },
                new GenreEntity()
                {
                    Id = new Guid("d6224e00-2078-4243-aed5-7e31b76a9946").ToString(),
                    ParentGenreId = null,
                    IsDeleted = false,
                    IsSqlEntity = true,
                    Locals = new List<GenreLocalEntity>()
                    {
                        new GenreLocalEntity()
                        {
                            Id = Guid.NewGuid().ToString(),
                            Name = "rally",
                            Culture = context.Cultures.First(c=>c.Code == "en")
                        },
                        new GenreLocalEntity()
                        {
                            Id = Guid.NewGuid().ToString(),
                            Name = "ралли",
                            Culture = context.Cultures.First(c=>c.Code == "ru")
                        }
                    }
                },
                new GenreEntity()
                {
                    Id = new Guid("d6224e00-2078-4243-aed5-7e31b76a9978").ToString(),
                    ParentGenreId = null,
                    IsDeleted = false,
                    IsSqlEntity = true,
                    Locals = new List<GenreLocalEntity>()
                    {
                        new GenreLocalEntity()
                        {
                            Id = Guid.NewGuid().ToString(),
                            Name = "Other",
                            Culture = context.Cultures.First(c=>c.Code == "en")
                        }
                    }
                }
                );

            context.SaveChanges();

            context.Publishers.AddOrUpdate(
                new PublisherEntity()
                {
                    Id = new Guid("d6224e00-2078-4243-aed5-7e31b76a9941").ToString(),
                    CompanyName = "Nale",
                    HomePage = "http://www.vk.com",
                    IsSqlEntity = true,
                    Locals = new List<PublisherLocalEntity>()
                    {
                        new PublisherLocalEntity()
                        {
                            Id = Guid.NewGuid().ToString(),
                            Description = "info",
                            Culture = context.Cultures.First(c=>c.Code == "en")
                        }
                    }
                },

                new PublisherEntity()
                {
                    Id = new Guid("d6224e00-2078-4243-aed5-7e31b76a9942").ToString(),
                    CompanyName = "MicrosoftStudio",
                    HomePage = "https://www.microsoftstudios.com/",
                    IsSqlEntity = true,
                    Locals = new List<PublisherLocalEntity>()
                    {
                        new PublisherLocalEntity()
                        {
                            Id = Guid.NewGuid().ToString(),
                            Description = "info",
                            Culture = context.Cultures.First(c=>c.Code == "en")
                        }
                    }
                });

            context.SaveChanges();

            context.Games.AddOrUpdate(
                new GameEntity()
                {
                    Id = new Guid("d6224e00-2078-4243-aed5-7e31b76a9901").ToString(),
                    Key = "AgeofEmpires",
                    IsDeleted = false,
                    PlatformTypes = new List<PlatformTypeEntity>() { context.PlatformTypes.First(x => x.Locals.Any(y => y.TypeName == "Android")), context.PlatformTypes.First(x => x.Locals.Any(y => y.TypeName == "iOS")) },
                    Genres = new List<GenreEntity> { context.Genres.First(x => x.Locals.Any(y => y.Name == "RTS")), context.Genres.First(x => x.Locals.Any(y => y.Name == "Sports")) },
                    Publisher = context.Publishers.First(x => x.CompanyName == "MicrosoftStudio"),
                    Price = 100,
                    IsSqlEntity = true,
                    Locals = new List<GameLocalEntity>()
                    {
                        new GameLocalEntity()
                        {
                            Id  = Guid.NewGuid().ToString(),
                            Description = "bla-bla-bla",
                            Culture = context.Cultures.First(c=>c.Code == "en")
                        }
                    }
                },

                new GameEntity()
                {
                    Id = new Guid("d6224e00-2078-4243-aed5-7e31b76a9902").ToString(),
                    Key = "CompanyofHeros",
                    IsDeleted = false,
                    PlatformTypes = new List<PlatformTypeEntity>() { context.PlatformTypes.First(x => x.Locals.Any(y => y.TypeName == "Android")), context.PlatformTypes.First(x => x.Locals.Any(y => y.TypeName == "iOS")) },
                    Genres = new List<GenreEntity> { context.Genres.First(x => x.Locals.Any(y => y.Name == "RTS")), context.Genres.First(x => x.Locals.Any(y => y.Name == "Races")) },
                    Price = 120,
                    Publisher = context.Publishers.First(x => x.CompanyName == "MicrosoftStudio"),
                    PublishedDate = new DateTime(2017, 07, 22),
                    IsSqlEntity = true,
                    Locals = new List<GameLocalEntity>()
                    {
                        new GameLocalEntity()
                        {
                            Id  = Guid.NewGuid().ToString(),
                            Description = "bla-bla-bla",
                            Culture = context.Cultures.First(c=>c.Code == "en")
                        }
                    }
                },

                new GameEntity()
                {
                    Id = new Guid("d6224e00-2078-4243-aed5-7e31b76a9903").ToString(),
                    Key = "TotalWar",
                    IsDeleted = false,
                    PlatformTypes = new List<PlatformTypeEntity>() { context.PlatformTypes.First(x => x.Locals.Any(y => y.TypeName == "Android")), context.PlatformTypes.First(x => x.Locals.Any(y => y.TypeName == "iOS")) },
                    Genres = new List<GenreEntity> { context.Genres.First(x => x.Locals.Any(y => y.Name == "Strategy")), context.Genres.First(x => x.Locals.Any(y => y.Name == "RTS")) },
                    Price = 400,
                    Publisher = context.Publishers.First(x => x.CompanyName == "Nale"),
                    PublishedDate = new DateTime(2016, 07, 22),
                    IsSqlEntity = true,
                    Locals = new List<GameLocalEntity>()
                    {
                        new GameLocalEntity()
                        {
                            Id  = Guid.NewGuid().ToString(),
                            Description = "bla-bla-bla",
                            Culture = context.Cultures.First(c=>c.Code == "en")
                        }
                    }
                },

                new GameEntity()
                {
                    Id = new Guid("d6224e00-2078-4243-aed5-7e31b76a9904").ToString(),
                    Key = "FIFA17",
                    IsDeleted = false,
                    PlatformTypes = new List<PlatformTypeEntity>() { context.PlatformTypes.First(x => x.Locals.Any(y => y.TypeName == "Android")) },
                    Genres = new List<GenreEntity> { context.Genres.First(x => x.Locals.Any(y => y.Name == "Strategy")), context.Genres.First(x => x.Locals.Any(y => y.Name == "RTS")) },
                    Price = 330,
                    Publisher = context.Publishers.First(x => x.CompanyName == "Nale"),
                    PublishedDate = new DateTime(2017, 06, 28),
                    IsSqlEntity = true,
                    Locals = new List<GameLocalEntity>()
                    {
                        new GameLocalEntity()
                        {
                            Id  = Guid.NewGuid().ToString(),
                            Description = "bla-bla-bla",
                            Culture = context.Cultures.First(c=>c.Code == "en")
                        }
                    }
                },

                new GameEntity()
                {
                    Id = new Guid("d6224e00-2078-4243-aed5-7e31b76a9905").ToString(),
                    Key = "Superracing",
                    IsDeleted = false,
                    PlatformTypes = new List<PlatformTypeEntity>() { context.PlatformTypes.First(x => x.Locals.Any(y => y.TypeName == "Android")), context.PlatformTypes.First(x => x.Locals.Any(y => y.TypeName == "iOS")) },
                    Genres = new List<GenreEntity> { context.Genres.First(x => x.Locals.Any(y => y.Name == "Strategy")) },
                    Price = 110,
                    Publisher = context.Publishers.First(x => x.CompanyName == "Nale"),
                    PublishedDate = new DateTime(2017, 07, 21),
                    IsSqlEntity = true,
                    Locals = new List<GameLocalEntity>()
                    {
                        new GameLocalEntity()
                        {
                            Id  = Guid.NewGuid().ToString(),
                            Description = "bla-bla-bla",
                            Culture = context.Cultures.First(c=>c.Code == "en")
                        }
                    }

                }
            );
            context.SaveChanges();

            for (int i = 6; i < 100; i++)
            {
                context.Games.AddOrUpdate(
                    new GameEntity()
                    {
                        Id = i.ToString(),
                        Key = "Game" + i,
                        IsDeleted = false,
                        PlatformTypes = new List<PlatformTypeEntity>() { context.PlatformTypes.AsEnumerable().First(x => x.Locals.Any(y => y.TypeName == "Android")), context.PlatformTypes.AsEnumerable().First(x => x.Locals.Any(y => y.TypeName == "iOS")) },
                        Genres = new List<GenreEntity> { context.Genres.AsEnumerable().First(x => x.Locals.Any(y => y.Name == "Strategy")), context.Genres.AsEnumerable().First(x => x.Locals.Any(y => y.Name == "rally")) },
                        Price = 110 + i,
                        Publisher = r.Next(1, 3) == 1
                            ? context.Publishers.First(x => x.CompanyName == "Nale")
                            : context.Publishers.First(x => x.CompanyName == "MicrosoftStudio"),
                        PublishedDate = new DateTime(2017, r.Next(1, 12), 21),
                        UnitsInStock = (short)r.Next(1, 15),
                        IsSqlEntity = true,
                        Locals = new List<GameLocalEntity>()
                        {
                            new GameLocalEntity()
                            {
                                Id  = Guid.NewGuid().ToString(),
                                Description = "game description",
                                Culture = context.Cultures.First(c=>c.Code == "en")
                            }
                        }
                    }
                );
                context.SaveChanges();
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
                    IsSqlEntity = true,
                    IsDisabled = false
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
                    IsSqlEntity = true,
                    IsDisabled = false
                },
                new CommentEntity()
                {
                    Id = new Guid("d6224e00-2078-4243-aed5-7e31b76a9903").ToString(),
                    GameId = context.Games.AsEnumerable().First(x => x.Key == "AgeofEmpires").Id,
                    Name = "Peter",
                    Body = "bla-bla-bla",
                    ParentCommentId = context.Comments.First().Id,
                    IsDeleted = false,
                    IsSqlEntity = true,
                    IsDisabled = false
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

            context.Roles.AddOrUpdate(
                new RoleEntity()
                {
                    Id = "1",
                    IsDeleted = false,
                    IsSqlEntity = true,
                    Role = RoleEnum.User
                },
                new RoleEntity()
                {
                    Id = "2",
                    IsDeleted = false,
                    IsSqlEntity = true,
                    Role = RoleEnum.Moderator
                },
                new RoleEntity()
                {
                    Id = "3",
                    IsDeleted = false,
                    IsSqlEntity = true,
                    Role = RoleEnum.Manager
                },
                new RoleEntity()
                {
                    Id = "5",
                    IsDeleted = false,
                    IsSqlEntity = true,
                    Role = RoleEnum.Publisher
                },
                new RoleEntity()
                {
                    Id = "4",
                    IsDeleted = false,
                    IsSqlEntity = true,
                    Role = RoleEnum.Administrator
                });


            context.SaveChanges();

            context.Users.AddOrUpdate(
                new UserEntity()
                {
                    Id = "1",
                    FirstName = "Ivan",
                    LastName = "Ivanow",
                    Login = "admin",
                    Email = "admin@ukr.net",
                    Password = "111111",
                    BirthDay = DateTime.UtcNow.AddYears(20),
                    IsDeleted = false,
                    IsSqlEntity = true,
                    Roles = new List<RoleEntity>()
                    {
                        context.Roles.First(x => x.Id == "4")
                    }
                },
                new UserEntity()
                {
                    Id = "2",
                    FirstName = "Ivan",
                    LastName = "Ivanow",
                    Login = "moderator",
                    Email = "moderator@ukr.net",
                    Password = "111111",
                    BirthDay = DateTime.UtcNow.AddYears(20),
                    IsDeleted = false,
                    IsSqlEntity = true,
                    Roles = new List<RoleEntity>()
                    {
                        context.Roles.First(x => x.Id == "2")
                    }
                },
                new UserEntity()
                {
                    Id = "3",
                    FirstName = "Ivan",
                    LastName = "Ivanow",
                    Login = "user",
                    Email = "user@ukr.net",
                    Password = "111111",
                    BirthDay = DateTime.UtcNow.AddYears(20),
                    IsDeleted = false,
                    IsSqlEntity = true,
                    Roles = new List<RoleEntity>()
                    {
                        context.Roles.First(x => x.Id == "1")
                    }
                },
                new UserEntity()
                {
                    Id = "4",
                    FirstName = "Ivan",
                    LastName = "Ivanow",
                    Login = "manager",
                    Email = "manager@ukr.net",
                    Password = "111111",
                    BirthDay = DateTime.UtcNow.AddYears(20),
                    IsDeleted = false,
                    IsSqlEntity = true,
                    Roles = new List<RoleEntity>()
                    {
                        context.Roles.First(x => x.Id == "3")
                    }
                },
                new UserEntity()
                {
                    Id = "5",
                    FirstName = "Ivan",
                    LastName = "Ivanow",
                    Login = "publisher",
                    Email = "publisher@ukr.net",
                    Password = "111111",
                    BirthDay = DateTime.UtcNow.AddYears(20),
                    IsDeleted = false,
                    IsSqlEntity = true,
                    Roles = new List<RoleEntity>()
                    {
                        context.Roles.First(x => x.Id == "5")
                    }
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
