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
        private static IMongoCollection<MongoCategory> _categories = _context.GetCollection<MongoCategory>();
        private static IMongoCollection<MongoSupplier> _suppliers = _context.GetCollection<MongoSupplier>();
        public static IQueryable<T> GetChildren<T>(this IQueryable<T> products)
        {
            if (typeof(T).Name == "MongoProduct")
            {
                var prod = (IEnumerable<MongoProduct>)products;
                prod = from a in prod
                    join b in _categories.AsQueryable() on a.CategoryID equals b.CategoryID
                    join c in _suppliers.AsQueryable() on a.SupplierID equals c.SupplierID
                    select new MongoProduct()
                    {
                        Id = a.Id,
                        CategoryID = a.CategoryID,
                        ProductID = a.ProductID,
                        SupplierID = a.SupplierID,
                        Discontinued = a.Discontinued,
                        UnitsInStock = a.UnitsInStock,
                        ProductName = a.ProductName,
                        UnitPrice = a.UnitPrice,
                        Category = new MongoCategory()
                        {
                            Id = b.Id,
                            CategoryID = b.CategoryID,
                            CategoryName = b.CategoryName
                        },
                        Supplier = new MongoSupplier()
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
