using Secs;

namespace Ingame.Receipt
{
    public struct DiscoverNewReceiptReq : IEcsComponent
    {
        public Receipt newReceipt;
    }
}