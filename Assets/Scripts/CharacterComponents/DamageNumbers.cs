using UnityEngine;
using TMPro;
using System.Collections;

namespace Scripts.CharacterComponents
{
    public class DamageNumbers : MonoBehaviour
    {
        public TextMeshPro text;
        public float lifetime = 1f;
        public float floatSpeed = 1f;
        public Color normalColor = new Color(1f, 0.75f, 0f); // Gold-like color

        private float elapsedTime;
        private Vector3 startOffset;
        private float floatDirection;

        void Awake()
{
    text = GetComponentInChildren<TextMeshPro>();
    if (text == null)
    {
        Debug.LogError("TextMeshPro component is missing in DamageNumbers prefab!");
    }
}


        void Start()
        {
            if (text == null) return;

            // **Randomized spawn position offset**
            float xOffset = Random.Range(-0.5f, 0.5f);
            float yOffset = Random.Range(0f, 0.3f); // Higher spawn for more variation
            startOffset = new Vector3(xOffset, yOffset, 0);
            transform.position += startOffset;

            // **Randomized float direction** (left or right)
            floatDirection = Random.Range(-0.5f, 0.5f);

            // Start fade-out effect
            StartCoroutine(FadeOutAndDestroy());
        }

        void Update()
        {
            // **Move upwards while slightly drifting left or right**
            transform.position += (Vector3.up * floatSpeed + Vector3.right * floatDirection * 0.2f) * Time.deltaTime;
        }

        public void SetDamage(int damageAmount)
        {
            if (text == null)
            {
                Debug.LogError("TextMeshPro is NULL in SetDamage!");
                return;
            }

            Debug.Log($"Damage Set: {damageAmount}");
            text.text = damageAmount.ToString();
            text.color = normalColor;

            // **Scale effect**
            float scaleMultiplier = 1.5f;
            text.transform.localScale = Vector3.one * scaleMultiplier;
            text.ForceMeshUpdate(); // Force TMP to refresh the text
            
            StartCoroutine(ShrinkEffect());
        }

        private IEnumerator FadeOutAndDestroy()
        {
            float startAlpha = text.color.a;
            while (elapsedTime < lifetime)
            {
                elapsedTime += Time.deltaTime;
                float fadeAmount = 1 - (elapsedTime / lifetime);
                text.color = new Color(text.color.r, text.color.g, text.color.b, fadeAmount);
                yield return null;
            }
            Destroy(gameObject);
        }

        private IEnumerator ShrinkEffect()
        {
            float duration = 0.2f;
            float time = 0;
            Vector3 startScale = text.transform.localScale;
            Vector3 endScale = Vector3.one;

            while (time < duration)
            {
                text.transform.localScale = Vector3.Lerp(startScale, endScale, time / duration);
                time += Time.deltaTime;
                yield return null;
            }
            text.transform.localScale = endScale;
        }
    }
}
