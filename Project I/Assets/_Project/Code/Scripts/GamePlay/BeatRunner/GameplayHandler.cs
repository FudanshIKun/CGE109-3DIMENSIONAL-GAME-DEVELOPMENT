using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;
using Wonderland.Types;
using Wonderland.Management;
using Wonderland.GamePlay.BeatRunner.Running;

namespace Wonderland.GamePlay.BeatRunner
{
    public class GameplayHandler : Management.GameplayHandler
    {
        #region Settings

        [Header("Settings")] 
        [SerializeField] private int fullSectionPoolAmount;
        [SerializeField] private int halfSectionPoolAmount;
        [SerializeField] private Section.SectionType nextSectionType;

        [Header("Section Management")] 
        [SerializeField] private Transform sectionParent;
        [SerializeField] private GameObject fullSectionPrefab;
        [SerializeField] private GameObject halfSectionPrefab;
        
        [Space(5)]
        [Header("Section Events")]
        [Space(2)]
        [SerializeField] private UnityEvent firstCellEvents;
        [SerializeField] private UnityEvent secondCellEvents;
        [SerializeField] private UnityEvent thirdCellEvents;
        [SerializeField] private UnityEvent fourthCellEvents;
        [SerializeField] private UnityEvent fifthCellEvents;
        [SerializeField] private UnityEvent sixthCellEvents;

        #endregion

        #region Player Management
        private Runner Runner { get; set; }
        
        #endregion

        #region Procedural Management
        private Section _previousSection;
        private Section _currentSection;
        private Section _nextSection;
        public Vector3 LastZ { private get; set; }

        private void OnPullSection(PoolObject poolObject)
        {
            var newSection = poolObject.GetComponent<Section>();
            
            if (_currentSection == null)
            {
                newSection.CreateNewGrid();
                _currentSection = newSection;
            }
            else
            {
                if (_previousSection == null)
                {
                    newSection.CreateNewGrid();
                    _nextSection = newSection;
                }
                else
                {
                    switch (_previousSection.type)
                    {
                        case Section.SectionType.FullSection:
                            _fullSectionPool.Push(_previousSection);
                            break;
                        case Section.SectionType.HalfSection :
                            _halfSectionPool.Push(_previousSection);
                            break;
                        default: 
                            Destroy(gameObject);
                            break;
                    }
                    
                    _previousSection = null;

                    if (_nextSection != null) return;
                    newSection.CreateNewGrid();
                    _nextSection = newSection;
                }
            }
        }
        private void OnPushSection(PoolObject poolObject)
        {
            var pushedSection = poolObject.GetComponent<Section>();
            #region Turn Section Invoked Bool To Default
            pushedSection.firstCellHasInvoked = false;
            pushedSection.secondCellHasInvoked = false;
            pushedSection.thirdCellHasInvoked = false;
            pushedSection.fourthCellHasInvoked = false;
            pushedSection.fifthCellHasInvoked = false;
            pushedSection.sixthCellHasInvoked = false;
            #endregion
        }
        private void InvokeSectionEvents(Types.Objects anyObject)
        {
            if (_currentSection == null) return;
            switch (_currentSection.CurrentCell(anyObject))
            {
                case 0 :
                    Logging.GamePlayLogger.Log("Player Isn't In Section: " + _currentSection.gameObject.name);
                    break;
                case 1 :
                    if (!_currentSection.firstCellHasInvoked)
                    {
                        _currentSection.firstCellHasInvoked = true;
                        firstCellEvents?.Invoke();
                    }
                    break;
                case 2 :
                    if (!_currentSection.secondCellHasInvoked)
                    {
                        _currentSection.secondCellHasInvoked = true;
                        secondCellEvents?.Invoke();
                    }
                    break;
                case 3 :
                    if (!_currentSection.thirdCellHasInvoked)
                    {
                        _currentSection.thirdCellHasInvoked = true;
                        thirdCellEvents?.Invoke();
                    }
                    break;
                case 4 :
                    if (!_currentSection.fourthCellHasInvoked)
                    {
                        _currentSection.fourthCellHasInvoked = true;
                        fourthCellEvents?.Invoke();
                    }
                    break;
                case 5 :
                    if (!_currentSection.fifthCellHasInvoked)
                    {
                        _currentSection.fifthCellHasInvoked = true;
                        fifthCellEvents?.Invoke();
                    }
                    break;
                case 6 :
                    if (!_currentSection.sixthCellHasInvoked)
                    {
                        _currentSection.sixthCellHasInvoked = true;
                        sixthCellEvents?.Invoke();
                    }
                    break;
            }
        }

        #endregion
        
        #region Pools Management
        private static ObjectPool<PoolObject> _fullSectionPool;
        private static ObjectPool<PoolObject> _halfSectionPool;
        private bool AllowPull => GameManager.CheckGameState(IManager.State.PlayState);

        #endregion
        
        #region Event Methods

        public void EnteringNewSection()
        {
            Logging.GamePlayLogger.Log("Change To New Section");
            
            if (_previousSection != null) return;
            _previousSection = _currentSection;
            _currentSection = _nextSection;
            _nextSection = null;
        }

        public void PullNextSection()
        {
            //if (!AllowPull) return;
            switch (nextSectionType)
            {
                case Section.SectionType.FullSection :
                    _fullSectionPool.PullGameObject(LastZ, Quaternion.identity, sectionParent);
                    break;
                case Section.SectionType.HalfSection :
                    _halfSectionPool.PullGameObject(LastZ, Quaternion.identity, sectionParent);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        #endregion

        #region Signal Methods

        public void ChangeNextSectionType()
        {
            nextSectionType = nextSectionType == Section.SectionType.FullSection ? Section.SectionType.HalfSection : Section.SectionType.FullSection;
        }        

        #endregion


        #region Methods

        public override Task SetUpGameplay()
        {
            void Action()
            {
                LastZ = new Vector3(0, 0, 0);
                PullNextSection();
                GameplayReady = true;
            }

            var setUpTask = new Task(Action);
            return setUpTask;
        }

        #endregion

        private void Awake()
        {
            Runner = GameObject.FindGameObjectWithTag("Player").GetComponent<Runner>();
            
            #region Pool Creation
            _fullSectionPool = new ObjectPool<PoolObject>(fullSectionPrefab, sectionParent.transform, OnPullSection, OnPushSection, fullSectionPoolAmount);
            _halfSectionPool = new ObjectPool<PoolObject>(halfSectionPrefab, sectionParent.transform, OnPullSection, OnPushSection, halfSectionPoolAmount);

            #endregion
        }
        
        protected override void OnEnable()
        {
            
        }

        private void Start()
        {
            SetUpGameplay();
        }

        private void FixedUpdate()
        {
            InvokeSectionEvents(Runner);
        }

        private void Update()
        {
            #region Logging
            Logging.GamePlayLogger.Log("FullSection Pool: " + _fullSectionPool.PooledCount);
            Logging.GamePlayLogger.Log("Next Section Type: " + nextSectionType);

            #endregion
        }

        protected override void OnDisable()
        {

        }

        private void OnGUI()
        {
            if (GUI.Button(new Rect(10, 70, 300, 30), "Pull a Section"))
            {
                PullNextSection();
            }
        }
    }
}