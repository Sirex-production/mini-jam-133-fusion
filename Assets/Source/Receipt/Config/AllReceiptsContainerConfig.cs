using System.Collections.Generic;
using UnityEngine;

namespace Ingame.Receipt
{
    [CreateAssetMenu(fileName = "AllReceiptsConfig", menuName = "Cards/AllReceiptsContainer")]
    public sealed class AllReceiptsContainerConfig : ScriptableObject
    {
        [SerializeField] private List<ReceiptConfig> allReceipts;
        
    }
}