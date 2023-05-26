using Ingame.Recipe;
using Secs;

namespace Ingame.Tasks 
{
    public struct OfferTaskItemReq : IEcsComponent
    {
        public ItemConfig offeredItem;
    }
}