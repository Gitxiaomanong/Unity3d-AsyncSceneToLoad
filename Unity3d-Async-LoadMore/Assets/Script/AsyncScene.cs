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
    /// �ж��ܲ����첽�������غ���û
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
        //����Э��
        StartCoroutine(AsuncLoad());
    }

    private IEnumerator AsuncLoad()
    {
        yield return null;

        isLodaScene = true;

        //��ʾ����UI
        showObj.SetActive(true);

        //��ȡ��ǰ��ĳ���������+1
         asyncOperation = SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex+1);

        //�������ڳ���׼�������������������
        asyncOperation.allowSceneActivation = false;

        //�����Ƿ�����ɣ���ֻ����
        while (!asyncOperation.isDone && isLodaScene)
        {
            //��3d������Ҫ���أ�������ϰ����û��������Ҫģ������ӳ�
            yield return new WaitForSeconds(1f);

            //��ȡ�������ȡ���ֻ������ֵSlider��
            sliderImg.value = asyncOperation.progress;

            //��ȡ�������ȡ���ֻ������ֵText�ı���
            textValue.text = asyncOperation.progress * 100 + "%";

            if (asyncOperation.progress >= 0.9f )
            {
                //��3d������Ҫ���أ�������ϰ����û��������Ҫģ������ӳ�
                yield return new WaitForSeconds(1f);

                sliderImg.value = 1;

                textValue.text = "�����Լ�����ϣ������������ת������";

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
