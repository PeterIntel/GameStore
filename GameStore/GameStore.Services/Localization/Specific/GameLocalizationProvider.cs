using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameStore.Domain.BusinessObjects;

namespace GameStore.Services.Localization.Specific
{
    public class GameLocalizationProvider : ILocalizationProvider<Game>
    {
        private readonly ILocalizationProvider<Genre> _genreLocalizationProvider;
        private readonly ILocalizationProvider<PlatformType> _platformTypeLocalizationProvider;

        public GameLocalizationProvider(ILocalizationProvider<Genre> genreLocalizationProvider, ILocalizationProvider<PlatformType> platformTypeLocalizationProvider)
        {
            _genreLocalizationProvider = genreLocalizationProvider;
            _platformTypeLocalizationProvider = platformTypeLocalizationProvider;
        }
        public Game Localize(Game game, string cultureCode) 
        {
            var local = game.Locals.FirstOrDefault(x => x.Culture.Code == cultureCode) ?? game.Locals.First();
            game.Description = local.Description;

            foreach (var genre in game.Genres)
            {
                _genreLocalizationProvider.Localize(genre, cultureCode);
            }

            foreach (var platformType in game.PlatformTypes)
            {
                _platformTypeLocalizationProvider.Localize(platformType, cultureCode);
            }

            return game;
        }
    }
}
