using System;
using System.Collections.Generic;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.VisualStyles;
using GameStore.DataAccess.Mongo.MongoEntities;
using MongoDB.Driver;

namespace GameStore.DataAccess.Mongo.DataProviders
{
    public static class DataProvider
    {
        private static GamesMongoContext _context = new GamesMongoContext();
        private static IMongoCollection<MongoCategoryEntity> _categories = _context.GetCollection<MongoCategoryEntity>();
        private static IMongoCollection<MongoSupplierEntity> _suppliers = _context.GetCollection<MongoSupplierEntity>();
        public static IQueryable<T> GetChildren<T>(this IQueryable<T> products)
        {
            if (typeof(T).Name == "MongoProductEntity")
            {
                var prod = (IEnumerable<MongoProductEntity>)products;
                prod = from a in prod
                    join b in _categories.AsQueryable() on a.CategoryID equals b.CategoryID into categories
                    join c in _suppliers.AsQueryable() on a.SupplierID equals c.SupplierID
                    select new MongoProductEntity()
                    {
                        Id = a.Id,
                        CategoryID = a.CategoryID,
                        ProductID = a.ProductID,
                        SupplierID = a.SupplierID,
                        Discontinued = a.Discontinued,
                        UnitsInStock = a.UnitsInStock,
                        ProductName = a.ProductName,
                        UnitPrice = a.UnitPrice,
                        Categories = categories,
                        Supplier = new MongoSupplierEntity()
                        {
                            Id = c.Id,
                            SupplierID = c.SupplierID,
                            CompanyName = c.CompanyName,
                            HomePage = c.HomePage
                        }
                    };
                return (IQueryable<T>)prod.AsQueryable();
            }
            return products;
        }
    }
}
