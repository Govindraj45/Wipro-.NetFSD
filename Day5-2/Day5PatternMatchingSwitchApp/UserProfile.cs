class UserProfile
{
    public UserProfile(string name, string role, int nameLength)
    {
        Name = name;
        Role = role;
        NameLength = nameLength;
    }

    public string Name { get; }
    public string Role { get; }
    public int NameLength { get; }
}
