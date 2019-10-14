# Antistasi Dev Deploy
Dynamically deploys any map template (Re-take Polan.Enoch) into mpmissions for straight testing, or separating it for packing into a PBO. 
## How To
Just place in the same folder as A3-Antistasi, and with one click, all the templates are deployed to *your* mpmissions. No forms, no configs, no bullshit.
## Antistasi Dev Deploy Configurator
Run anywhere to open up a  GUI which allows you to overide all `Antistasi Dev Deploy.exe` to output to the specified directory. Features Full-Fledged Folder Selector and not bog standard Directory Selector. To remove all traces of this program, press `Erase Registry Value` to erase the overrides.
## Technical Specifications 
* Antistasi Dev Deploy gets your **currently** selected Arma 3 Profile.
* A valid map template has a "." in its directory name. The map templates are in `A3-Antistasi/Templates`.
* Also Works when placed inside the Tools folder found inside `official-antistasi-community/A3-Antistasi/`.
<br/>
<br/>
<br/>

# Arma-3-Dev-Deploy
*Version 2.0 is being currently being so it works with more dynamic setups.*
Created to allow testing inDev Versions of Antistasi Easier by coping all specified templates into the User's mpmissions.
Simply place above the respority root folder (the same folder the git ignore is in). 
## How To
But if you take it right now put it in your respority root folder (same folder the git ignore is in). Run it once, press `y`, it will open up the config location. Open template.selected.cfg and navigate to the line `A3Profile=FrostsBite;`. Replace `FrostsBite` with the profile name you launch Arma 3 with.
<br/>

Now evertime you run A3DD, it will create mission files inside your mpmissions with the same name as the mission template folders in Antisatsi/Templates (ie `A3-Antistasi.altis`). Now you can run these missions inside multi-player LAN host 
<br/>

If you want copy these files somewhere else, go to documents/Arma 3 - Other Profiles/MyProfileName/mpmissions.
