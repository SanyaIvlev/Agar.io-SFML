using Agar.io_SFML.Configs;
using SFML.Audio;
using SFML.Graphics;

namespace Agar.io_SFML.Audio;

public class AudioSystem
{
    private FileInfo[] _audioFilesInfo;
    
    private Dictionary<string, Sound> _sounds;

    public AudioSystem(RenderWindow window)
    {
        _sounds = new Dictionary<string, Sound>();
        
        DirectoryInfo directoryInfo = new DirectoryInfo(PathUtils.AudioDirectory);
        _audioFilesInfo = directoryInfo.GetFiles("*.mp3");

        window.Closed += OnProgramClosed;
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
    
    private void OnProgramClosed(object? sender, EventArgs e)
    {
        foreach (var sound in _sounds.Values)
        {
            sound.SoundBuffer.Dispose();
            sound.Dispose();
        }
    }

}