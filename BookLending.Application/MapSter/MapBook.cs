using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookLending.Application.Dtos;
using BookLending.Domain.Models;
using Mapster;
namespace BookLending.Application.MapSter
{
    public static class MapBook
    {
        public static void RegisterMappings()
        {
            //map from to 
            TypeAdapterConfig<Book, GetBook>.NewConfig();
            TypeAdapterConfig<CreatingBook, Book>.NewConfig();

        }

    }
}
