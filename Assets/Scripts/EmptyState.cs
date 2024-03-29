using UnityEngine;
using UnityEngine.UI;

public class EmptyState : MergableState
{
    public EmptyState(Image image) : base(image)
    {
    }

    public override void Put()
    {
        throw new System.NotImplementedException();
    }
}
