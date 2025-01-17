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
}