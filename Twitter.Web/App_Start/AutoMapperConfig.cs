namespace Twitter.Web.App_Start
{
    using AutoMapper.Mappers;
    using AutoMapper;
    using System.Web.Mvc;
    using Twitter.Models;
    using Twitter.Web.ViewModels.Users;
    using Twitter.Web.ViewModels.Tweets;

    public static class AutoMapperConfig
    {
        public static void RegisterMappings()
        {
            AutoMapper.Mapper.CreateMap<User, UserViewModel>();
            AutoMapper.Mapper.CreateMap<Tweet, TweetViewModel>();
        }
    }
}