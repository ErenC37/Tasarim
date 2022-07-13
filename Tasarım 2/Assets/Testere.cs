using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
public class Testere : MonoBehaviour
{
    GameObject[] points;     // Uğrayacağı durakların listesi

    bool rangeWay = true;      // yol bitti mi?

    bool rangeCal = true;     // mesafe hesaplanacak mı?

    Vector3 range;   // mesafe vektörü

    int rangeCount = 0;  // durak listem için gerekli sayaç


    void Start()
    {
        points = new GameObject[transform.childCount];

        for (int i = 0; i < points.Length; i++)
        {
            points[i] = transform.GetChild(0).gameObject;   // listeden child eksildiği için her seferde sıfıra dönmek gerekli!!
            points[i].transform.SetParent(transform.parent);
        }



    }


    void Update()
    {
        
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()    
    {
        for (int i = 0; i < transform.childCount; i++)   // duraklar arasında çizgi oluşturma
        {

            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.GetChild(i).transform.position, 1);
        }
        for (int i = 0; i < transform.childCount - 1; i++)     // son elemanda liste aşımı hatası almamak için -1 kullanılmalı !!
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawLine(transform.GetChild(i).transform.position, transform.GetChild(i+1).transform.position);
        }


    }
#endif

    private void FixedUpdate()
    {
        transform.Rotate(0, 0, -5);
        gezin();
    }


    void gezin()
    {
        if (rangeCal)
        {

            range = (points[rangeCount].transform.position - transform.position).normalized;    
            rangeCal = false;

        }
        float mesafe = Vector3.Distance(transform.position, points[rangeCount].transform.position);

        transform.position += range * Time.deltaTime * 10;    

        if(mesafe < 0.5f)      // durağa vardıysa yeni durağı hesapla 
        {
            rangeCal = true;
            if(rangeCount == points.Length - 1)
            {
                rangeWay = false;
            }
            else if (rangeCount == 0)    // durak listesi bittiyse yolu döndür
            {
                rangeWay = true;
            }

            if (rangeWay)    //  yolun sonuna kadar ilerle 
            {
                rangeCount++;
            }
            else      // yol bittiyse durakları geri gez 
            {
             rangeCount--;
            }

        }


    }

   
}

#if UNITY_EDITOR
[CustomEditor(typeof(Testere))]
[System.Serializable]

class testereEditor : Editor
{
    public override void OnInspectorGUI()
    {
        Testere script = (Testere)target;

        if (GUILayout.Button("Add"))       //  editör içi buton oluşturma
        {
            GameObject obje = new GameObject();
            obje.transform.parent = script.transform;
            obje.transform.position = script.transform.position;
            obje.name = script.transform.childCount.ToString();
        }

    }




}
#endif
