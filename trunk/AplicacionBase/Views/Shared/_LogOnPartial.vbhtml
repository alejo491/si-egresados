@If Request.IsAuthenticated Then
    @<text>Bienvenido <strong>@User.Identity.Name</strong>!
    [ @Html.ActionLink("Log Off", "LogOff", "Account") ]</text>
Else
    @:<text>♠ @Html.ActionLink("Iniciar Sesion", "LogOn", "Account") </text>
End If
