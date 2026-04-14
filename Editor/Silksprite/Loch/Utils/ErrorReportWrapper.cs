using JetBrains.Annotations;
using UnityEngine;

#if LOCH_NDMF_SUPPORT
using nadena.dev.ndmf;
using NdmfErrorReport = nadena.dev.ndmf.ErrorReport;
#endif

#if LOCH_ABLET_SUPPORT
using Ablet.ErrorReporting;
using AbletErrorReport = Ablet.ErrorReporting.ErrorReport;
#endif

#if LOCH_NDMF_SUPPORT && LOCH_ABLET_SUPPORT
using Ablet.API;
#endif

namespace Silksprite.Loch.Utils
{
    [PublicAPI]
    public static class ErrorReportWrapper
    {
#if LOCH_NDMF_SUPPORT && LOCH_ABLET_SUPPORT
        static bool UseNdmf => !AbletSymbols.PreferAblet;
#elif LOCH_NDMF_SUPPORT
        static bool UseNdmf => true;
#else
        static bool UseNdmf => false;
#endif

#if LOCH_ABLET_SUPPORT && LOCH_NDMF_SUPPORT 
        static bool UseAblet => AbletSymbols.PreferAblet;
#elif LOCH_ABLET_SUPPORT
        static bool UseAblet => true;
#else
        static bool UseAblet => false;
#endif
        
        public static void LogWarningFormat(LocalizedContent loc)
        {
#if LOCH_NDMF_SUPPORT
            if (UseNdmf)
            {
                NdmfErrorReport.ReportError(new WrappedError(ErrorSeverity.NonFatal, loc, null));
                return;
            }
#endif
#if LOCH_ABLET_SUPPORT
            if (UseAblet)
            {
                AbletErrorReport.LogWarning(loc.Tr);
                return;
            }
#endif
            Debug.LogWarning(loc.Tr);
        }

        public static void LogWarningFormat(LocalizedContent loc, Object target)
        {
#if LOCH_NDMF_SUPPORT
            if (UseNdmf)
            {
                NdmfErrorReport.ReportError(new WrappedError(ErrorSeverity.NonFatal, loc, target));
                return;
            }
#endif
#if LOCH_ABLET_SUPPORT
            if (UseAblet)
            {
                using var _ = new InterestScope(target);
                AbletErrorReport.LogWarning(loc.Tr);
                return;
            }
#endif
            Debug.LogWarning(loc.Tr, target);
        }

    }
}
