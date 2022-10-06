using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SilindirYonet : MonoBehaviour, IPointerDownHandler, IPointerUpHandler /*Tiklama islemlerini yakalamak icin gereken eventlar.*/
{
    bool ButtonPressed;
    public GameObject Silindir;
    [SerializeField] private float DonusCapi;
    [SerializeField] private string Yon;

    /*Surekli olan basilmayi algilamak istiyoruz.*/
    public void OnPointerDown(PointerEventData eventData)
    {
        //Debug.Log("Basiyorum.");
        ButtonPressed = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        //Debug.Log("Biraktim.");
        ButtonPressed = false;
    }

    void Update()
    {
        if (ButtonPressed)
        {
            if (Yon == "Left")
            {
                Silindir.transform.Rotate(new Vector3(0f, DonusCapi * Time.deltaTime, 0f), Space.Self);
            }
            else
            {
                Silindir.transform.Rotate(new Vector3(0f, -DonusCapi * Time.deltaTime, 0f), Space.Self);
            }                
        }
    }
}
