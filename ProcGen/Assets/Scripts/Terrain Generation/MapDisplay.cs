//Displays the height map and applies it to the 3D mesh

using UnityEngine;
using System.Collections;

public class MapDisplay : MonoBehaviour {

    public Renderer textureRender;
    public MeshFilter meshFilter;
    public MeshRenderer meshRenderer;
    public MeshCollider meshCollider;
    public GameObject meshObject;
    public static Texture2D meshTexture;
    public bool drawnMesh = false;


    public Renderer islandTextureRender;
    public MeshFilter islandMeshFilter;
    public MeshRenderer islandMeshRenderer;
    public MeshCollider islandMeshCollider;
    public GameObject islandMeshObject;
    public static Texture2D islandMeshTexture;
    public bool drawnIslands = false;


    void Awake()
    {
        meshRenderer.sharedMaterial.mainTexture = meshTexture;
        islandMeshRenderer.sharedMaterial.mainTexture = islandMeshTexture;
    }

    public void DrawTexture(Texture2D texture)
    {        
        textureRender.sharedMaterial.mainTexture = texture;
        textureRender.transform.localScale = new Vector3(texture.width, 1, texture.height);
    }

    public void DrawIslandTexture(Texture2D texture)
    {
        islandTextureRender.sharedMaterial.mainTexture = texture;
        islandTextureRender.transform.localScale = new Vector3(texture.width, 1, texture.height);
    }
    public void DrawMesh(MeshData meshData)
    {
        meshFilter.sharedMesh = meshData.CreateMesh();
        meshCollider.sharedMesh = meshFilter.sharedMesh;
        drawnMesh = true;
    }

    public void DrawIsland(MeshData meshData)
    {
        islandMeshFilter.sharedMesh = meshData.CreateMesh();
        islandMeshCollider.sharedMesh = islandMeshFilter.sharedMesh;
        drawnIslands = true;
    }
}
