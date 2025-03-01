using Agar.io_SFML.Engine;

namespace Agar.io_SFML.GameSeaBattle;

public class ProfileProcesser
{
    private List<Profile> _profiles;

    public ProfileProcesser()
    {
        _profiles = JsonProcesser.GetProfiles();
    }

    public List<Profile> GetProfiles()
    {
        _profiles = JsonProcesser.GetProfiles();

        return _profiles;
    }
    
    public void UpdateProfile(Profile updatedProfile)
    {
        int index = _profiles.IndexOf(updatedProfile);
        
        if (index == -1)
            return;
        
        _profiles[index] = updatedProfile;
        
        JsonProcesser.SaveProfile(_profiles);
    }
}