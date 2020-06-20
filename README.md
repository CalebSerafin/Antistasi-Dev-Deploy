**Please find binaries inside the `Releases` tab. Double clicking a .cs won't run it.**
***
# Antistasi Dev Deploy
Dynamically deploys any map template (Antistasi-Concretium Civitas.coci-concretiumcivitas) into mpmissions for straight testing, or separating it for packing into a PBO. Example Output: `"Antistasi-Concretium Civitas-2-2-2.coci-concretiumcivitas"`
## How To
Just place in the same folder as A3-Antistasi, and with one click, all the templates are deployed to *your* mpmissions. Integrates with FileBank to pack PBOs.

## Technical Specifications 
* Run `Antistasi Dev Deploy.exe` with `/h` argument to see help list.
* Should be physically placed in `YourGitRepository/` or `YourGitRepository/Tools`.
* Shortcuts,Batches etc. won't change its current directory.
* A valid map template has a "." in its directory name. The map templates should be in `GitRepository/Map-Templates`.
* Antistasi Dev Deploy gets your **currently** selected Arma 3 Profile.
* If your Arma 3 Profile Name cannot be fetched (ie. A3 Not installed), It will export to `PackagedMissions` in the current directory.

## Antistasi Dev Deploy Configurator \[Add-on\]
Run anywhere to open up a  GUI which allows you to overide global settings for `Antistasi Dev Deploy.exe`. 
* Output to a specified directory. Features Full-Fledged Folder Selector and not bog standard Directory Selector. 
* Open the output folder in Windows Explorer.
* To remove all traces of this program, press `Erase Registry Value` to erase the overrides.
