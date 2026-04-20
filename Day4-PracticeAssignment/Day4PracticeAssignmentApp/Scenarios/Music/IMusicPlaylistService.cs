namespace Day4PracticeAssignmentApp.Scenarios.Music;

interface IMusicPlaylistService
{
    void AddSong(string songName, int rating, string artistName);

    void AddSongAfter(string existingSong, string newSong, int rating, string artistName);

    void RemoveSong(string songName);

    void DisplayPlaylist();

    void DisplaySongsByRating();

    void DisplaySongsByArtist();
}
