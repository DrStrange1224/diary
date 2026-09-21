using System.Globalization;
using System.IO;
using Diary.Models;
using Microsoft.Data.Sqlite;

namespace Diary.Services;

public static class NoteStore
{
    private static string DbPath => Path.Combine(AppContext.BaseDirectory, "diary.db");

    public static void Initialize()
    {
        using var connection = OpenConnection();
        using var command = connection.CreateCommand();
        command.CommandText =
            """
            CREATE TABLE IF NOT EXISTS Entries (
                Id TEXT PRIMARY KEY,
                Date TEXT NOT NULL,
                Title TEXT NOT NULL,
                Content TEXT NOT NULL,
                CreatedAt TEXT NOT NULL
            );
            """;
        command.ExecuteNonQuery();
    }

    public static List<DiaryEntry> LoadAll()
    {
        var entries = new List<DiaryEntry>();

        using var connection = OpenConnection();
        using var command = connection.CreateCommand();
        command.CommandText = "SELECT Id, Date, Title, Content, CreatedAt FROM Entries ORDER BY Date;";
        using var reader = command.ExecuteReader();
        while (reader.Read())
        {
            entries.Add(new DiaryEntry
            {
                Id = Guid.Parse(reader.GetString(0)),
                Date = FromDb(reader.GetString(1)),
                Title = reader.GetString(2),
                Content = reader.GetString(3),
                CreatedAt = FromDb(reader.GetString(4))
            });
        }

        return entries;
    }

    public static void Save(DiaryEntry entry)
    {
        using var connection = OpenConnection();
        using var command = connection.CreateCommand();
        command.CommandText =
            """
            INSERT OR REPLACE INTO Entries (Id, Date, Title, Content, CreatedAt)
            VALUES ($id, $date, $title, $content, $createdAt);
            """;
        command.Parameters.AddWithValue("$id", entry.Id.ToString());
        command.Parameters.AddWithValue("$date", ToDb(entry.Date));
        command.Parameters.AddWithValue("$title", entry.Title);
        command.Parameters.AddWithValue("$content", entry.Content);
        command.Parameters.AddWithValue("$createdAt", ToDb(entry.CreatedAt));
        command.ExecuteNonQuery();
    }

    private static SqliteConnection OpenConnection()
    {
        var connection = new SqliteConnection($"Data Source={DbPath}");
        connection.Open();
        return connection;
    }

    private static string ToDb(DateTime value)
        => value.ToString("o", CultureInfo.InvariantCulture);

    private static DateTime FromDb(string value)
        => DateTime.Parse(value, CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind);
}