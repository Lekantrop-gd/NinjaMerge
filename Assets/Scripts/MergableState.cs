using UnityEngine.UI;

public abstract class MergableState
{
    private Image _image;

    public MergableState(Image image)
    {
        SetImage(image);
    }

    protected void SetImage(Image image)
    {
        _image.sprite = image.sprite;
    }

    public abstract void Take();
    public abstract void Put();

}
