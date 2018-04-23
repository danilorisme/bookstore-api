using System;
namespace BookStore.Models
{
    public class Book
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ISBN { get; set; }
        public string Publisher { get; set; }
        public int Pages { get; set; }
        public string Author { get; set; }
        public string Cover { get; set; }
    }
}
