using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using System.Threading;
using UnityEngine.UIElements;

// cara penggunaan : 
// 1. Assign sprite pada tempatnya masing-masing
// 2. Assign Rigidbody2D ke sprite meteor dan pastikan gravitynya 0
// 3. Assign Rigidbody2D ke tempatnya di script
// 4. Assign Circlecollider2D ke sprite meteor
// 5. Isi data yang diperlukan
// 6. Catatan penting : pastikan sprite meteor yang pertama diletakkan di bawah kamera untuk memberikan waktu untuk scriptnya untuk jalan

public class meteor : MonoBehaviour
{
    // Pengumpulan data dari user

    // Input sprite meteor
    public GameObject spriteMeteor;
    // Input rigidbody meteor, pastikan gravity = 0
    public Rigidbody2D rigidBodyMeteor;
    // Input posisi awal kamera
    public Camera spriteCameraUtama;
    public float posisiAwalKamera = 0f;
    // Input sprite player
    public GameObject spritePlayer;
    // Input jarak spawn meteor ke kamera dalam sumbu x
    public float jarakKeKamera;
    // Input waktu (dalam detik) untuk spawn satu meteor
    public float waktuPerMeteor;
    // Input dimensi y (dalam hands-on dimensi y adalah size kamera) sebagai batas random sumbu y meteor
    public float yDimensiMinimum;
    public float yDimensiMaksimum;
    // Input kecepatan meteor
    public float kecepatanMeteor;
    // Input waktu sebelum meteor dihapus (destroy)
    public float waktuDestroyMeteor;
    // Bool untuk memastikan spawn meteor terkontrol dan tidak terjadi meteor shower
    private bool meteorAktif = false;
    
    // Start is called before the first frame update
    // Untuk spawn meteor
    IEnumerator ir()
    {
        while (true)
        {
        yield return new WaitForSeconds(waktuPerMeteor);
            // Memastikan bahwa tidak terjadi duplikasi meteor sehingga terjadi meteor shower
            if (!meteorAktif)
            {
                // Mengakses data posisi kamera
                float posisiAwalKamera = spriteCameraUtama.transform.position.x;
                
                // Mengatur posisi y meteor
                float y = UnityEngine.Random.Range(yDimensiMinimum,yDimensiMaksimum);
                Vector2 pos = new Vector2(posisiAwalKamera+jarakKeKamera, y);

                // Membuat (spawn) meteor
                Instantiate(spriteMeteor, pos, Quaternion.identity);

                // Menggerakkan meteor
                rigidBodyMeteor.velocity = Vector2.left*kecepatanMeteor;
                
                // Marking bahwa ada meteor yang masih aktif
                meteorAktif = true;

                // Destroy meteor
                Destroy(spriteMeteor, waktuDestroyMeteor);
                yield return new WaitForSeconds(waktuDestroyMeteor);
                meteorAktif = false;
            }
        }
    }
    void Start()
    // Start coroutine IEnumerator
    {
        StartCoroutine(ir());
    }

    // Update is called once per frame
    void Update()
    {
        // Semua fungsi spawn sudah ditanggung oleh coroutine
    }

    // Fungsi untuk collision terhadap player
    void OnCollisionEnter(Collision spritePlayer)
    {
        Destroy(spritePlayer.gameObject);
    }
}


