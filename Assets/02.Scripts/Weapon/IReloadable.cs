using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IReloadable
{
    void StartReload(); // 재장전 시작
    void StopReload();  // 재장전 종료
}
