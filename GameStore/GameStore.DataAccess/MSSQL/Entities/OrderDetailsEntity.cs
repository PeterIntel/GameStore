namespace GameStore.DataAccess.MSSQL.Entities
{
    public class OrderDetailsEntity : BasicEntity
    {
        public string GameId { set; get; }

        public string OrderId { set; get; }

        public decimal Price { set; get; }

        public short Quantity { set; get; }

        public double Discount { set; get; }

        public virtual GameEntity Game { set; get; }

        public virtual OrderEntity Order { set; get; }
    }
}
