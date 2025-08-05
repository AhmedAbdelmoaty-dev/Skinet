using Domain.Entites;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Payment.Commands.CreateOrUpdatePaymentIntent
{
    public class CreateOrUpdatePaymentIntentCommand:IRequest<ShoppingCart>
    {
      public  string CartId { get; set; }
    }
}
