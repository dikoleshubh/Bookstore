using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository.Interfaces
{
   public interface ICartRL
    {
        public ICollection<UserCart> AddBookToCart(long ID, long BookID);





    }
}
