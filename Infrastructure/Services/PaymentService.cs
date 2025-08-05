using Application.Contracts.Repositories;
using Application.Contracts.Services;
using Domain.Entites;
using Microsoft.Extensions.Configuration;
using Stripe;

namespace Infrastructure.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IGenericRepository<DeliveryMethod> _dmRepo;
        private readonly ICartService _cartService;
        private readonly IProductRepository _productRepository;
        private readonly IConfiguration _config;

        public PaymentService(IGenericRepository<DeliveryMethod> dmRepo
            ,ICartService cartService, IProductRepository productRepository,IConfiguration config)
        {
            _dmRepo = dmRepo;
            _cartService = cartService;
            _productRepository = productRepository;
            _config = config;
        }
        public async Task<ShoppingCart?> CreateOrUpdatePaymentInit(string cartId)
        {
            StripeConfiguration.ApiKey = _config["StripeSettings:SecretKey"];
            var cart = await _cartService.GetCartAsync(cartId);
            if (cart == null) return null;
            
            var shippingPrice = 0m; 

            if (cart.DeliveryMethodId.HasValue)
            {
                var deliveryMethod= await  _dmRepo.GetByIdAsync((int)cart.DeliveryMethodId);
                if (deliveryMethod == null) return null;

                shippingPrice = deliveryMethod.Price;

            }
            foreach(var item in cart.Items)
            {
                var product = await _productRepository.GetByIdAsync(item.ProductId );
                if (product == null) return null;
                if (product.Price != item.Price)
                {
                    item.Price = product.Price;
                }
            }

            var paymentIntentService = new PaymentIntentService();
            PaymentIntent? intent = null;
            if (string.IsNullOrEmpty(cart.PaymentIntentId))
            {
                var options = new PaymentIntentCreateOptions
                {
                    Amount=(long) cart.Items.Sum(x=>x.Quantity*(x.Price*100))
                    + (long)shippingPrice*100,
                    Currency = "usd",
                    PaymentMethodTypes = ["card"]
                };
                intent=await paymentIntentService.CreateAsync(options);
                cart.PaymentIntentId = intent.Id;
                cart.ClientSecret = intent.ClientSecret;

            }
            else
            {
                var options = new PaymentIntentUpdateOptions
                {
                    Amount = (long)cart.Items.Sum(x => x.Quantity * (x.Price * 100))
                             + (long)shippingPrice * 100,
                };
                intent = await paymentIntentService.UpdateAsync(cart.PaymentIntentId,options);
            }
                await _cartService.SetCartAsync(cart);
            return cart;
        }
    }
}
