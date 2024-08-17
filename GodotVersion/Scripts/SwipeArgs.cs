public partial class SwipeInput
{
    public class SwipeArgs
    {
        public SwipeType swipeType;
        public SwipeArgs(SwipeType swipeType)
        {
            this.swipeType = swipeType;
        }
        public bool IsLeftSwipe()
        {
            return swipeType == SwipeType.left;
        }
    }
    
}
