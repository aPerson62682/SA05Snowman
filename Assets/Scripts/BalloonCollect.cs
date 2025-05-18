using UnityEngine;

public class BalloonCollect : MonoBehaviour
{
    private bool collected = false;
    public bool isBlack = false; // New flag

    public string balloonSide;

    void OnTriggerEnter(Collider other)
    {
        if (collected) return;

        if (other.CompareTag("Player"))
        {
            collected = true;
            Debug.Log("Balloon Collected!");

            if (isBlack)
            {
                PlayerScore.instance.AddPoint(-3); // Subtract 3
                AudioManager.instance.PlaySFX("BlackCollect");
            }
            else
            {
                PlayerScore.instance.AddPoint(1);
                AudioManager.instance.PlaySFX("YellowCollect");
                AudioManager.instance.PlaySFX("YellowCollect2");
            }

            BalloonManager.instance.RemoveBalloon(balloonSide);
            Destroy(gameObject);
        }
    }
}