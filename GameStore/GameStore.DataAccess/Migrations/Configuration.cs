namespace GameStore.DataAccess.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using GameStore.DataAccess.Entities;
    using System.Collections.Generic;

    public sealed class Configuration : DbMigrationsConfiguration<GameStore.DataAccess.GamesContext>
    {
        public Configuration()
        {

        }

        protected override void Seed(GameStore.DataAccess.GamesContext context)
        {
            //  This method will be called after migrating to the latest version.

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

            context.Games.AddOrUpdate(
                new GameEntity()
                {
                    Id = 1,
                    Key = "Age of Empires",
                    Description = "bla-bla-bla",
                    IsDeleted = false,
                    PlatformTypes = new List<PlatformTypeEntity>() { context.PlatformTypes.Where(x => x.Id == 1).First(), context.PlatformTypes.Where(x => x.Id == 2).First() },
                    Genres = new List<GenreEntity> { context.Genres.Where(x => x.Id == 5).First(), context.Genres.Where(x => x.Id == 7).First() }
                },

                new GameEntity()
                {
                    Id = 2,
                    Key = "Company of Heros",
                    Description = "bla-bla-bla",
                    IsDeleted = false,
                    PlatformTypes = new List<PlatformTypeEntity>() { context.PlatformTypes.Where(x => x.Id == 1).First(), context.PlatformTypes.Where(x => x.Id == 2).First() },
                    Genres = new List<GenreEntity> { context.Genres.Where(x => x.Id == 5).First(), context.Genres.Where(x => x.Id == 7).First() }
                },

                new GameEntity()
                {
                    Id = 3,
                    Key = "Total War",
                    Description = "bla-bla-bla",
                    IsDeleted = false,
                    PlatformTypes = new List<PlatformTypeEntity>() { context.PlatformTypes.Where(x => x.Id == 1).First(), context.PlatformTypes.Where(x => x.Id == 2).First() },
                    Genres = new List<GenreEntity> { context.Genres.Where(x => x.Id == 5).First(), context.Genres.Where(x => x.Id == 6).First() }
                },

                new GameEntity()
                {
                    Id = 4,
                    Key = "FIFA 17",
                    Description = "bla-bla-bla",
                    IsDeleted = false,
                    PlatformTypes = new List<PlatformTypeEntity>() { context.PlatformTypes.Where(x => x.Id == 1).First(), context.PlatformTypes.Where(x => x.Id == 2).First() },
                    Genres = new List<GenreEntity> { context.Genres.Where(x => x.Id == 2).First(), context.Genres.Where(x => x.Id == 3).First() }
                },

                new GameEntity()
                {
                    Id = 5,
                    Key = "Super racing",
                    Description = "bla-bla-bla",
                    IsDeleted = false,
                    PlatformTypes = new List<PlatformTypeEntity>() { context.PlatformTypes.Where(x => x.Id == 1).First() },
                    Genres = new List<GenreEntity> { context.Genres.Where(x => x.Id == 8).First() }
                }
                );

            context.Comments.AddOrUpdate(
                new CommentEntity() { Id = 1, GameId = 1, Name = "Peter", Body = "bla-bla-bla", ParentCommentId = null, IsDeleted = true },
                new CommentEntity() { Id = 2, GameId = 1, Name = "Peter", Body = "bla-bla-bla", ParentCommentId = 1, IsDeleted = true },
                new CommentEntity() { Id = 3, GameId = 1, Name = "Peter", Body = "bla-bla-bla", ParentCommentId = 2, IsDeleted = true }
                );
        }
    }
}
