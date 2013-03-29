﻿<!DOCTYPE html>
<html>
<head>
    <title>@ViewData("Title")</title>
    <link href="@Url.Content("~/Content/Site.css")" rel="stylesheet" type="text/css" />
    <script src="@Url.Content("~/Scripts/jquery-1.5.1.min.js")" type="text/javascript"></script>
</head>
<body>
    <div id="Maqueta">
        <div id="izquierda">
        </div>        
        <div class="page">
            <div id="cabecera">
                <div id="header">
                    <div id = "superior">
                        <div id="logindisplay">
                            @Html.Partial("_LogOnPartial")
                        </div>
                        <div id="msg_sistema">
                            SISTEMA DE INFORMACION EGRESADOS PIS
                        </div>
                    </div>

                </div>
                <div id="menucontainer">
                        <div id="menuizq">
                            <div id="menuder">
                                <ul id="menu">
                                    <li class="first">@Html.ActionLink("Home", "Index", "Home")</li>
                                    <li>@Html.ActionLink("About", "About", "Home")</li>
                                    <li>@Html.ActionLink("About", "About", "Home")</li>
                                    <li class="end">@Html.ActionLink("About", "About", "Home")</li>
                                </ul>
                            </div>
                        </div>
                    </div>
                    </div>
            <div id="contenido">
                
                <div id="main">
                        @RenderBody()
                </div>  
            </div> 
            <div id="footer"></div>                    
        </div>       
        <div id="derecha">
        </div>         
    </div>    
</body>
</html>