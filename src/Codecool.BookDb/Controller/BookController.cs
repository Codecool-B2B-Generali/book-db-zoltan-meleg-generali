using Codecool.BookDb.Manager;
using Codecool.BookDb.Model;
using Codecool.BookDb.View;
using System.Collections.Generic;

namespace Codecool.BookDb.Controller
{
    class BookController
    {
        private AuthorDao authorDao;
        private BookDao bookDao;

        private ConsoleView consoleView;

        public BookController()
        {
            bookDao = new BookDao(BookDbManager.Connect());
            authorDao = new AuthorDao(BookDbManager.Connect());

            consoleView = new ConsoleView();

            Menu();
        }

        private void Menu()
        {
            char choice;
            do
            {
                choice = consoleView.MainMenu();
                switch (choice)
                {
                    case 'a':
                        WorkWithAuthors();
                        break;
                    case 'b':
                        WorkWithBooks();
                        break;
                }
            } while (choice != 'q');
        }

        private void WorkWithAuthors()
        {
            char choice;
            do
            {
                choice = consoleView.SubMenu("Authors");
                switch (choice)
                {
                    case 'l':
                        consoleView.ListAuthors(authorDao.GetAll());
                        break;
                    case 'a':
                        AddAuthor();
                        break;
                    case 'e':
                        EditAuthor();
                        break;
                }
            } while (choice != 'q');
        }

        private void AddAuthor()
        {
            Author author = consoleView.AddAuthor();
            authorDao.Add(author);
        }

        private void EditAuthor()
        {
            Author author = consoleView.SelectAuthor(authorDao.GetAll(), 0);
            author = consoleView.EditAuthor(author);
            authorDao.Update(author);
        }

        private void WorkWithBooks()
        {
            char choice;
            do
            {
                choice = consoleView.SubMenu("Books");
                switch (choice)
                {
                    case 'l':
                        consoleView.ListBooks(bookDao.GetAll());
                        break;
                    case 'a':
                        AddBook();
                        break;
                    case 'e':
                        EditBook();
                        break;
                }
            } while (choice != 'q');
        }

        private void AddBook()
        {
            Book book = consoleView.AddBook(authorDao.GetAll());
            bookDao.Add(book);
        }

        private void EditBook()
        {
            Book book = consoleView.SelectBook(bookDao.GetAll());
            book = consoleView.EditBook(book, authorDao.GetAll());
            bookDao.Update(book);
        }
    }
}
