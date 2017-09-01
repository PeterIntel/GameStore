using GameStore.DataAccess.MSSQL;

namespace GameStore.DataAccess.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly GamesSqlContext _context;

         public UnitOfWork(GamesSqlContext context)
        {
            _context = context;
        }
        
        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
