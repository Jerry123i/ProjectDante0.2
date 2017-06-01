using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BossCutsceneScript : MonoBehaviour {

    public Camera cutSceneCamera;

    public Sprite texto1, texto2, texto3, texto4, texto5, texto6, texto7, texto8;

    public Sprite img1, img2;

    public Image blackFade, holderT1, holderT2;
    public SpriteRenderer background;

    public float timer;
    public int cenaCounter;

    public AudioClip audioMurmur;
    public AudioClip audioCelebration;
    public AudioSource auSource;
    public bool playingAudio;

    // Use this for initialization
    void Start () {

        playingAudio = false;
        cutSceneCamera.transform.position = new Vector3(1.7f, 4.8f, -10f);
        cenaCounter = 1;
        timer = 0;
        holderT1.sprite = texto1;
        holderT2.sprite = texto2;
        background.sprite = img1;

        FullTransparent(holderT1);
        FullTransparent(holderT2);

        auSource.PlayOneShot(audioCelebration);

    }
	
	// Update is called once per frame
	void Update () {

        if (Input.GetMouseButton(0))
        {
            SceneManager.LoadScene("BossFight");
        }

        timer += Time.deltaTime;
        
            if (!(background.sprite == img2))
        {
            cutSceneCamera.transform.Translate(-Time.deltaTime/6f, -Time.deltaTime/3, 0);

            if (timer < 4f)
            {
                FadeOut(blackFade, 2.5f);
            }

            else if (timer > 4.3f && timer < 6.3f)
            {
                FadeIn(holderT1, 1f);
            }

            else if (timer > 9.3f && timer < 12.3f)
            {
                FadeOut(holderT1, 1.5f);
                auSource.volume -= Time.deltaTime/4;
                if (auSource.volume <= 0)
                {
                    auSource.Pause();
                }
            }

            else if (timer > 12.3f && timer < 16.3f)
            {                

                if (!playingAudio)
                {
                    auSource.clip = audioMurmur;
                    auSource.volume = 1f;
                    auSource.Play();
                    //auSource.PlayOneShot(audioMurmur);
                    playingAudio = true;
                }

                //auSource.volume = 0.5f;
                holderT1.sprite = texto2;
                FadeIn(holderT1, 3f);
            }

            else if (timer > 16.3f && timer < 18.3f)
            {
                FadeOut(holderT1, 2f);
            }

            if (timer > 18.3f && timer < 20f)
            {
                FadeIn(blackFade, 1f);
            }

            if (timer >= 20f && timer < 22f)
            {
                background.sprite = null;
                FullTransparent(blackFade);
            }

            if (timer > 22f && timer < 26f)
            {
                holderT1.sprite = texto3;
                holderT2.sprite = texto4;
                FadeIn(holderT1, 2f);
            }

            if (timer > 25f)
            {
                FadeIn(holderT2, 2f);
                auSource.Stop();
            }

            if (timer > 28f)
            {
                FadeIn(blackFade, 3f);

                if (blackFade.color.a >= 1)
                {
                    cutSceneCamera.transform.position = new Vector3(4.25f, -3.7f, -10f);
                    background.sprite = img2;
                    FullTransparent(holderT1);
                    FullTransparent(holderT2);
                }

            }
        }

        else
        {
            if (cenaCounter >= 2)
            {
                cutSceneCamera.transform.Translate(-Time.deltaTime / 2f, Time.deltaTime / 2f, 0);
            }
            
            Image2Part();
        }

        

    }

    void FadeIn(Image imagem, float speed)
    {
        if (imagem.color.a < 1)
        {
            imagem.color += new Color(0, 0, 0, Time.deltaTime / speed);
        }
    }

    void FadeOut(Image imagem, float speed)
    {
        if (imagem.color.a > 0)
        {
            imagem.color -= new Color(0, 0, 0, Time.deltaTime / speed);
        }
    }

    void FullTransparent(Image imagem)
    {
        imagem.color = new Color(imagem.color.r, imagem.color.g, imagem.color.b, 0);
    }

    void FullOpaque(Image imagem)
    {
        imagem.color = new Color(imagem.color.r, imagem.color.g, imagem.color.b, 1);
    }

    void Image2Part()
    {

        switch (cenaCounter)
        {
            case 1:                
                FadeOut(blackFade, 3f);
                if(blackFade.color.a <= 0)
                {
                    cenaCounter=2;
                }
                break;

            case 2:
                holderT1.sprite = texto5;
                FadeIn(holderT1, 3f);
                if (holderT1.color.a >= 1)
                {
                    cenaCounter=3;
                }
                break;

            case 3:
                holderT2.sprite = texto6;
                FadeIn(holderT2, 3f);
                if (holderT2.color.a >= 1)
                {
                    cenaCounter = 4;
                }
                break;

            case 4:
                FadeOut(holderT1, 2f);
                holderT2.color = holderT1.color;
                if (holderT1.color.a <= 0)
                {
                    cenaCounter = 5;
                }
                break;

            case 5:
                holderT1.sprite = texto7;
                holderT2.sprite = texto8;
                FadeIn(holderT1, 3f);
                if(holderT1.color.a >= 1)
                {
                    cenaCounter = 6;
                }
                break;

            case 6:
                FadeIn(holderT2, 3f);
                if(holderT2.color.a >= 1)
                {
                    cenaCounter = 7;
                }
                break;

            case 7:
                FadeIn(blackFade, 3f);
                if(blackFade.color.a >= 1)
                {
                    SceneManager.LoadScene("BossFight");
                }
                break;

            default:
                Debug.Log("Fim");
                break;

        }


    }

}
