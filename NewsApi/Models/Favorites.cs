namespace NewsApi.Models
{
    //收藏新闻的数据结构
    public class FavoriteNews
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Summary { get; set; }
        public DateTime DateAdded { get; set; }
    }
}

