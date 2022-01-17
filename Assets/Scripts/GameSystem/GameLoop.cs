using BoardSystem;
using CardSystem;
using GameSystem.GameStates;
using GameSystem.Models;
using GameSystem.MoveCommands;
using GameSystem.Views;
using StateSystem;
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

    [SerializeField]
    private GameObject _menu;

    [SerializeField]
    private GameObject _startButton;

    [SerializeField]
    private GameObject _gameOver;

    [SerializeField]
    private GameObject _deck;

    public Board<IGamePiece> Board { get; } = new Board<IGamePiece>(3);

    public HexenPiece Piece = new HexenPiece();

    public readonly Deck<MoveCommandBase> Deck = new Deck<MoveCommandBase>();
    public Hand<MoveCommandBase> Hand;

    private StateMachine<GameStateBase> _gameStateMachine;

    private void ConnectViewsToModel()
    {
        var pieceViews = FindObjectsOfType<HexenPieceView>();
        foreach (var pieceView in pieceViews)
        {
            var worldPosition = pieceView.transform.position;
            var boardPosition = _positionHelper.ToBoardPosition(worldPosition);

            var tile = Board.TileAt(boardPosition);

            Piece = new HexenPiece();

            Board.Place(tile, Piece);
            pieceView.Model = Piece;
        }
        var tileViews = FindObjectsOfType<TileView>();
        foreach (var tileView in tileViews)
        {
            var worldPosition = tileView.transform.position;
            var boardPosition = _positionHelper.ToBoardPosition(worldPosition);

            var tile = Board.TileAt(boardPosition);

            Board.Set(tile);
        }
    }
    public void OnCardDragBegin(CardView cardView)
    => _gameStateMachine.CurrentState.BeginDrag(cardView);

    public void OnEnteredTile(Tile hoverTile)
        => _gameStateMachine.CurrentState.EnteredTile(hoverTile);

    public void OnExitedTile(Tile hoverTile)
    => _gameStateMachine.CurrentState.ExitedTile(hoverTile);
    public void OnDropped(Tile hoverTile)
        => _gameStateMachine.CurrentState.Dropped(hoverTile);

    private void Start()
    {
        Board<IGamePiece> board = Board;
        Deck.RegisterMoveCommand("ForwardAttack", new ForwardAttackMoveCommand(board));
        Deck.RegisterMoveCommand("SwipeAttack", new SwipeAttackMoveCommand(board));
        Deck.RegisterMoveCommand("Teleport", new TeleportMoveCommand(board));
        Deck.RegisterMoveCommand("Pushback", new PushbackMoveCommand(board));
        Deck.RegisterMoveCommand("Bomb", new BombMoveCommand(board));
        Deck.AddCard("ForwardAttack", 3);
        Deck.AddCard("SwipeAttack", 3);
        Deck.AddCard("Teleport", 3);
        Deck.AddCard("Pushback", 3);
        Deck.AddCard("Bomb", 3);
        Hand = Deck.CreateHand(5);
        ConnectToDeck(Hand);
        ConnectViewsToModel();

        _gameStateMachine = new StateMachine<GameStateBase>();
        _gameStateMachine.Register(GameStateBase.StartState, new StartGameState(_gameStateMachine, _menu , _startButton, _deck));
        _gameStateMachine.Register(GameStateBase.PlayingState, new PlayingGameState(_gameStateMachine));
        _gameStateMachine.Register(GameStateBase.GameOverState, new GameOverGameState(_gameStateMachine, _menu, _gameOver));

        _gameStateMachine.InitialState = GameStateBase.StartState;

        StartCoroutine(OnPostStart());
    }

    public void Started()
=> _gameStateMachine.CurrentState.Started();
    public void Ended()
=> _gameStateMachine.CurrentState.Ended();

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
