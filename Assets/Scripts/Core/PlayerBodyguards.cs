using System.Collections.Generic;
using UnityEngine;
using Zenject;
using System.Linq;

namespace Main
{
    public class PlayerBodyguards : MonoBehaviour
    {
        [SerializeField] private List<Bodyguard> _activeBodyguards = new List<Bodyguard>();
        [SerializeField] private BodyguardPoint[] _bodyguardSpawnPoints;

        private PlayerStats _playerStats;

        private ObjectPool<Bodyguard> _bodyGuards;
        [SerializeField] private GameObject _bodyguardPref;

        private const int UpperLeft = 1;
        private const int UpperRight = 5;
        private const int LowerLeft = 2;
        private const int LowerRight = 4;
        private const int LowerMiddle = 3;

        private const int MaxBodyguards = 5;

        private int _amountOfBodyguards;

        private DiContainer _diContainer;

        [Inject]
        private void Construct(PlayerStats playerStats, DiContainer diContainer)
        {
            _playerStats = playerStats;
            _diContainer = diContainer;
        }

        private void Start()
        {
            _bodyGuards = new ObjectPool<Bodyguard>(_bodyguardPref, _diContainer);
        }

        private void OnEnable()
        {
            _playerStats.OnBodyguardAmountChanged += AddBodyguard;
        }

        private void OnDisable()
        {
            _playerStats.OnBodyguardAmountChanged -= AddBodyguard;
        }

        private void PlaceBodyguards()
        {

            switch (_amountOfBodyguards)
            {
                case 0:
                    break;
                case 1:
                    _activeBodyguards[0].PrepareSecuring(GetPointByIndex(UpperLeft));
                    break;
                case 2:
                    _activeBodyguards[0].PrepareSecuring(GetPointByIndex(UpperLeft));
                    _activeBodyguards[1].PrepareSecuring(GetPointByIndex(UpperRight));

                    break;
                case 3:
                    _activeBodyguards[0].PrepareSecuring(GetPointByIndex(UpperLeft));
                    _activeBodyguards[1].PrepareSecuring(GetPointByIndex(LowerMiddle));
                    _activeBodyguards[2].PrepareSecuring(GetPointByIndex(UpperRight));
                    break;
                case 4:
                    _activeBodyguards[0].PrepareSecuring(GetPointByIndex(UpperLeft));
                    _activeBodyguards[1].PrepareSecuring(GetPointByIndex(LowerLeft));
                    _activeBodyguards[2].PrepareSecuring(GetPointByIndex(LowerRight));
                    _activeBodyguards[3].PrepareSecuring(GetPointByIndex(UpperRight));
                    break;

                case 5:
                    _activeBodyguards[0].PrepareSecuring(GetPointByIndex(UpperLeft));
                    _activeBodyguards[1].PrepareSecuring(GetPointByIndex(LowerLeft));
                    _activeBodyguards[2].PrepareSecuring(GetPointByIndex(LowerMiddle));
                    _activeBodyguards[3].PrepareSecuring(GetPointByIndex(LowerRight));
                    _activeBodyguards[4].PrepareSecuring(GetPointByIndex(UpperRight));
                    break;
                default:

                    break;
            }
        }

        private BodyguardPoint GetPointByIndex(int index)
        {
            for (int i = 0; i < _bodyguardSpawnPoints.Length; i++)
            {
                BodyguardPoint bodyguardPoint = _bodyguardSpawnPoints[i];
                if (bodyguardPoint.PointIndex == index)
                {
                    return bodyguardPoint;
                }
                else
                {
                    continue;
                }
            }
            return null;
        }


        public void AddBodyguard(int amount)
        {
            _amountOfBodyguards += amount;

            for (int i = 0; i < amount; i++)
            {
                Bodyguard bodyguard = _bodyGuards.PullZenject();
                bodyguard.transform.position = transform.position;
                bodyguard.transform.parent = gameObject.transform;
                _activeBodyguards.Add(bodyguard);
            }
            PlaceBodyguards();
        }

        public void RemoveBodyguard(Bodyguard bodyguard)
        {
            _activeBodyguards.Remove(bodyguard);
            _amountOfBodyguards--;
            PlaceBodyguards();

        }

        public void TryUseBodyguard(Collider collider)
        {
            if (_amountOfBodyguards == 0) return;

            _activeBodyguards[0].Defend(collider);
        }
    }
}


