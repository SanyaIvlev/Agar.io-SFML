namespace Agar.io_SFML.GameSeaBattle.Players;

public class Player : Actor, IUpdatable
{
    public Field field;
    
    public (int x, int y) ShootingPosition;
    public int OpponentShipsDestroyed = 0;

    public void Initialize(bool needsInteractChange)
    {
        field.NeedsInteractChange = needsInteractChange;
    }

    public void InitalizeShootingPosition((int x, int y) position)
    {
        ShootingPosition = position;
    }

    public void Update()
    {
        if (ShootingPosition == (-1, -1))
            return;

        Cell shootingCell = field.GetCell(ShootingPosition.x, ShootingPosition.y);
        
        if (shootingCell.HasShot)
            return;
        
        shootingCell.Shoot();

    }
}