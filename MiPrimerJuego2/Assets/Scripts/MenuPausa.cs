using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPausa : MonoBehaviour
{
    public GameObject menuPausa;
    public GameObject botonPausa;
    public bool juegoPausado = false;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            if (juegoPausado)
            {
                Reanudar();
            }

            else
            {
                Pausar();
            }
        }

    }

    public void Reanudar()
    {
        menuPausa.SetActive(false);
        botonPausa.SetActive(true);
        Time.timeScale = 1;
        juegoPausado = false;

    }

    public void Pausar()
    {
        menuPausa.SetActive(true);
        botonPausa.SetActive(false);
        Time.timeScale = 0;
        juegoPausado = true;


    }

    public void Reiniciar()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1;
    }
}
