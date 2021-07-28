using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository.Interfaces
{
    public interface IBookRL
    {
        ResponseBook AddBook(ResponseBook book);
        ICollection<ResponseBook> GetUserBooks();
        bool DeleteBook(long bookID);
        ResponseBook UpdateBook(long BookID, ResponseBook book);
        ICollection<ResponseBook> GetPriceSortBooks(long ID, bool sort);


    }
}
