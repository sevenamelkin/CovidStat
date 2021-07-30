using CovidStat.Services;
using CovidStat.Services;

namespace CovidStat.Interfaces
{
    public interface IBaseService
    {
        BaseService.AdapterTagValueDto GetStatus();
    }
}