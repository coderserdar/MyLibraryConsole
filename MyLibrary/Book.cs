
public class Book
{
    public int Id { get; set; }
    public required string BookName { get; set; }
    public required string BookCategory { get; set; }
    public required string Writer { get; set; }
    public string? BookDescription { get; set; }
    public int ActiveStatus { get; set; }
    public required int ReadUnread { get; set; }
}