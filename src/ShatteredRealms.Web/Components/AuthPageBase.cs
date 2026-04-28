using Microsoft.AspNetCore.Components;
using ShatteredRealms.Web.Services;

namespace ShatteredRealms.Web.Components;

public abstract class AuthPageBase : ComponentBase
{
    [Inject] protected AuthService AuthService { get; set; } = default!;
    [Inject] protected NavigationManager Navigation { get; set; } = default!;

    protected virtual string? RequiredPermission => null;
    protected virtual string PermissionDeniedRedirect => "/";

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (!firstRender) return;
        if (!await AuthService.IsAuthenticatedAsync())
        {
            Navigation.NavigateTo("/login");
            return;
        }

        if (RequiredPermission != null && !await AuthService.HasPermissionAsync(RequiredPermission))
        {
            Navigation.NavigateTo(PermissionDeniedRedirect);
            return;
        }

        await OnAuthenticatedAsync();
        StateHasChanged();
    }

    protected virtual Task OnAuthenticatedAsync() => Task.CompletedTask;
}
