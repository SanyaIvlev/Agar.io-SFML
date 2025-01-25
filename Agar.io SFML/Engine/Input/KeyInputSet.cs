using System.Data;
using SFML.Window;

namespace Agar.io_SFML;

public class KeyInputSet
{
    private List<KeyInput> _keyBinds;

    public KeyInputSet()
    {
        _keyBinds = new List<KeyInput>();
    }

    public void AddKeyBind(string name, Keyboard.Key keyBind)
    {
        KeyInput input = new(name, keyBind);
        _keyBinds.Add(input);
    }

    public KeyInput GetKeyBindByName(string name)
    {
        foreach (var key in _keyBinds)
        {
            if (key.Name == name)
            {
                return key; 
            }
        }

        return null;
    }

    public void Update()
    {
        foreach (var keyBind in _keyBinds)
        {
            keyBind.Update();
        }
    }
}