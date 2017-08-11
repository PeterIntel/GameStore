using System;
using System.Collections.Generic;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.VisualStyles;
using GameStore.DataAccess.Mongo.MongoEntities;
using GameStore.Domain.BusinessObjects;
using MongoDB.Driver;

namespace GameStore.DataAccess.Mongo.DataProviders
{
    public static class DataProvider
    {
        private static GamesMongoContext _context = new GamesMongoContext();
        private static IMongoCollection<MongoCategoryEntity> _categories = _context.GetCollection<MongoCategoryEntity>();
        private static IMongoCollection<MongoSupplierEntity> _suppliers = _context.GetCollection<MongoSupplierEntity>();
        private static IMongoCollection<MongoOrderDetailsEntity> _orderDetails = _context.GetCollection<MongoOrderDetailsEntity>();
        private static IMongoCollection<MongoProductEntity> _products = _context.GetCollection<MongoProductEntity>();
        public static IQueryable<T> GetChildren<T>(this IQueryable<T> items)
        {
            if (typeof(T).Name == "MongoProductEntity")
            {
                var products = (IEnumerable<MongoProductEntity>)items;
                products = from a in products
                           join b in _categories.AsQueryable() on a.CategoryID equals b.CategoryID into categories
                           join supplier in _suppliers.AsQueryable() on a.SupplierID equals supplier.SupplierID
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
                               Supplier = supplier
                           };
                return (IQueryable<T>)products.AsQueryable();
            }
            if (typeof(T).Name == "MongoOrderEntity")
            {
                var orders = (IEnumerable<MongoOrderEntity>)items;

                orders = from order in orders
                         join orderDetails in _orderDetails.AsQueryable() on order.OrderID equals orderDetails.OrderID into
                         ordersDetails
                         select new MongoOrderEntity()
                         {
                             Id = order.Id,
                             CustomerID = order.CustomerID,
                             OrderID = order.OrderID,
                             OrderDate = order.OrderDate,
                             OrderDetails = from details in ordersDetails
                                            select new MongoOrderDetailsEntity()
                                            {
                                                Id = details.Id,
                                                ProductID = details.ProductID,
                                                OrderID = details.OrderID,
                                                Discount = details.Discount,
                                                UnitPrice = details.UnitPrice,
                                                Quantity = details.Quantity,
                                                Product = (from product in (IEnumerable<MongoProductEntity>)_products.AsQueryable()
                                                           where product.ProductID == details.ProductID
                                                           select product).SingleOrDefault()
                                            }
                         };
                return (IQueryable<T>)orders.AsQueryable();
            }
            return items;
        }
    }
}
