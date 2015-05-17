namespace Twitter.Web.ViewModels.Common
{
    using Mappings;
    using System;
    using System.Linq.Expressions;
    using Twitter.Models;


    public class LanguageViewModel : IMapFrom<UserLanguage>
    {
        public static Expression<Func<UserLanguage, LanguageViewModel>> ViewModel
        {
            get
            {
                return x => new LanguageViewModel
                {
                    Name = x.Name
                };
            }
        }

        public string Name { get; set; }
    }
}