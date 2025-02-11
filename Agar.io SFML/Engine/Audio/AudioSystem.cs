using Agar.io_SFML.Configs;
using SFML.Audio;

namespace Agar.io_SFML.Audio;

public class AudioSystem // розмiстив цей файл у папцi Game, бо тут enum AudioType Залежить вiд умов гри
{
    private FileInfo[] _audioFilesInfo;
    
    private Dictionary<string, Sound> _sounds;

    private string _firstKill;
    private string _secondKill;
    private string _thirdKill;
    private string _fourthKill;
    private string _fifthKill;
    private string _kill;

    public AudioSystem()
    {
        _sounds = new Dictionary<string, Sound>();
        
        DirectoryInfo directoryInfo = new DirectoryInfo(Path.GetFullPath(Directory.GetCurrentDirectory()));
        _audioFilesInfo = directoryInfo.GetFiles("*.mp3");

        _firstKill = AudioConfig.FirstBlood;
        _secondKill = AudioConfig.DoubleKill;
        _thirdKill = AudioConfig.TripleKill;
        _fourthKill = AudioConfig.UltraKill;
        _fifthKill = AudioConfig.MegaKill;
        _kill = AudioConfig.Kill;
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

    public void PlaySoundLooped(string audioName)
    {
        var sound = _sounds[audioName];
        
        sound.Loop = true;
        sound.Play();
    }
    
    public void PlayEliminationSound(int eliminationsNumber)
    {
        string playingSoundType = eliminationsNumber switch
        {
            1 => _firstKill,
            2 => _secondKill,
            3 => _thirdKill,
            4 => _fourthKill,
            5 => _fifthKill,
            _ => _kill,
        };
        
        PlaySoundOnce(playingSoundType);
    }
}