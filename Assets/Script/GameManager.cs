using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("Top Ayarlari")]
    [SerializeField] private GameObject[] Toplar;
    [SerializeField] private GameObject FirePoint;
    [SerializeField] private float TopGucu;
    int AktifTopIndex; /*Bu sayede liste icerisinde rahat dolas.*/
    [SerializeField] private Animator TopAtar;
    [SerializeField] private ParticleSystem TopEfekt;
    [SerializeField] private ParticleSystem[] SayiOldu;
    //int AktifTopEfektiIndex;
    [SerializeField] private AudioSource[] TopSesleri;
    int AktifTopSesEfektiIndex;

    [Header("Level Ayarlari")]
    [SerializeField] private int HedefTopSayisi; /*Level'i tamamlamak icin skor sayisi.*/
    [SerializeField] private int MevcutTopSayisi;
    int GirenTopSayisi; /*Kovaya skor olan top sayisi. Bu sayede slider etkileþim.*/
    [SerializeField] private Slider LevelSlider;
    [SerializeField] private TextMeshProUGUI KalanTopSayisi_Text;

    [Header("Panel Ayarlari")]
    [SerializeField] private GameObject[] Paneller;
    [SerializeField] private TextMeshProUGUI YildizSayisi;
    [SerializeField] private TextMeshProUGUI KazandinLevelSayisi;
    [SerializeField] private TextMeshProUGUI KaybettinLevelSayisi;

    [Header("Diger Ayarlar")]
    [SerializeField] private Renderer SeffafKova;
    float KovaninTabanDegeri;
    float KovaDolmasiIcýnGerekenHerTillingMiktari;
    [SerializeField] private AudioSource[] Sesler;
    string _sahneAdi;

    void Start()
    {
        KovaninTabanDegeri = .5f;
        KovaDolmasiIcýnGerekenHerTillingMiktari = .25f / HedefTopSayisi; /*Kova texture'nýn tamamlanmasi icin her basarili atista ilerlemesi gereken tilling miktari.*/
        AktifTopSesEfektiIndex = 0;
        LevelSlider.maxValue = HedefTopSayisi; /*Bu sayede her basarili atista slider degeri oku.*/
        KalanTopSayisi_Text.text = MevcutTopSayisi.ToString();
        _sahneAdi = SceneManager.GetActiveScene().name;
    }

    void Update()
    {
        //if(Time.timeScale != 0) /*Oyun bitince timescale kontrol et ki paneller esnasýnda oyun oynanmasin.*/
        //{
        //    if (Input.GetKeyDown(KeyCode.Space))
        //    {
        //        //GameObject tp = Instantiate(Top, FirePoint.transform.position, Quaternion.identity);
        //        //tp.GetComponent<Rigidbody>().AddForce(tp.transform.TransformDirection(90, 90, 0) * TopGucu, ForceMode.Force); /*Topa yön verip güç uygulamak için rigidbodysine eriþtik.*/

        //        //    foreach (var item in Toplar)
        //        //    {
        //        //        if (!item.activeInHierarchy)
        //        //        {
        //        //            item.transform.SetPositionAndRotation(FirePoint.transform.position, FirePoint.transform.rotation);
        //        //            item.SetActive(true);
        //        //            item.GetComponent<Rigidbody>().AddForce(item.transform.TransformDirection(90, 90, 0) * TopGucu, ForceMode.Force);
        //        //            break;
        //        //        }
        //        //    }

        //        Toplar[AktifTopIndex].transform.SetPositionAndRotation(FirePoint.transform.position, FirePoint.transform.rotation);
        //        Toplar[AktifTopIndex].SetActive(true);
        //        Toplar[AktifTopIndex].GetComponent<Rigidbody>().AddForce(Toplar[AktifTopIndex].transform.TransformDirection(90, 90, 0) * TopGucu, ForceMode.Force);
        //        if (Toplar.Length - 1 == AktifTopIndex)
        //        {
        //            AktifTopIndex = 0;
        //        }
        //        else
        //        {
        //            AktifTopIndex++; /*Bir sonraki topu aktif etmek icin gerekli.*/
        //        }

        //        MevcutTopSayisi--;
        //        KalanTopSayisi_Text.text = MevcutTopSayisi.ToString();

        //        TopAtar.Play("TopAtar");
        //        TopEfekt.Play();
        //        Sesler[2].Play();
        //    }
        //}
    }

    public void TopGirdi()
    {
        GirenTopSayisi++;
        LevelSlider.value = GirenTopSayisi;

        //foreach (var item in TopSesleri)
        //{
        //    if (!item.gameObject.activeInHierarchy)
        //    {
        //        item.gameObject.SetActive(true);
        //        item.Play();
        //        break;
        //    }
        //}

        TopSesleri[AktifTopSesEfektiIndex].Play();
        if (TopSesleri.Length - 1 == AktifTopSesEfektiIndex)
        {
            AktifTopSesEfektiIndex = 0;
        }
        else
        {
            AktifTopSesEfektiIndex++;
        }

        if (GirenTopSayisi == HedefTopSayisi)
        {
            Time.timeScale = 0f;
            PlayerPrefs.SetInt("Level", PlayerPrefs.GetInt("Level") + 1);
            PlayerPrefs.SetInt("Yildiz", PlayerPrefs.GetInt("Yildiz") + 15);
            YildizSayisi.text = PlayerPrefs.GetInt("Yildiz").ToString();

            //KazandinLevelSayisi.text = "LEVEL: " + SceneManager.GetActiveScene().name;

            KazandinLevelSayisi.text = "LEVEL: " + _sahneAdi;
            Paneller[1].SetActive(true);
            Sesler[1].Play();
        }

        int sayi = 0; /*Son olarak ayni anda 2 top attik ve mevcuttopsayisi 0 oldu ama aktif topumuz var. Oyun bitti. Ama devam etmesi gerek. Bunun kontrolu.*/
        foreach (var item in Toplar)
        {
            if(item.activeInHierarchy)
            {
                sayi++;
            }
        }
        if(sayi==0)
        {
            if (MevcutTopSayisi == 0 && GirenTopSayisi != HedefTopSayisi)
            {
                //KaybettinLevelSayisi.text = "LEVEL: " + SceneManager.GetActiveScene().name;

                //KaybettinLevelSayisi.text = "LEVEL: " + _sahneAdi;
                //Paneller[2].SetActive(true);
                //Sesler[0].Play();

                Kaybettin();
            }

            if ((MevcutTopSayisi + GirenTopSayisi) < HedefTopSayisi)
            {
                //KaybettinLevelSayisi.text = "LEVEL: " + SceneManager.GetActiveScene().name;

                //KaybettinLevelSayisi.text = "LEVEL: " + _sahneAdi;
                //Paneller[2].SetActive(true);
                //Sesler[0].Play();

                Kaybettin();
            }
        }
    }

    public void TopGirmedi()
    {
        int sayi = 0;
        foreach (var item in Toplar)
        {
            if (item.activeInHierarchy)
            {
                sayi++;
            }
        }
        if (sayi == 0)
        {
            if (MevcutTopSayisi == 0)
            {
                //KaybettinLevelSayisi.text = "LEVEL: " + SceneManager.GetActiveScene().name;

                //KaybettinLevelSayisi.text = "LEVEL: " + _sahneAdi;
                //Paneller[2].SetActive(true);
                //Sesler[0].Play();

                Kaybettin();
            }

            if ((MevcutTopSayisi + GirenTopSayisi) < HedefTopSayisi)
            {
                //KaybettinLevelSayisi.text = "LEVEL: " + SceneManager.GetActiveScene().name;

                //KaybettinLevelSayisi.text = "LEVEL: " + _sahneAdi;
                //Paneller[2].SetActive(true);
                //Sesler[0].Play();

                Kaybettin();
            }
        }
    }

    public void OyunuDurdur()
    {
        Paneller[0].SetActive(true);
        Time.timeScale = 0f;
    }

    public void PanellerIcýnButtonIslemleri(string islem)
    {
        switch (islem)
        {
            case "Resume":
                Paneller[0].SetActive(false);
                Time.timeScale = 1f;
                break;
            case "Quit":
                Application.Quit();
                break;
            case "Settings":

                break;
            case "Restart":
                Time.timeScale = 1f;
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                break;
            case "Next":
                Time.timeScale = 1f;
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                break;
        }
    }

    public void TopEfektiOlustur(Vector3 Pozisyon, Color TopRengi)
    {
        foreach (var item in SayiOldu)
        {
            if (!item.gameObject.activeInHierarchy)
            {
                item.gameObject.SetActive(true);
                item.transform.position = Pozisyon;
                var main = item.main; /*Efekt komponentindeki startcolor'a eris.*/
                main.startColor = TopRengi;
                item.GetComponent<ParticleSystem>().Play();
                break;
            }
        }

        //SayiOldu[AktifTopEfektiIndex].transform.position = Pozisyon.position;
        //var main = SayiOldu[AktifTopEfektiIndex].main;
        //main.startColor = TopRengi;
        //SayiOldu[AktifTopEfektiIndex].gameObject.SetActive(true);
        //SayiOldu[AktifTopEfektiIndex].GetComponent<ParticleSystem>().Play();
        //if (SayiOldu.Length - 1 == AktifTopEfektiIndex)
        //{
        //    AktifTopEfektiIndex = 0;
        //}
        //else
        //{
        //    AktifTopEfektiIndex++; /*Bir sonraki topu aktif etmek icin gerekli.*/
        //}
    }

    public void RenkDegis()
    {
        KovaninTabanDegeri -= KovaDolmasiIcýnGerekenHerTillingMiktari;
        SeffafKova.material.SetTextureScale("_MainTex", new Vector2(1f, KovaninTabanDegeri)); /*Bu sayede bir materyaldeki texture tilling degerine eris.*/
    }

    void Kaybettin()
    {
        Time.timeScale = 0f;
        KaybettinLevelSayisi.text = "LEVEL: " + _sahneAdi;
        Paneller[2].SetActive(true);
        Sesler[0].Play();
    }

    public void TopAt()
    {
        if (Time.timeScale != 0) /*Oyun bitince timescale kontrol et ki paneller esnasýnda oyun oynanmasin.*/
        {            
                //GameObject tp = Instantiate(Top, FirePoint.transform.position, Quaternion.identity);
                //tp.GetComponent<Rigidbody>().AddForce(tp.transform.TransformDirection(90, 90, 0) * TopGucu, ForceMode.Force); /*Topa yön verip güç uygulamak için rigidbodysine eriþtik.*/

                //    foreach (var item in Toplar)
                //    {
                //        if (!item.activeInHierarchy)
                //        {
                //            item.transform.SetPositionAndRotation(FirePoint.transform.position, FirePoint.transform.rotation);
                //            item.SetActive(true);
                //            item.GetComponent<Rigidbody>().AddForce(item.transform.TransformDirection(90, 90, 0) * TopGucu, ForceMode.Force);
                //            break;
                //        }
                //    }

                Toplar[AktifTopIndex].transform.SetPositionAndRotation(FirePoint.transform.position, FirePoint.transform.rotation);
                Toplar[AktifTopIndex].SetActive(true);
                Toplar[AktifTopIndex].GetComponent<Rigidbody>().AddForce(Toplar[AktifTopIndex].transform.TransformDirection(90, 90, 0) * TopGucu, ForceMode.Force);
                if (Toplar.Length - 1 == AktifTopIndex)
                {
                    AktifTopIndex = 0;
                }
                else
                {
                    AktifTopIndex++; /*Bir sonraki topu aktif etmek icin gerekli.*/
                }

                MevcutTopSayisi--;
                KalanTopSayisi_Text.text = MevcutTopSayisi.ToString();

                TopAtar.Play("TopAtar");
                TopEfekt.Play();
                Sesler[2].Play();           
        }
    }
}
