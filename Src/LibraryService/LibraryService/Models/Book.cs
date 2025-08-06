using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryService.Models
{
    public class Book
    {
        public int id { get; set; }
        public string? name { get; set; }
        public string? description { get; set; }
    }
}
