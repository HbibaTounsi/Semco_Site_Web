using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SiteWeb.Application.Common
{
    public static class SlugHelper
    {
        public static string GenerateSlug(string phrase)
        {
            if (string.IsNullOrWhiteSpace(phrase))
                return string.Empty;

            // Normalize text (remove accents)
            string normalized = phrase.Normalize(NormalizationForm.FormD);
            var sb = new StringBuilder();

            foreach (char c in normalized)
            {
                UnicodeCategory uc = CharUnicodeInfo.GetUnicodeCategory(c);
                if (uc != UnicodeCategory.NonSpacingMark)
                {
                    sb.Append(c);
                }
            }

            string str = sb.ToString().Normalize(NormalizationForm.FormC).ToLowerInvariant();

            // Replace µ with micron
            str = str.Replace("µ", "microns");

            // Invalid chars to hyphens
            str = Regex.Replace(str, @"[^a-z0-9\s-]", "");
            // Multiple spaces/hyphens into single hyphen
            str = Regex.Replace(str, @"[\s-]+", " ").Trim();
            str = Regex.Replace(str, @"\s", "-");

            return str;
        }
    }
}
