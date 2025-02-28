using Agar.io_SFML.Engine;
using Agar.io_SFML.GameSeaBattle.Events;

namespace Agar.io_SFML.GameSeaBattle;

public class Cell : Button
{
    public bool HasShip;
    public bool HasShot;

    public Cell()
    {
        HasShip = false;
        HasShot = false;
        
        AddCallback(Shoot);
    }

    public void Shoot()
    {
        HasShot = true;
        EventBus<OnShooted>.Raise(new());
    }
}