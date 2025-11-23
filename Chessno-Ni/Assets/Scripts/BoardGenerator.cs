using UnityEngine;

public class BoardGenerator : MonoBehaviour
{
    // Zmienne publiczne widoczne w Inspectorze
    public GameObject squarePrefab;
    public Material lightMaterial;
    public Material darkMaterial;
    public float squareSize = 1f; // Rozmiar naszego pola to 1x1

    private const int BOARD_SIZE = 8;
    private char[] files = { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H' };

    void Start()
    {
        GenerateBoard();
    }

    void GenerateBoard()
    {
        // Pętla iterująca po rzędach (rank)
        for (int rank = 1; rank <= BOARD_SIZE; rank++)
        {
            // Pętla iterująca po kolumnach (file)
            for (int file = 0; file < BOARD_SIZE; file++)
            {
                // 1. Obliczanie pozycji (start od -3.5, żeby środek był w 0,0)
                float xPos = (file * squareSize) - 3.5f * squareSize;
                float zPos = (rank * squareSize) - 4.5f * squareSize;
                Vector3 position = new Vector3(xPos, 0f, zPos);

                // 2. Tworzenie obiektu pola
                GameObject square = Instantiate(squarePrefab, position, Quaternion.identity);
                square.transform.parent = transform; // Ustawienie Szachownicy jako rodzica w Hierarchy

                // 3. Nadawanie nazwy (np. A1, B2)
                string name = files[file].ToString() + rank.ToString();
                square.name = name;

                // 4. Przypisywanie odpowiedniego koloru
                Renderer rend = square.GetComponent<Renderer>();
                
                // Sprawdza, czy pole jest jasne czy ciemne (wzór szachownicy)
                bool isLight = (file + rank) % 2 == 0; 
                rend.material = isLight ? lightMaterial : darkMaterial;
            }
        }
    }
}