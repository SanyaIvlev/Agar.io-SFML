namespace Agar.io_SFML.GameSeaBattle.Actors;

public class Player : Actor
{
    public Field field;
    public Action<uint> OnDestroyedShipsChanged;
    
    public (int x, int y) ShootingPosition;
    public uint OpponentShipsDestroyed = 0;
    
    public bool NeedsUpdate;

    public void Update()
    {
        Cell shootingCell = field.GetCell(ShootingPosition.x, ShootingPosition.y);

        if (shootingCell.HasShot)
        {
            return;
        }
        shootingCell.Shoot();
    }

    public void OnShipDestroyed()
    {
        OpponentShipsDestroyed++;
        OnDestroyedShipsChanged?.Invoke(OpponentShipsDestroyed);
    }
}