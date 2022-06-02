using DG.Tweening;
using UnityEngine;

namespace DefaultNamespace
{
    public class TutorialController : MonoBehaviour
    {
        [SerializeField]
        private RectTransform _handRect;
        [SerializeField]
        private RectTransform _unitCardRect;

        public TutorStage currentStage;

        private Sequence _cameraSequence;
        private Sequence _unitSequence;
        private Sequence _spawnSequence;

        private Transform _baseTransform;
        
        public void CameraMoveStage(Transform baseTransform)
        {
            _baseTransform = baseTransform;
            currentStage = TutorStage.Camera;
            
            Vector3 basePosUI = Camera.main.WorldToScreenPoint(baseTransform.position);
            _handRect.gameObject.SetActive(true);
            _handRect.position = basePosUI;
            
            _cameraSequence = DOTween.Sequence();
            _cameraSequence.Append(_handRect.DOMove(basePosUI + Vector3.up * 150f, 1f));
            _cameraSequence.Append(_handRect.DOMove(basePosUI, 1f));
            _cameraSequence.Append(_handRect.DOMove(basePosUI + Vector3.down * 150f, 1f));
            _cameraSequence.Append(_handRect.DOMove(basePosUI, 1f));
            
            _cameraSequence.Append(_handRect.DOMove(basePosUI + Vector3.left * 150f, 1f));
            _cameraSequence.Append(_handRect.DOMove(basePosUI, 1f));
            _cameraSequence.Append(_handRect.DOMove(basePosUI + Vector3.right * 150f, 1f));
            _cameraSequence.Append(_handRect.DOMove(basePosUI, 1f));

            _cameraSequence.SetLoops(-1, LoopType.Restart);
        }
        
        private void UnitSelectStage()
        {
            currentStage = TutorStage.Unit;
            
            _handRect.gameObject.SetActive(true);
            _handRect.position = _unitCardRect.position - Vector3.up * 100f; 

            _unitSequence.Append(_handRect.DOScale(Vector3.one * 1.5f, 1f).SetLoops(-1, LoopType.Yoyo));
        }
        
        private void SpawnStage()
        {
            currentStage = TutorStage.Spawn;
            Vector3 basePosUI = Camera.main.WorldToScreenPoint(_baseTransform.position);

            _handRect.position = basePosUI + (Vector3.down * 500f) + (Vector3.left * 150f) ;

            _spawnSequence.Append(_handRect.DOMove(_handRect.position + Vector3.right * 300f, 1f)
                .SetLoops(-1, LoopType.Restart));
        }


        public void FinishStage(TutorStage stage)
        {
            if (stage == TutorStage.Camera)
            {
                _cameraSequence.Kill();
                UnitSelectStage();
            }
            else if(stage == TutorStage.Unit)
            {
                _unitSequence.Kill();
                SpawnStage();
            }
            else if(stage == TutorStage.Spawn)
            {
                _spawnSequence.Kill();
                currentStage = TutorStage.None;
                _handRect.gameObject.SetActive(false);
            }
        }
    }
}