using System;
using System.Collections.Generic;
using System.Linq;
using DataAcessClassLibrary;
using static System.Reflection.Metadata.BlobBuilder;

namespace BusinessLogicClassLibrary
{
  
    public class BLLBookInventory
    {
       public int ID { get; set; }

       public int UserId { get; set; }
      public  int BookId { get; set; }
        public bool CheckedIn { get; set; }

        DABookInventory inventory = new DABookInventory();
        List<BLLBookInventory> BLLBookInventorys = new List<BLLBookInventory>();
        public BLLBookInventory()
        {
            this.ID = 0;
            this.UserId = 0;
            this.BookId = 0;
            this.CheckedIn = false;
        }
        public BLLBookInventory(int iD, int userId, int bookId, bool checkedIn)
        {
            this.ID = iD;
            this.UserId = userId;
            this.BookId = bookId;
            this.CheckedIn = checkedIn;
        }

        
        public void addBookToInventory(int BookId,int UserId)
        {
           DABookInventory inventory = new DABookInventory();
            inventory.AddToBookInventory(BookId, UserId);
        }
        public bool ExistInInventory(int Bookid,int UserId)
        {
            bool sucess;
            List<BLLBookInventory> bookList = Map(inventory.GetAllBookInventory());
            foreach (BLLBookInventory book in bookList)
            {
                Console.WriteLine(book.UserId);
                Console.WriteLine(book.BookId);
                Console.WriteLine(book.CheckedIn);
            }
             sucess = bookList.Any(p=> p.BookId == Bookid && p.UserId == UserId && p.CheckedIn == true);
            return sucess;
        }
    
     
        
       

        public void checkedInInventory(int BookId, int UserId)
        {
           inventory.Checkin(BookId,UserId);
        }
        
      
      
        public void printSingleBook(int id)
        {
            DABookInventory dABook = new DABookInventory();
            dABook = inventory.GetUserCheckedOutBooks(id);
            if (dABook != null)
            {


                BLLBookInventory books = Map(inventory.GetUserCheckedOutBooks(id));
                Console.WriteLine("The Book Id  " + books.ID);
                Console.WriteLine("Book name " +books.BookId);
                Console.WriteLine("Author " + books.UserId);
            }
            else
            {
                Console.WriteLine("Book does not exist");
            }
        }
        public void printAllBooks()
        {
            List<BLLBookInventory> bookList = Map(inventory.GetAllBookInventory());
            foreach (BLLBookInventory book in bookList)
            {
                Console.WriteLine("The Book Id  " + book.ID);
                Console.WriteLine("Book name " + book.BookId);
                Console.WriteLine("Author " + book.UserId);
            }
        }
        public List<BLLBookInventory> Map(List<DABookInventory> dABooks)
        {
            List<BLLBookInventory> bLLBookInventories = new List<BLLBookInventory>();
            foreach (DABookInventory dbook in dABooks)
            {
                BLLBookInventory inventory = new BLLBookInventory();
                inventory.ID = dbook.ID;
                inventory.BookId = dbook.BookId;
                inventory.UserId = dbook.UserId;
                inventory.CheckedIn = dbook.CheckedIn;
                bLLBookInventories.Add(inventory);
            }
            return bLLBookInventories;
        }
        public BLLBookInventory Map(DABookInventory dABook)
        {
            BLLBookInventory inventory = new BLLBookInventory();
            inventory.ID = dABook.ID;
            inventory.BookId = dABook.BookId;
            inventory.UserId = dABook.UserId;
            inventory.CheckedIn = dABook.CheckedIn;
            return inventory;
        }
    }
}
