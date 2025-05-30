using UnityEngine;

public class ObstructionMask : MonoBehaviour
{
    // public Tilemap tilemap;
    public Transform player;
    public float radius = 1.5f;
    public Vector3 offset;
    
    public LayerMask obstructionLayer;
    public Material targetMaterial;
    public Transform cameraTransform;

    private bool wasObstructed = false;
    
    void Update()
    {
        targetMaterial.SetVector("_PlayerPos", player.position + offset);
        
        // Check if there's an obstruction between player and camera
        // var direction = cameraTransform.position - player.position;
        // var hit = Physics2D.Raycast(player.position, direction.normalized, direction.magnitude, obstructionLayer);
        //
        // var isObstructed = hit.collider;
        //
        // targetMaterial.SetVector("_PlayerPos", player.position);
        // targetMaterial.SetFloat("_Radius", isObstructed ? radius : 0f);
        
        // if (isObstructed || (!isObstructed && wasObstructed))
        // {
        //     targetMaterial.SetVector("_PlayerPos", player.position);
        //     targetMaterial.SetFloat("_Radius", isObstructed ? radius : 0f);
        // }
        //
        // wasObstructed = isObstructed;
    }
}