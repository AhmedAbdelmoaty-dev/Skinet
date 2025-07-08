using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Products.Query.GetAllTypes
{
    public class GetAllTypesQuery:IRequest<IReadOnlyList<string>>
    {
    }
}
