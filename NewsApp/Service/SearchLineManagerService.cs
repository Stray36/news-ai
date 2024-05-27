using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace NewsApp.Service
{
    public class SearchLineManagerService
    {
        // Patterns for regular expressions
        private readonly string DOMAIN_PATTERN = @"https?://[^\s/$.?#].[^\s]*";

        public bool IsSearchContainsDomain(string search)
        {
            return Regex.IsMatch(search, DOMAIN_PATTERN, RegexOptions.IgnoreCase);
        }

        public Dictionary<string, List<string>> ParseSearchQuery(string searchLine)
        {
            var splitted = searchLine.Split('\n').ToList();

            for (int i = 0; i < splitted.Count; i++)
            {
                splitted[i] = splitted[i].TrimEnd('\r');
            }

            var dictionaryQuery = new Dictionary<string, List<string>>()
            {
                {"Domain", new List<string>() },
                {"KeyWord", new List<string>()}
            };

            foreach (var line in splitted)
            {
                if (IsSearchContainsDomain(line))
                {
                    dictionaryQuery["Domain"].Add(line);
                }
                else
                {
                    dictionaryQuery["KeyWord"].Add(line);
                }
            }

            return dictionaryQuery;
        }
    }
}