namespace Agar.io_SFML.GameSeaBattle;

public class Cell : Button
{
    public bool HasShip;
    public bool HasShot;

    public Cell()
    {
        HasShip = false;
        HasShot = false;
        
        AddCallback(OnShoot);
    }

    private void OnShoot()
    {
        HasShot = true;
    }
}