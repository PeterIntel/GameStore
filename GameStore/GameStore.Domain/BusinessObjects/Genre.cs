using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace GameStore.Domain.BusinessObjects
{
    public class Genre
    {
        public string Id { set; get; }
        public string Name { set; get; }
        public string ParentGenreId { set; get; }
        public bool IsChecked { set; get; }
        public IEnumerable<Genre> Genres { set; get; }
    }
}
