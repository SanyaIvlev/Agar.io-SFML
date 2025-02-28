using Agar.io_SFML.Engine;
using SFML.Window;

namespace Agar.io_SFML;

public class ButtonBindsSet
{
    private List<BaseInput> _keyBinds;

    public ButtonBindsSet()
    {
        Service<ButtonBindsSet>.Set(this);
        _keyBinds = new();
    }

    public KeyInput AddKeyboardBind(Keyboard.Key keyBind)
    {
        KeyInput input = new(keyBind);
        _keyBinds.Add(input);
        
        return input;
    }

    public MouseInput AddMouseBind(Mouse.Button button)
    {
        MouseInput input = new(button);
        _keyBinds.Add(input);
        
        return input;
    }
    
    public void UpdateCallbacks()
    {
        foreach (var keyBind in _keyBinds)
        {
            keyBind.UpdateCallbacks();
        }
    }

    public void ResetBinds()
    {
        foreach (var keyBind in _keyBinds)
        {
            keyBind.RemoveCallBacks();
        }
    }
}