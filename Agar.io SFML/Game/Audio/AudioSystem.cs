using SFML.Audio;

namespace Agar.io_SFML.Audio;

public enum AudioType
{
    Kill,
    FirstBlood,
    DoubleKill,
    TripleKill,
    UltraKill,
    MegaKill,
    Eating,
    Victory,
    Lose,
} 

public class AudioSystem // розмiстив цей файл у папцi Game, бо тут enum AudioType Залежить вiд умов гри
{
    private FileInfo[] _audioFilesInfo;
    
    private Dictionary<AudioType, Sound> _sounds;

    public AudioSystem()
    {
        _sounds = new Dictionary<AudioType, Sound>();
        
        DirectoryInfo directoryInfo = new DirectoryInfo(Path.GetFullPath(Directory.GetCurrentDirectory()));
        _audioFilesInfo = directoryInfo.GetFiles("*.mp3");
    }

    public void Initialize()
    {
        foreach (var audioFile in _audioFilesInfo)
        {
            string audioName = Path.GetFileNameWithoutExtension(audioFile.Name);
            
            if (Enum.TryParse(audioName, out AudioType audioType))
            {
                SoundBuffer soundBuffer = new SoundBuffer(audioFile.FullName);
                Sound sound = new Sound(soundBuffer);
                
                _sounds.Add(audioType, sound);
            }
        }
    }

    public void PlaySoundOnce(AudioType audioType)
    {
        _sounds[audioType].Play();
    }

    public void PlaySoundLooped(AudioType audioType)
    {
        var sound = _sounds[audioType];
        
        sound.Loop = true;
        sound.Play();
    }
    
    
}