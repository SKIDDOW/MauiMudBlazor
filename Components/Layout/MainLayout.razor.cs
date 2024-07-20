using MudBlazor;
using System.Net.NetworkInformation;
using MauiMudBlazor.Texts;

namespace MauiMudBlazor.Components.Layout
{
    public partial class MainLayout
    {
        //private MudTheme _theme = new();
        private bool _isDarkMode;
        private MudThemeProvider? _mudThemeProvider;

        // Customize theme colors if you want rather than defaults.
        // https://mudblazor.com/customization/overview#custom-themes        

        MudTheme _theme = new MudTheme()
        {
            // Colors : https://mudblazor.com/features/colors#material-colors-list-of-material-colors

            //PaletteLight = new PaletteLight()
            //{
            //    Primary = MudBlazor.Colors.Yellow.Darken3,
            //    Secondary = MudBlazor.Colors.Yellow.Accent4,
            //    AppbarBackground = MudBlazor.Colors.Yellow.Darken4,
            //},
            //PaletteDark = new PaletteDark()
            //{
            //    Primary = MudBlazor.Colors.Yellow.Darken4,
            //},

            //LayoutProperties = new LayoutProperties()
            //{
            //    DrawerWidthLeft = "260px",
            //    DrawerWidthRight = "300px"
            //}
        };

        bool _drawerOpen = true;

        void DrawerToggle()
        {
            _drawerOpen = !_drawerOpen;
        }

        //protected override async Task OnAfterRenderAsync(bool firstRender)
        //{
        //    if (firstRender)
        //    {
        //        _isDarkMode = await _mudThemeProvider.GetSystemPreference();
        //        //StateHasChanged();
        //    }
        //}

        protected override async Task OnInitializedAsync()
        {
            
            // check theme status
            if (!string.IsNullOrWhiteSpace(SecureStorage.Default.GetAsync(SecureStorageKey.IsDarkMode).ToString()))
            {
                string status = await SecureStorage.Default.GetAsync(SecureStorageKey.IsDarkMode);
                if (status != null && status == "True")
                {
                    _isDarkMode = true;
                }
                else
                {
                    _isDarkMode = false;
                }
            }

            StateHasChanged();

            themeHelper.DarkModeChanged += HandleDarkModeChange;
        }

        // Event handler for DarkModeChanged event
        private async void HandleDarkModeChange(bool isDarkMode)
        {
            _isDarkMode = isDarkMode;

            if (_isDarkMode)
            {
                _isDarkMode = false;
                await InvokeAsync(StateHasChanged);
            }
            else if (!_isDarkMode)
            {
                _isDarkMode = true;
                await InvokeAsync(StateHasChanged);
            }
            await SecureStorage.Default.SetAsync(SecureStorageKey.IsDarkMode, _isDarkMode.ToString());
        }

        // This event handler is called when the MudSwitch is toggled
        private async Task ToggleDarkMode(bool value)
        {
            _isDarkMode = value;
            themeHelper.RaiseDarkModeChanged(value);
        }
    }
}