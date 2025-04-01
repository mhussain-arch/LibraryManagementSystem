using System;
using Colorful;
using System.Drawing;
using Console = Colorful.Console;

public class Library
{
    private List<string> _booksCollection = new List<string>();

    public Library() 
    {
        this._booksCollection.Capacity = 10; // List cannot hold more than 10 elements
        _booksCollection.Add("The Great Gatsby");
        _booksCollection.Add("Pride and Prejudice");
        _booksCollection.Add("Don Quixote");
        _booksCollection.Add("Moby-Dick");
    }

    /// <summary>
    /// Method <c>GreetUser</c> greets the user in a colourful way.
    /// </summary>
    public void GreetUser()
    {
        Console.WriteWithGradient("Welcome to rikzt0r's Library Management App!\n", Color.LightBlue, Color.Pink, 2);
    }


    public void AddBook()
    {
        // Check if the max limit of 10 has reached or not
        int booksCount = this._booksCollection.Count;
        if (booksCount >= this._booksCollection.Capacity)
        {
            Console.WriteLine("Books in the libary are at capacity!");
            Console.WriteLine("Total books in library are {0}", booksCount);
            return;
        }

        string? bookTitle = null;
        while(bookTitle == null)
        {
            Console.WriteLine("Provide the book title: ");
            bookTitle = Console.ReadLine();
            if (bookTitle == null) 
            {
                Console.WriteLine("Book title not provided!");
            } 
            else 
            {
                this._booksCollection.Add(bookTitle);
            }
        }
        
    }

    /// <summary>
    /// Method <c>RemoveBook</c> removes a book from the list of available books. 
    /// If the removal is successfull it returns <c>true</c>, else <c> false</c>.
    /// </summary>
    /// <returns>bool</returns>
    public bool RemoveBook()
    {
        int booksCount = this._booksCollection.Count;
        this.ListBooks();
        Console.WriteLine("Select the book to remove: ");
        int bookNumber = 0;
        if (!Int32.TryParse(Console.ReadLine(), out bookNumber) || bookNumber > booksCount)
        {
            Console.WriteLine("Invalid input specified!");
            return false;
        }
        this._booksCollection.RemoveAt(bookNumber - 1);
        return true;
    }
    /// <summary>
    /// Method <c>ListBooks</c> lists all the books available.
    /// </summary>
    public void ListBooks()
    {
        int count = 1;
        foreach(string bookTitle in this._booksCollection)
        {
            Console.WriteLine("{0}. {1}", count, bookTitle);
            count++;
        }
    }

    public string? SearchBooks()
    {
        Console.Write("Search for a book: ");
        string searchString = Console.ReadLine();
        return this._booksCollection.Find(s => {
            return s.ToLower().Equals(searchString.ToLower());
        });
    }
}

public class Program
{
    static void Main(string[] args)
    {
        bool exitStatus = false;
        Library libraryManagement = new Library();
        libraryManagement.GreetUser();
        while (!exitStatus)
        {
            // Console.Clear();
            string mode = "0";
            Console.WriteLine("1. Add a book");
            Console.WriteLine("2. Delete a book");
            Console.WriteLine("3. List available books");
            Console.WriteLine("4. Search for a book");
            Console.WriteLine("5. Borrow a book");
            Console.WriteLine("6. Flag book as check-in or check-out");
            Console.WriteLine("7. Exit");
            Console.Write(": ");
            mode = Console.ReadLine();
            switch (mode)
            {
                case "1":
                    libraryManagement.AddBook();
                    break;
                case "2":
                    libraryManagement.RemoveBook();
                    break;
                case "3":
                    libraryManagement.ListBooks();
                    break;
                case "4":
                    string? queriedBook = libraryManagement.SearchBooks();
                    if (queriedBook != null)
                    {
                        Console.WriteLine("\"{0}\" is available!", queriedBook);
                    }
                    else
                    {
                        Console.WriteLine("The book is not available.");
                    }
                    break;
                case "7":
                    exitStatus = true;
                    break;
                default:
                    Console.WriteLine("Invalid option!");
                    break;
            }
            Console.WriteLine("Press Enter to continue!", Color.Green);
            Console.ReadLine();
        }
    }
}