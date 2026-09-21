namespace Diary.Models;

public class DayCell
{
    public DateTime Date { get; set; }
    public bool IsCurrentMonth { get; set; }
    public bool IsToday { get; set; }
    public int NoteCount { get; set; }
    public int DayNumber => Date.Day;
}