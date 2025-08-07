namespace e_commercial.Constants
{
    public enum  OrderStatusEnum
    {

        Pending = 1, // Chờ xử lý
        Approved = 2, // Đang xử lý
        Shipping = 3, // Chờ giao hàng
        Delivered = 4, // Đã giao hàng thành công
        CancelledByAdmin = 5, // Đã hủy by admin
        CancelledByUser = 6, // Đã hủy by user
        Returned = 6, // Đã trả hàng
        Refunded = 7 // Đã hoàn tiền




    }
}
