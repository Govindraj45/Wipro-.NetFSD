using Day4PracticeAssignmentApp.Common;

namespace Day4PracticeAssignmentApp.Scenarios.Music;

class MusicPlaylistManagerDemo : IScenarioDemo
{
    public string Title => "Scenario 4: Music Playlist Manager";

    public void Run()
    {
        IMusicPlaylistService playlistService = new MusicPlaylistService();

        playlistService.AddSong("Believer", 5, "Imagine Dragons");
        playlistService.AddSong("Perfect", 4, "Ed Sheeran");
        playlistService.AddSongAfter("Believer", "Faded", 3, "Alan Walker");

        ConsoleOutput.WriteSubHeading("LinkedList Playlist");
        playlistService.DisplayPlaylist();

        ConsoleOutput.WriteSubHeading("SortedList Songs By Rating");
        playlistService.DisplaySongsByRating();

        ConsoleOutput.WriteSubHeading("SortedDictionary Songs By Artist");
        playlistService.DisplaySongsByArtist();

        ConsoleOutput.WriteSubHeading("Remove Song");
        playlistService.RemoveSong("Faded");
        playlistService.DisplayPlaylist();
    }
}
