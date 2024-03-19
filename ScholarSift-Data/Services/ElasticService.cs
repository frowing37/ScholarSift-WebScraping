using System.Net;
using Nest;
using ScholarSift_Data.Repostories;
using ScholarSift_Entity.Concrete;
using ScholarSift_Entity.DTO;

namespace ScholarSift_Data.Services;

public class ElasticService
{
    private readonly ElasticRepostory _elasticRepostory;

    public ElasticService(ElasticRepostory elasticRepostory) => _elasticRepostory = elasticRepostory;

    public async Task<ResponseDto<ElasticArticleDto>> SaveAsync(ElasticArticleCreateDto request)
    {
        var response = await _elasticRepostory.SaveAsync(request.CreateArticle());

        if (response is null)
        {
            return ResponseDto<ElasticArticleDto>.Fail(new List<string> { "kayıt esnasında bir hata meydana geldi" },
                System.Net.HttpStatusCode.InternalServerError);
        }

        return ResponseDto<ElasticArticleDto>.Success(response.CreateDto(), HttpStatusCode.Created);
    }
}