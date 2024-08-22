using UnityEngine;

public class Parallax : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;

    private float _singleTextureHeight;

    private void Start()
    {
        SetUpTexture();
    }

    private void SetUpTexture()
    {
        Sprite sprite = GetComponentInChildren<SpriteRenderer>().sprite;
        _singleTextureHeight = sprite.texture.height / sprite.pixelsPerUnit;
    }

    private void Update()
    {
        Scroll();
        CheckReset();
    }

    private void Scroll()
    {
        float newYPosition = _moveSpeed * Time.deltaTime;
        transform.position += new Vector3(0f, newYPosition, 0f);
    }

    private void CheckReset()
    {
        if ((Mathf.Abs(transform.position.y) - _singleTextureHeight * 2) > 0)
        {
            transform.position = new Vector3(transform.position.x, 0f, 0f);
        }
    }
}
