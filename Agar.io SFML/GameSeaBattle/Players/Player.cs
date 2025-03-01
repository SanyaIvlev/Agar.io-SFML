namespace Agar.io_SFML.GameSeaBattle.Players;

public class Player : Actor
{
    public Field field;
    
    public (int x, int y) ShootingPosition;
    public int OpponentShipsDestroyed = 0;
    
    public bool NeedsUpdate;

    public void Update()
    {
        Cell shootingCell = field.GetCell(ShootingPosition.x, ShootingPosition.y);
        
        if (shootingCell.HasShot)
            return;
        
        shootingCell.Shoot();

    }
}