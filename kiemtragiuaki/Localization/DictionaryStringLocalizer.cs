using Microsoft.Extensions.Localization;

namespace kiemtragiuaki.Localization
{
    // Marker type for IStringLocalizer<SharedResource>.
    public class SharedResource { }

    public class DictionaryStringLocalizer : IStringLocalizer
    {
        public LocalizedString this[string name]
        {
            get
            {
                var found = Translations.TryGet(name, out var value);
                return new LocalizedString(name, value, resourceNotFound: !found);
            }
        }

        public LocalizedString this[string name, params object[] arguments]
        {
            get
            {
                var found = Translations.TryGet(name, out var value);
                return new LocalizedString(name, string.Format(value, arguments), resourceNotFound: !found);
            }
        }

        public IEnumerable<LocalizedString> GetAllStrings(bool includeParentCultures) =>
            Translations.All().Select(kv => new LocalizedString(kv.Key, kv.Value));
    }

    public class DictionaryStringLocalizerFactory : IStringLocalizerFactory
    {
        private static readonly DictionaryStringLocalizer Instance = new();

        public IStringLocalizer Create(Type resourceSource) => Instance;

        public IStringLocalizer Create(string baseName, string location) => Instance;
    }
}
