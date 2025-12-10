using UnityEngine;

public class FullBasketCourtBuilder : MonoBehaviour
{
    public Material lineMaterial;
    public float lineWidth = 0.05f;

    // Basket sahasý ölçüleri (NBA)
    float courtWidth = 15f;      // yan çizgi arasý
    float courtLength = 28f;     // dip çizgi arasý
    float threePointRadius = 6.75f;
    float freeThrowRadius = 1.8f;
    float centerCircleRadius = 1.8f;

    void Start()
    {
        float halfW = courtWidth / 2f;
        float halfL = courtLength / 2f;

        // === SAHA SINIRLARI ===
        CreateLine("SidelineLeft", new Vector3(-halfW, 0.01f, -halfL), new Vector3(-halfW, 0.01f, halfL));
        CreateLine("SidelineRight", new Vector3(halfW, 0.01f, -halfL), new Vector3(halfW, 0.01f, halfL));
        CreateLine("BaselineBottom", new Vector3(-halfW, 0.01f, -halfL), new Vector3(halfW, 0.01f, -halfL));
        CreateLine("BaselineTop", new Vector3(-halfW, 0.01f, halfL), new Vector3(halfW, 0.01f, halfL));

        // === ORTA ÇÝZGÝ ===
        CreateLine("HalfCourt", new Vector3(-halfW, 0.01f, 0), new Vector3(halfW, 0.01f, 0));

        // === ORTA DAÝRE ===
        CreateCircle("CenterCircle", Vector3.zero, centerCircleRadius);

        // === SERBEST ATIÞ KUTUSU (ALT TARAF) ===
        CreateRectangle("FreeThrowBoxBottom", new Vector3(0, 0.01f, -halfL + 5.8f), 4.9f, 5.8f);
        CreateSemiCircle("FreeThrowSemiBottom", new Vector3(0, 0.01f, -halfL + 5.8f), freeThrowRadius, false);

        // === SERBEST ATIÞ KUTUSU (ÜST TARAF) ===
        CreateRectangle("FreeThrowBoxTop", new Vector3(0, 0.01f, halfL - 5.8f), 4.9f, 5.8f);
        CreateSemiCircle("FreeThrowSemiTop", new Vector3(0, 0.01f, halfL - 5.8f), freeThrowRadius, true);

        // === 3 SAYI ÇÝZGÝLERÝ ===
        CreateSemiCircle("ThreePointBottom", new Vector3(0, 0.01f, -halfL), threePointRadius, false);
        CreateSemiCircle("ThreePointTop", new Vector3(0, 0.01f, halfL), threePointRadius, true);

        // === RESTRICTED AREA (ÇEMBER ALTI) ===
        CreateSemiCircle("RestrictedBottom", new Vector3(0, 0.01f, -halfL + 1.25f), 1.25f, false);
        CreateSemiCircle("RestrictedTop", new Vector3(0, 0.01f, halfL - 1.25f), 1.25f, true);
    }

    void CreateLine(string name, Vector3 start, Vector3 end)
    {
        GameObject obj = new GameObject(name);
        obj.transform.parent = transform;

        LineRenderer lr = obj.AddComponent<LineRenderer>();
        lr.material = lineMaterial;
        lr.positionCount = 2;
        lr.startWidth = lr.endWidth = lineWidth;
        lr.SetPosition(0, start);
        lr.SetPosition(1, end);
        lr.numCapVertices = 4;
        lr.numCornerVertices = 4;
    }

    void CreateCircle(string name, Vector3 center, float radius)
    {
        int segments = 120;
        GameObject obj = new GameObject(name);
        obj.transform.parent = transform;

        LineRenderer lr = obj.AddComponent<LineRenderer>();
        lr.material = lineMaterial;
        lr.positionCount = segments + 1;
        lr.startWidth = lr.endWidth = lineWidth;

        for (int i = 0; i <= segments; i++)
        {
            float angle = i * (2 * Mathf.PI / segments);
            float x = center.x + radius * Mathf.Cos(angle);
            float z = center.z + radius * Mathf.Sin(angle);
            lr.SetPosition(i, new Vector3(x, 0.01f, z));
        }
    }

    void CreateSemiCircle(string name, Vector3 center, float radius, bool flipped)
    {
        int segments = 80;
        GameObject obj = new GameObject(name);
        obj.transform.parent = transform;

        LineRenderer lr = obj.AddComponent<LineRenderer>();
        lr.material = lineMaterial;
        lr.positionCount = segments + 1;
        lr.startWidth = lr.endWidth = lineWidth;

        for (int i = 0; i <= segments; i++)
        {
            float angle = (i * Mathf.PI / segments);
            if (!flipped) angle += Mathf.PI;

            float x = center.x + radius * Mathf.Cos(angle);
            float z = center.z + radius * Mathf.Sin(angle);
            lr.SetPosition(i, new Vector3(x, 0.01f, z));
        }
    }

    void CreateRectangle(string name, Vector3 center, float width, float height)
    {
        GameObject obj = new GameObject(name);
        obj.transform.parent = transform;

        float halfW = width / 2f;
        float halfH = height / 2f;

        CreateLine(name + "_Top", new Vector3(center.x - halfW, 0.01f, center.z + halfH),
                                     new Vector3(center.x + halfW, 0.01f, center.z + halfH));
        CreateLine(name + "_Bottom", new Vector3(center.x - halfW, 0.01f, center.z - halfH),
                                        new Vector3(center.x + halfW, 0.01f, center.z - halfH));
        CreateLine(name + "_Left", new Vector3(center.x - halfW, 0.01f, center.z - halfH),
                                      new Vector3(center.x - halfW, 0.01f, center.z + halfH));
        CreateLine(name + "_Right", new Vector3(center.x + halfW, 0.01f, center.z - halfH),
                                       new Vector3(center.x + halfW, 0.01f, center.z + halfH));
    }
}
