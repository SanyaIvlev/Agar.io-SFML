using Agar.io_SFML.Engine.Scene;

namespace Agar.io_SFML.GameSeaBattle;

public class MainMenu : Scene
{
    private ProfileProcesser _profileProcesser;
    
    private List<Profile> _profiles;
    
    public override void Start()
    {
        _profileProcesser = new ProfileProcesser();
        
        _profiles = _profileProcesser.GetProfiles();
        
    }
}