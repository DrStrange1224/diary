namespace Diary.ViewModels;

public class MainViewModel : ViewModelBase
{
    private string _title = string.Empty;
    private string _content = string.Empty;

    public string Title
    {
        get => _title;
        set => SetProperty(ref _title, value);
    }

    public string Content
    {
        get => _content;
        set => SetProperty(ref _content, value);
    }
}