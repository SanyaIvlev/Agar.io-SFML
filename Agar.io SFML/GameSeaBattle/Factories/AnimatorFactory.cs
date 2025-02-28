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
        _textureLoader = TextureLoader.Instance;
        
        _emptyCell = _textureLoader.LoadTexturesFrom("Cell", "Empty");
        _shipCell = _textureLoader.LoadTexturesFrom("Cell", "HasShip");
        _missedShotCell = _textureLoader.LoadTexturesFrom("Cell", "MissedShot");
        _destroyedShipCell = _textureLoader.LoadTexturesFrom("Cell", "DestroyedShip");
        _hoveredCell = _textureLoader.LoadTexturesFrom("Cell", "Hovered");
    }

    public void CreateCellAnimator(Field field, Cell cell)
    {
        ShapeAnimator animator = CreateActor<ShapeAnimator>();
        
        State emptyState = new(_emptyCell, 300);
        State shipCellState = new(_shipCell, 300);
        State missedShotCellState = new(_missedShotCell, 300);
        State destroyedShipCellState = new(_destroyedShipCell, 300);
        State hoveredCellState = new(_hoveredCell, 300);

        animator
            .Initialize(cell.shape)
            .AddInitialState(emptyState)
            .AddTransition(emptyState, shipCellState, () => cell.HasShip && !field.IsInteractable)
            .AddTransition(shipCellState, emptyState, () => cell.HasShip && field.IsInteractable)
            .AddTransition(emptyState, destroyedShipCellState, () => cell.HasShip && field.IsInteractable && cell.HasShot)
            .AddTransition(shipCellState, destroyedShipCellState, () => cell.HasShip && !field.IsInteractable && cell.HasShot)
            .AddTransition(emptyState, hoveredCellState, () => field.IsInteractable && cell.IsHovered() && !cell.HasShot)
            .AddTransition(hoveredCellState, emptyState, () => field.IsInteractable && !cell.IsHovered())
            .AddTransition(hoveredCellState, missedShotCellState, () => field.IsInteractable && cell.IsHovered() && cell.HasShot && !cell.HasShip)
            .AddTransition(hoveredCellState, missedShotCellState, () => field.IsInteractable && !cell.IsHovered() && cell.HasShot && !cell.HasShip)
            .AddTransition(hoveredCellState, destroyedShipCellState, () => field.IsInteractable && cell.IsHovered() && cell.HasShot && cell.HasShip)
            .AddTransition(hoveredCellState, destroyedShipCellState, () => field.IsInteractable && !cell.IsHovered() && cell.HasShot && cell.HasShip)
            .AddTransition(emptyState, missedShotCellState, () => !cell.HasShip && cell.HasShot);
    }
}