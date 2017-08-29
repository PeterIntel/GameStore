namespace GameStore.DataAccess.MSSQL.Entities
{
    public class CommentEntity : BasicEntity
    {
        public CommentEntity()
        {
            IsDisabled = false;
        }

        public string Name { set; get; }
        public string Body { set; get; }
        public string ParentCommentId { set; get; }
        public bool IsDisabled { set; get; }
        public string GameId { set; get; }
        public virtual GameEntity Game { set; get; }
    }
}
