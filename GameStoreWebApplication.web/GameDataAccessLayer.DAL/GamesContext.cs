using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Security.Cryptography.X509Certificates;
using DomainLayer.contracts.DomainModels;

namespace GameDataAccessLayer.DAL
{
    class GamesContext : DbContext
    {
        public GamesContext() : base("GamesContext")
        {

        }

        public virtual DbSet<Comment> Comments { set; get; }
        public virtual DbSet<Game> Games { set; get; }
        public virtual DbSet<Genre> Genres { set; get; }
        public virtual DbSet<PlatformType> PlatformTypes { set; get; }
    }
}
