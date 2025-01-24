using System.Data;
using SFML.Window;

namespace Agar.io_SFML;

public class KeyBindBundle
{
    private List<KeyBind> _keyBinds;

    public KeyBindBundle()
    {
        _keyBinds = new List<KeyBind>();
    }

    public void AddKeyBind(string name, Keyboard.Key keyBind)
    {
        KeyBind bind = new(name, keyBind);
        _keyBinds.Add(bind);
    }

    public KeyBind GetKeyBindByName(string name) 
        => _keyBinds.Single(bind => bind.Name == name);

    public void Update()
    {
        foreach (var keyBind in _keyBinds)
        {
            keyBind.Update();
        }
    }
}