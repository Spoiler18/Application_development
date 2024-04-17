using Microsoft.JSInterop;

namespace BlogUI.Extensions
{
    public static class JsRuntimeExtensions
    {
        public static ValueTask ToastrSuccess(this IJSRuntime jSRunttime, string message)
        {
            return jSRunttime.InvokeVoidAsync("ShowToastr", "success", message);
        }
        public static ValueTask ToastrError(this IJSRuntime jSRunttime, string message)
        {
            return jSRunttime.InvokeVoidAsync("ShowToastr", "error", message);
        }
    }
}
