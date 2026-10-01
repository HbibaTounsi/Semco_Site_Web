using SiteWeb.Core.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiteWeb.Core.Entities
{
    public class Product : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public string Slug { get; set; } = string.Empty;
        public string Reference { get; set; } = string.Empty;
        public string ShortDescription { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string? ImageUrl { get; set; }
        public string? Dimensions { get; set; }
        public string? Color { get; set; }
        public string? Material { get; set; }
        public string? Specifications { get; set; }
        public bool IsFeatured { get; set; } = false;

        public int CategoryId { get; set; }
        public virtual Category Category { get; set; } = null!;
    }
}
