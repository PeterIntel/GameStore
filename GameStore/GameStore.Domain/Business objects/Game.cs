using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Web.Script.Serialization;

namespace GameStore.Domain.Business_objects
{
    public class Game : BasicDomainEntity
    {
        public int Id { set; get; }

        [Index(IsUnique = true)]
        [StringLength(450)]
        public string Key { set; get; }
        public string Description { set; get; }

        [ScriptIgnore(ApplyToOverrides = true)]
        public IList<Comment> Comments { set; get; }

        [ScriptIgnore(ApplyToOverrides = true)]
        public virtual IList<Genre> Genres { set; get; }

        [ScriptIgnore(ApplyToOverrides = true)]
        public virtual IList<PlatformType> PlatformTypes { set; get; }
    }
}
