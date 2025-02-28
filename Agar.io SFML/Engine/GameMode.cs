using Agar.io_SFML.Engine;

namespace Agar.io_SFML;

public class GameMode
{
    public bool IsGameEnded;

    public GameMode()
    {
        Service<GameMode>.Set(this);
    }
}