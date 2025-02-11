using Agar.io_SFML.Configs;

namespace Agar.io_SFML.Audio;

public class AgarioAudioSystem : AudioSystem
{
    private string _firstKill;
    private string _secondKill;
    private string _thirdKill;
    private string _fourthKill;
    private string _fifthKill;
    private string _kill;

    public AgarioAudioSystem()
    {
        _firstKill = AudioConfig.FirstBlood;
        _secondKill = AudioConfig.DoubleKill;
        _thirdKill = AudioConfig.TripleKill;
        _fourthKill = AudioConfig.UltraKill;
        _fifthKill = AudioConfig.MegaKill;
        _kill = AudioConfig.Kill;
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