using BoardSystem;
using CardSystem;
using GameSystem.Models;
using GameSystem.Models.MoveCommands;
using GameSystem.Views;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;

public class GameLoop : SingletonMonoBehavior<GameLoop>
{
    public event EventHandler Initalized;
    [SerializeField]
    private PositionHelper _positionHelper;
    public Board<IGamePiece> Board { get; } = new Board<IGamePiece>(3);

    HexenPiece _piece = new HexenPiece();

    private readonly Deck<MoveCommandBase> _deck = new Deck<MoveCommandBase>();
    Hand<MoveCommandBase> hand;
    private MoveCommandBase _draggedMoveCommand;

    private CardView _card;

    private List<Tile> _validTiles = new List<Tile>();

    private void ConnectViewsToModel()
    {
        var pieceViews = FindObjectsOfType<HexenPieceView>();
        foreach (var pieceView in pieceViews)
        {
            var worldPosition = pieceView.transform.position;
            var boardPosition = _positionHelper.ToBoardPosition(worldPosition);

            var tile = Board.TileAt(boardPosition);

            _piece = new HexenPiece();

            Board.Place(tile, _piece);
            pieceView.Model = _piece;
        }
    }

    //fixed

    internal void OnCardDragBegin(CardView cardView)
    {
        _card = cardView;
        _draggedMoveCommand = _deck.GetMoveCommand(cardView.Model);
    }

    internal void OnCardDropped(Tile hoverTile)
    {
        var tile = Board.TileOf(_piece);
        if (_draggedMoveCommand.ContainsTile(tile, hoverTile))
        {
            _draggedMoveCommand.OnDropTile(tile, hoverTile);
            hand.RemoveCard(_card.Model);
            hand.FillHand();
        }
    }

    internal void OnEnterTile(Tile hoverTile)
    {
        if (_draggedMoveCommand != null)
        {
            var tile = Board.TileOf(_piece);
            _validTiles = _draggedMoveCommand.OnHoverTile(tile, hoverTile);
            Board.HighLight(_validTiles);
        }
    }

    internal void OnExitTile(Tile hoverTile)
    {
        Board.Unhighlight(_validTiles);
        _validTiles.Clear();
    }

    private void Start()
    {
        Board<IGamePiece> board = Board;
        _deck.RegisterMoveCommand("ForwardAttack", new ForwardAttackMoveCommand(board));
        _deck.RegisterMoveCommand("SwipeAttack", new SwipeAttackMoveCommand(board));
        _deck.RegisterMoveCommand("Teleport", new TeleportMoveCommand(board));
        _deck.RegisterMoveCommand("Pushback", new PushbackMoveCommand(board));
        _deck.RegisterMoveCommand("Bomb", new BombMoveCommand(board));
        _deck.AddCard("ForwardAttack", 3);
        _deck.AddCard("SwipeAttack", 3);
        _deck.AddCard("Teleport", 3);
        _deck.AddCard("Pushback", 3);
        _deck.AddCard("Bomb", 3);
        hand = _deck.CreateHand(5);
        ConnectToDeck(hand);
        ConnectViewsToModel();
        StartCoroutine(OnPostStart());
    }

    private void ConnectToDeck(Hand<MoveCommandBase> hand)
    {
        FindObjectOfType<DeckView>().Model = hand;
    }

    private IEnumerator OnPostStart()
    {
        yield return new WaitForEndOfFrame();
        OnInitialized(EventArgs.Empty);
    }

    protected virtual void OnInitialized(EventArgs args)
    {
        EventHandler handler = Initalized;
        handler?.Invoke(this, args);
    }
}
