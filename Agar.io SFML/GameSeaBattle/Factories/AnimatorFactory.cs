using Agar.io_SFML.Animations;
using Agar.io_SFML.Engine;
using Agar.io_SFML.Factory;
using SFML.Graphics;

namespace Agar.io_SFML.GameSeaBattle;

public class AnimatorFactory : ActorFactory
{
    private TextureLoader _textureLoader;

    private Texture[] _destroyedShipCell;
    private Texture[] _hoveredCell;
    private Texture[] _shipCell;
    private Texture[] _missedShotCell;
    private Texture[] _emptyCell;
    
    public AnimatorFactory()
    {
        Service<AnimatorFactory>.Set(this);
        
        _textureLoader = TextureLoader.Instance;
        
        _emptyCell = _textureLoader.LoadTexturesFrom("Cell", "Empty");
        _shipCell = _textureLoader.LoadTexturesFrom("Cell", "HasShip");
        _missedShotCell = _textureLoader.LoadTexturesFrom("Cell", "MissedShot");
        _destroyedShipCell = _textureLoader.LoadTexturesFrom("Cell", "DestroyedShip");
        _hoveredCell = _textureLoader.LoadTexturesFrom("Cell", "Hovered");
    }

    public void CreateCellAnimator(Cell cell)
    {
        ShapeAnimator animator = CreateActor<ShapeAnimator>();
        
        State emptyState = new(_emptyCell, 200);
        State shipCellState = new(_shipCell, 200);
        State missedShotCellState = new(_missedShotCell, 200);
        State destroyedShipCellState = new(_destroyedShipCell, 200);
        State hoveredCellState = new(_hoveredCell, 200);

        animator
            .Initialize(cell.shape)
            .AddInitialState(emptyState)
            .AddTransition(emptyState, shipCellState, () => cell.HasShip && !cell.IsClickable)
            .AddTransition(shipCellState, emptyState, () => cell.HasShip && cell.IsClickable)
            .AddTransition(emptyState, destroyedShipCellState, () => cell.HasShip && cell.IsClickable && cell.HasShot)
            .AddTransition(shipCellState, destroyedShipCellState, () => cell.HasShip && !cell.IsClickable && cell.HasShot)
            .AddTransition(emptyState, hoveredCellState, () => cell.IsClickable && cell.IsHovered() && !cell.HasShot)
            .AddTransition(hoveredCellState, emptyState, () => cell.IsClickable && !cell.IsHovered())
            .AddTransition(hoveredCellState, missedShotCellState, () => cell.IsClickable && cell.IsHovered() && cell.HasShot && !cell.HasShip)
            .AddTransition(hoveredCellState, missedShotCellState, () => cell.IsClickable && !cell.IsHovered() && cell.HasShot && !cell.HasShip)
            .AddTransition(hoveredCellState, destroyedShipCellState, () => cell.IsClickable && cell.IsHovered() && cell.HasShot && cell.HasShip)
            .AddTransition(hoveredCellState, destroyedShipCellState, () => cell.IsClickable && !cell.IsHovered() && cell.HasShot && cell.HasShip)
            .AddTransition(emptyState, missedShotCellState, () => !cell.HasShip && cell.HasShot);
    }
}