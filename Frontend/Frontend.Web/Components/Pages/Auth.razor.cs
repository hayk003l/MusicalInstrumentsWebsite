using Microsoft.JSInterop;
    
namespace Frontend.Web.Components.Pages
{
    public partial class Auth
    {
        private bool jsLoaded = false;

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender && !jsLoaded)
            {
                // Dynamically load auth.js if not already loaded
                await JS.InvokeVoidAsync("eval", @"
                if (!window.authTabs) {
                    var s=document.createElement('script');
                    s.src='js/auth.js';
                    document.body.appendChild(s);
                }");

                // Wait a short delay to ensure the script loads
                await Task.Delay(50);

                // Initialize authTabs
                await JS.InvokeVoidAsync("authTabs.init");

                jsLoaded = true;
            }
        }

    }
}

