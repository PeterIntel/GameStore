using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GameStore.Web.Filters
{
    public class FilterTranslatorAttribute : TranslatorAttributeBase
    {
        //public FilterTranslatorAttribute(string argumentName) : base(argumentName)
        //{
        //}

        //public override void OnActionExecuting(ActionExecutingContext filterContext)
        //{
        //    base.OnActionExecuting(filterContext);
        //    var viewModel = filterContext.ActionParameters[ArgumentName] as GamesAndFiltrationViewModel;


        //    if (viewModel == null)
        //    {
        //        return;
        //    }

        //    viewModel.SelectedGenreNames = LocalizedToOriginalTranslator.GetListOfOriginalGenreNames(viewModel.SelectedGenreNames);
        //    viewModel.SelectedPlatformTypesNames = LocalizedToOriginalTranslator.GetListOfOriginalPlatformTypesName(viewModel.SelectedPlatformTypesNames);
        //}

        //public override void OnResultExecuting(ResultExecutingContext filterContext)
        //{
        //    base.OnResultExecuting(filterContext);
        //    var viewModel = filterContext.Controller.ViewData.Model as GamesAndFiltrationViewModel;

        //    if (viewModel == null)
        //    {
        //        return;
        //    }

        //    viewModel.SelectedGenreNames =
        //        OriginalToLocalizedTranslator.GetListOfTranslatedGenreNames(viewModel.SelectedGenreNames);
        //    viewModel.SelectedPlatformTypesNames =
        //        OriginalToLocalizedTranslator.GetListOfTransaltedPlatformTypeNames(viewModel.SelectedPlatformTypesNames);

        //}
    }
}