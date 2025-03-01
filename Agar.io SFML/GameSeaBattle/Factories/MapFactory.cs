using Agar.io_SFML.Configs;
using Agar.io_SFML.Engine;
using SFML.Graphics;
using SFML.System;

namespace Agar.io_SFML.GameSeaBattle;

public class MapFactory : ActorFactory
{
    private AnimatorFactory _animatorFactory;

    private int _fieldWidth;
    private int _fieldHeight;
    
    private GameType _gameType;

    public MapFactory()
    {
        _animatorFactory = new AnimatorFactory();
        
        _fieldWidth = SeaBattleFieldConfig.Width;
        _fieldHeight = SeaBattleFieldConfig.Height;

        _gameType = Service<GameType>.Get;
    }

    public Field CreateField(bool isHumanField, bool isFieldOfStartingPlayer)
    {
        Field field = new Field();

        field.NeedsUpdateInteract = _gameType switch
        {
            GameType.PVP => true,
            GameType.PVE when isHumanField => false,
            GameType.PVE => true,
            GameType.EVE => false,
        };
        
        field.IsInteractable = !isFieldOfStartingPlayer;

        (field.NeedsUpdateVisibility, field.AreShipsVisible) = _gameType switch
        {
            GameType.PVP => (true, !isFieldOfStartingPlayer),
            GameType.PVE => (false, isHumanField),
            GameType.EVE => (false, true),
        };

        Cell[,] initialCells = new Cell[_fieldHeight, _fieldWidth];
        
        for(int y = 0; y < _fieldHeight; y++)
        {
            for (int x = 0; x < _fieldWidth; x++)
            {
                initialCells[y,x] = CreateCell(field, x, y);
            }
        }
        
        field.Initialize(initialCells);
        
        field.TryUpdate();
        
        field.SetCellsClickable(!isFieldOfStartingPlayer && (isHumanField || (!isHumanField && _gameType == GameType.PVE)));

        return field;
    }

    private Cell CreateCell(Field field, int columnX, int rowY)
    {
        Cell cell = CreateActor<Cell>();
        
        RectangleShape cellShape = new RectangleShape() 
        {
            Size = new(50, 50),
        };
        
        cell.shape = cellShape;

        cellShape.Position = new(cellShape.Size.X * columnX + columnX * 10, cellShape.Size.Y * rowY + rowY * 10);
        
        cellShape.Position += new Vector2f(SeaBattleWindowConfig.WindowWidth / 16f, SeaBattleWindowConfig.WindowHeight / 8f);
        
        cell.Initialize(cellShape);
        
        _animatorFactory.CreateCellAnimator(cell, field);

        return cell;
    }
}