using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SampleScene : MonoBehaviour
{
    // Start is called before the first frame update
    private void Start()
    {
        // 시작시 캐릭터 선택 팝업 띄우기
        UIManager.Instance.ShowPopup<CharacterChangePopup>();

        //PoolManager.Instance.NpcRegen(2);
    }
}
