using AutoMapper;
using URLShortener.Application.ViewModels;
using URLShortener.Domain.Models;

namespace URLShortener.Application;

public static class URLShortenerMapper
{
    public static IReadOnlyList<URLViewModel> ToViewModel(this IReadOnlyList<URL> input)
    {
        var config = new MapperConfiguration(cfg => cfg.CreateMap<URL, URLViewModel>());

        var mapper = new Mapper(config);
        return mapper.Map<IReadOnlyList<URLViewModel>>(input);
    }
}
