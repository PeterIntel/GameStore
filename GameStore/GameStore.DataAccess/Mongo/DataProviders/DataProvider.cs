using System.Collections.Generic;
using System.Linq;
using GameStore.DataAccess.Mongo.MongoEntities;
using MongoDB.Driver;

namespace GameStore.DataAccess.Mongo.DataProviders
{
    public static class DataProvider
    {
        private static readonly GamesMongoContext Context = new GamesMongoContext();
        private static readonly IMongoCollection<MongoCategoryEntity> Categories = Context.GetCollection<MongoCategoryEntity>();
        private static readonly IMongoCollection<MongoSupplierEntity> Suppliers = Context.GetCollection<MongoSupplierEntity>();
        private static readonly IMongoCollection<MongoOrderDetailsEntity> OrderDetails = Context.GetCollection<MongoOrderDetailsEntity>();
        private static readonly IMongoCollection<MongoProductEntity> Products = Context.GetCollection<MongoProductEntity>();

        /// <summary>
        /// The function gets categiries and supplier for all products
        /// </summary>
        /// <param name="products"></param>
        /// <returns></returns>
        public static IQueryable<MongoProductEntity> GetNestedEntities(this IQueryable<MongoProductEntity> products)
        {
                var newProducts = from a in (IEnumerable<MongoProductEntity>) products
                    join b in Categories.AsQueryable() on a.CategoryID equals b.CategoryID into categories
                    join supplier in Suppliers.AsQueryable() on a.SupplierID equals supplier.SupplierID
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
                return (IQueryable<MongoProductEntity>) newProducts.AsQueryable();
        }
        
        /// <summary>
        /// The function gets order details for each order and product for each order details
        /// </summary>
        /// <param name="orders"></param>
        /// <returns></returns>
        public static IQueryable<MongoOrderEntity> GetNestedEntities(this IQueryable<MongoOrderEntity> orders)
        {
            var newOrders = from order in (IEnumerable<MongoOrderEntity>)orders
                     join orderDetails in OrderDetails.AsQueryable() on order.OrderID equals orderDetails.OrderID into
                     ordersDetails
                            select new MongoOrderEntity()
                     {
                         Id = order.Id,
                         CustomerID = order.CustomerID,
                         OrderID = order.OrderID,
                         OrderDate = order.OrderDate,
                         OrderDetails = 
                         from details in ordersDetails
                                        select new MongoOrderDetailsEntity()
                                        {
                                            Id = details.Id,
                                            ProductID = details.ProductID,
                                            OrderID = details.OrderID,
                                            Discount = details.Discount,
                                            UnitPrice = details.UnitPrice,
                                            Quantity = details.Quantity,
                                            Product = Products.Find(Builders<MongoProductEntity>.Filter.Eq("ProductID", details.ProductID)).FirstOrDefault()
                                        }
                     };

            return (IQueryable<MongoOrderEntity>)newOrders.AsQueryable();
        }


    }
}

