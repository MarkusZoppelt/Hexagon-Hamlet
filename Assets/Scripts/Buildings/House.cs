﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class House : Building
{
    public int malusForNonHouse = 1;
    public int TotalProduction
    {
        get
        {
            int total = baseProduction;
            List<Tile> tilesInRange = locationTile.GetAllTilesAround(1);
            foreach(Tile tile in tilesInRange)
            {
                if(tile.placedBuilding == null)
                {
                    continue;
                }

                if(tile.placedBuilding.GetType().Name == "House")
                {
                    continue;
                }

                total -= malusForNonHouse;
            }

            return total;
        }
    }

    public override void Produce()
    {
        Treasury.instance.AddMoney(TotalProduction);
    }
}