using System.Collections.Generic;
using Silksprite.Loch.Core;

namespace Silksprite.Loch.UIElements.Base
{
    class LochElementRefresher
    {
        public static readonly LochElementRefresher Instance = new LochElementRefresher();

        readonly HashSet<ILochElement> _lochElements = new HashSet<ILochElement>();
        LochElementRefresher()
        {
            LochRepository.Instance.OnLocaleChanged += LochRefresh;
        }

        public void Add(ILochElement element)
        {
            _lochElements.Add(element);
            element.LochRefresh();
        }

        public void Remove(ILochElement element) => _lochElements.Remove(element);

        void LochRefresh()
        {
            foreach (var lochElement in _lochElements)
            {
                lochElement.LochRefresh();
            }
        }
    }
}
