using BookLending.Application.Dtos;
using BookLending.Domain.Models;
using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookLending.Application.MapSter
{
    public static class MappBorrowing
    {

        public static void MapBorrow()
        {
            TypeAdapterConfig<CreateBrowingDto,Borrowing>.NewConfig();
            TypeAdapterConfig<ReturnBook,Borrowing >.NewConfig();
            
        }
    }
}
