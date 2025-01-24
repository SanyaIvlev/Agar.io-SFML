using SFML.System;

namespace Agar.io_SFML.Extensions;

public static class ListExtensions
{
    public static void SwapRemove<T>(this List<T> list, T removingObject)
    {
        int removingIndex = list.IndexOf(removingObject);

        if (removingIndex == -1)
            return;
        
        list[removingIndex] = list[^1];
        list.RemoveAt(list.Count - 1);
    }

    public static Player FindNearestPlayer(this List<Player> players, Player comparingPlayer)
    {
        Player nearestPlayer = null;
        float minimalSquaredDistance = float.MaxValue;

        foreach (var player in players)
        {
            if(comparingPlayer == player) 
                continue;
            
            Vector2f currentPlayerPosition = player.Position;

            var comparingPlayerPosition = comparingPlayer.Position;
            float currentSquaredDistance = comparingPlayerPosition.GetSquaredDistanceTo(currentPlayerPosition);

            if (currentSquaredDistance < minimalSquaredDistance)
            {
                minimalSquaredDistance = currentSquaredDistance;
                nearestPlayer = player;
            }
        }
        
        return nearestPlayer;
    }
}