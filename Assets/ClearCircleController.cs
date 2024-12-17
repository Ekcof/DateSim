using UnityEngine;


/// <summary>
/// This script allows 
/// </summary>
public class ClearCircleController : MonoBehaviour
{
    [SerializeField] private RectTransform _canvasRectTransform;
    [SerializeField] private Material _spotlightMaterial;
    [SerializeField] private RectTransform _imageRectTransform;
    [SerializeField] private float _spotlightWidth = 5f;
    [SerializeField][Range(0f, 1f)] private float _minAlpha = 0.8f;
    [SerializeField] private float _enlargeCoef = 2f;
    private bool _isEnabled;
    private float _spotlightHeight;


    private void Awake()
    {
        CalculateSpotlightSize();
        _isEnabled = true;
    }

    private void OnDisable()
    {
        _isEnabled = false;
    }

    private void OnEnable()
    {
        CalculateSpotlightSize();
        _isEnabled = true;
    }

    private void Update()
    {
        if (_isEnabled)
        {
            ChangeSharedCenter();
        }
    }

    private void CalculateSpotlightSize()
    {
        Texture texture = _spotlightMaterial.mainTexture;
        if (_imageRectTransform.rect.width > _imageRectTransform.rect.height)
        {
            _spotlightWidth = _imageRectTransform.rect.width * texture.width / _canvasRectTransform.rect.width / 2 * _enlargeCoef;
            _spotlightHeight = _spotlightWidth * _canvasRectTransform.rect.width / _canvasRectTransform.rect.height;
        }
        else
        {
            _spotlightHeight = _imageRectTransform.rect.height * texture.height / _canvasRectTransform.rect.height / 2 * _enlargeCoef;
            _spotlightWidth = _spotlightHeight * _canvasRectTransform.rect.height / _canvasRectTransform.rect.width;
        }
    }

    private void ChangeSharedCenter()
    {
        if (_spotlightMaterial == null)
        {
            Debug.LogAssertion("Lack of material");
            return;
        }

        // Получаем позицию центра _imageRectTransform в мировых координатах
        Vector3 worldCenter = _imageRectTransform.position;
        // Преобразуем мировые координаты в координаты экрана
        Vector2 screenCenter = RectTransformUtility.WorldToScreenPoint(null, worldCenter);

        Vector2 localCursor;
        // Преобразуем координаты экрана в локальные координаты канваса
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(_canvasRectTransform, screenCenter, null, out localCursor))
        {
            Vector2 normalizedPoint = Rect.PointToNormalized(_canvasRectTransform.rect, localCursor);
            _spotlightMaterial.SetVector("_SpotCenter", new Vector4(normalizedPoint.x, normalizedPoint.y, 0, 0));
        }

        // Обновление размеров и минимальной альфы, если они изменились
        UpdateMaterialProperties();
    }

    private void UpdateMaterialProperties()
    {
        if (_spotlightMaterial.GetFloat("_CursorSizeX") != _spotlightWidth)
        {
            _spotlightMaterial.SetFloat("_CursorSizeX", _spotlightWidth);
        }
        if (_spotlightMaterial.GetFloat("_CursorSizeY") != _spotlightHeight)
        {
            _spotlightMaterial.SetFloat("_CursorSizeY", _spotlightHeight);
        }
        if (_spotlightMaterial.GetFloat("_minAlpha") != _minAlpha)
        {
            _spotlightMaterial.SetFloat("_minAlpha", _minAlpha);
        }
    }
}