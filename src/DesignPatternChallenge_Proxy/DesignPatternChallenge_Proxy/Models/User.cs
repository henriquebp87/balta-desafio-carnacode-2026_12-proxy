namespace DesignPatternChallenge_Proxy.Models;

internal class User(string username, int clearanceLevel)
{
    public string Username { get; set; } = username;
    public int ClearanceLevel { get; set; } = clearanceLevel;
}