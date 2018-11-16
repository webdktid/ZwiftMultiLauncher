# ZwiftMultiLauncher
## Start 2,3 og 4 Zwifts on the same Machine


Inspired by http://blog.gnu-designs.com/howto-run-multiple-zwift-sessions-on-the-same-pc-windows-only/, but this program is taking care of everything. 

- If you select 2 zwifts in the config file, the program will create C:\Users\zwift01 and C:\Users\zwift02 and copy zwift config files the that location.  
- Disable fullscreen for these new users
- Set resolution according the "resolution" setting in the config file 
- Start the 2 instanses of zwift
- resize the windows


## Installation

Dump ZwiftLauncher.exe and ZwiftLauncher.config in a directory (eg. desktop, ore wherever).

modify the config file for fit your needs. 

    <add key="number" value="2" /> <!--select the number og zwifts 2,3 of 4-->
    <add key="resize" value="true" /> <!--do resizing, after zwift start-->
    <add key="autoclose" value="120" />     <!--this launcher closes after 2 minuts-->
    <add key="resolution" value="1024x576" /><!--modify resolution 512x288, 1024x576,1280x720,1920x1080 -->
    <add key="usersfolder" value="C:\Users" />
    <add key="zwiftlocation" value="C:\Program Files (x86)\zwift" />


Drop me a line, if you think that this is a usefull program. 
tino.dainese@gmail.com
