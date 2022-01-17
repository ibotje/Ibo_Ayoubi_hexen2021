﻿using BoardSystem;
using GameSystem.Models;
using GameSystem.BoardQueries;
using System;
using System.Collections.Generic;

namespace GameSystem.Models.MoveCommands
{
    [MoveCommandName("Bomb")]
    public class BombMoveCommand : MoveCommandBase
    {
        private readonly Board<IGamePiece> _board;

        private readonly BoardRadiusMovement _moveArea;

        private Position centerTile;

        public BombMoveCommand(Board<IGamePiece> board)
        {
            _moveArea = new BoardRadiusMovement(board);
            _board = board;
            centerTile = new Position(0, 0, 0);
        }

        public override void OnDropTile(Tile playerTile, Tile hoverTile)
        {
            List<Tile> tiles = OnHoverTile(playerTile, hoverTile);
            foreach (Tile tile in tiles)
            {
                if (_board.PieceAt(tile) == null)
                    continue;
                _board.Take(tile);
            }
        }

        public override List<Tile> OnHoverTile(Tile playerTile, Tile hoverTile)
        {
            Tile center = _board.TileAt(centerTile);
            List<Tile> tiles = _moveArea.Radius(center, 1);
            tiles.AddRange(_moveArea.Radius(center, 2));
            tiles.AddRange(_moveArea.Radius(center, 3));
            tiles.AddRange(_moveArea.Radius(center, 0));

            if (!tiles.Contains(hoverTile))
                return tiles;

            int num = tiles.IndexOf(hoverTile);

            //int num1 = ModuloCalc(num - 1, tiles.Count - 1);
            //int num2 = ModuloCalc(num + 1, tiles.Count - 1);
            //return new List<Tile>() { tiles[num], tiles[num], tiles[num] };
            List<Tile> bombTiles = _moveArea.Radius(hoverTile, 1);
            bombTiles.Add(hoverTile);
            return bombTiles;
        }
        private int ModuloCalc(int number, int mod)
        {
            return (number % mod + mod) % mod;
        }

        public override bool ContainsTile(Tile playerTile, Tile hoverTile)
        {
            List<Tile> tiles = OnHoverTile(playerTile, hoverTile);
            if (!tiles.Contains(hoverTile))
                return false;
            return true;
        }
    }
}