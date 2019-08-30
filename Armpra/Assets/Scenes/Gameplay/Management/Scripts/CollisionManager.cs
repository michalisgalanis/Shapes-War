using UnityEngine;

public class CollisionManager : MonoBehaviour {
    public void Awake() {
        Debug.Log(this + "Awoken!");
        Physics2D.IgnoreLayerCollision(8, 5);   //Background Particles & UI
        Physics2D.IgnoreLayerCollision(8, 8);   //Background Particles & Background Particles
        Physics2D.IgnoreLayerCollision(8, 9);   //Background Particles & Shield
        Physics2D.IgnoreLayerCollision(8, 10);  //Background Particles & Projectiles
        Physics2D.IgnoreLayerCollision(8, 12);  //Background Particles & Enemy
        Physics2D.IgnoreLayerCollision(8, 13);  //Background Particles & Player
        Physics2D.IgnoreLayerCollision(8, 14);  //Background Particles & Shockwave
        Physics2D.IgnoreLayerCollision(8, 15);  //Background Particles & Powerups
        Physics2D.IgnoreLayerCollision(9, 14);  //Shield & Shockwave
        Physics2D.IgnoreLayerCollision(12, 15); //Enemy & Powerups
        Physics2D.IgnoreLayerCollision(13, 14); //Player & Shockwave
    }
}
