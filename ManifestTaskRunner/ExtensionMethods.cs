using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Media.Imaging;
using Microsoft.VisualStudio.Imaging.Interop;
using Microsoft.VisualStudio.PlatformUI;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;

namespace ManifestTaskRunner
{
    public static class ExtensionMethods
    {
        public static BitmapSource ToBitmap(this ImageMoniker moniker, int size)
        {
            ThreadHelper.ThrowIfNotOnUIThread();

            IVsImageService2 imageService = ServiceProvider.GlobalProvider.GetService<SVsImageService, IVsImageService2>();
            Color backColor = VSColorTheme.GetThemedColor(EnvironmentColors.ToolWindowBackgroundColorKey);

            var imageAttributes = new ImageAttributes
            {
                Flags = (uint)_ImageAttributesFlags.IAF_RequiredFlags | unchecked((uint)_ImageAttributesFlags.IAF_Background),
                ImageType = (uint)_UIImageType.IT_Bitmap,
                Format = (uint)_UIDataFormat.DF_WPF,
                LogicalHeight = size,
                LogicalWidth = size,
                Background = (uint)backColor.ToArgb(),
                StructSize = Marshal.SizeOf(typeof(ImageAttributes))
            };

            IVsUIObject result = imageService.GetImage(moniker, imageAttributes);

            result.get_Data(out var data);

            return data as BitmapSource;
        }
    }
}
