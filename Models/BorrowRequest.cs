namespace LibraryManagementSystem.Models
{
    public class BorrowRequest
    {
        public int BorrowRequestId { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public int BookId { get; set; }
        public Book Book { get; set; }
        public DateTime RequestDate { get; set; }
        public string Status { get; set; }
    }
}
