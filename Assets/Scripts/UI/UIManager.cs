﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    [SerializeField] private TileDescription tileDescription;
    [SerializeField] private BuildingDescription buildingDescription;

    private void Awake() 
    {
        if(instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
    }

    public bool IsBuildingDescriptionDisplayed
    {
        get { return buildingDescription.gameObject.activeInHierarchy; }
    }

    public void ShowTileDescription(TileDescriptionData data) 
    {
        if(data == null) 
        {
            tileDescription?.Hide();
            return;
        }
        tileDescription?.Show();
        tileDescription?.SetData(data);
    }

    public void HideTileDescription()
    {
        tileDescription?.Hide();
    }

    public void ShowBuildingDescription(Building building, bool includeProduction = true) 
    {
        if(building.descriptionData == null) 
        {
            buildingDescription.Hide();
            return;
        }

        buildingDescription.Show(building, includeProduction);
    }

    public void HideBuildingDescription() 
    {
        buildingDescription.Hide();
    }
}
