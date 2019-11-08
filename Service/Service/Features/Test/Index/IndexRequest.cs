using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace Service.Features.Test.Index
{
    public class IndexRequest : IRequest<IndexResponse>
    {
        public string CardId { get; set; }
    }

    public class IndexResponse
    {
        public string Guid { get; set; }
    }

    public class IndexHandler: IRequestHandler<IndexRequest, IndexResponse>
    {
        public IndexHandler()
        {
            
        }
        public async Task<IndexResponse> Handle(IndexRequest request, CancellationToken cancellationToken)
        {



            return new IndexResponse
            {
                Guid = Guid.NewGuid().ToString()
            };
        }
    }
}
