using UnityEngine;

public class DishWellMesh : MonoBehaviour
{
    void Start()
    {
    Mesh mesh = new Mesh();
    GetComponent<MeshFilter>().mesh = mesh;
    
    float height = 1f;
    int nbSides = 24;
    
    // Outer wall is at radius + thick / 2, inner wall at radius - thick / 2
    // http://wiki.unity3d.com/index.php/ProceduralPrimitives#C.23_-_Torus
    float radius = 5.1f;
    float thick = .2f; 
    float minus = radius - thick * .5f;
    float plus = radius + thick * .5f;
    
    int nbVerticesCap = nbSides * 2 + 2;
    int nbVerticesSides = nbSides * 2 + 2;
    #region Vertices
    
    // bottom + top + sides
    Vector3[] vertices = new Vector3[nbVerticesCap * 2 + nbVerticesSides * 2];
    int vert = 0;
    float _2pi = Mathf.PI * 2f;
    
    // Bottom cap
    int sideCounter = 0;
    while( vert < nbVerticesCap )
    {
        sideCounter = sideCounter == nbSides ? 0 : sideCounter;
    
        float r1 = (float)(sideCounter++) / nbSides * _2pi;
        float cos = Mathf.Cos(r1);
        float sin = Mathf.Sin(r1);

        vertices[vert] = new Vector3( cos * minus, 0f, sin * minus);
        vertices[vert+1] = new Vector3( cos * plus, 0f, sin * plus);
        vert += 2;
    }
    
    // Top cap
    sideCounter = 0;
    while( vert < nbVerticesCap * 2 )
    {
        sideCounter = sideCounter == nbSides ? 0 : sideCounter;
    
        float r1 = (float)(sideCounter++) / nbSides * _2pi;
        float cos = Mathf.Cos(r1);
        float sin = Mathf.Sin(r1);
        vertices[vert] = new Vector3( cos * minus, height, sin * minus);
        vertices[vert+1] = new Vector3( cos * plus, height, sin * plus);
        vert += 2;
    }
    
    // Sides (out)
    sideCounter = 0;
    while (vert < nbVerticesCap * 2 + nbVerticesSides )
    {
        sideCounter = sideCounter == nbSides ? 0 : sideCounter;
    
        float r1 = (float)(sideCounter++) / nbSides * _2pi;
        float cos = Mathf.Cos(r1);
        float sin = Mathf.Sin(r1);
        float cosPlus = cos * plus;
        float sinPlus = sin * plus;
    
        vertices[vert] = new Vector3(cosPlus, height, sinPlus);
        vertices[vert + 1] = new Vector3(cosPlus, 0, sinPlus);
        vert+=2;
    }
    
    // Sides (in)
    sideCounter = 0;
    while (vert < vertices.Length )
    {
        sideCounter = sideCounter == nbSides ? 0 : sideCounter;
    
        float r1 = (float)(sideCounter++) / nbSides * _2pi;
        float cos = Mathf.Cos(r1);
        float sin = Mathf.Sin(r1);
        float cosMinus = cos * minus;
        float sinMinus = sin * minus;
    
        vertices[vert] = new Vector3(cosMinus, height, sinMinus);
        vertices[vert + 1] = new Vector3(cosMinus, 0, sinMinus);
        vert += 2;
    }
    #endregion
    
    #region Normales
    
    // bottom + top + sides
    Vector3[] normales = new Vector3[vertices.Length];
    vert = 0;
    
    // Bottom cap
    while( vert < nbVerticesCap )
    {
        normales[vert++] = Vector3.down;
    }
    
    // Top cap
    while( vert < nbVerticesCap * 2 )
    {
        normales[vert++] = Vector3.up;
    }
    
    // Sides (out)
    sideCounter = 0;
    while (vert < nbVerticesCap * 2 + nbVerticesSides )
    {
        sideCounter = sideCounter == nbSides ? 0 : sideCounter;
    
        float r1 = (float)(sideCounter++) / nbSides * _2pi;
    
        normales[vert] = new Vector3(Mathf.Cos(r1), 0f, Mathf.Sin(r1));
        normales[vert+1] = normales[vert];
        vert+=2;
    }
    
    // Sides (in)
    sideCounter = 0;
    while (vert < vertices.Length )
    {
        sideCounter = sideCounter == nbSides ? 0 : sideCounter;
    
        float r1 = (float)(sideCounter++) / nbSides * _2pi;
    
        normales[vert] = -(new Vector3(Mathf.Cos(r1), 0f, Mathf.Sin(r1)));
        normales[vert+1] = normales[vert];
        vert+=2;
    }
    #endregion
    
    #region UVs
    Vector2[] uvs = new Vector2[vertices.Length];
    
    vert = 0;
    // Bottom cap
    sideCounter = 0;
    while( vert < nbVerticesCap )
    {
        float t = (float)(sideCounter++) / nbSides;
        uvs[ vert++ ] = new Vector2( 0f, t );
        uvs[ vert++ ] = new Vector2( 1f, t );
    }
    
    // Top cap
    sideCounter = 0;
    while( vert < nbVerticesCap * 2 )
    {
        float t = (float)(sideCounter++) / nbSides;
        uvs[ vert++ ] = new Vector2( 0f, t );
        uvs[ vert++ ] = new Vector2( 1f, t );
    }
    
    // Sides (out)
    sideCounter = 0;
    while (vert < nbVerticesCap * 2 + nbVerticesSides )
    {
        float t = (float)(sideCounter++) / nbSides;
        uvs[ vert++ ] = new Vector2( t, 0f );
        uvs[ vert++ ] = new Vector2( t, 1f );
    }
    
    // Sides (in)
    sideCounter = 0;
    while (vert < vertices.Length )
    {
        float t = (float)(sideCounter++) / nbSides;
        uvs[ vert++ ] = new Vector2( t, 0f );
        uvs[ vert++ ] = new Vector2( t, 1f );
    }
    #endregion
    
    #region Triangles
    int nbFace = nbSides * 4;
    int nbTriangles = nbFace * 2;
    int nbIndexes = nbTriangles * 3;
    int[] triangles = new int[nbIndexes];
    
    // Bottom cap
    int i = 0;
    sideCounter = 0;
    while (sideCounter < nbSides)
    {
        int current = sideCounter * 2;
        int next = sideCounter * 2 + 2;
    
        triangles[ i++ ] = next + 1;
        triangles[ i++ ] = next;
        triangles[ i++ ] = current;
    
        triangles[ i++ ] = current + 1;
        triangles[ i++ ] = next + 1;
        triangles[ i++ ] = current;
    
        sideCounter++;
    }
    
    // Top cap
    while (sideCounter < nbSides * 2)
    {
        int current = sideCounter * 2 + 2;
        int next = sideCounter * 2 + 4;
    
        triangles[ i++ ] = current;
        triangles[ i++ ] = next;
        triangles[ i++ ] = next + 1;
    
        triangles[ i++ ] = current;
        triangles[ i++ ] = next + 1;
        triangles[ i++ ] = current + 1;
    
        sideCounter++;
    }
    
    // Sides (out)
    while( sideCounter < nbSides * 3 )
    {
        int current = sideCounter * 2 + 4;
        int next = sideCounter * 2 + 6;
    
        triangles[ i++ ] = current;
        triangles[ i++ ] = next;
        triangles[ i++ ] = next + 1;
    
        triangles[ i++ ] = current;
        triangles[ i++ ] = next + 1;
        triangles[ i++ ] = current + 1;
    
        sideCounter++;
    }
    
    
    // Sides (in)
    while( sideCounter < nbSides * 4 )
    {
        int current = sideCounter * 2 + 6;
        int next = sideCounter * 2 + 8;
    
        triangles[ i++ ] = next + 1;
        triangles[ i++ ] = next;
        triangles[ i++ ] = current;
    
        triangles[ i++ ] = current + 1;
        triangles[ i++ ] = next + 1;
        triangles[ i++ ] = current;
    
        sideCounter++;
    }
    #endregion
    
    mesh.vertices = vertices;
    mesh.normals = normales;
    mesh.uv = uvs;
    mesh.triangles = triangles;
    
    mesh.RecalculateBounds();
    ;
    }
}
