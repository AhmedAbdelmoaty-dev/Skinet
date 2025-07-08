using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Products.Query.GetAllBrands
{
    public class GetAllBrandsQuery : IRequest<IReadOnlyList<string>>
    {
    }
}
