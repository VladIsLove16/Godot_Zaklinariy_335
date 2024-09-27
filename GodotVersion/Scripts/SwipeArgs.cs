public partial class SwipeInput
{
    public class SwipeArgs
    {
        public SwipeType swipeType;
        public bool isDoubleTap = false;
        public SwipeArgs(SwipeType swipeType)
        {
            this.swipeType = swipeType;
        }
        public SwipeArgs(SwipeType swipeType, bool isDoubleTap)
        {
            this.swipeType = swipeType;
            this.isDoubleTap = isDoubleTap;
        }
        public bool IsLeftSwipe()
        {
            return swipeType == SwipeType.left;
        }
    }
    
}
