using UnityEngine;
using System.Collections.Generic;

public class CubeSpawner : MonoBehaviour
{
    public GameObject cubePrefab; // Küp prefabı

    void Start()
    {
        // Dosyadan spawn pozisyonlarını okuyun
        List<Vector3> spawnPositions = ReadSpawnPositions();

        if (spawnPositions == null)
        {
            Debug.LogError("Spawn pozisyonları okunamadı!");
            return;
        }

        // Belirtilen sayıda ve pozisyonlarda küpleri oluşturun
        for (int i = 0; i < spawnPositions.Count; i++)
        {
            // Küpü spawnlayın
            GameObject cube = Instantiate(cubePrefab, spawnPositions[i], Quaternion.identity);
        }
    }

    // Dosyadan spawn pozisyonlarını okuyan fonksiyon
    List<Vector3> ReadSpawnPositions()
    {
        List<Vector3> positions = new List<Vector3>();

        // Dosyayı yükle
        TextAsset jsonFile = Resources.Load<TextAsset>("level0");

        // Dosya yüklenemediyse hata ver ve null dön
        if (jsonFile == null)
        {
            Debug.LogError("spawn_positions.json dosyası yüklenemedi!");
            return null;
        }

        // JSON verisini dönüştürün
        SpawnPositionsData data = JsonUtility.FromJson<SpawnPositionsData>(jsonFile.text);

        // JSON verisi null ise hata ver ve null dön
        if (data == null)
        {
            Debug.LogError("JSON verisi okunamadı!");
            return null;
        }

        // Pozisyonları listeye ekle
        foreach (var pos in data.positions)
        {
            positions.Add(new Vector3(pos.x, pos.y, pos.z));
        }

        return positions;
    }
}

[System.Serializable]
public class SpawnPositionsData
{
    public List<SpawnPosition> positions;
}

[System.Serializable]
public class SpawnPosition
{
    public float x;
    public float y;
    public float z;
}
