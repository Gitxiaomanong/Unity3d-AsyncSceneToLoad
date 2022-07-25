using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AsyncScene : MonoBehaviour
{
    public Text textValue;
    public Slider sliderImg;
    public GameObject showObj;

    AsyncOperation asyncOperation;

    /// <summary>
    /// 判断能不能异步场景加载好了没
    /// </summary>
    private bool isLodaScene;
    private bool isLodaScene1;


    private void Update()
    {
        if (isLodaScene&& isLodaScene1)
        {
            if (Input.anyKeyDown)
            {
                asyncOperation.allowSceneActivation = true;
                isLodaScene = false;
            }
        }
    }

    public void startToLoadScene()
    {
        //启动协程
        StartCoroutine(AsuncLoad());
    }

    private IEnumerator AsuncLoad()
    {
        yield return null;

        isLodaScene = true;

        //显示加载UI
        showObj.SetActive(true);

        //获取当前活动的场景的索引+1
         asyncOperation = SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex+1);

        //不允许在场景准备就绪后立即激活场景。
        asyncOperation.allowSceneActivation = false;

        //操作是否已完成？（只读）
        while (!asyncOperation.isDone && isLodaScene)
        {
            //如3d场景需要加载，这里练习场景没东西所以要模拟舔加延迟
            yield return new WaitForSeconds(1f);

            //获取操作进度。（只读）赋值Slider上
            sliderImg.value = asyncOperation.progress;

            //获取操作进度。（只读）赋值Text文本上
            textValue.text = asyncOperation.progress * 100 + "%";

            if (asyncOperation.progress >= 0.9f )
            {
                //如3d场景需要加载，这里练习场景没东西所以要模拟舔加延迟
                yield return new WaitForSeconds(1f);

                sliderImg.value = 1;

                textValue.text = "场景以加载完毕，按下任意键跳转场景。";

                isLodaScene = false;
            }
            yield return null;
        }
        if (sliderImg.value == 1)
        {
            isLodaScene = true;
            isLodaScene1 = true;
        }
    }
 
}
