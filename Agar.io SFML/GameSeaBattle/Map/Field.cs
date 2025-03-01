﻿using Agar.io_SFML.Configs;

namespace Agar.io_SFML.GameSeaBattle;

public class Field
{
    public bool NeedsUpdateVisibility;
    public bool NeedsUpdateInteract;
    
    public bool AreShipsVisible;
    public bool IsInteractable;


    private int _width;
    private int _height;

    private Cell[,] _cells;
    
    private List<string> _ships = new(10) { "####", "###", "###", "##", "##", "##", "#", "#", "#", "#" };

    public Field()
    {
        _width = SeaBattleFieldConfig.Width;
        _height = SeaBattleFieldConfig.Height;
    }

    public void TryUpdate()
    {
        if (NeedsUpdateInteract)
            SetCellsClickable(!IsInteractable);
        
        if(NeedsUpdateVisibility)
            AreShipsVisible = !AreShipsVisible;
        
    }

    public Cell GetCell(int x, int y)
        => _cells[y, x];
    
    public void Initialize(Cell[,] cells)
    {
        _cells = cells;
        
        foreach (string ship in _ships)
        {
            bool isHorizontal = GetRandomState();
            bool isPlaced = false;

            int i = 0;
            
            while (!isPlaced)
            {
                if (i > 150)
                {
                    break;
                }

                TryPlaceShip(isHorizontal, ship, out isPlaced);
                
                i++;
            }
        }
        
        SetCellsClickable(false);
    }

    public void SetCellsClickable(bool isClickable)
    {
        for(int y = 0; y < _height; y++)
        {
            for (int x = 0; x < _width; x++)
            {
                _cells[y,x].IsClickable = isClickable;
            }
        }
        
        IsInteractable = isClickable;
    }

    private void TryPlaceShip(bool isHorizontal, string ship, out bool isPlaced)
    {
        (int x, int y) randomCell = GetRandomCell();
        
        if (isHorizontal && CanPlaceHorizontal(ship.Length, randomCell))
        {
            PlaceHorizontal(ship.Length, randomCell);
            isPlaced = true;
            return;
        }
        
        if(!isHorizontal && CanPlaceVerticalShip(ship.Length, randomCell))
        {
            PlaceVertical(ship.Length, randomCell);
            isPlaced = true;
            return;
        }
        
        isPlaced = false;
    }
    
    public (int x, int y) GetRandomCell()
    {
        int x = MyRandom.Next(0, _width);
        int y = MyRandom.Next(0, _height);
        
        return (x, y);
    }
    
    private bool CanPlaceHorizontal(int shipLength, (int x, int y)randomCell)
    {
        for (int i = 0; i < shipLength; i++)
        {
            if (randomCell.x + i >= _width || _cells[randomCell.y, randomCell.x + i].HasShip)
            {
                return false;
            }
        }
        return true;
    }
    
    private void PlaceHorizontal(int shipLength, (int x, int y)randomCell)
    {
        for (int i = 0; i < shipLength; i++)
        {
            _cells[randomCell.y, randomCell.x + i].HasShip = true;
        }
    }
    
    private bool CanPlaceVerticalShip(int shipLength, (int x, int y)randomCell)
    {
        for (int i = 0; i < shipLength; i++)
        {
            if (randomCell.y + i >= _height || _cells[randomCell.y + i, randomCell.x].HasShip)
            {
                return false;
            }
        }

        return true;
    }
    
    private void PlaceVertical(int shipLength, (int x, int y)randomCell)
    {
        for (int i = 0; i < shipLength; i++)
        {
            _cells[randomCell.y + i, randomCell.x].HasShip = true;
        }
    }

    private bool GetRandomState()
        => MyRandom.Next(0, 2) == 1;
}