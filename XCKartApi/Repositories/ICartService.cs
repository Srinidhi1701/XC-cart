using XCKartApi.Models;

namespace XCKartApi.Repositories
{
    public interface ICartService
    {
        public List<CartModel> GetCart();

        public void AddCartDetails(CartModel cart);
        public void AddCartToDatabase(CartModel cartModel);

        public void DeleteCart(int id);
        public void UpdateCart(CartModel cart);
        public void UpdateCartDatabase(CartModel cart);
    }
}
