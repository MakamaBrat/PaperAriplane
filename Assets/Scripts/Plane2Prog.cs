using UnityEngine;

public class Plane2Prog : MonoBehaviour
{
    public MenuTravel menuTrave;
    public AudioSource au;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag=="Bad")
        {
            menuTrave.makeMenu(3);
            au.Play();
        }
    }
}
