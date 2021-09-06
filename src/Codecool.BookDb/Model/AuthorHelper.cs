using Codecool.BookDb.Manager;
using System;
using System.Collections.Generic;
using System.Text;

namespace Codecool.BookDb.Model
{
    static class AuthorHelper
    {
        public static Author GetAuthor(int id)
        {
            AuthorDao authorDao = new AuthorDao(BookDbManager.Connect());
            return authorDao.Get(id);
        }
    }
}
