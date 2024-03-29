# Dotnet Rc Car

# This is a Remote Control Car, implemented entirely by DOTNET.
The project consists of a web application, mobile application, an esp32 hardware and MQTT broker.
<table align="center">
   <tbody>
      <tr>
   <td width="25%">
      <img src="https://user-images.githubusercontent.com/1418779/158342063-81faf540-3421-470d-94cd-0a2626988c14.png" >
   </td>
   <td width="25%">
      <img src="https://user-images.githubusercontent.com/1418779/158209597-2e0f12f3-ce57-40e8-9280-aff0aeed20f6.png" >
   </td>
   <td width="25%">
      <img src="https://user-images.githubusercontent.com/1418779/158358346-30a061af-284e-4d9e-ae57-1a10a82c78c2.png">
   </td>
   <td width="25%">
      <img src="https://user-images.githubusercontent.com/1418779/158344238-53fad39f-65f3-47b7-ade2-22d0e1f48765.png">
   </td>
   </tr>
   </tbody>
</table>

*   Web application is a [Blazor WASM](https://dotnet.microsoft.com/en-us/apps/aspnet/web-apps/blazor) application.
    * Libraries
      *   [MQTTnet](https://github.com/dotnet/MQTTnet) is a high performance .NET library for MQTT based communication
      *   [Blazorise](Blazorise) is a component library built on top of Blazor with support for CSS frameworks like Bootstrap, Bulma, AntDesign and Material.
      *   [Blazored LocalStorage](https://github.com/Blazored/LocalStorage) is a library that provides access to the browsers local storage APIs for Blazor applications
*   Mobile application is a [MAUI](https://docs.microsoft.com/en-us/dotnet/maui/what-is-maui) application.
    *   Libraries
        *   [MQTTnet](https://github.com/dotnet/MQTTnet) is a high performance .NET library for MQTT based communication
        *   [.NET MAUI Community Toolkit](https://github.com/CommunityToolkit/Maui) is a collection of common elements for development with .NET MAUI that developers tend to replicate across multiple apps.
        *   [MVVM Helpers](https://github.com/jamesmontemagno/mvvm-helpers) Collection of MVVM helper classes for any application.
*   Esp32 app is a [.net nanoframework](https://www.nanoframework.net/) application.
    *   Libraries
        *   [.NET nanoFramework libraries](https://github.com/nanoframework?type=source)
        *   [.NET nanoFramework M2Mqtt](https://github.com/nanoframework/nanoFramework.m2mqtt) is an initial port of the MQTT Client Library [M2Mqtt](https://github.com/eclipse/paho.mqtt.m2mqtt)
*   The MQTT broker that I used is [EMQX](https://www.emqx.io/).

# Tips
- It is currently (3/15/2022) necessary to use [Visual Studio 2022 version 17.2 Preview](https://docs.microsoft.com/en-us/visualstudio/releases/2022/release-notes-preview) to create MAUI applications.
- for developing .net nanoframework applications see [Getting Started Guide for managed code (C#)](https://docs.nanoframework.net/content/getting-started-guides/getting-started-managed.html)
- In order to improve performance, I installed the MQTT broker on WLS2 Ubuntu. if you do the same use [Netsh interface portproxy commands](https://docs.microsoft.com/en-us/windows-server/networking/technologies/netsh/netsh-interface-portproxy#delete-v4tov4) for port forwarding.
- You should also open relevant ports on your host system's firewall, usually 1883 for MQTT/TCP and 8083 for websocket.
