namespace Alfred.Services;

public interface IObjectSerializer
{
    string? Serialize<T>(T? obj) where T: class;
    T? Deserialize<T>(string? data) where T : class;
}