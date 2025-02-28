namespace Agar.io_SFML.GameSeaBattle.Players;

public class Player : Actor, IUpdatable
{
    public Field field;
    
    public (int x, int y) ShootingPosition;
    public int OpponentShipsDestroyed = 0;
    
    private bool _needsUpdate;

    public void SetUpdateNeed(bool needsUpdate)
    {
        _needsUpdate = needsUpdate;
    }

    public void Update()
    {
        if (!_needsUpdate)
            return;

        Cell shootingCell = field.GetCell(ShootingPosition.x, ShootingPosition.y);
        
        if (shootingCell.HasShot)
            return;
        
        shootingCell.Shoot();

    }
}