namespace Day4PracticeAssignmentApp.Scenarios.Music;

class MusicPlaylistService : IMusicPlaylistService
{
    private readonly LinkedList<string> playlistSongs = new();
    private readonly SortedList<int, string> songsByRating = new();
    private readonly SortedDictionary<string, string> songsByArtist = new(StringComparer.OrdinalIgnoreCase);

    public void AddSong(string songName, int rating, string artistName)
    {
        playlistSongs.AddLast(songName);
        songsByRating[rating] = songName;
        songsByArtist[artistName] = songName;
    }

    public void AddSongAfter(string existingSong, string newSong, int rating, string artistName)
    {
        LinkedListNode<string>? existingSongNode = playlistSongs.Find(existingSong);
        if (existingSongNode is null)
        {
            AddSong(newSong, rating, artistName);
            return;
        }

        playlistSongs.AddAfter(existingSongNode, newSong);
        songsByRating[rating] = newSong;
        songsByArtist[artistName] = newSong;
    }

    public void RemoveSong(string songName)
    {
        playlistSongs.Remove(songName);

        int? ratingToRemove = songsByRating.FirstOrDefault(song => song.Value == songName).Key;
        if (ratingToRemove.HasValue && songsByRating.ContainsKey(ratingToRemove.Value))
        {
            songsByRating.Remove(ratingToRemove.Value);
        }

        string? artistToRemove = songsByArtist.FirstOrDefault(song => song.Value == songName).Key;
        if (!string.IsNullOrWhiteSpace(artistToRemove))
        {
            songsByArtist.Remove(artistToRemove);
        }

        Console.WriteLine($"Removed song: {songName}");
    }

    public void DisplayPlaylist()
    {
        Console.WriteLine("Playlist order: " + string.Join(" -> ", playlistSongs));
    }

    public void DisplaySongsByRating()
    {
        foreach (KeyValuePair<int, string> song in songsByRating)
        {
            Console.WriteLine($"Rating {song.Key}: {song.Value}");
        }
    }

    public void DisplaySongsByArtist()
    {
        foreach (KeyValuePair<string, string> song in songsByArtist)
        {
            Console.WriteLine($"Artist: {song.Key}, Song: {song.Value}");
        }
    }
}
