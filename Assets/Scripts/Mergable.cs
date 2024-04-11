using UnityEngine;

public class Mergable : MonoBehaviour
{
    [SerializeField] private Mergable _superior;

    public Mergable Superior => _superior;
}
