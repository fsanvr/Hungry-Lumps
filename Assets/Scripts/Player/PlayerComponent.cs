using System;
using DG.Tweening;
using UnityEngine;

public class PlayerComponent : InitializableBehaviour
{
    private AnimationController _controller;

    private readonly float _rotateDuration = 0.02f;
    private float _moveDuration;
    private Sequence _rotateSequence;
    private Sequence _moveSequence;

    public void Move(Cell cell)
    {
        var dx = cell.position.x - transform.position.x;
        if (dx != 0.0f)
        {
            RotateX(Math.Sign(dx), _rotateDuration);
        }
        Move(cell.position, _moveDuration);
    }
    
    private void RotateX(float sign, float duration)
    {
        _rotateSequence?.Kill();
        _rotateSequence = DOTween.Sequence().Append(
            transform
                .DOScaleX(sign, duration)
                .SetEase(Ease.InOutBounce)
                .SetLink(this.gameObject)
        );
    }

    private void Move(Vector2 position, float duration)
    {
        _controller.StartMoveAnimation();
        _moveSequence?.Kill();
        _moveSequence = DOTween.Sequence().Append(
            transform
                .DOMove(position, duration)
                .SetEase(Ease.InOutQuad)
                .SetLink(this.gameObject)
                .OnComplete(_controller.EndMoveAnimation)
        );
    }

    protected override void MyInit(LevelData data)
    {
        _controller = GetComponent<AnimationController>();
        _moveDuration = data.pet.moveComponent.moveData.GetMoveDuration();

        var startPosition = data.Grid.StartPosition;
        var playerSpawnPoint = new Vector3(startPosition.x, startPosition.y, 0);
        SetPosition(playerSpawnPoint);
    }

    private void SetPosition(Vector3 position)
    {
        transform.position = position;
    }
}