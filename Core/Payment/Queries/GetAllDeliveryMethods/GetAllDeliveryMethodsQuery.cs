using Domain.Entites;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Payment.Queries.GetAllDeliveryMethods
{
    public class GetAllDeliveryMethodsQuery:IRequest<IReadOnlyList<DeliveryMethod>>
    {
    }
}
