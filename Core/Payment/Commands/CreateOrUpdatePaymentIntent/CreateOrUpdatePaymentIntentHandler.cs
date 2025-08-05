using Application.Contracts.Services;
using Application.Exceptions;
using Domain.Entites;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Payment.Commands.CreateOrUpdatePaymentIntent
{
  
    internal class CreateOrUpdatePaymentIntentHandler : IRequestHandler<CreateOrUpdatePaymentIntentCommand, ShoppingCart>
    {
        private readonly IPaymentService _paymentService;

        public CreateOrUpdatePaymentIntentHandler(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }
        public Task<ShoppingCart> Handle(CreateOrUpdatePaymentIntentCommand request, CancellationToken cancellationToken)
        {
          var cart=   _paymentService.CreateOrUpdatePaymentInit(request.CartId);
            if(cart == null)
            {
                throw new BadRequestException("Proplem with your cart");
            }
            return cart;
        }
    }
}
