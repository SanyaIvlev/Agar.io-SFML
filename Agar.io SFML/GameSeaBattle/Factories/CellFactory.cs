using Agar.io_SFML.Configs;
using SFML.Graphics;
using SFML.System;

namespace Agar.io_SFML.GameSeaBattle;

public class CellFactory : ActorFactory
{
    public Cell CreateCell(int columnX, int rowY)
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

        return cell;
    }
}