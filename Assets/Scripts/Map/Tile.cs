﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public enum TileType {Ocean, Grassland, Forest, Mountain, Hand, None}

public class Tile : MonoBehaviour
{
    [Header("Set in Editor")]
    public TileType type;
    public GameObject hoverMarker = null;
    public TileDescriptionData descriptionData;

    [Header("Set during play, but visible for Debugging")]
    public Vector2 coordinates;
    public Map map;

    [HideInInspector]
    public Building placedBuilding = null;
    public bool RepresentsHand { get { return type == TileType.Hand; } }

    private SpriteRenderer hoverMarkerRenderer;

    public bool IsOccupied
    {
        get
        {
            return placedBuilding != null;
        }
    }

    private void Awake()
    {
        hoverMarkerRenderer = hoverMarker.GetComponent<SpriteRenderer>();
    }

#if UNITY_STANDALONE
    private void OnMouseEnter()
    {
        if(!Helpers.IsMouseOverUI())
        {
            hoverMarker.SetActive(true);
            UIManager.instance?.ShowTileDescription(descriptionData);

            if(placedBuilding != null) 
            {
                UIManager.instance?.ShowBuildingDescription(placedBuilding);
            }
            else
            {
                UIManager.instance?.HideBuildingDescription();
            }
        }
        else
        {
            hoverMarker.SetActive(false);
            UIManager.instance?.HideTileDescription();
        }

        PlacementController.instance?.UpdateCardProduction(this);
        PlacementController.instance?.IndicateProductionChanges(this);
        PlacementController.instance?.UpdateInvalidTileIndicator(this);
        hoverMarkerRenderer.color = PlacementController.instance != null ? PlacementController.instance.GetMarkerColor(this) : hoverMarkerRenderer.color;
    }
#else
    public void OnMouseEnter()
    {
        if (Helpers.IsMouseOverUI())
        {
            return;
        }

        hoverMarker.SetActive(true);
        hoverMarkerRenderer.color = PlacementController.instance != null ? PlacementController.instance.GetMarkerColor(this) : hoverMarkerRenderer.color;
        PlacementController.instance?.PlaceSelectedCardNextToTile(this);
        PlacementController.instance?.UpdateCardProduction(this);
        PlacementController.instance?.IndicateProductionChanges(this);
        PlacementController.instance?.UpdateInvalidTileIndicator(this);
    }
#endif

    private void OnMouseExit()
    {
        hoverMarker.SetActive(false);
    }

    public void PlaceBuilding(Building building)
    {
        building.transform.position = transform.position;
        building.transform.SetParent(transform);

        this.placedBuilding = building;
        building.PlaceOn(this);
    }

    public void Free()
    {
        this.placedBuilding = null;
    }

    public List<Tile> GetAllTilesAround(int maxDistance)
    {
        return map.GetAllTilesAround(this, maxDistance);
    }
}
