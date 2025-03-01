using System.Text.Json;
using Agar.io_SFML.GameSeaBattle;
using Newtonsoft.Json;

namespace Agar.io_SFML.Engine;

public static class JsonProcesser
{
    private static string _jsonPath;
    private static string _jsonFile;

    static JsonProcesser()
    {
        _jsonPath = PathUtils.SavesDirectory + "profiles.json";
        _jsonFile = File.ReadAllText(_jsonPath);
    }
    
    public static List<Profile> GetProfiles()
    {
        var profiles = JsonConvert.DeserializeObject<List<Profile>>(_jsonFile);
        
        if (profiles == null)
            return new List<Profile>();

        return profiles;
    }

    public static void SaveProfile(List<Profile> profiles)
    {
        var options = new JsonSerializerOptions { IncludeFields = true };
        
        var json = System.Text.Json.JsonSerializer.Serialize(profiles, options);
        
        File.WriteAllText(_jsonPath, json);
    }
}