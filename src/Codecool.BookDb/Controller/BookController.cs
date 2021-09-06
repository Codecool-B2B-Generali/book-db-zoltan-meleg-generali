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

        private List<Author> authors;
        private List<Book> books;

        private ConsoleView consoleView;

        public BookController()
        {
            bookDao = new BookDao(BookDbManager.Connect());
            authorDao = new AuthorDao(BookDbManager.Connect());

            consoleView = new ConsoleView();

            authors = authorDao.GetAll();
            books = bookDao.GetAll();

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
            authors = authorDao.GetAll();
            char choice;
            do
            {
                choice = consoleView.SubMenu("Authors");
                switch (choice)
                {
                    case 'l':
                        consoleView.ListAuthors(authors);
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
            authors.Clear();
            authors = authorDao.GetAll();
        }

        private void EditAuthor()
        {
            Author author = consoleView.SelectAuthor(authors, authors[0]);
            author = consoleView.EditAuthor(author);
            authorDao.Update(author);
            authors.Clear();
            authors = authorDao.GetAll();
        }

        private void WorkWithBooks()
        {
            books = bookDao.GetAll();
            char choice;
            do
            {
                choice = consoleView.SubMenu("Books");
                switch (choice)
                {
                    case 'l':
                        consoleView.ListBooks(books);
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
            Book book = consoleView.AddBook(authors);
            bookDao.Add(book);
            books.Clear();
            books = bookDao.GetAll();
        }

        private void EditBook()
        {
            Book book = consoleView.SelectBook(books);
            book = consoleView.EditBook(book, authors);
            bookDao.Update(book);
            books.Clear();
            books = bookDao.GetAll();
        }
    }
}
