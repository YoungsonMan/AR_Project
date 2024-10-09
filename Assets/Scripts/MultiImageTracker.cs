using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class MultiImageTracker : MonoBehaviour
{
    [SerializeField] ARTrackedImageManager imageManager;

    [SerializeField] GameObject playerPrefab;
    [SerializeField] GameObject enemyPrefab;


    private void OnEnable()
    {
        imageManager.trackedImagesChanged += OnImageChange;
    }
    private void OnDisable()
    {
        imageManager.trackedImagesChanged -= OnImageChange;
    }

    private void OnImageChange(ARTrackedImagesChangedEventArgs args)
    {
        // 이미지 추적 됐으면
        foreach (ARTrackedImage trackedImage in args.added)
        {
            // 이미지 라이브러리에서 이미지 이름 확인
            string imageName = trackedImage.referenceImage.name;

            // 새로운 게임오브젝트를 트래킹한 이미지의 자식으로 생성
            switch (imageName)
            {
                case "Player":
                    GameObject player = Instantiate(playerPrefab, trackedImage.transform.position, trackedImage.transform.rotation);
                    break;
                case "Enemy":
                    GameObject enemy = Instantiate(enemyPrefab, trackedImage.transform.position, trackedImage.transform.rotation);
                    break;

            }
        }

        // 기존이미지가 변경(이동, 회전) 됐을때
        foreach(ARTrackedImage trackedImage in args.updated)
        {
            // 이미지의 변경사항이 있는 경우 자식으로 있던 게임오브젝트 위치 & 회전 갱신
            trackedImage.transform.GetChild(0).position = trackedImage.transform.position;
            trackedImage.transform.GetChild(0).rotation = trackedImage.transform.rotation;
        }

        // 기존이미지가 사라졌을 때
        foreach(ARTrackedImage trackedImage in args.removed)
        {
            // 이미지가 사라진 경우 자식으로 있었던 게임오브젝트를 삭제
            Destroy(trackedImage.transform.GetChild(0).gameObject);
        }

    }

}
