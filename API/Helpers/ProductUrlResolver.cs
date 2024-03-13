using AutoMapper;
using Core.Entities;
using API.Dtos;
namespace API.Helpers
{
    public class ProductUrlResolver : IValueResolver<Product, ProducttoReturnDto, string>
    {
        private readonly IConfiguration _config;
        public ProductUrlResolver(IConfiguration config)
        {
            _config= config;
        }

        public string Resolve(Product source, ProducttoReturnDto destination, string destMember, ResolutionContext context){
            if(!string.IsNullOrEmpty(source.PictureUrl))
            {
                return _config["ApiUrl"] + source.PictureUrl;
            }
            return null;
        }
    }
}