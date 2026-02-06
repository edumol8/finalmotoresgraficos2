using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class MaterialFlash : MonoBehaviour
{
    private Renderer _renderer;
    private Color _originalColor;
    private MaterialPropertyBlock _materialPropertyBlock;

    private void Awake()
    {
        _renderer = GetComponentInChildren<Renderer>();

        _originalColor = _renderer.material.color;
        _materialPropertyBlock = new MaterialPropertyBlock();
    }

    public void StartFlash(float flashDuration, Color flashColor, float flashesPerSecond)
    {
        StopAllCoroutines();
        StartCoroutine(FlashCoroutine(flashDuration, flashColor, flashesPerSecond));
    }

    private IEnumerator FlashCoroutine(float flashDuration, Color flashColor, float flashesPerSecond)
    {
        float elapsedFlashTime = 0;
        float elapsedFlashPercentage = 0;
        float numberOfFlashes = flashDuration * flashesPerSecond;

        while (elapsedFlashPercentage < 1)
        {
            elapsedFlashPercentage = elapsedFlashTime / flashDuration;

            _renderer.GetPropertyBlock(_materialPropertyBlock);

            _materialPropertyBlock.SetColor("_BaseColor", Color.Lerp(_originalColor, flashColor, Mathf.PingPong(elapsedFlashPercentage * 2 * numberOfFlashes, 1)));

            _renderer.SetPropertyBlock(_materialPropertyBlock);

            yield return null;
            elapsedFlashTime += Time.deltaTime;
        }

        _materialPropertyBlock.SetColor("_BaseColor", _originalColor);
    }
}


