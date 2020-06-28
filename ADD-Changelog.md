**Change-log:**
* v4 (4.5.0.0):<br/>
Arma 3 Tools: FileBank integration.<br/>
File operations now use Multi-Threading, which fully utilizes the CPU.<br/>
The Configurator can now run ADD from it's last execution.<br/>
Navigates down 1 or up 5 directories looking for Repository.<br/>

* v4b5 (4.4..):<br/>
Template finding is now multi-threaded.<br/>
User is notified if filter caused empty list.<br/>
Fixed command-line filter prefix<br/>
Window is only hidden from argument.<br/>
Auto-Yes can be enabled from argument.<br/>
HelpProvider replaced by ToolTip and Minimize.<br/>
`Antistasi Dev Deploy Benchmark.exe` added.<br/>
Incorrect switch will cause help page to appear.<br/>
Fixed bug where ADD filtered using full path of template.<br/>
Fixed bug where ADD use local folder for stringtable search.<br/>

* v4b4 (4.4.9.0):<br/>
Fixed bug where ADD was unable to navigate to the StringTable correctly.<br/>

* v4b3 (4.3.8.0):<br/>
File operations now use Multi-Threading, which fully utilizes the CPU.<br/>
Navigates down 1 or up 5 directories looking for Repository.<br/>

* v4b2:<br/>
Valid Map-Templates end in a .map and have a mission.sqm.<br/>
Periods in Map-Templates are preserved.<br/>

* v4b1:<br/>
Arma 3 Tools: FileBank integration.<br/>
The Configurator can now run ADD from it's last execution.<br/>
Specific Map-Templates can be selected for PBO packing.<br/>
Source can now be overridden.<br/>
* v3.1:<br/>
Default version number used if stringtable.xml is corrupt.<br/>
XML Error is expressed to user regardless of visibility settings.<br/>
Fixed help augments not opening console window.<br/>
File size reduced due to smaller logo and output strings.<br/>
File Assembly information is now accurate.<br/>
* v3:<br/>
Version switch /v added.<br/>
Help switch /h added.<br/>
Help text and version number added for Antistasi Dev Deploy Configurator. Antistasi Dev Deploy Configurator Registry put into a separate class. Registry crash fixed when accessing non-existent values.<br/>
* b1.4:<br/>
A label was added to the Configurator.<br/>
Icons of x48 added.<br/>
* b1.3:<br/>
Fixed directory locality for the root folder checker.<br/>
* b1.2:<br/>
Antistasi Dev Deploy now creates the output folder if it doesn't exist.<br/>
If your Arma 3 Profile Name doesn't exist or is missing (ie. A3 not installed), and you have not selected an output override: It will now export to PackagedMissions in the same Directory.<br/>
(Requires Use of Configurator) Option added to open the output folder in Windows Explorer.<br/>
* b1.1:<br/>
Added Antistasi Dev Deploy Configurator<br/>
* v1.0:<br/>
Antistasi Dev Deploy debug build for no forms, no config, and no-nonsense version.<br/>