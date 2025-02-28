using Agar.io_SFML.Configs;
using Agar.io_SFML.Engine.Scene;
using SFML.System;

namespace Agar.io_SFML.GameSeaBattle;

public class SeaBattleGame : Scene
{
    private Field _field;
    private Field _field2;
    
    public override void Start()
    {
        CreateFields();

        AdjustRightFieldPosition();
    }

    private void AdjustRightFieldPosition()
    {
        int windowWidth = SeaBattleWindowConfig.WindowWidth;
        
        int fieldWidth = SeaBattleFieldConfig.Width;
        int fieldHeight = SeaBattleFieldConfig.Height;
        
        for (int y = 0; y < fieldHeight; y++)
        {
            for (int x = 0; x < fieldWidth; x++)
            {
                var cell = _field2.GetCell(x, y);
                Vector2f position = cell.GetPosition();
                Vector2f newPosition = new(position.X + windowWidth / 2f, position.Y);
                cell.SetPosition(newPosition);
            }
        }
    }

    private void CreateFields()
    {
        _field = new Field();
        _field.Generate();
        
        _field2 = new Field();
        _field2.Generate();
    }

    public override void Update()
    {
        
    }
}