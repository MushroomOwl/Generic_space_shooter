using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

namespace Game
{
    public class BonusSpawnHelper : MonoBehaviour
    {
        [SerializeField] private GameObject[] _PickUpsPrefabs;
        [SerializeField] private Dictionary<KeyCode, GameObject> _KeyToPickUp;

        private void Start()
        {
            _KeyToPickUp = new Dictionary<KeyCode, GameObject>();
            KeyCode key = KeyCode.Alpha1;

            for (int i = 1; i < _PickUpsPrefabs.Length; i++, key++)
            {
                if (_PickUpsPrefabs[i] == null)
                {
                    continue;
                }
                _KeyToPickUp.Add(key, _PickUpsPrefabs[i]);
            }
        }

        private void Update()
        {
            Vector3 mouseAbsolute = Input.mousePosition;
            Vector3 mouseRelative = Camera.main.ScreenToWorldPoint(mouseAbsolute);
            mouseRelative.Set(mouseRelative.x, mouseRelative.y, 0);

            if (_KeyToPickUp == null)
            {
                return;
            }

            foreach (KeyValuePair<KeyCode, GameObject> pair in _KeyToPickUp)
            {
                if (Input.GetKeyDown(pair.Key) && pair.Value != null)
                {
                    Instantiate(pair.Value, mouseRelative, Quaternion.identity);
                }
            }
        }
    }
}
