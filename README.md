<b>Installation</b>

To run this console app you need to have the following installed on your computer:
<br/><b>.NET SDK.</b> for .NET CLI(command line interface). It is availible on https://dotnet.microsoft.com/en-us/download. <br/>This project was made using <b>.NET 8.0</b>.

Steps to install the project:

```
git clone https://github.com/DorianLeci/Internship-2-C-Sharp.git
cd Internship-3-OOP
dotnet run
```
This app has 4 menus inside <b>Passenger Menu</b>: <b>Flight Menu</b>,<b> Airplane Menu </b> and <b>Crew Menu </b>.
You can navigate through menus using numbers shown as part of each menu.Each number on those four menus leads to new submenu and so on.
<br/>
Number 0 is always option for leaving child menu and going back to the parent menu.<br/>If you are positioned in Main Menu,by pressing 0 you can exit the app.
<br/>
You can not <b>skip</b> menus(go from User Menu to Trip Menu directly).You can only go from parent to child directory and vice versa one step at a time.

When inputting numbers of menus do not press enter beacuse programs uses ReadKey(). Also, during process of adding new airplane when you are asked to choose flight category ReadKey() function is used again.

When adding,deleting or editing some item you are asked to press one key (y/n) to confirm your action so you do not need to press enter.



<b>All passengers in system must have unique email adress.

All flights in system must have unique names. All must have specific format with two lowercase letters and three to four numbers.For example AA7894 or CR894. Program tolerates input of lowercase letters.

All airplanes in system must have unique names. Airplanes can have letters and numbers in their name.

Crews and crew members can have same names they differ by id which every item mentioned above has. Crew names can not have numbers in their names,although I added crews with numbers in their names for easier navigation in seed.</b>


If you want to use system as passenger you must first register at least one passenger,then login. Beacuse of seed functions inside program some passengers are already registered.

In general system corrects input of letters,for example if you input name as DoRiAn it will be corrected as Dorian. It does the same thing when you search items by name.


