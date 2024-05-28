using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace NewsApp.Service
{
    // 管理并解析用户的搜索结果
    public class SearchLineManagerService
    {
        // 使用正则表达式正确匹配域名
        private readonly string DOMAIN_PATTERN = @"https?://[^\s/$.?#].[^\s]*";

        public bool IsSearchContainsDomain(string search)
        {
            return Regex.IsMatch(search, DOMAIN_PATTERN, RegexOptions.IgnoreCase);
        }

        // 解析搜索字符串
        public Dictionary<string, List<string>> ParseSearchQuery(string searchLine)
        {
            // 按行分割
            var splitted = searchLine.Split('\n').ToList();

            for (int i = 0; i < splitted.Count; i++)
            {
                splitted[i] = splitted[i].TrimEnd('\r'); // 去掉末尾回车符
            }

            // 初始化结果字典
            var dictionaryQuery = new Dictionary<string, List<string>>()
            {
                {"Domain", new List<string>() }, // 域名
                {"KeyWord", new List<string>()}  // 关键字
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