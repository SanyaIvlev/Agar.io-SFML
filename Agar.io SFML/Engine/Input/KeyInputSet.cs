using System.Data;
using Agar.io_SFML.Engine;
using SFML.Window;

namespace Agar.io_SFML;

public class KeyInputSet
{
    private List<KeyInput> _keyBinds;

    public KeyInputSet()
    {
        Dependency.Register(this);
        _keyBinds = new();
    }

    public KeyInput AddKeyBind(Keyboard.Key keyBind)
    {
        KeyInput input = new(keyBind);
        _keyBinds.Add(input);
        
        return input;
    }

    public void ReadInputs()
    {
        foreach (var keyBind in _keyBinds)
        {
            keyBind.ReadInput();
        }
    }
    
    public void UpdateCallbacks()
    {
        foreach (var keyBind in _keyBinds)
        {
            keyBind.UpdateCallbacks();
        }
    }
}