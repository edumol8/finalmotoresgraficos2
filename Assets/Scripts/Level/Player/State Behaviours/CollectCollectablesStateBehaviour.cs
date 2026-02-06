using UnityEngine;

public class CollectCollectablesStateBehaviour : StateBehaviour
{
    private CollisionController _collisionController;

    public CollectCollectablesStateBehaviour(CollisionController collisionController)
    {
        _collisionController = collisionController;
    }

    public override void Enter(float transitionDuration)
    {
        _collisionController.OnEnterTrigger += OnEnterTrigger;
    }

    public override void Exit()
    {
        _collisionController.OnEnterTrigger -= OnEnterTrigger;
    }

    private void OnEnterTrigger(Collider other)
    {
        var collectable = other.GetComponent<Collectable>();

        if (collectable != null)
        {
            collectable.Collect(_collisionController.gameObject);
        }
    }
}