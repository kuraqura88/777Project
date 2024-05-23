using UnityEngine;
using UnityEngine.U2D.Animation;

public class PlayerController : MonoBehaviour
{
    public StatusHandler statusHandler;

    public SpriteLibrary library;

    public Animator animator;
    public SpriteResolver spriteResolver;


    public void ChangeStutus(CharacterStats stat)
    {
        if(stat != null)
            statusHandler.SetCharacterStat(stat);
    }

    public void ChangeCharacter(SpriteLibraryAsset asset)
    {
        if (library != null)
        {
            if (asset != null)
            {
                library.spriteLibraryAsset = asset;

                Debug.Log(asset.name);
                if(library.spriteLibraryAsset == asset)
                {
                    Debug.Log("성공");
                }
            }
            else
            {
                Debug.Log("에셋이 없습니다");
            }
        }
        else
        {
            Debug.Log("라이브러리가 없습니다");
        }
    }
    public void RefreshCharacter()
    {
        library.RefreshSpriteResolvers();
    }

    public void Enter()
    {
        Debug.Log("등장");
    }
}