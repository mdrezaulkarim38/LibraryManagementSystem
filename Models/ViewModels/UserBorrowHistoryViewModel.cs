using LibraryManagementSystem.Models;
namespace LibraryManagementSystem.Models.ViewModels;

public class UserBorrowHistoryViewModel
{
    public IEnumerable<BorrowedBook>? BorrowedBooks { get; set; }
    public IEnumerable<BorrowRequest>? BorrowRequests { get; set; }
}
