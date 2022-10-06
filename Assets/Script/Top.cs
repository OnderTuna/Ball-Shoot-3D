using UnityEngine;

public class Top : MonoBehaviour
{
    Rigidbody rb;
    Renderer renk;
    public GameManager _GameManager;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        renk = GetComponent<Renderer>(); /*Topun materyaline erisip rengine ulas.*/
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Kova"))
        {
            //_GameManager.TopEfektiOlustur(gameObject.transform.position, renk.material.color); /*Kendi konumunda olusturmak istersek sadece transform, carpistigi nesne konumu ise other.transform yap.*/
            //gameObject.transform.localPosition = Vector3.zero; /*Local pozisyon almamizin nedeni bu objeler cocuk obje*/
            //gameObject.transform.localRotation = Quaternion.Euler(Vector3.zero);
            //rb.velocity = Vector3.zero; /*Rigidbody komponentini de sýfýrla. Bu sayede top istenilen bicimde hareket etsin.*/
            //rb.angularVelocity = Vector3.zero;
            //gameObject.SetActive(false);

            CarpismaTetikle();
            _GameManager.TopGirdi();
            _GameManager.RenkDegis();
        }

        if (other.gameObject.CompareTag("Kaybol"))
        {
            //_GameManager.TopEfektiOlustur(gameObject.transform.position, renk.material.color);
            //gameObject.transform.localPosition = Vector3.zero;
            //gameObject.transform.localRotation = Quaternion.Euler(Vector3.zero);
            //rb.velocity = Vector3.zero;
            //rb.angularVelocity = Vector3.zero;
            //gameObject.SetActive(false);

            CarpismaTetikle();
            _GameManager.TopGirmedi();
        }
    }

    void CarpismaTetikle()
    {
        _GameManager.TopEfektiOlustur(gameObject.transform.position, renk.material.color); /*Kendi konumunda olusturmak istersek sadece transform, carpistigi nesne konumu ise other.transform yap.*/
        gameObject.transform.localPosition = Vector3.zero; /*Local pozisyon almamizin nedeni bu objeler cocuk obje*/
        gameObject.transform.localRotation = Quaternion.Euler(Vector3.zero);
        rb.velocity = Vector3.zero; /*Rigidbody komponentini de sýfýrla. Bu sayede top istenilen bicimde hareket etsin.*/
        rb.angularVelocity = Vector3.zero;
        gameObject.SetActive(false);
    }
}
