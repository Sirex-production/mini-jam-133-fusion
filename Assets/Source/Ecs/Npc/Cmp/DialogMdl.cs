using System.Text;
using Secs;
using TMPro;
using UnityEngine.UI;

namespace Ingame.Npc
{
    public struct DialogMdl : IEcsComponent
    {
        public TextMeshProUGUI text;
        public Image image;
        public string taskText;
    }
}