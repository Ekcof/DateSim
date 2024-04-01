using UnityEngine;

public class ClearCircleController : MonoBehaviour
{
    [SerializeField] private RectTransform _canvasRectTransform;
    [SerializeField] private Material _spotlightMaterial;
    [SerializeField] private RectTransform _imageRectTransform;
    [SerializeField] private float _cursorSizeX = 5f;
    [SerializeField][Range(0f, 1f)] private float _minAlpha = 0.8f;
    [SerializeField] private float _enlargeCoef = 2f;
    private bool _isEnabled;
    private float _cursorSizeY;


    private void Awake()
    {
        GetTheCurrentSizeForShader();
        _isEnabled = true;
    }

    private void OnDisable()
    {
        _isEnabled = false;
    }

    private void OnEnable()
    {
        GetTheCurrentSizeForShader();
        _isEnabled = true;
    }

    private void Update()
    {
        if (_isEnabled)
        {
            ChangeSharedCenter();
        }
    }

    private void GetTheCurrentSizeForShader()
    {
        Texture texture = _spotlightMaterial.mainTexture;
        if (_imageRectTransform.rect.width > _imageRectTransform.rect.height)
        {
            _cursorSizeX = _imageRectTransform.rect.width * texture.width / _canvasRectTransform.rect.width / 2 * _enlargeCoef;
            _cursorSizeY = _cursorSizeX * _canvasRectTransform.rect.width / _canvasRectTransform.rect.height;
        }
        else
        {
            _cursorSizeY = _imageRectTransform.rect.height * texture.height / _canvasRectTransform.rect.height / 2 * _enlargeCoef;
            _cursorSizeX = _cursorSizeY * _canvasRectTransform.rect.height / _canvasRectTransform.rect.width;
        }
    }

    private void ChangeSharedCenter()
    {
        if (_spotlightMaterial == null)
        {
            Debug.LogAssertion("Lack of material");
            return;
        }

        Vector2 localCursor;
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(_canvasRectTransform, Input.mousePosition, null, out localCursor))
        {
            Vector2 normalizedPoint = Rect.PointToNormalized(_canvasRectTransform.rect, localCursor);
            _spotlightMaterial.SetVector("_SpotCenter", new Vector4(normalizedPoint.x, normalizedPoint.y, 0, 0));
        }

        if (_spotlightMaterial.GetFloat("_CursorSizeX") != _cursorSizeX)
        {
            _spotlightMaterial.SetFloat("_CursorSizeX", _cursorSizeX);
        }
        if (_spotlightMaterial.GetFloat("_CursorSizeY") != _cursorSizeY)
        {
            _spotlightMaterial.SetFloat("_CursorSizeY", _cursorSizeY);
        }
        if (_spotlightMaterial.GetFloat("_minAlpha") != _minAlpha)
        {
            _spotlightMaterial.SetFloat("_minAlpha", _minAlpha);
        }
    }
}