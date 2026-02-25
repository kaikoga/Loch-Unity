using System.Collections.Generic;
using System.Linq;

namespace Silksprite.Loch.Core
{
    public class LocaleSet
    {
        internal readonly Locale[] Locales;
        internal readonly string[] LocaleCodes;
        internal readonly string[] LocaleDisplayNames;

        internal bool IsSelectable => LocaleCodes.Length > 1;

        public LocaleSet(IEnumerable<Locale> locales)
        {
            Locales = locales.OrderBy(locale => locale.LocaleCode).ToArray();
            LocaleCodes = Locales.Select(locale => locale.LocaleCode).ToArray();
            LocaleDisplayNames = Locales.Select(locale => locale.DisplayName).ToArray();
        }
    }
}
