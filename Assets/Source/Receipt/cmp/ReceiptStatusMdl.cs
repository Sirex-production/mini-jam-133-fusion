using System.Collections.Generic;
using Secs;

namespace Ingame.Receipt
{
    public struct ReceiptStatusMdl : IEcsComponent
    {
        public List<Receipt> discoveredReceipts;
        public List<Receipt> unlockedReceipts;
    }
}