static class FileHandlingDemo
{
    public static void Run()
    {
        // Step 1: Define the file paths.
        string filePath = "sample.txt";
        string copyPath = "copy.txt";

        try
        {
            // Step 2: If there is an error, it will be handled by the catch block.
            Console.WriteLine("Creating a file...");
            File.Create(filePath).Close(); // Closing the file is important.

            // Step 3: Write data to the file.
            Console.WriteLine("Writing to a file...");
            File.WriteAllText(filePath, "Hello, this is the first line of the file..!!");

            // Step 4: Append data to the file.
            Console.WriteLine("Appending data to the file..!!");
            File.AppendAllText(filePath, Environment.NewLine + "This data is appended to the text.");

            // Step 5: Read from the file.
            Console.WriteLine("Reading from the file...");
            string content = File.ReadAllText(filePath);
            Console.WriteLine(content);

            // Step 6: Copy the file.
            Console.WriteLine("Copying the file...");
            File.Copy(filePath, copyPath, overwrite: true);

            // Step 7: Confirm copied file content.
            Console.WriteLine("Reading from copied file...");
            string copiedContent = File.ReadAllText(copyPath);
            Console.WriteLine(copiedContent);
        }
        catch (FileNotFoundException exception)
        {
            Console.WriteLine("File was not found: " + exception.Message);
        }
        catch (UnauthorizedAccessException exception)
        {
            Console.WriteLine("Access denied: " + exception.Message);
        }
        catch (IOException exception)
        {
            Console.WriteLine("File handling error: " + exception.Message);
        }
        catch (Exception exception)
        {
            Console.WriteLine("Unexpected error: " + exception.Message);
        }
        finally
        {
            Console.WriteLine("File handling demo completed.");
        }
    }
}
