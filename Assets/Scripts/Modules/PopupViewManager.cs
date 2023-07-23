using Object = UnityEngine.Object;
using UnityEngine;
using System.Linq;
using Settings;

namespace UI
{
    public sealed class PopupViewManager
    {
        private PopupViewBase _currentPopup;
        private Transform _popupParent;
        private PrefabsSet _prefabsSet;

        public void Show<T>(T settings) where T : Popup
        {
            if (_currentPopup != null)
                return;

            _prefabsSet ??= SettingsProvider.Get<PrefabsSet>();
            _popupParent = GameObject.FindGameObjectWithTag("PopupParent").transform;

            var popupPrefab = _prefabsSet.Popups.First(x => x.GetComponent<PopupView<T>>() != null)
                .GetComponent<PopupView<T>>();
            var instance = Object.Instantiate(popupPrefab, _popupParent, false);

            instance.Setup(settings);
            instance.Show();

            _currentPopup = instance;
        }

        public void HideCurrentPopup()
        {
            if (_currentPopup == null)
            {
#if DEBUG
                Debug.LogError("Try close current Popup[null]");
#endif
                return;
            }

            _currentPopup.Hide();
        }
    }
}
