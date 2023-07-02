using System.Threading.Tasks;
using UnityEngine.UI;
using UnityEngine;
using Settings;
using Scenes;

namespace Starters
{
    public class StarterMain : Starter, ISceneLoadHandler<string>
    {
        [SerializeField] private Image _background;
        private string _npcPresetId;

        protected override async Task Initialize()
        {
            var npcPreset = SettingsProvider.Get<NpcCommonSettings>().GetPreset(_npcPresetId);

            _background.sprite = npcPreset.Background;
            npcPreset.Prefabs.ForEach(p =>
            {
                Instantiate(p);
            });

            await Task.Yield();
        }

        private async void Awake()
        {
            LoadSceneProcessor.Instance.InvokeLoadAction();
            await Initialize();
        }

        public override void OnSceneLoad(string argument)
        {
            _npcPresetId = argument;
        }
    }
}