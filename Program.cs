using System;
using Colorful;
using System.Drawing;
using Console = Colorful.Console;
using System.Runtime.InteropServices;

public class Library
{
    private List<string> _booksCollection = new List<string>();
    private List<bool> _borrowList = new List<bool>(new bool[10]);
    private uint _borrowCount = 0;

    public Library() 
    {
        this._booksCollection.Capacity = 10; // List cannot hold more than 10 elements
        this._borrowList.Capacity = 10;
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

    public void BorrowBook()
    {
        if (this._borrowCount >= 3)
        {
            Console.WriteLine("You have borrowed the maximum amount of books already!");
            Console.WriteLine("Total number of books borrowed are {0}", this._borrowCount);
            return;
        }
        this.ListBooks();
        Console.Write("Select a book to borrow: ");
        int selectedBook = Int32.Parse(Console.ReadLine());
        // Check for invalid book number
        if (selectedBook <= 0 || selectedBook > this._booksCollection.Count)
        {
            Console.WriteLine("Invalid book number provided!");
            return;
        }
        // Allow the book to be borrowed if no one borrowed it.
        this.CheckOut(selectedBook);
        return;
    }

    private bool CheckOut(int selectedBook)
    {
        // Allow the book to be borrowed if no one borrowed it.
        if (!this._borrowList.ElementAt<bool>(selectedBook - 1))
        {
            this._borrowList[selectedBook - 1] = true;
            Console.WriteLine("You have successfully borrowed the book!");
            this._borrowCount++;
            return true;
        }
        else
        {
            Console.WriteLine("The book has already been borrowed!");
            return false;
        }
    }

    private void _ListBorrowedBooks()
    {
        int count = 1;
        for (int i = 0; i < this._borrowList.Capacity; i++)
        {
            if (this._borrowList[i])
            {
                Console.WriteLine("{0}. {1}", count, this._booksCollection[i]);
            }
        }
    }

    public void CheckIn()
    {
        if (this._borrowCount <= 0)
        {
            Console.WriteLine("You do not have any borrowed books!");
            return;
        }
        this._ListBorrowedBooks();
        Console.Write("Select the book you would like to check in: ");
        int selectedBook = Int32.Parse(Console.ReadLine());
        if (selectedBook <= 0 || selectedBook > 3)
        {
            Console.WriteLine("Invalid book number specified.");
            return;
        }
        // Mark the book as checked in
        this._borrowList[selectedBook - 1] = false;
        Console.WriteLine("The book has been returned!");
        return;
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
            Console.WriteLine("6. Check in book");
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
                case "5":
                    libraryManagement.BorrowBook();
                    break;
                case "6":
                    libraryManagement.CheckIn();
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