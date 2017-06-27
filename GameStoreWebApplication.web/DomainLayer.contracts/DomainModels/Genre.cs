using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DomainLayer.contracts.DomainModels
{
    public class Genre : BasicEntity 
    {
        public int Id { set; get; }

        [Index(IsUnique = true)]
        [StringLength(450)]
        public string Name { set; get; }
        public int? ParentGenreId { set; get; }
        public virtual IList<Genre> Genres { set; get; }
        public virtual IList<Game> Games { set; get; }
    }
}
