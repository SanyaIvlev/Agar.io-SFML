using System.Data;
using SFML.Window;

namespace Agar.io_SFML;

public class KeyInputSet
{
    private List<KeyInput> _keyBinds;

    public KeyInputSet()
    {
        _keyBinds = new();
    }

    public KeyInput AddKeyBind(Keyboard.Key keyBind)
    {
        KeyInput input = new(keyBind);
        _keyBinds.Add(input);
        
        return input;
    }
    
    public void Update()
    {
        foreach (var keyBind in _keyBinds)
        {
            keyBind.Update();
        }
    }
}