using GameStore.Domain.BusinessObjects;

namespace GameStore.Web.ViewModels
{
    public class RoleViewModel
    {
        public string Id { set; get; }
        public RoleEnum Role { set; get; }
        public bool IsChecked { set; get; }
    }
}