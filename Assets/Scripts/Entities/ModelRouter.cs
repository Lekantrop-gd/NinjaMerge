using UnityEngine;

public class ModelRouter : MonoBehaviour
{
    [SerializeField] private Transform _headRoot;
    [SerializeField] private Transform _faceRoot;
    [SerializeField] private Transform _hairRoot;
    [SerializeField] private Transform _hatRoot;
    [SerializeField] private Transform _leftHandRoot;
    [SerializeField] private Transform _rightHandRoot;

    public Transform HeadRoot => _headRoot;
    public Transform FaceRoot => _faceRoot;
    public Transform HairRoot => _hairRoot;
    public Transform HatRoot => _hatRoot;
    public Transform LeftHandRoot => _leftHandRoot;
    public Transform RightHandRoot => _rightHandRoot;
}
