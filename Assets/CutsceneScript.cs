using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CutsceneScript : MonoBehaviour {

    public Camera cutSceneCamera;

    public Sprite texto1_1, texto1_2, texto2_1, texto2_2, texto3_1, texto3_2, texto4_1, texto4_2, texto4_3;

    public Sprite img1, img2, img3;
    
    public Image blackFade, holderT1, holderT2, holderT3;
    public SpriteRenderer background;

    public float timer;
    public int cenaCounter;

    // Use this for initialization
    void Start () {

        cutSceneCamera.transform.position = new Vector3(-3.32f, -1.5f, 0);
        cenaCounter = 1;
        timer = 0;
        holderT1.sprite = texto1_1;
        holderT2.sprite = texto1_2;
        background.sprite = img1;

        FullTransparent(holderT1);
        FullTransparent(holderT2);
		
	}
	
	// Update is called once per frame
	void Update () {

        timer += Time.deltaTime;

        switch (cenaCounter)
        {
            case 1:
                Cena1();
                break;

            case 2:
                Cena2();
                break;

            case 3:
                Cena3();
                break;

            case 4:
                Cena4();
                break;
        }
                


    }

    void Cena1()
    {

      
        cutSceneCamera.transform.Translate(0, Time.deltaTime * 0.8f, 0);


        if (blackFade.color.a > 0 && timer < 6.3f)
        {
            FadeOut(blackFade, 1f);
        }

        if (timer > 2f && holderT1.color.a <= 1)
        {
            FadeIn(holderT1, 3f);
        }

        if (timer > 5f && holderT2.color.a <= 1)
        {
            FadeIn(holderT2, 3f);
        }

        if (timer >= 9.8f)
        {
            FadeIn(blackFade, 1f);
        }

        if (timer >= 11.5)
        {
            cutSceneCamera.transform.position = new Vector3(2f, 10.5f, 0);

            cenaCounter++;
            timer = 0;
            holderT1.sprite = texto2_1;
            holderT2.sprite = texto2_2;
            background.sprite = img2;

            FullTransparent(holderT1);
            FullTransparent(holderT2);
        }
    }

    void Cena2()
    {
        if (cutSceneCamera.transform.position.y >= -1.6f)
        {
            cutSceneCamera.transform.Translate(0, -Time.deltaTime, 0);
        }

        else
        {
            cutSceneCamera.transform.Translate(-Time.deltaTime, 0, 0);
        }

        if (blackFade.color.a > 0 && timer < 6.3f)
        {
            FadeOut(blackFade, 1f);
        }

        if (timer > 2f && holderT1.color.a <= 1)
        {
            FadeIn(holderT1, 3f);
        }

        if (timer > 5f && holderT2.color.a <= 1)
        {
            FadeIn(holderT2, 3f);
        }

        if (timer >= 12f)
        {
            FadeIn(blackFade, 1f);
        }

        if (timer >= 13f)
        {
            cutSceneCamera.transform.position = new Vector3(-6.7f, 8.1f, 0);

            cenaCounter++;
            timer = 0;
            holderT1.sprite = texto3_1;
            holderT2.sprite = texto3_2;
            background.sprite = img3;

            FullTransparent(holderT1);
            FullTransparent(holderT2);
        }

    }

    void Cena3()
    {
        cutSceneCamera.transform.Translate(Time.deltaTime*0.5f, -Time.deltaTime*0.5f,0);

        if (blackFade.color.a > 0 && timer < 6.3f)
        {
            FadeOut(blackFade, 1f);
        }

        if (timer > 2f && holderT1.color.a <= 1)
        {
            FadeIn(holderT1, 3f);
        }

        if (timer > 5f && holderT2.color.a <= 1)
        {
            FadeIn(holderT2, 3f);
        }

        if (timer >= 8.3f)
        {
            FadeIn(blackFade, 1f);
        }

        if (timer >= 10)
        {
            cenaCounter++;
            timer = 0;
            holderT1.sprite = texto4_1;
            holderT2.sprite = texto4_2;
            holderT3.sprite = texto4_3;

            FullTransparent(holderT1);
            FullTransparent(holderT2);
            background.gameObject.SetActive(false);
            FullTransparent(blackFade);
        }
    }

    void Cena4()
    {

        if (timer >= 1.5f && timer <4f)
        {
            FadeIn(holderT1, 2.5f);
            FadeIn(holderT2, 2.5f);
        }

        if (timer > 4.3f && timer < 5f)
        {
            FullOpaque(holderT1);
            FullOpaque(holderT2);
            FullOpaque(holderT3);
        }

        if (timer >= 5.5f)
        {
            FadeOut(holderT1, 1f);
            holderT2.color = holderT1.color;
        }

        if (timer >= 7.5)
        {
            FadeOut(holderT3, 1f);
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
        imagem.color = new Color(1, 1, 1, 0);
    }

    void FullOpaque(Image imagem)
    {
        imagem.color = new Color(1, 1, 1, 1);
    }
}
