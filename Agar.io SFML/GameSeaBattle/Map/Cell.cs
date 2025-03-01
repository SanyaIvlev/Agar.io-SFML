using Agar.io_SFML.Engine;
using Agar.io_SFML.GameSeaBattle.Events;

namespace Agar.io_SFML.GameSeaBattle;

public class Cell : Button
{
    public bool HasShip;
    public bool HasShot;

    private OnShooted _onShootedEvent; 

    public Cell()
    {
        HasShip = false;
        HasShot = false;
        
        _onShootedEvent = new OnShooted();
        _onShootedEvent.ShootedCell = this;
        
        AddCallback(Shoot);
    }

    public void Shoot()
    {
        HasShot = true;
        EventBus<OnShooted>.Raise(_onShootedEvent);
    }
}