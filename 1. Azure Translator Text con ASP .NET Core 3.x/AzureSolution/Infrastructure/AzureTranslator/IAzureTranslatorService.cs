using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.AzureTranslator
{
    public interface IAzureTranslatorService
    {
        Task<List<AzureTranslatorModel>> Execute(List<AzureTranslatorRequestBody> requestBody);
    }
}
