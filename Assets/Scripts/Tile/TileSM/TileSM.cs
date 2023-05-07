using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileSM
{
    private TileBaseState currentTileState = null;
    private TileService tileService;
    private TileEmptyState emptyState;
    private TileSingleState singleState;
    private TileDoubleState doubleState;
    private TileTripleState tripleState;

    public TileSM() {
        emptyState = new TileEmptyState(this);
        singleState = new TileSingleState(this);
        doubleState = new TileDoubleState(this);
        tripleState = new TileTripleState(this);
    }

    public void SetTileService(TileService _tileService) {
        tileService = _tileService;
    }

    public TileService GetTileService() {
        return tileService;
    }

    public void SwitchState(TileState tileState) {
        TileBaseState newTileBaseState = GetTileBaseStateFromEnum(tileState);
        if (currentTileState == newTileBaseState) {
            return;
        } else if (currentTileState != null) {
            currentTileState.OnStateExit();
        }
        currentTileState = newTileBaseState;
        currentTileState.OnStateEnter();
    }

    private TileBaseState GetTileBaseStateFromEnum(TileState tileState)
    {
        if (tileState == TileState.SINGLE) {
            return singleState;
        } else if (tileState == TileState.DOUBLE) {
            return doubleState;
        } else if (tileState == TileState.TRIPLE) {
            return tripleState;
        } else if (tileState == TileState.EMPTY) {
            return emptyState;
        } else {
            return null;
        }
    }

    public void TileStateUpdate() {
        if (currentTileState != null)
            currentTileState.OnStateUpdate();
    }
}
