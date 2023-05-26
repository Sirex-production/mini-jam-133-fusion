using Secs;
using UnityEngine;
using Zenject;

namespace Ingame.Receipt 
{
    public sealed class UnlockNewReceiptsSys : IEcsRunSystem
    {
        public void OnRun()
        {
            Debug.LogWarning("123");
        }
    }
}