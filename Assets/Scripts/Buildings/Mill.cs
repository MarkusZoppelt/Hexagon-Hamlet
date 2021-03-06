﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mill : Building
{
    public int bonusForBakery = 10;
    public int bonusForBrewery = -10;

    public override int CalculateProduction(Tile tile)
    {
        List<Tile> tilesInRange = tile.GetAllTilesAround(collectionRange);
        int collectedFunds = baseProduction;

        foreach(var tempTile in tilesInRange)
        {
            if(tempTile.placedBuilding == null)
            {
                continue;
            }

            if(tempTile.placedBuilding is Brewery)
            {
                Brewery brewery = tempTile.placedBuilding as Brewery;
                collectedFunds += brewery.bonusForMill;
            }

            if(tempTile.placedBuilding is Field)
            {
                Field field = tempTile.placedBuilding as Field;
                collectedFunds += field.bonusForMill;
            }
        }

        return collectedFunds;
    }

    internal override int CalculateProductionChanges(Building newNeighbour, Tile neighbourTile)
    {
        int change = 0;

        if(newNeighbour is Brewery)
        {
            Brewery brewery = newNeighbour as Brewery;
            change += brewery.bonusForMill;
        }

        if(newNeighbour is Field)
        {
            Field field = newNeighbour as Field;
            change += field.bonusForMill;
        }

        if(neighbourTile.placedBuilding != null)
        {
            if(neighbourTile.placedBuilding is Brewery)
            {
                Brewery brewery = neighbourTile.placedBuilding as Brewery;
                change -= brewery.bonusForMill;
            }

            if(neighbourTile.placedBuilding is Field)
            {
                Field field = neighbourTile.placedBuilding as Field;
                change -= field.bonusForMill;
            }
        }

        return change;
    }
}
