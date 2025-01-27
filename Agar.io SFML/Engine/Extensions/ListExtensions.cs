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

    public static Controller FindNearestController(this List<Controller> controllers, Controller comparingController)
    {
        Controller nearestController = null;
        float minimalSquaredDistance = float.MaxValue;

        foreach (var controller in controllers)
        {
            if(comparingController == controller) 
                continue;

            var currentPlayer = controller.ControlledPlayer;
            var comparingPlayer = comparingController.ControlledPlayer;
            
            Vector2f currentPlayerPosition = currentPlayer.Position;
            var comparingPlayerPosition = comparingPlayer.Position;

            float currentSquaredDistance = comparingPlayerPosition.GetSquaredDistanceTo(currentPlayerPosition);

            if (currentSquaredDistance < minimalSquaredDistance)
            {
                minimalSquaredDistance = currentSquaredDistance;
                nearestController = controller;
            }
        }
        
        return nearestController;
    }
}