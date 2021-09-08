using Codecool.BookDb.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Codecool.BookDb.View
{
    class ConsoleView
    {
        private UserInterface userInterface;

        public ConsoleView()
        {
            userInterface = new UserInterface();
        }

        public char MainMenu()
        {
            userInterface.ClearScreen();
            userInterface.PrintTitle("MAIN MENU");
            userInterface.PrintOption('a', "Authors");
            userInterface.PrintOption('b', "Books");
            userInterface.PrintOption('q', "Quit");
            return userInterface.Choice("abq");
        }

        public char SubMenu(string title)
        {
            userInterface.ClearScreen();
            userInterface.PrintTitle($"Work with: {title}");
            userInterface.PrintOption('l', "List");
            userInterface.PrintOption('a', "Add");
            userInterface.PrintOption('e', "Edit");
            userInterface.PrintOption('q', "Quit");
            return userInterface.Choice("laeq");
        }

        public void ListAuthors(List<Author> authors, bool waitForUser=true)
        {
            userInterface.PrintLn("\nList of authors:");
            foreach (var author in authors)
            {
                userInterface.PrintLn(author.ToString());
            }
            if (waitForUser)
            {
                userInterface.WaitForUser();
            }
        }

        public Author AddAuthor()
        {
            string firstName = userInterface.ReadString("First name?", "N/A");
            string lastName = userInterface.ReadString("Last name?", "N/A");
            DateTime birthDate =userInterface.ReadDate("Birth date?", new DateTime(1900, 01, 01));
            return new Author(firstName, lastName, birthDate);
        }

        public Author SelectAuthor(List<Author> authors, int defaultIndex)
        {
            Author selectedAuthor;
            do
            {
                ListAuthors(authors, false);
                int selectedId = userInterface.ReadInt("Select author id", authors[defaultIndex].Id);
                selectedAuthor = authors.Find(author => author.Id == selectedId);
                if (selectedAuthor != null)
                {
                    break;
                }
                userInterface.PrintLn($"Author not found with id: {selectedId}");
            } while (true);

            return selectedAuthor;
        }

        public Author EditAuthor(Author author)
        {
            string firstName = userInterface.ReadString("First name, if changed?", author.FirstName);
            string lastName = userInterface.ReadString("Last name, if changed?", author.LastName);
            DateTime birthDate = userInterface.ReadDate("Birth date, if changed?", author.BirthDate);

            Author editedAuthor = new Author(firstName, lastName, birthDate);
            editedAuthor.Id = author.Id;
            return editedAuthor;
        }

        public void ListBooks(List<Book> books, bool waitForUser = true)
        {
            userInterface.PrintLn("\nList of books:");
            foreach (var book in books)
            {
                userInterface.PrintLn(book.ToString());
            }
            if (waitForUser)
            {
                userInterface.WaitForUser();
            }
        }

        public Book AddBook(List<Author> authors)
        {
            Author author = SelectAuthor(authors, 0);
            string title = userInterface.ReadString("Title?", "N/A");
            return new Book(author, title);
        }

        public Book SelectBook(List<Book> books)
        {
            Book selectedBook;
            do
            {
                ListBooks(books, false);
                int selectedId = userInterface.ReadInt("Select book id", 1);
                selectedBook = books.Find(book => book.Id == selectedId);
                if (selectedBook != null)
                {
                    break;
                }
                userInterface.PrintLn($"Book not found with id: {selectedId}");
            } while (true);

            return selectedBook;
        }

        public Book EditBook(Book book, List<Author> authors)
        {
            Author author = SelectAuthor(authors, authors.FindIndex(x => x.Id == book.Author.Id));

            string title = userInterface.ReadString("Title, if changed?", book.Title);

            Book editedBook = new Book(author, title);
            editedBook.Id = book.Id;
            return editedBook;
        }
    }
}
