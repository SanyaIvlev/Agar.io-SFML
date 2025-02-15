using Agar.io_SFML.Configs;
using SFML.Audio;

namespace Agar.io_SFML.Audio;

public class AudioSystem
{
    private FileInfo[] _audioFilesInfo;
    
    private Dictionary<string, Sound> _sounds;

    public AudioSystem()
    {
        _sounds = new Dictionary<string, Sound>();
        
        DirectoryInfo directoryInfo = new DirectoryInfo(Path.GetFullPath(Directory.GetCurrentDirectory()));
        _audioFilesInfo = directoryInfo.GetFiles("*.mp3");
    }

    public void Initialize()
    {
        foreach (var audioFile in _audioFilesInfo)
        {
            string audioName = Path.GetFileNameWithoutExtension(audioFile.Name);
            
            SoundBuffer soundBuffer = new SoundBuffer(audioFile.FullName);
            Sound sound = new Sound(soundBuffer);
                
            _sounds.Add(audioName, sound);
        }
    }

    public void PlaySoundOnce(string audioName)
    {
        _sounds[audioName].Play();
    }
}