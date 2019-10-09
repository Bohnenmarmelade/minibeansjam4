using UnityEngine;

public class SnapToPixelGrid : MonoBehaviour
{
	private int pixelsPerUnit = 16;
    public Vector3 overallDisplacement = Vector3.zero;
    public Vector3 lastFramelocalPosition;
    public Vector3 startlocalPosition;

    private void Start()
    {
        startlocalPosition = transform.localPosition;
        lastFramelocalPosition = transform.localPosition;
        SpriteRenderer spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        //if (spriteRenderer)
        //    pixelsPerUnit = (int)spriteRenderer.sprite.pixelsPerUnit;
    }

    private void LateUpdate()
    {
        Vector3 displacementThisFrame = lastFramelocalPosition - transform.localPosition;
        //Debug.Log("DISPLACEMENT THIS FRAME: " + displacementThisFrame);
        overallDisplacement += displacementThisFrame;

        Vector3 newlocalPosition = startlocalPosition + overallDisplacement;
        newlocalPosition.x = Mathf.Round(newlocalPosition.x * pixelsPerUnit) / pixelsPerUnit;
        newlocalPosition.y = Mathf.Round(newlocalPosition.y * pixelsPerUnit) / pixelsPerUnit;

        lastFramelocalPosition = transform.localPosition;
        transform.localPosition = newlocalPosition;
	}

    public Vector3 GetRealPosition()
    {
        return startlocalPosition + overallDisplacement;
    }
}