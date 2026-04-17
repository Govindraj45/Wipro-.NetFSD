using System.Speech.Synthesis;

// Step 1: Import System.Speech.Synthesis to access SpeechSynthesizer.
// Step 2: Create an instance of SpeechSynthesizer.
// Step 3: Ask the user to enter text.
// Step 4: Convert the entered text to speech and play it back.

Console.Write("Enter text to convert to speech: ");
string userInput = Console.ReadLine() ?? string.Empty;

if (string.IsNullOrWhiteSpace(userInput))
{
    Console.WriteLine("No text entered.");
    return;
}

Console.WriteLine("You entered: " + userInput);

if (!OperatingSystem.IsWindows())
{
    Console.WriteLine("Speech playback using System.Speech.Synthesis is supported on Windows.");
    return;
}

using SpeechSynthesizer synthesizer = new();
synthesizer.Speak(userInput);
