namespace LibraryManagementSystem.Models
{
    public class BorrowedBook
    {
        public int BorrowedBookId { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public int BookId { get; set; }
        public Book Book { get; set; }
        public DateTime BorrowedDate { get; set; }
        public DateTime ReturnDueDate { get; set; }
        public bool IsReturned { get; set; }
        public bool IsUserRequestReturn {  get; set; }
    }
}
