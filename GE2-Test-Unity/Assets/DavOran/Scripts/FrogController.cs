using UnityEngine;
using System.Collections;

public class FrogController : MonoBehaviour
{
    public Transform[] lilyPads; 
    public float jumpHeight = 2f; 
    public float jumpDuration = 1f; 

    private int currentLilyPadIndex = 0;
    private float frogHeightOffset; 

    void Start()
    {
        frogHeightOffset = transform.position.y - lilyPads[0].position.y;

        StartCoroutine(JumpBetweenLilyPads());
    }

    IEnumerator JumpBetweenLilyPads()
    {
        while (true)
        {
            currentLilyPadIndex = (currentLilyPadIndex + 1) % lilyPads.Length;

            Vector3 startPosition = transform.position;
            Vector3 targetPosition = lilyPads[currentLilyPadIndex].position;
            targetPosition.y += frogHeightOffset;
            float elapsedTime = 0f;

            while (elapsedTime < jumpDuration)
            {
                float t = elapsedTime / jumpDuration;
                Vector3 nextPosition = Vector3.Lerp(startPosition, targetPosition, t);
                nextPosition.y += jumpHeight * Mathf.Sin(Mathf.PI * t);
                transform.position = nextPosition;

                elapsedTime += Time.deltaTime;
                yield return null;
            }

            transform.position = targetPosition;

            yield return new WaitForSeconds(1f);
        }
    }
}
