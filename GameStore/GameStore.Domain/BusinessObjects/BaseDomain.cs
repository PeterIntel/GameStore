namespace GameStore.Domain.BusinessObjects
{
    public class BasicDomain
    {
        public string Id { set; get; }

        public bool IsSqlEntity { set; get; }

        public bool IsDeleted { set; get; }
    }
}
