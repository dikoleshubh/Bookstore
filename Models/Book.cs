using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Models
{

    public class BookModel
    {
        [Required]
        public string BookName { get; set; }
        //public long BookID { get; set; }
        public string BookDescription { get; set; } = "";
        [Required]
        [Range(1, 1000000000, ErrorMessage = "Book Price must be between 1 to 100000")]
        public int BookPrice { get; set; }
        [Required]
        public string AuthorName { get; set; }
        [Required]
        [Range(0, 1000000000, ErrorMessage = "Quantity must be between 0 to 10000000")]
        public int Quantity { get; set; }
    }













}
