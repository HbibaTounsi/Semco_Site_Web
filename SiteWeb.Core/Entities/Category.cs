using SiteWeb.Core.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiteWeb.Core.Entities
{
    public class Category : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public string Slug { get; set; } = string.Empty;
        public string? Description { get; set; }
        public string? ImageUrl { get; set; }
        public int DisplayOrder { get; set; } = 0;

        // Navigation property
        public virtual ICollection<Product> Products { get; set; } = new List<Product>();
    }
}
