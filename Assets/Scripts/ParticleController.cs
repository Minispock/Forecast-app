using UnityEngine;
using System.Collections;

public class ParticleController : MonoBehaviour
{

    [SerializeField]
    private GameObject[] particles;
    private int activeEffect;


    // Update is called once per frame
    void Update()
    {
        Vector3 position = Camera.main.ScreenToWorldPoint(Input.mousePosition);//particles move to cursor

        position.z = 0;
        transform.position = position;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (activeEffect < 2) activeEffect++;
            else activeEffect = 0;

            for (int i = 0; i < 3; i++)
            {
                particles[i].SetActive(i == activeEffect);
            }
        }

    }

    public void ChooseEffect(int code)// particle effect sets due to today weather picture 
    {


        if (code >= 10 && code <= 12 || code == 40 || code == 42)
        {
            particles[0].SetActive(true);
            particles[1].SetActive(false);
            particles[2].SetActive(false);

            activeEffect = 0;
        }

        else if (code == 32 || code == 34 || code == 36)
        {
            particles[0].SetActive(false);
            particles[1].SetActive(true);
            particles[2].SetActive(false);

            activeEffect = 1;
        }

        else
        {
            particles[0].SetActive(false);
            particles[1].SetActive(false);
            particles[2].SetActive(true);

            activeEffect = 2;
        }
    }
}
