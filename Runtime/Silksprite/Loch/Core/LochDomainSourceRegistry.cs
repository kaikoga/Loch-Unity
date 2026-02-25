using System.Collections.Generic;

namespace Silksprite.Loch.Core
{
    public class LochDomainSourceRegistry
    {
        public static readonly LochDomainSourceRegistry Instance = new LochDomainSourceRegistry();

        readonly List<ILochDomainSource> _sources = new List<ILochDomainSource>();

        public void Add(ILochDomainSource source)
        {
            _sources.Add(source);
        }

        public void ReloadSources()
        {
            LochRepository.Instance.ClearAllCaches();
            foreach (var source in _sources)
            {
                source.Load(LochRepository.Instance);
            }
        }
    }
}
